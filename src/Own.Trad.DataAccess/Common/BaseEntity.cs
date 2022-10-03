using System;
using System.Collections.Generic;
using System.Text;

namespace Own.Trad.DataAccess.Common
{
    public abstract class BaseEntity
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
