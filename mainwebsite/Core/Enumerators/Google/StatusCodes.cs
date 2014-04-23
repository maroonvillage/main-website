using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MaroonVillage.Core.Enumerators.Google
{
    public enum StatusCodes
    {
        [Description("OK")]
        Ok,
        [Description("ZERO_RESULTS")]
        ZeroResults,
        [Description("OVER_QUERY_LIMIT")]
        OverQueryLimit,
        [Description("REQUEST_DENIED")]
        RequestDenied,
        [Description("INVALID_REQUEST")]
        InvalidRequest,
        [Description("UKNOWN_ERROR")]
        UnknownError
    }
}
