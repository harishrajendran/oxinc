using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using Dapper;
using DailyAttendanceSystem.Models;

namespace DailyAttendanceSystem.Helper
{
    public static class Repository
    {
        private static readonly string connectionstring = @"Data Source=localhost;port=3307;Initial Catalog=userdb;User Id=root;password=Password-1";
        //System.Configuration.ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        //@"Data Source=localhost;port=3307;Initial Catalog=userdb;User Id=root;password=Password-1";
        public static IEnumerable<object> executeBrowserSP(string procedurename, DynamicParameters param)
        {
            using (MySqlConnection sqlconn = new MySqlConnection(connectionstring))
            {
                sqlconn.Open();
                return sqlconn.Query<object>(procedurename, param, commandType: CommandType.StoredProcedure);
            }
        }
        public static void executeINsertOrUpdateSP(string procedurename, object param)
        {
            using (MySqlConnection sqlconn = new MySqlConnection(connectionstring))
            {
                sqlconn.Open();
                sqlconn.Execute(procedurename, param, commandType: CommandType.StoredProcedure);                
            }
        }
        public static IEnumerable<StudentDetail> executeStudentDetailquery(string query)
        {
            using (MySqlConnection sqlconn = new MySqlConnection(connectionstring))
            {

                sqlconn.Open();
                return sqlconn.Query<StudentDetail>(query);
            }
        }
        public static IEnumerable<ClassConfig> executeclassconfigquery(string query)
        {
            using (MySqlConnection sqlconn = new MySqlConnection(connectionstring))
            {

                sqlconn.Open();
                return sqlconn.Query<ClassConfig>(query);
            }
        }
        public static IEnumerable<sectionconfig> executesectionconfigquery(string query)
        {
            using (MySqlConnection sqlconn = new MySqlConnection(connectionstring))
            {

                sqlconn.Open();
                return sqlconn.Query<sectionconfig>(query);
            }
        }
        public static IEnumerable<DailyAttendanceDetail> executeRFIDQueryquery(string query)
        {
            using (MySqlConnection sqlconn = new MySqlConnection(connectionstring))
            {

                sqlconn.Open();
                return sqlconn.Query<DailyAttendanceDetail>(query);
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
