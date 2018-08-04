using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using Dapper;

namespace Oxinc.Models
{
    public static class Repository
    {
        private static readonly  string connectionstring= System.Configuration.ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        //@"Data Source=localhost;port=3307;Initial Catalog=userdb;User Id=root;password=Password-1";
        public static IEnumerable<object> execute(string procedurename,DynamicParameters param)
        {
            using (MySqlConnection sqlconn = new MySqlConnection(connectionstring))
            {
                sqlconn.Open();
                return sqlconn.Query<object>(procedurename,param,commandType:CommandType.StoredProcedure);
            }
        }
        public static IEnumerable<UserAuthorize> executequery(string query)
        {
            using (MySqlConnection sqlconn = new MySqlConnection(connectionstring))
            {

                    sqlconn.Open();
                    return sqlconn.Query<UserAuthorize>(query);
            }
        }
        public static IEnumerable<object> executegenericquery(string query)
        {
            using (MySqlConnection sqlconn = new MySqlConnection(connectionstring))
            {

                sqlconn.Open();
                return sqlconn.Query<object>(query);
            }
        }
    }
}