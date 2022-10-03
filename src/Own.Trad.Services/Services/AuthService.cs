using System.Threading.Tasks;
using Own.Trad.DataAccess.Repositories;
using Own.Trad.DataAccess.Tables;
using Own.Trad.Framework.Authentication;
using Own.Trad.Framework.Errors;
using Own.Trad.Framework.OResult;

namespace Own.Trad.Services.Interfaces
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(
            IUserRepository userRepository,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<OResult<AuthenticationResult>> Login(string email, string password)
        {
            // query user
            var user = await _userRepository.GetUserByEmail(email);
            if (user is null)
            {
                return Errors.User.UserNotExist;
            }

            if (user.Password != password)
            {
                return Errors.User.InvalidPassword;
            }

            // create token
            var token = _jwtTokenGenerator.CreateToken(user.Id, user.UserName);

            return new AuthenticationResult
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Token = token
            };
        }

        public async Task<OResult<AuthenticationResult>> Register(string email, string userName, string password)
        {
            // validate the user doesn't exist
            if (await _userRepository.GetUserByEmail(email) != null)
            {
                return Errors.User.EmailIsTaken;
            }

            // create user
            var user = new SysUser
            {
                Email = email,
                UserName = userName,
                Password = password
            };

            await _userRepository.AddUser(user);

            // create jwt token
            var token = _jwtTokenGenerator.CreateToken(user.Id, user.UserName);

            return new AuthenticationResult
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Token = token
            };
        }
    }
}