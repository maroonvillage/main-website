using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MaroonVillage.Core.Enumerators.Google
{
    public enum ComponentTypes
    {
        [Description("route")]
        Route,
        [Description("locality")]
        Locality,
        [Description("administrative_area")]
        AdministrativeArea,
        [Description("postal_code")]
        PostalCode,
        [Description("country")]
        Country
    }
}
