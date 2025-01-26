using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using _10023767_P2.Properties;

namespace _10023767_P2.Repositories
{
    public abstract class RepositoryBase
    {
        private readonly string _connString; 
        public RepositoryBase()
        {
            _connString =  Settings.Default.MyAppDatabaseConnString; 
        }
        protected SqlConnection GetSqlConnection()
        {
            return new SqlConnection(_connString);
        }
    }
}
