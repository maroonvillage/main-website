using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MaroonVillage.Core.Enumerators
{
    public enum ResponseOutputTypes
    {
        [Description("json")]
        Json,
        [Description("xml")]
        Xml
    }
}
