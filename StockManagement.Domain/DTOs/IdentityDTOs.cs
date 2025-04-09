namespace StockManagement.Domain.DTOs
{
    public class RegisterDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class ForgotPasswordDTO
    {
        public string Email { get; set; }
    }

    public class ResetPasswordDTO
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
} 