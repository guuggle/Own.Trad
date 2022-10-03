using System;
namespace Own.Trad.Framework.Services
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}