using Microsoft.EntityFrameworkCore;
using Own.Trad.DataAccess.Tables;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Own.Trad.DataAccess.Data
{
    public partial class OwnDbContext : DbContext
    {
        public DbSet<SysUser> SysUser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
