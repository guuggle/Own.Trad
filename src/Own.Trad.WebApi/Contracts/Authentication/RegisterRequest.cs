namespace Own.Trad.WebApi.Contracts.Authentication
{
    public class RegisterRequest
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}