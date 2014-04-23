using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaroonVillage.Core.DomainModel
{
    public interface IGooglePlacesRequestParameters
    {
        string Key { get; set; }
        string Lattitude { get; set; }
        string Longitude { get; set; }
        int Radius { get; set; }
        bool Sensor { get; set; }
        string RankBy { get; set; }
        string Keyword { get; set; }
        string Language { get; set; }
        int MinPrice { get; set; }
        int MaxPrice { get; set; }
        string Name { get; set; }
        string OpenNow { get; }
        bool ShowOnlyOpenNow { get; set; }
        string Output { get; set; }
        string PageToken { get; set; }
       string Types { get; set; }
    }

    public class GooglePlacesRequestParameters : IGooglePlacesRequestParameters
    {
        public string Key { get; set; }
        public string Lattitude { get; set; }
        public string Longitude { get; set; }
        public int Radius { get; set; }
        public bool Sensor { get; set; }
        public string RankBy { get; set; }
        public string Keyword { get; set; }
        public string Language { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public string Name { get; set; }
        public string OpenNow { get { return "opennow"; } }
        public bool ShowOnlyOpenNow { get; set; }
        public string Output { get; set; }
        public string PageToken { get; set; }
        public string Types { get; set; }
    }
}
