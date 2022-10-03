using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Own.Trad.DataAccess.Tables;
using Own.Trad.DataAccess.Data;
using Own.Trad.DataAccess.Repositories;

namespace Own.Trad.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly OwnDbContext _context;

        public UserRepository(OwnDbContext context)
        {
            this._context = context;
        }

        public async Task AddUser(SysUser user)
        {
            await Task.CompletedTask;
        }

        public async Task<SysUser> GetUserByEmail(string email)
        {
            return await _context.SysUser.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
