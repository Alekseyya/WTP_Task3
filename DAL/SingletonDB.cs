using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Singleton
    {
        private static string _connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=WPFDb;Trusted_Connection=True;";

        private static Singleton _instance =null;
        private static readonly object padlock = new object();

        protected Singleton()
        {

        }
        public IDbConnection GetOpenConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
        public static Singleton Instance()
        {
            lock (padlock)
            {
                if (_instance == null)
                {
                    _instance = new Singleton();

                }
                return _instance;
            }
        }
    }
}
