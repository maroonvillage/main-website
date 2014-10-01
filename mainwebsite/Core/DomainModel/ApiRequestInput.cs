using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaroonVillage.Core.DomainModel
{
    public class ApiRequestInput
    {
        public int RequestInputId { get; set; }
        public int ApiConfigId { get; set; }
        public string ParameterName { get; set; }
        public bool IsComponent { get; set; }
        public bool IsRequired { get; set; }
        public int PrimaryId { get; set; }
        public bool IsKey { get; set; }

    }
}
