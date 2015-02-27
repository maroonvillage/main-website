using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MaroonVillage.Core.Enumerators.TED
{
    public enum TedApiResourceTypes
    {
        [Description("countries")]
        Countries,
        [Description("events")]
        Events,
        [Description("languages")]
        Languages,
        [Description("quotes")]
        Quotes,
        [Description("ratings")]
        Ratings,
        [Description("rating_words")]
        RatingWords,
        [Description("speakers")]
        Speakers,
        [Description("states")]
        States,
        [Description("tedx_events")]
        TedXEvents,
        [Description("tedx_groups")]
        TedXGroups,
        [Description("tedx_speakers")]
        TedXSpeakers,
        [Description("tedx_venues")]
        TedXVenues,
        [Description("talks")]
        Talks,
        [Description("themes")]
        Themes,
        [Description("playlists")]
        PlayLists,
        [Description("search")]
        Search,

    }
}
