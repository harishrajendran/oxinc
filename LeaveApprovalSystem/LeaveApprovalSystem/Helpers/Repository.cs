using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using Dapper;
using LeaveApprovalSystem.Models;
using System.Configuration;

namespace LeaveApprovalSystem.Helpers
{
    public static class Repository
    {
        public static string GetRDSConnectionString()
        {
            var appConfig = ConfigurationManager.AppSettings;

            string dbname = appConfig["RDS_DB_NAME"];

            if (string.IsNullOrEmpty(dbname)) return null;

            string username = appConfig["RDS_USERNAME"];
            string password = appConfig["RDS_PASSWORD"];
            string hostname = appConfig["RDS_HOSTNAME"];
            string port = appConfig["RDS_PORT"];

            return "Data Source=" + hostname + ";Initial Catalog=" + dbname + ";User ID=" + username + ";Password=" + password + ";";
        }

        private static readonly string connectionstring = GetRDSConnectionString();
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
        public static List<BrowseModel> executeLeaveRequsetBrowsequery(filtercriteria param)
        {
            using (MySqlConnection sqlconn = new MySqlConnection(connectionstring))
            {

                sqlconn.Open();
                return sqlconn.Query<BrowseModel>("GetLeaveApprovalRequestForBrowse", param, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public static LeaveApprovalEdit executeLeaveRequsetEditquery(int param)
        {
            using (MySqlConnection sqlconn = new MySqlConnection(connectionstring))
            {

                sqlconn.Open();
                return sqlconn.Query<LeaveApprovalEdit>("GetLeaveApprovalRequestForEdit", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
    }
}