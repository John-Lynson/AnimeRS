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
        public readonly string connectionString;
        public DatabaseConnection(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void OpenConnection(Action<DbConnection> action)
        {
            using (DbConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                action.Invoke(connection);
            }
        }
    }
}
