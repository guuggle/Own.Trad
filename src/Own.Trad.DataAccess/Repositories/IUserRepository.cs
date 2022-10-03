using Own.Trad.DataAccess.Tables;
using System.Threading.Tasks;

namespace Own.Trad.DataAccess.Repositories
{
    public interface IUserRepository
    {
        Task<SysUser> GetUserByEmail(string email);
        Task AddUser(SysUser user);
    }
}
