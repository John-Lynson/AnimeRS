using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeRS.Data.Database
{
    public class DatabaseConnection
    {
        public string ConnectionString { get; private set; }

        public DatabaseConnection(string connectionString)
        {
            ConnectionString = connectionString;
        }

        // Andere methodes en logica...
    }
}
