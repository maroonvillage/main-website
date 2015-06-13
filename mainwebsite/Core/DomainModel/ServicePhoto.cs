
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaroonVillage.Core.DomainModel
{
    public class ServicePhoto
    {
        public string ServiceName { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string OwnerRealName { get; set; }
        public string ProfileUrl { get; set; }
        public string PhotoId { get; set; }
        public string PhotoTitle { get; set; }
        public string PhotoDescription { get; set; }
        public string TagList { get; set; }
        public string OriginalUrl { get; set; }
        public string Thumbnail { get; set; }
    }
}
