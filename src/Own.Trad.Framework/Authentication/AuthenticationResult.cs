using System;

namespace Own.Trad.Framework.Authentication
{
    public class AuthenticationResult
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}