using System.ComponentModel;
namespace MaroonVillage.Core.Enumerators
{
    public enum CacheKeys
    {
        [Description("MvPlaces_All")]
        [DefaultValue(1440)]
        MvPlaces,
        [Description("ApiConfigByName_{0}")]
        [DefaultValue(1440)]
        ApiConfigByName,
        [Description("CsvDataSet_{0}")]
        [DefaultValue(1440)]
        CsvDataSet
    }
}
