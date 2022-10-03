using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Own.Trad.DataAccess.Data
{
    public partial class OwnDbContext : DbContext
    {
        public OwnDbContext(DbContextOptions<OwnDbContext> options) : base(options)
        {
        }

    }
}
