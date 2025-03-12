namespace JPFMS_BankKeyboard.Services
{
    public class AuthenticationService
    {

        private static readonly Dictionary<string, (string Password, int Attempts)> Users = new();

        public bool ValidateCPF()
        {
           


            return true;
        }

        public bool ValidatePassword()
        {
            return true;
        }

    }
}
