using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsUserData
    {

        public static int AddUser(int PersonID, string UserName, string Password, bool IsActive)
        {
            int UserID = -1;

            using (SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection))
            {
                string query = @"INSERT INTO Users (PersonID, UserName, Password, IsActive) 
                         VALUES (@PersonID, @UserName, @Password, @IsActive);
                         SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(query, Connect);
                cmd.Parameters.AddWithValue("@PersonID", PersonID);
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@Password", Password);
                cmd.Parameters.AddWithValue("@IsActive", IsActive);

                try
                {
                    Connect.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                    {
                        UserID = InsertedID;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            return UserID;
        }

        public static bool UpdateUser(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            bool Updated = false;

            using (SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection))
            {
                string query = @"UPDATE Users
                         SET PersonID = @PersonID,
                             UserName = @UserName,
                             Password = @Password,
                             IsActive = @IsActive
                         WHERE UserID = @UserID";

                SqlCommand cmd = new SqlCommand(query, Connect);
                cmd.Parameters.AddWithValue("@UserID", UserID);
                cmd.Parameters.AddWithValue("@PersonID", PersonID);
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@Password", Password);
                cmd.Parameters.AddWithValue("@IsActive", IsActive);

                try
                {
                    Connect.Open();
                    int RowsAffected = cmd.ExecuteNonQuery();
                    Updated = RowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            return Updated;
        }

        public static bool DeleteUser(int UserID)
        {
            bool IsDeleted = false;

            using (SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection))
            {
                string query = @"DELETE FROM Users WHERE UserID = @UserID";

                SqlCommand cmd = new SqlCommand(query, Connect);
                cmd.Parameters.AddWithValue("@UserID", UserID);

                try
                {
                    Connect.Open();
                    int RowsAffected = cmd.ExecuteNonQuery();
                    IsDeleted = RowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            return IsDeleted;
        }

        public static bool FindUserByUsernameAndPassword(string UserName, string Password,
                 ref int UserID, ref int PersonID, ref bool IsActive)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.Connection);

            string query = "SELECT * FROM Users WHERE Username = @Username and Password=@Password;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Username", UserName);
            command.Parameters.AddWithValue("@Password", Password);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;
                    UserID = (int)reader["UserID"];
                    PersonID = (int)reader["PersonID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];


                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool FindUserById(int UserID, ref int PersonID, ref string UserName, ref string Password, ref bool IsActive)
        {
            bool IsFound = false;

            using (SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection))
            {
                string query = @"SELECT * FROM Users WHERE UserID = @UserID";

                SqlCommand cmd = new SqlCommand(query, Connect);
                cmd.Parameters.AddWithValue("@UserID", UserID);

                try
                {
                    Connect.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        IsFound = true;
                        PersonID = Convert.ToInt32(reader["PersonID"]);
                        UserName = reader["UserName"].ToString();
                        Password = reader["Password"].ToString();
                        IsActive = Convert.ToBoolean(reader["IsActive"]);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            return IsFound;
        }
       //Extar wiht Instructor
        public static bool GetUserInfoByPersonID(int PersonID, ref int UserID, ref string UserName,
        ref string Password, ref bool IsActive)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.Connection);

            string query = "SELECT * FROM Users WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    UserID = (int)reader["UserID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];


                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
       //
        public static bool IsUserExists(string UserName)
        {
            bool Exists = false;

            using (SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection))
            {
                string query = @"SELECT found=1 FROM Users WHERE UserName = @UserName";

                SqlCommand cmd = new SqlCommand(query, Connect);
                cmd.Parameters.AddWithValue("@UserName", UserName);

                try
                {
                    Connect.Open();
                    Exists = Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            return Exists;
        }

        public static bool IsUserExist(int UserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.Connection);

            string query = "SELECT Found=1 FROM Users WHERE UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool IsUserExistForPersonID(int PersonID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.Connection);

            string query = "SELECT Found=1 FROM Users WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static DataTable GetAllUsers()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"SELECT  Users.UserID, Users.PersonID,
                            FullName = People.FirstName + ' ' + People.SecondName + ' ' + ISNULL( People.ThirdName,'') +' ' + People.LastName,
                             Users.UserName, Users.IsActive
                             FROM  Users INNER JOIN
                                    People ON Users.PersonID = People.PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }






    }
}
