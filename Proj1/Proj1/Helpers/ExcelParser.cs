using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Proj1.Metamodel;
using Proj1.Metamodel.User;
using Z.Dapper.Plus;

namespace Proj1.Helpers
{
    public class ExcelParser
    {
        const string _userSheetName = "[Users$]";
        const string _roleSheetName = "[Roles$]";
        const string _rolesForUserSheetName = "[RolesForUsers$]";
        const string _userSiteAccessSheetName = "[UserSiteAccesses$]";
        

        public static void PersistExcelData(string excelJson)
        {
            DataSet dataSet = new DataSet();
            try
            {
                dataSet = (DataSet)JsonConvert.DeserializeObject(excelJson, typeof(DataSet));
            }
            catch
            {
                throw new Exception("Parse exception. Please contact the adminstrator");
            }

            IEnumerable<User> users;
            IEnumerable<Role> roles;
            IEnumerable<RolesForUser> rolesForUsers;
            IEnumerable<UserSiteAccess> userSiteAccesses;
            FillEntities(dataSet, out users, out roles, out userSiteAccesses, out rolesForUsers);
            //Persist(users, roles, userSiteAccesses, rolesForUsers);
        }

        private static void FillEntities(DataSet dataSet, out IEnumerable<User> users, out IEnumerable<Role> roles, out IEnumerable<UserSiteAccess> userSiteAccesses, out IEnumerable<RolesForUser> rolesForUsers)
        {
            users = FillUser(dataSet.Tables[_userSheetName]);
            ValidationHelper.ValidateEntities(users);
            
            roles = FillRole(dataSet.Tables[_roleSheetName]);

            rolesForUsers = FillRoleForUser(dataSet.Tables[_rolesForUserSheetName]);

            userSiteAccesses = FillUserSiteAccess(dataSet.Tables[_userSiteAccessSheetName]);
        }

        //private static DataSet GetDataSet(string worksheetName)
        //{
        //    String sExcelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties=Excel 12.0";
        //    //String sExcelConnectionString = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=" + filename + ";Extended Properties=Excel 8.0;Mode=ReadWrite|Share Deny None;Persist Security Info=False";
        //    OleDbConnection OleDbConn = new OleDbConnection(sExcelConnectionString);

        //    OleDbConn.Open();
        //    OleDbCommand OleDbCmd = new OleDbCommand(("SELECT * FROM " + worksheetName), OleDbConn);

        //    DataSet dataSet = new DataSet();

        //    OleDbDataAdapter sda = new OleDbDataAdapter(OleDbCmd);
        //    sda.Fill(dataSet);
        //    OleDbConn.Close();
        //    return dataSet;
        //}

        private static IEnumerable<User> FillUser(DataTable userTable)
        {
            List<User> users = new List<User>();
            var rowCount = 0;

            foreach (DataRow row in userTable.Rows)
            {
                if (row.ItemArray.All(x => x == null || (x != null && string.IsNullOrWhiteSpace(x.ToString()))))
                    continue;

                rowCount++;

                try
                {
                    users.Add(new User
                    {
                        FirstName = row.Field<string>("FirstName"),
                        LastName = row.Field<string>("LastName"),
                        MobileNumber = Convert.ToInt64(row["MobileNumber"]),
                        Password = row.Field<string>("Password"),
                        LoginName = row.Field<string>("LoginName"),
                        ActivationDate = row.Field<DateTime?>("ActivationDate"),
                        DeactivationDate = row.Field<DateTime?>("DeactivationDate"),
                        Email = row.Field<string>("Email"),
                        IsBlocked = row.Field<bool>("IsBlocked"),
                        ForcePasswordReset = row.Field<bool>("ForcePasswordReset"),
                        LoginBlockedTime = row.Field<DateTime?>("LoginBlockedTime"),
                        LoginFailureCounter = Convert.ToInt32(row["LoginFailureCounter"]),
                        Row = rowCount
                        //Status = row.Field<StatusValues?>("Status") ?? StatusValues.Unknown
                    });
                }
                catch(InvalidCastException ex)
                {
                    ValidationResult.AddValidation(rowCount, ex.Message);
                    ValidationResult.AddValidation(rowCount, "Please check the date formats of this row.");
                }
            }

            return users;
        }

