using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EquiMarket.Common;
using EquiMarket.DAL;
using System.Data.Entity.Spatial;

namespace EquiMarket.Models
{
    public enum Sex
    {
        Stallion,
        Gelding,
        Mare
    }

    public class Horse
    {
        public int ID { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        //dd.MM.yyyy

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime BirthDate { get; set; }

        public Sex Sex { get; set; }

        public int BreedID { get; set; }

        [NotMapped]
        [AutoComplete("Breed", "Autocomplete", "BreedID")]
        public string BreedLabel 
        { 
            get 
            {
                if (BreedID > 0)
                {
                    EquiContext db = new EquiContext();
                    return db.Breeds.Find(BreedID).TitleSK;
                }
                else return String.Empty;
            } 
        }

        [StringLength(255)]
        public string FathersName { get; set; }

        [StringLength(255)]
        public string MothersName { get; set; }
        
        public int? KVH { get; set; }
        
        public int? KVP { get; set; }

        [StringLength(2055)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        
        public decimal? Price { get; set; }

        public bool IsBreedingOffer { get; set; }

        public DbGeography Location { get; set; }

        //public int UserId { get; set; }

        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Video> Videos { get; set; }

    }
}