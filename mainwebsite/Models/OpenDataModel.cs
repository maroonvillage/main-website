using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MaroonVillage.Core.DomainModel;


namespace MainWebsite.Models
{
    public class OpenDataModel : DefaultModel
    {
        public string ApiNamePrefix
        {
            get
            {
                return "OpenData";
            }

        }

        private string _currentState;
        public string CurrentState { get { return _currentState; } set { _currentState = value; } }

        public List<Field> OpenDataDatasets { get; set; }

        public List<UsState> UsStates { get; set; }

        public List<LiquorLicense> LiquorLicenses { get; set; }
    }

    public class Field
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}