using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EquiMarket.Models
{
    public class Breed
    {
        public int ID { get; set; }
        public string TitleSK { get; set; }
        public string TitleEN { get; set; }

        [NotMapped]
        public string IdAsString { get { return ID.ToString(); } }
    }
}