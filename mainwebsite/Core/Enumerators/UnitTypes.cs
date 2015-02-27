using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MaroonVillage.Core.Enumerators
{
    public enum UnitTypes
    {
        [Description("Metric")]
        Metric,
        [Description("Impreial")]
        Imperial,
        [Description("None")]
        None,


    }
}
