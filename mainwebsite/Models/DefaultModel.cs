using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace MainWebsite.Models
{
    public class DefaultModel : DynamicObject
    {
        #region Private Fields
        private dynamic _data;
        private string _pageTitle; 
        #endregion


        #region Public Properties
        /// <summary>
        /// 
        /// </summary>
        public dynamic Data { get { return _data ?? (_data = new ExpandoObject()); } set { _data = value; } }

        /// <summary>
        /// 
        /// </summary>
        public string GoogleMapsV3Url
        {
            get
            {
                return !string.IsNullOrEmpty(ConfigurationManager.AppSettings["GoogleMapsV3Url"]) ? ConfigurationManager.AppSettings["GoogleMapsV3Url"] : string.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string GoogleMapsPlacesLibary
        {
            get
            {
                return !string.IsNullOrEmpty(ConfigurationManager.AppSettings["GoogleMapsV3Url"]) ? string.Format("{0}{1}", ConfigurationManager.AppSettings["GoogleMapsV3Url"], "?libraries=places") : string.Empty;
            }
        }

        /// <summary>
        /// Title for the page to be rendered inside &lt;title&gt;&lt;/title&gt;
        /// </summary>
        public string PageTitle
        {
            get { return _pageTitle ?? string.Empty; }
            set { _pageTitle = value; }
        }

        /// <summary>
        /// Current logged in user or null if Anonymous
        /// </summary>
        public IPrincipal User { get; set; }
        #endregion

    }
}