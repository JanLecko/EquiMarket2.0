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

        public const string ThumbnailPrefix = "thumb_";
        public const string MediumPrefix = "medium_";
        public const string LargePrefix = "large_";

        public static Dictionary<string, string> versions = new Dictionary<string, string>(){
            { ThumbnailPrefix, Common.AppSettings.ThumbnailSettings },
            { MediumPrefix, Common.AppSettings.MediumSettings },
            { LargePrefix, Common.AppSettings.LargeSettings }
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
                foreach (string prefix in versions.Keys)
                {
                    //Generate a filename (GUIDs are best).
                    string fileName = Path.Combine(uploadFolder, prefix + hpf.FileName);

                    //Let the image builder add the correct extension based on the output file type
                    fileName = ImageBuilder.Current.Build(hpf, fileName, new ResizeSettings(versions[prefix]), false, true);

                    images.Add(new Image()
                    {
                        HorseID = horseID,
                        FileName = fileName
                    });
                }
            }
            return images;
        }




    }
}