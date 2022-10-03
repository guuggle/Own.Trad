using System;
using System.Collections.Generic;
using System.Text;

namespace Own.Trad.DataAccess.Common
{
    public interface AuditableEntity
    {
        public DateTime Created { get; set; }
        public string Creator { get; set; }
        public DateTime? Modified { get; set; }
        public string Modifier { get; set; }
    }
}
