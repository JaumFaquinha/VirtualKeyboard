using JPFMS_BankKeyboard.Model;

namespace JPFMS_BankKeyboard.Requests
{
    public class PasswordRequest
    {
        public string? CPF { get; set; }
        public Keys[]? Password { get; set; }

        public bool Success { get; set; } = false;

        public string? Message { get; set; }
    }
}
