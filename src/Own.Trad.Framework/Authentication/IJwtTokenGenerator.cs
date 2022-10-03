using System;
namespace Own.Trad.Framework.Authentication
{
    public interface IJwtTokenGenerator
    {
        string CreateToken(string userId, string userName);
    }
}