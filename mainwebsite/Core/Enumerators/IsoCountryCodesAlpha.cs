using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MaroonVillage.Core.Enumerators
{

    /// <summary>
    /// Based upon ISO 3166-1.  this will be replaced by a database table
    /// </summary>
    public enum IsoCountryCodesAlpha2
    {
        //list of codes
        //http://en.wikipedia.org/wiki/ISO_3166-1
        //...
        [Description("US")]
        UnitedStates,
       //...
        [Description("Unknown")]
        Unknown
    }


    public enum IsoCountryCodesAlpha3
    {
        //list of codes
        //http://en.wikipedia.org/wiki/ISO_3166-1
        //...
        [Description("USA")]
        UnitedStates,
        //...
        [Description("Unknown")]
        Unknown
    }

}
