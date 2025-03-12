using JPFMS_BankKeyboard.Model;
using JPFMS_BankKeyboard.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using JPFMS_BankKeyboard.Database;
using JPFMS_BankKeyboard.Requests;
using Microsoft.AspNetCore.Cors;
using JPFMS_BankKeyboard.Responses;

[ApiController]
[Route("api/AuthController")]
public class AuthController : ControllerBase
{
    private readonly IMongoCollection<Person>? _users;
    private readonly AuthenticationService authServeice;
    private readonly KeyboardService keyboardService;

    public AuthController()
    {
        this.authServeice = new AuthenticationService();
        this.keyboardService = new KeyboardService();
        try
        {
            var database = Connection.GetDatabase();
            _users = database.GetCollection<Person>("Person");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    [HttpPost("validate-cpf")]
    public IActionResult ValidateCPF([FromBody] CpfRequest cpfRequest)
    {
        try 
        {
            string cpf = cpfRequest.CPF;

            var filtro = Builders<Person>.Filter.Eq(u => u.CPF, cpf);
            var user = _users.Find(filtro).FirstOrDefault();
            if (user != null)
            {
                cpfRequest.Ok = true;
                return Ok(new ValidationResponse() { Success = true});
            }

            return BadRequest(new ValidationResponse { Success = false, Message = "CPF não encontrado" });
        }
        catch (Exception ex)
        {
            return BadRequest(new ValidationResponse { Success = false, Message = ex.Message });
        }
    }

    [HttpGet("generate-keyboard")]
    public IActionResult GenerateKeyboard()
    {
        var keys = KeyGenerator.Generate();
        return Ok(keys);
    }

    [HttpPost("validate-password")]
    public IActionResult ValidatePassword([FromBody] PasswordRequest request)
    {
        try
        {
            var user = _users.Find(u => u.CPF == request.CPF).FirstOrDefault();
            if (user == null)
                return BadRequest(new ValidationResponse { Success = false, Message = "Usuário não encontrado" });

            if (user.Attempts == 3)
                return BadRequest(new ValidationResponse { Success = false, Message = "Conta bloqueada! Entre em contato com o banco para realizar o desbloqueio" });

            if (request.Password.Length == 6)
            {
                string userPassword = user.Password;
                for (int i = 0; i < userPassword.Count(); i++)
                {
                    Keys pressedKey = request.Password[i];
                    string caractere = userPassword[i].ToString();

                    if (pressedKey.N1.ToString() != caractere && pressedKey.N2.ToString() != caractere)
                    {
                        var attemptUpdate = Builders<Person>.Update.Inc(u => u.Attempts, 1);
                        _users.UpdateOne(u => u.CPF == request.CPF, attemptUpdate);
                        return BadRequest(new ValidationResponse{ Success = false, Message = $"Senha incorreta (Tentativa: {user.Attempts + 1})" });
                    }
                }
            }
            else
            {
                var attemptUpdate = Builders<Person>.Update.Inc(u => u.Attempts, 1);
                _users.UpdateOne(u => u.CPF == request.CPF, attemptUpdate);
                return BadRequest(new ValidationResponse { Success = false, Message = $"Senha incorreta (Tentativa: {user.Attempts + 1})" });
            }
            //Atualiza o contador para 0
            var update = Builders<Person>.Update.Set(u => u.Attempts, 0);
            _users.UpdateOne(u => u.CPF == request.CPF, update);
            request.Password = new Keys[1];
            request.Success = true;
            request.Message = "Sucesso!";
            return Ok(new ValidationResponse { Success = true, Message = "Sucesso!" });
        }
        catch (Exception ex)
        {
            return BadRequest(new ValidationResponse { Success = false, Message = ex.Message });
        }      
    }

    [HttpPost("reset-password")]
    public IActionResult ResetPassword([FromBody] ChangePasswordRequest request)
    {
        var update = Builders<Person>.Update.Set(u => u.Password, request.NewPassword).Set(u => u.Attempts, 0);
        _users.UpdateOne(u => u.CPF == request.CPF, update);
        return Ok(new ValidationResponse { Success = true, Message = "Senha redefinida com sucesso" });
    }
}
