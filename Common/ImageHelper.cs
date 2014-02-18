using EquiMarket.Models;
using ImageResizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace EquiMarket.Common
{

    public enum ImageFormat
    {
        Thumbnail,
        Medium,
        Large,
    }

    public static class ImageHelper
    {

        public const string ContentType = "jpg"; // TODO: ...


        //public const string ThumbnailPrefix = "thumb_";
        //public const string MediumPrefix = "medium_";
        //public const string LargePrefix = "large_";

        public static Dictionary<string, string> versions = new Dictionary<string, string>(){
            { ImageFormat.Thumbnail.ToString(), Common.AppSettings.ThumbnailSettings },
            { ImageFormat.Medium.ToString(), Common.AppSettings.MediumSettings },
            { ImageFormat.Large.ToString(), Common.AppSettings.LargeSettings }
        };

        public static List<Image> SaveImages(HttpRequestBase request, Horse horse)
        {
            var images = new List<Image>();

            foreach (string fileKey in request.Files.Keys)
            {
                HttpPostedFileBase hpf = request.Files[fileKey] as HttpPostedFileBase;
                
                if (hpf.ContentLength <= 0)
                    continue;

                //Save thumbnail
                foreach (string prefix in versions.Keys)
                {
                    //Generate a filename (GUIDs are best).
                    string fileName = GetImageFullPath(hpf.FileName, horse.ID, (ImageFormat)Enum.Parse(typeof(ImageFormat), prefix));

                    //Let the image builder add the correct extension based on the output file type
                    fileName = ImageBuilder.Current.Build(hpf, fileName, new ResizeSettings(versions[prefix]), false, true);

                    images.Add(new Image()
                    {
                        HorseID = horse.ID,
                        FileName = fileName
                    });
                }
            }
            return images;
        }

        public static string GetImageFullPath(string fileName, int horseID, ImageFormat format)
        {
            switch (format)
            {
                case ImageFormat.Thumbnail:
                    return Path.Combine(GetUploadPath(horseID, true), format + "_" + fileName);
                case ImageFormat.Medium:
                    return Path.Combine(GetUploadPath(horseID, true), format + "_" + fileName);
                case ImageFormat.Large:
                    return Path.Combine(GetUploadPath(horseID, true), format + "_" + fileName);
                default:
                    return string.Empty;
            }   
        }

        public static string GetUploadPath(int horseID, bool create)
        {
            string uploadFolder = Path.Combine(Common.AppSettings.FileUploadPath, horseID.ToString());
            
            if (!Directory.Exists(uploadFolder)) 
                Directory.CreateDirectory(uploadFolder);

            return uploadFolder;
        }


        public static string DefaultImagePath(ImageFormat format)
        {
            switch (format)
            {
                case ImageFormat.Thumbnail:
                    return HttpContext.Current.Server.MapPath("~/Content/Img/thumb_default.jpg");
                case ImageFormat.Medium:
                    return HttpContext.Current.Server.MapPath("~/Content/Img/medium_default.jpg");
                case ImageFormat.Large:
                    return HttpContext.Current.Server.MapPath("~/Content/Img/large_default.jpg");
                default:
                    return string.Empty;
            }
        }

    }
}