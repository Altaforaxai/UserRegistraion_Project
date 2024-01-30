using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserRegistraion_Project
{
    internal class User_data
    {
        
        private string connectionString = "Data Source=DESKTOP-KMSQP1Q;Initial Catalog=UserDb;Integrated Security=True";

        public void Attempts(string username)
        {
            if (IsUserExist(new User { Username = username }))
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string updateQuery = "UPDATE Users SET Attempts = 1 WHERE Username = @Username";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, connection);
                    updateCmd.Parameters.AddWithValue("@Username", username);
                    updateCmd.ExecuteNonQuery();
                }
            }
        }

        public int GetNumberOfAttemts(string username)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string updateQuery = "select Attempts from Users WHERE Username = @username";
                SqlCommand updateCmd = new SqlCommand(updateQuery, connection);
                updateCmd.Parameters.AddWithValue("@Username", username);
                updateCmd.ExecuteNonQuery();

                return 0;
            }

            

        }

        public int UpdateUsersAttempts(string username, int Attempts)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string updateQuery = "UPDATE Users SET Attempts = @Attempts WHERE Username = @username";
                SqlCommand updateCmd = new SqlCommand(updateQuery, connection);
                updateCmd.Parameters.AddWithValue("@username", username);
                updateCmd.Parameters.AddWithValue("@attempts", Attempts);
                updateCmd.ExecuteNonQuery();

                return 0;
            }
        }

        public bool IsUserExist(User user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if the username or email already exists
                string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username And Password =@Password";
                SqlCommand checkCmd = new SqlCommand(checkQuery, connection);
                checkCmd.Parameters.AddWithValue("@Username", user.Username);
                checkCmd.Parameters.AddWithValue("@Password", user.UserPassword);
               // checkCmd.Parameters.AddWithValue("@EmailVeified", user.Email);
                int existingUserCount = (int)checkCmd.ExecuteScalar();

                return existingUserCount > 0;
            }
        }

        public bool IsLocked(string username)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT Attempts FROM Users WHERE Username = @Username";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@Username", username);
                int attempts = (int)cmd.ExecuteScalar();

                return attempts > 3;
            }
        }




        public void AddUser(User user)
        {
            bool userExists = IsUserExist(user);

            if (userExists)
            {
                Console.WriteLine("Username already exists. Please choose a different username.");
            }
            else
            {
                //Console.WriteLine("user successfully added");
                // Code to insert the user into the database
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string insertQuery = "INSERT INTO Users (ID,Username, Password) VALUES (@id, @Username, @Password)";
                    SqlCommand insertCmd = new SqlCommand(insertQuery, connection);
                    insertCmd.Parameters.AddWithValue("@id", user.id);
                    insertCmd.Parameters.AddWithValue("@Username", user.Username);
                    
                    insertCmd.Parameters.AddWithValue("@Password", user.UserPassword);

                    int rowsAffected = insertCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("\n User created successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Failed to create user. Please try again.");
                    }
                }
            }
        }
        public bool LoginUser(string Username, String Password)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT COUNT(*) FROM Users WHERE Username = @Username And Password = @Password";
                SqlCommand cmd= new SqlCommand(sql, connection); 
                //cmd=new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@Username", Username);
                cmd.Parameters.AddWithValue("@Password", Password);
               // cmd.Parameters.AddWithValue("@Email", Email);

                int usercCount = (int) cmd.ExecuteScalar();
                if (usercCount < 0)
                {
                    Console.WriteLine("User does not exist");
                    return true;
                }
                else
                {
                    Attempts(Username);
                    int attempts = GetNumberOfAttemts(Username);
                    if (attempts > 3 || IsLocked(Username))
                    {
                        Console.WriteLine("You have reached maximum tries. Your account has been locked.");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Password");
                        return false;
                    }
                    // sql = "SELECT Attempts FROM Users WHERE Username = @Username";
                    // cmd = new SqlCommand(sql, connection);
                    // cmd.Parameters.AddWithValue("@Username", Username);
                    // cmd.Parameters.AddWithValue("@Password", Password);
                    // //int Attempts = (int) cmd.ExecuteScalar();
                    //int userCount = (int) cmd.ExecuteScalar();
                    // if(usercCount > 0)
                    // {
                    //     Console.WriteLine("Login Successfully");
                    //     return true;
                    // }
                    // else
                    // {
                    //     Attempts(Username);
                    //     sql = "SELECT Attempts FROM Users WHERE Username = @Username";
                    //     cmd= new SqlCommand(sql, connection);
                    //     cmd.Parameters.AddWithValue("@Username", Username);
                    //     int attempts = (int) cmd.ExecuteScalar();
                    //     if (attempts > 3)
                    //     {
                    //         Console.WriteLine("You have reached maxim try our account has been locked");
                    //     }
                    //     else
                    //     {
                    //         Console.WriteLine("Invalid Password");
                    //     }
                    //     return false;
                    // }
                }
                
                    //return usercCount> 0;
            }
        }
        //public void Attempts(string username)
        //{
        //    using(SqlConnection connection=new SqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        string updatequery = "UPDATE Users SET Attempts = Attempts + 1 WHERE Username = @Username";
        //        SqlCommand updatecmd = new SqlCommand(updatequery, connection);
        //        updatecmd.Parameters.AddWithValue ("username", username);
        //        updatecmd.ExecuteNonQuery();
        //    }
        //}
        //public void deleteUser(string username)
        //{
        //    using(SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        string deletequery = "DELETE FROM USER WHERE Username = @username";
        //        SqlCommand deltcmd= new SqlCommand(deletequery, connection);
        //        deltcmd.Parameters.AddWithValue ("username", username);
        //        int rowsAffected= deltcmd.ExecuteNonQuery();
        //        if(rowsAffected > 0)
        //        {
        //            Console.WriteLine("User deleted Successfully");
        //        }
        //        else
        //        {
        //            Console.WriteLine("user Not found");
        //        }
        //    }
        //}
           
           
    }
}

