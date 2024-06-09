using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ShippingService.Connections
{
    public class Connect
    {
        private readonly IConfiguration _config;
        private readonly SqlConnection _connection;

        public Connect(IConfiguration config)
        {
            _config = config;
            _connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        }

        public SqlConnection GetConnectDb()
        {
            return _connection;
        }
    }
}