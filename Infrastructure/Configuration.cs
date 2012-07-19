using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Infrastructure
{
    public class Configuration
    {
        public static readonly string ConnectionString;

        static Configuration()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["Gallery"].ConnectionString;
        }
    }
}