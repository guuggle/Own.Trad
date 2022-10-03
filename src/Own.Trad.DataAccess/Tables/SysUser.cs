using Own.Trad.DataAccess.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Own.Trad.DataAccess.Tables
{
    [Table("sys_user")]
    public class SysUser : AuditableBaseEntity
    {
        [Column("email")]
        public string Email { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [Column("user_name")]
        public string UserName { get; set; }
    }
}
