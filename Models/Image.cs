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

        [NotMapped]
        public string ThumbnailFullPath
        {
            get 
            {
                return Path.Combine(Common.AppSettings.FileUploadPath, HorseID.ToString(), Common.ImageHelper.ThumbnailPrefix + FileName);
            }
        }

        [NotMapped]
        public string MediumFullPath
        {
            get
            {
                return Path.Combine(Common.AppSettings.FileUploadPath, HorseID.ToString(), Common.ImageHelper.MediumPrefix + FileName);
            }
        }

        [NotMapped]
        public string LargeFullPath
        {
            get
            {
                return Path.Combine(Common.AppSettings.FileUploadPath, HorseID.ToString(), Common.ImageHelper.LargePrefix + FileName);
            }
        }

        public virtual Horse Horse { get; set; }
    }
}