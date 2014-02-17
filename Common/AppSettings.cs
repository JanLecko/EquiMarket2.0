using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EquiMarket.Common
{
    public static class AppSettings
    {

        public static string ThumbnailSettings {
            get { return System.Configuration.ConfigurationManager.AppSettings["ThumbnailSettings"]; }
        }

        public static string MediumSettings {
            get { return System.Configuration.ConfigurationManager.AppSettings["MediumSettings"]; }
        }

        public static string LargeSettings {
            get { return System.Configuration.ConfigurationManager.AppSettings["LargeSettings"]; }
        }

        public static string FileUploadPath {
            get { return System.Configuration.ConfigurationManager.AppSettings["FileUploadPath"]; }
        }
        
    }
}