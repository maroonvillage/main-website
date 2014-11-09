using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MaroonVillage.Core.DomainModel
{

    public interface IFlickrPhotoSearchParameters
    {
        string Key { get; set; }
        string UserId { get; set; }
        string Tags { get; set; }
        string TagMode { get; set; }
        string Text { get; set; }
        string MinUploadDate { get; set; }
        string MaxUploadDate { get; set; }
        string MinTakenDate { get; set; }
        string MaxTakenDate { get; set; }
        string License { get; set; }
        string Sort { get; set; }
        string PrivacyFilter { get; set; }
        string Bbox { get; set; }
        string Accuracy { get; set; }
        string SafeSearch { get; set; }
        string ContentType { get; set; }
        string MachineTags { get; set; }
        string MachineTagMode { get; set; }
        string GroupId { get; set; }
        string Contacts { get; set; }
        string WoeId { get; set; }
        string PlaceId { get; set; }
        string Media { get; set; }
        string HasGeo { get; set; }
        string GeoContext { get; set; }
        string Lattitude { get; set; }
        string Longitude { get; set; }
        string Radius { get; set; }
        string RadiusUnits { get; set; }
        string IsCommons { get; set; }
        string InGallery { get; set; }
        string IsGetty { get; set; }
        string Extras { get; set; }
        string PerPage { get; set; }
        string Page { get; set; }


    }

    public class FlickrPhotoSearchParameters : IFlickrPhotoSearchParameters
    {
        [Description("api_key")]
        public string Key { get; set; }
        [Description("user_id")]
        public string UserId { get; set; }
        [Description("tags")]
        public string Tags { get; set; }
        [Description("tag_mode")]
        public string TagMode { get; set; }
        [Description("text")]
        public string Text { get; set; }
        public string MinUploadDate { get; set; }
        public string MaxUploadDate { get; set; }
        public string MinTakenDate { get; set; }
        public string MaxTakenDate { get; set; }
        public string License { get; set; }
        public string Sort { get; set; }
        public string PrivacyFilter { get; set; }
        public string Bbox { get; set; }
        public string Accuracy { get; set; }
        public string SafeSearch { get; set; }
        public string ContentType { get; set; }
        public string MachineTags { get; set; }
        public string MachineTagMode { get; set; }
        public string GroupId { get; set; }
        public string Contacts { get; set; }
        public string WoeId { get; set; }
        public string PlaceId { get; set; }
        public string Media { get; set; }
        public string HasGeo { get; set; }
        public string GeoContext { get; set; }
        [Description("lat")]
        public string Lattitude { get; set; }
        [Description("lon")]
        public string Longitude { get; set; }
        public string Radius { get; set; }
        public string RadiusUnits { get; set; }
        [Description("is_commons")]
        public string IsCommons { get; set; }
        public string InGallery { get; set; }
        public string IsGetty { get; set; }
        public string Extras { get; set; }
        public string PerPage { get; set; }
        public string Page { get; set; }
    }
}
