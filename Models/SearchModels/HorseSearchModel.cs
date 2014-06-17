using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EquiMarket.Models.SearchModels
{
    public class HorseSearchModel
    {
        public int? AgeFrom { get; set; }
        public int? AgeTo { get; set; }
        public int? KVHFrom { get; set; } 
        public int? KVHTo { get; set; }
        public int[] Breeds { get; set; }
        public decimal? PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }

        public bool HasValue {

            get {
                return (
                    AgeFrom.HasValue    ||
                    AgeTo.HasValue      ||
                    KVHFrom.HasValue    ||
                    KVHTo.HasValue      ||
                    PriceFrom.HasValue  ||
                    PriceTo.HasValue    ||
                    (Breeds != null && Breeds.Length > 0));
            }

        }

    }
}