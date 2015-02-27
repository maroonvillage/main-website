using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaroonVillage.Core.DomainModel
{
    public class AlprCapture
    {

        public DateTime CaptureDate { get; set; }
        public DateTime CaptureTime { get; set; }

        public float Lattitude { get; set; }
        public float Longitude { get; set; }

    }
}
