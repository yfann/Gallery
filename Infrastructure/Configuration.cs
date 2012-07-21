using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Infrastructure
{
    public class Configuration
    {
        public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["Gallery"].ConnectionString;
        public static readonly int ThumbnailWidth = Convert.ToInt32(ConfigurationManager.AppSettings["ThumbnailWidth"]);
        public static readonly int ThumbnailHeight = Convert.ToInt32(ConfigurationManager.AppSettings["ThumbnailHeight"]);
    }
}