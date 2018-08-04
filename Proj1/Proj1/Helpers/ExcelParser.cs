using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.OleDb;
using System.Linq;
using MySql.Data.MySqlClient;
using Proj1.Metamodel;
using Proj1.Metamodel.User;
using Z.Dapper.Plus;
 
namespace Proj1.Helpers
{
    public class ExcelParser
    {
        public static string FileName = "D:\\UsersExcel.xlsx";

        const string _userSheetName = "[Users$]";
        const string _roleSheetName = "[Roles$]";
        const string _rolesForUserSheetName = "[RolesForUsers$]";
        const string _userSiteAccessSheetName = "[UserSiteAccesses$]";

        public static void PersistExcelData()
        {
            FillEntities(out IEnumerable<User> users, out IEnumerable<Role> roles, out IEnumerable<UserSiteAccess> userSiteAccesses, out IEnumerable<RolesForUser> rolesForUsers);
            Persist(users, roles, userSiteAccesses, rolesForUsers);
        }

        private static void FillEntities(out IEnumerable<User> users, out IEnumerable<Role> roles, out IEnumerable<UserSiteAccess> userSiteAccesses, out IEnumerable<RolesForUser> rolesForUsers)
        {
            var userDataSet = GetDataSet(_userSheetName);
            users = FillUser(userDataSet);
            ValidationHelper.ValidateEntities(users);

            var userSiteAccessDataSet = GetDataSet(_userSiteAccessSheetName);
            userSiteAccesses = FillUserSiteAccess(userSiteAccessDataSet);

            var roleDataSet = GetDataSet(_roleSheetName);
            roles = FillRole(roleDataSet);

            var rolesForUserDataSet = GetDataSet(_rolesForUserSheetName);
            rolesForUsers = FillRoleForUser(rolesForUserDataSet);
        }

        private static DataSet GetDataSet(string worksheetName)
        {
            String sExcelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties=Excel 12.0";
            //String sExcelConnectionString = "Provider=Microsoft.Jet.Oledb.4.0;Data Source=" + filename + ";Extended Properties=Excel 8.0;Mode=ReadWrite|Share Deny None;Persist Security Info=False";
            OleDbConnection OleDbConn = new OleDbConnection(sExcelConnectionString);

            OleDbConn.Open();
            OleDbCommand OleDbCmd = new OleDbCommand(("SELECT * FROM " + worksheetName), OleDbConn);

            DataSet dataSet = new DataSet();

            OleDbDataAdapter sda = new OleDbDataAdapter(OleDbCmd);
            sda.Fill(dataSet);
            OleDbConn.Close();
            return dataSet;
        }

        private static IEnumerable<User> FillUser(DataSet parsedDataSet)
        {
            var userTable = parsedDataSet.Tables[0];
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
                    ValidationResult.AddValidation(rowCount, "Please check the date formats of this row.");
                }
            }

            return users;
        }

        private static IEnumerable<Role> FillRole(DataSet parsedDataSet)
        {
            var roleTable = parsedDataSet.Tables[0];
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

        private static IEnumerable<RolesForUser> FillRoleForUser(DataSet parsedDataSet)
        {
            var rolesForUserTable = parsedDataSet.Tables[0];
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

        private static IEnumerable<UserSiteAccess> FillUserSiteAccess(DataSet parsedDataSet)
        {
            var userSiteAccessTable = parsedDataSet.Tables[0];
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
    }
}