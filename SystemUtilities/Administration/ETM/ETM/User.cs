using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using McAltWebsite.App_Code;
using System.Web;

namespace McAltWebsite.Models
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

        public void Search(int id)
        {
            GetUserByID(id);
        }

        public void Search(string name)
        {
            GetUserByName(name);
        }

        private void GetUserByName(string name)
        {
            if (string.IsNullOrEmpty(name) == false && string.IsNullOrWhiteSpace(name) == false)
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    string query = "select * from Users where " +
                                   "Users.Username = @0";

                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["content"].ConnectionString;
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@0", name);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            this.ID = int.Parse(reader[0].ToString());
                            this.Role = int.Parse(reader[1].ToString());
                            this.RegisterDate = DateTime.Parse(reader[2].ToString());
                            this.Username = reader[3].ToString();
                            this.Salt = reader[4].ToString();
                            this.Hash = reader[5].ToString();
                            this.Email = reader[6].ToString();
                        }
                    }
                    command.Dispose();
                    connection.Dispose();
                }
            }
        }

        private void GetUserByID(int id)
        {
            if (id > 0)
            {
                using (SqlConnection connection = new SqlConnection())
                {
                    string query = "select * from Users where " +
                                   "Users.ID = @0";

                    connection.ConnectionString = ConfigurationManager.ConnectionStrings["content"].ConnectionString;
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@0", id);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            this.ID = int.Parse(reader[0].ToString());
                            this.Role = int.Parse(reader[1].ToString());
                            this.RegisterDate = DateTime.Parse(reader[2].ToString());
                            this.Username = reader[3].ToString();
                            this.Salt = reader[4].ToString();
                            this.Hash = reader[5].ToString();
                            this.Email = reader[6].ToString();
                        }
                    }
                    command.Dispose();
                    connection.Dispose();
                }
            }
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
                string query = "insert into Users(UserRole,RegisteredOn,Username,UserSalt,UserHash,Email,Banned) " +
                                "values(@0, @1, @2, @3, @4, @5,@6)";
                connection.ConnectionString = ConfigurationManager.ConnectionStrings["content"].ConnectionString;
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@0", 1);
                command.Parameters.AddWithValue("@1", this.RegisterDate);
                command.Parameters.AddWithValue("@2", this.Username);
                command.Parameters.AddWithValue("@3", this.Salt);
                command.Parameters.AddWithValue("@4", this.Hash);
                command.Parameters.AddWithValue("@5", this.Email);
                command.Parameters.AddWithValue("@6", false);
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

                //delete ticket thread
                query = "delete from Ticket_Thread " +
                        "where Ticket_Thread.Regarding in " +
                        "(select User_Tickets.ID " + 
                        "from User_Tickets " + 
                        "where User_Tickets.SubmittedBy = @0)";
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

                //delete ban history
                query = "delete from UserBanHistory " +
                               "where UserBanHistory.ActionTarget = @0";
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@0", this.ID);
                command.ExecuteNonQuery();
                command.Dispose();

                //delete logins
                query = "delete from BannedUsers " +
                               "where BannedUsers.BannedUser = @0";
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