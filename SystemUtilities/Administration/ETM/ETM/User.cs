using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using McAltCom.App_Code;
using System.Web;

namespace Mc-Alt.Models
{
    public partial class User
    {
        public int ID { get; set; }
        public int Role { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Username { get; set; }
        public string Salt { get; private set; }
        public string Hash { get; private set; }
        public string Email { get; set; }

        public User()
        {
            SetDefaults();
        }

        public void SetPassword(string password)
        {
            HashPassword(password);
        }

        private void SetDefaults()
        {
            this.ID = 0;
            this.Role = 0;
            this.RegisterDate = DateTime.Now;
            this.Username = string.Empty;
            this.Salt = string.Empty;
            this.Hash = string.Empty;
            this.Email = string.Empty;
        }

        private void HashPassword(string password)
        {
            this.Salt = App_Code.Security.GetSalt(15);
            this.Hash = App_Code.Security.OneWayEncrypt(password, this.Salt);
        }

        #region Record Keeping

        public void Save()
        {
            if (this.ID == 0)
            {
                CreateRecord();
            }
            else
            {
                UpdateRecord();
            }
        }

        public void Delete()
        {
            RemoveRecord();
        }

        private void CreateRecord()
        {
            using (SqlConnection connection = new SqlConnection())
            {
                string query = "insert into Users(UserRole,RegisteredOn,Username,UserSalt,UserHash,Email) " +
                                "values(@0, @1, @2, @3, @4, @5)";
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["content"].ConnectionString;
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@0", 1);
                command.Parameters.AddWithValue("@1", this.RegisterDate);
                command.Parameters.AddWithValue("@2", this.Username);
                command.Parameters.AddWithValue("@3", this.Salt);
                command.Parameters.AddWithValue("@4", this.Hash);
                command.Parameters.AddWithValue("@5", this.Email);
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
        }

        private void UpdateRecord()
        {
            using (SqlConnection connection = new SqlConnection())
            {
                string query = "update Users " + 
                               "set UserRole = @0, RegisteredOn = @1, Username = @2, UserSalt = @3, UserHash = @4, Email = @5 " +
                               "where Users.ID = @6";
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["content"].ConnectionString;
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@0", this.Role);
                command.Parameters.AddWithValue("@1", this.RegisterDate);
                command.Parameters.AddWithValue("@2", this.Username);
                command.Parameters.AddWithValue("@3", this.Salt);
                command.Parameters.AddWithValue("@4", this.Hash);
                command.Parameters.AddWithValue("@5", this.Email);
                command.Parameters.AddWithValue("@6", this.ID);
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
            }
        }

        private void RemoveRecord()
        {
            //delete all records associated to this user
            RemoveAssociatedRecords();
            
            //delete user
            using (SqlConnection connection = new SqlConnection())
            {
                string query = "delete from Users " +
                               "where Users.ID = @0";
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["content"].ConnectionString;
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@0", this.ID);
                command.ExecuteNonQuery();
                command.Dispose();
            }
        }

        private void RemoveAssociatedRecords()
        {
            using (SqlConnection connection = new SqlConnection())
            {
                string query = string.Empty;
                SqlCommand command;

                connection.ConnectionString = ConfigurationManager.ConnectionStrings["content"].ConnectionString;
                connection.Open();

                //delete logins
                query = "delete from Logins " +
                               "where Logins.WhoIs = @0";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@0", this.ID);
                command.ExecuteNonQuery();
                command.Dispose();

                //delete logouts
                query = "delete from Logouts " +
                               "where Logouts.WhoIs = @0";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@0", this.ID);
                command.ExecuteNonQuery();
                command.Dispose();

                //delete Accounts generated
                query = "delete from GeneratedAccount " +
                               "where GeneratedAccount.CreatedBy = @0";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@0", this.ID);
                command.ExecuteNonQuery();
                command.Dispose();

                //delete tickets
                query = "delete from User_Tickets " +
                               "where User_Tickets.SubmittedBy = @0";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@0", this.ID);
                command.ExecuteNonQuery();
                command.Dispose();

                connection.Close();
            }
        }

        #endregion

    }
}