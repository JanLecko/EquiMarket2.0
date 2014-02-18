using EquiMarket.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Web;

namespace EquiMarket.Models
{
    public class Image
    {
        public int ID { get; set; }
        public string FileName { get; set; }
        
        public int HorseID { get; set; }

        public virtual Horse Horse { get; set; }

        [NotMapped]
        public string ThumbnailFullPath
        {
            get 
            {
                return ImageHelper.GetImageFullPath(FileName, HorseID, ImageFormat.Thumbnail);
            }
        }

        [NotMapped]
        public string MediumFullPath
        {
            get
            {
                return ImageHelper.GetImageFullPath(FileName, HorseID, ImageFormat.Medium);
            }
        }

        [NotMapped]
        public string LargeFullPath
        {
            get
            {
                return ImageHelper.GetImageFullPath(FileName, HorseID, ImageFormat.Large);
            }
        }

        
    }
}