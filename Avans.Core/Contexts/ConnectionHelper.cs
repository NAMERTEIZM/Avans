using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avans.Core.Contexts
{
    public class ConnectionHelper
    {
        private static IConfiguration _configuration = null!;
        private static string _connstr;


        public static void SetConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
            _connstr = configuration.GetConnectionString("myconn");

        }
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connstr);
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
