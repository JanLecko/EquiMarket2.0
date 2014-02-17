using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EquiMarket.Models
{
    public class Video
    {
        public int ID { get; set; }
        public string URL { get; set; }
        public int HorseID { get; set; }

        public virtual Horse Horse { get; set; }
    }
}