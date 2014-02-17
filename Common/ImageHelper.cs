using EquiMarket.Models;
using ImageResizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace EquiMarket.Common
{
    public static class ImageHelper
    {

        // TODO : 
        // hod to do web.config 
        // nacitat pomocou System.Configuration.ConfigurationManager.AppSettings["XXX"]

        public const string ThumbnailSuffix = "_thumb";
        public const string MediumSuffix = "_medium";
        public const string LargeSuffix = "_large";

        public static Dictionary<string, string> versions = new Dictionary<string, string>(){
            { ThumbnailSuffix, System.Configuration.ConfigurationManager.AppSettings["ThumbnailSettings"]},
            { MediumSuffix, System.Configuration.ConfigurationManager.AppSettings["MediumSettings"]},
            { LargeSuffix, System.Configuration.ConfigurationManager.AppSettings["LargeSettings"]}
        };

        public static List<Image> SaveImages(HttpRequestBase request, int horseID)
        {
            var images = new List<Image>();

            foreach (string fileKey in request.Files.Keys)
            {
                HttpPostedFileBase hpf = request.Files[fileKey] as HttpPostedFileBase;
                
                if (hpf.ContentLength <= 0)
                    continue;

                //   AppDomain.CurrentDomain.BaseDirectory,
                string uploadFolder = HttpContext.Current.Server.MapPath(string.Format("~/Files/", horseID.ToString()));

                if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);

                //Save thumbnail
                foreach (string suffix in versions.Keys)
                {
                    //Generate a filename (GUIDs are best).
                    string fileName = Path.Combine(uploadFolder, hpf.FileName + suffix);

                    //Let the image builder add the correct extension based on the output file type
                    fileName = ImageBuilder.Current.Build(hpf, fileName, new ResizeSettings(versions[suffix]), false, true);

                    images.Add(new Image()
                    {
                        HorseID = horseID,
                        FilePath = fileName
                    });
                }
            }
            return images;
        }




    }
}