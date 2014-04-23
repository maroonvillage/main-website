using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MaroonVillage.Core.Enumerators.Google
{
    public enum ResultsOuput
    {
        [Description("json")]
        Json,
        [Description("xml")]
        Xml
    }
}
