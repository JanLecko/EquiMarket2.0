using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EquiMarket.Models
{
    public class Image
    {
        public int ID { get; set; }
        public string FilePath { get; set; }
        public string ThmubnailFilePath { get; set; }
        public int HorseID { get; set; }

        public virtual Horse Horse { get; set; }
    }
}