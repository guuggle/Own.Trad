using System;

namespace Own.Trad.Framework.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now; // TODO: utcNOW?
    }
}