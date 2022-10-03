using System.Collections.Generic;

namespace Own.Trad.Framework.OResult
{
    public interface IOResult
    {
        List<OError> Errors { get; }
    }
}

