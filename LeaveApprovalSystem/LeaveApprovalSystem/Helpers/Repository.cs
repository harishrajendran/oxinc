using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using Dapper;
using LeaveApprovalSystem.Models;

namespace LeaveApprovalSystem.Helpers
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