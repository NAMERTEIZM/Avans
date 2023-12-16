using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avans.Core.Contexts
{
    public class ConnectionHelper
    {
        private static IConfiguration _configuration = null!;


        public static void SetConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public static IConfiguration GetConfiguration()
        {
            return _configuration;
        }


        public static string GetConnectionString()
        {
            return GetConfiguration().GetSection("ConnectionStrings:myconn").Value;
        }
    }
}
