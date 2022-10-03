using System.Threading.Tasks;
using Own.Trad.DataAccess.Tables;
using Own.Trad.Framework.Authentication;
using Own.Trad.Framework.OResult;

namespace Own.Trad.Services.Interfaces
{
    public interface IAuthService
    {
        Task<OResult<AuthenticationResult>> Register(string email, string userName, string password);
        Task<OResult<AuthenticationResult>> Login(string email, string password);
    }
}