        private static IEnumerable<Role> FillRole(DataTable roleTable)
        {
            List<Role> roles = new List<Role>();

            foreach (DataRow row in roleTable.Rows)
            {
                if (row.ItemArray.All(x => x == null || (x != null && string.IsNullOrWhiteSpace(x.ToString()))))
                    continue;

                roles.Add(
                    new Role
                    {
                        Name = row.Field<string>("Name"),
                        SystemDefinedName = row.Field<string>("SystemDefinedName"),
                        IsActive = row.Field<bool>("IsActive"),
                        ActivationDate = row.Field<DateTime?>("ActivationDate"),
                        DeactivationDate = row.Field<DateTime?>("DeactivationDate"),
                        IsSystemDefined = row.Field<bool>("IsSystemDefined")
                    });
            }

            return roles;
        }

        private static IEnumerable<RolesForUser> FillRoleForUser(DataTable rolesForUserTable)
        {
            List<RolesForUser> rolesForUsers = new List<RolesForUser>();

            foreach (DataRow row in rolesForUserTable.Rows)
            {
                if (row.ItemArray.All(x => x == null || (x != null && string.IsNullOrWhiteSpace(x.ToString()))))
                    continue;

                rolesForUsers.Add(
                new RolesForUser
                {
                    IsActive = row.Field<bool>("IsActive"),
                    ActivationDate = row.Field<DateTime?>("ActivationDate"),
                    DeactivationDate = row.Field<DateTime?>("DeactivationDate"),
                    RoleId = Convert.ToInt32(row["RoleId"]),
                    UserId = Convert.ToInt32(row["UserId"])
                });
            }
            return rolesForUsers;
        }

        private static IEnumerable<UserSiteAccess> FillUserSiteAccess(DataTable userSiteAccessTable)
        {
            List<UserSiteAccess> userSiteAccesses = new List<UserSiteAccess>();

            foreach (DataRow row in userSiteAccessTable.Rows)
            {
                if (row.ItemArray.All(x => x == null || (x != null && string.IsNullOrWhiteSpace(x.ToString()))))
                    continue;
                userSiteAccesses.Add(
                new UserSiteAccess
                {
                    IsActive = row.Field<bool>("IsActive"),
                    SiteConfigId = Convert.ToInt32(row["SiteConfigId"]),
                    UserId = Convert.ToInt32(row["UserId"])
                });
            }
            return userSiteAccesses;
        }

        private static void Persist(IEnumerable<User> users, IEnumerable<Role> roles, IEnumerable<UserSiteAccess> userSiteAccesses, IEnumerable<RolesForUser> rolesForUsers)
        {
            PersistUser(users);
            PersistRole(roles);
            PersistRolesForUser(rolesForUsers);
            PersistUserSiteAccess(userSiteAccesses);
        }

        private static void PersistUser(IEnumerable<User> users)
        {
            var connection = WebApiConfig.Connection();
            try
            {
                connection.Open();
                DapperPlusManager.Entity<User>().Table("Users");
                connection.BulkInsert(users);
                connection.Close();
            }
            catch (MySqlException)
            {
                throw;
            }
        }

        private static void PersistRole(IEnumerable<Role> roles)
        {
            var connection = WebApiConfig.Connection();
            try
            {
                connection.Open();
                DapperPlusManager.Entity<Role>().Table("Roles");
                connection.BulkInsert(roles);
                connection.Close();
            }
            catch (MySqlException)
            {
                throw;
            }
        }

        private static void PersistRolesForUser(IEnumerable<RolesForUser> rolesForUsers)
        {
            var connection = WebApiConfig.Connection();
            try
            {
                connection.Open();
                DapperPlusManager.Entity<RolesForUser>().Table("RolesForUsers");
                connection.BulkInsert(rolesForUsers);
                connection.Close();
            }
            catch (MySqlException)
            {
                throw;
            }
        }

        private static void PersistUserSiteAccess(IEnumerable<UserSiteAccess> userSiteAccesses)
        {
            var connection = WebApiConfig.Connection();
            try
            {
                connection.Open();
                DapperPlusManager.Entity<UserSiteAccess>().Table("UserSiteAccesses");
                connection.BulkInsert(userSiteAccesses);
                connection.Close();
            }
            catch (MySqlException)
            {
                throw;
            }
        }

        public static IEnumerable<User> GetUsersForBrowse()
        {
            var connection = WebApiConfig.Connection();
            var users = new List<User>();
            try
            {
                connection.Open();
                var sqlCommand = "SELECT * FROM Users WHERE Status = 'Active'";
                users.AddRange(connection.Query<User>(sqlCommand));
                connection.Close();
            }
            catch(MySqlException)
            {
                throw;
            }

            return users;
        }
    }
}