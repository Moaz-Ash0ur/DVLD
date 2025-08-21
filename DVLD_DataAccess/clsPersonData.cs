using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsPersonData
    {
        public static int AddPerson(string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName,
                                    DateTime DateOfBirth, int Gender, string Address, string Phone, string Email,
                                    int NationalityCountryID, string ImagePath)
        {
            int PersonID = -1;

            SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"INSERT INTO People (NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, 
                      Address, Phone, Email, NationalityCountryID, ImagePath) 
                     VALUES (@NationalNo, @FirstName, @SecondName, @ThirdName, @LastName, @DateOfBirth, @Gendor, 
                             @Address, @Phone, @Email, @NationalityCountryID, @ImagePath);
                     SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(query, Connect);

            cmd.Parameters.AddWithValue("@NationalNo", NationalNo);
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@SecondName", SecondName);
            cmd.Parameters.AddWithValue("@ThirdName", ThirdName);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            cmd.Parameters.AddWithValue("@Gendor", Gender);
            cmd.Parameters.AddWithValue("@Address", Address);
            cmd.Parameters.AddWithValue("@Phone", Phone);
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
            cmd.Parameters.AddWithValue("@ImagePath", ImagePath);

            try
            {
                Connect.Open();

                object result = cmd.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    PersonID = InsertedID;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Log or handle the exception
            }
            finally
            {
                Connect.Close();
            }

            return PersonID;
        }


        public static bool FindPerson(int PersonID, ref string NationalNo, ref string FirstName, ref string SecondName,
                                       ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref int Gender,
                                       ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID,
                                       ref string ImagePath)
        {
            bool IsFound = false;

            SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"SELECT * FROM People WHERE PersonID = @PersonID";

            SqlCommand cmd = new SqlCommand(query, Connect);

            cmd.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                Connect.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    NationalNo = reader["NationalNo"].ToString();
                    FirstName = reader["FirstName"].ToString();
                    SecondName = reader["SecondName"].ToString();
                    ThirdName = reader["ThirdName"].ToString();
                    LastName = reader["LastName"].ToString();
                    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                    Gender = Convert.ToInt32(reader["Gendor"]);
                    Address = reader["Address"].ToString();
                    Phone = reader["Phone"].ToString();
                    Email = reader["Email"].ToString();
                    NationalityCountryID = Convert.ToInt32(reader["NationalityCountryID"]);
                    ImagePath = reader["ImagePath"].ToString();
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Log or handle the exception
            }
            finally
            {
                Connect.Close();
            }

            return IsFound;
        }


        public static bool UpdatePerson(int PersonID, string NationalNo, string FirstName, string SecondName,
                                string ThirdName, string LastName, DateTime DateOfBirth, int Gender,
                                string Address, string Phone, string Email, int NationalityCountryID,
                                string ImagePath)
        {
            bool Updated = false;

            using (SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection))
            {
                string query = @"UPDATE People
                         SET NationalNo = @NationalNo,
                             FirstName = @FirstName,
                             SecondName = @SecondName,
                             ThirdName = @ThirdName,
                             LastName = @LastName,
                             DateOfBirth = @DateOfBirth,
                             Gendor = @Gendor,
                             Address = @Address,
                             Phone = @Phone,
                             Email = @Email,
                             NationalityCountryID = @NationalityCountryID,
                             ImagePath = @ImagePath
                         WHERE PersonID = @PersonID";

                SqlCommand cmd = new SqlCommand(query, Connect);

                cmd.Parameters.AddWithValue("@PersonID", PersonID);
                cmd.Parameters.AddWithValue("@NationalNo", NationalNo);
                cmd.Parameters.AddWithValue("@FirstName", FirstName);
                cmd.Parameters.AddWithValue("@SecondName", SecondName);
                cmd.Parameters.AddWithValue("@ThirdName", ThirdName);
                cmd.Parameters.AddWithValue("@LastName", LastName);
                cmd.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                cmd.Parameters.AddWithValue("@Gendor", Gender);
                cmd.Parameters.AddWithValue("@Address", Address);
                cmd.Parameters.AddWithValue("@Phone", Phone);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
                cmd.Parameters.AddWithValue("@ImagePath", ImagePath);

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



        public static bool FindPersonByNationalNo(string NationalNo, ref int PersonID, ref string FirstName, ref string SecondName,
                                          ref string ThirdName, ref string LastName, ref DateTime DateOfBirth, ref int Gender,
                                          ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID,
                                          ref string ImagePath)
        {
            bool IsFound = false;

            using (SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection))
            {
                string query = @"SELECT * FROM People WHERE NationalNo = @NationalNo";

                SqlCommand cmd = new SqlCommand(query, Connect);
                cmd.Parameters.AddWithValue("@NationalNo", NationalNo);

                try
                {
                    Connect.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        IsFound = true;
                        PersonID = Convert.ToInt32(reader["PersonID"]);
                        FirstName = reader["FirstName"].ToString();
                        SecondName = reader["SecondName"].ToString();
                        ThirdName = reader["ThirdName"].ToString();
                        LastName = reader["LastName"].ToString();
                        DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                        Gender = Convert.ToInt32(reader["Gendor"]);
                        Address = reader["Address"].ToString();
                        Phone = reader["Phone"].ToString();
                        Email = reader["Email"].ToString();
                        NationalityCountryID = Convert.ToInt32(reader["NationalityCountryID"]);
                        ImagePath = reader["ImagePath"].ToString();
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


        public static bool DeletePerson(int PersonID)
        {
            bool IsDeleted = false;

            SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"DELETE FROM People WHERE PersonID = @PersonID;";

            SqlCommand cmd = new SqlCommand(query, Connect);

            cmd.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                Connect.Open();

                int RowsAffected = cmd.ExecuteNonQuery();

                IsDeleted = RowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
            }
            finally
            {
                Connect.Close();
            }

            return IsDeleted;
        }

        public static DataTable GetAllCountryNames()
        {
            DataTable countryTable = new DataTable();

            SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection);

            string query = "SELECT * FROM Countries;";

            SqlCommand cmd = new SqlCommand(query, Connect);

            try
            {
                Connect.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    countryTable.Load(reader);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Log or handle the exception
            }
            finally
            {
                Connect.Close();
            }

            return countryTable;

        }

        public static string GetCountryNameByID(int countryNumber)
        {
            string countryName = null;

            SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection);

            string query = "SELECT CountryName FROM Countries WHERE CountryID = @CountryID;";

            SqlCommand cmd = new SqlCommand(query, Connect);

            cmd.Parameters.AddWithValue("@CountryID", countryNumber);

            try
            {
                Connect.Open();

                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    countryName = result.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Log or handle the exception
            }
            finally
            {
                Connect.Close();
            }

            return countryName;
        }


        //check about person realted and is exsit
        public static bool IsPersonExist(string NationalNo)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.Connection);

            string query = "SELECT Found=1 FROM People WHERE NationalNo = @NationalNo";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool IsPersonExist(int personId)
        {
            bool exists = false;

            SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection);
            
                string query = @"select found=1 FROM People WHERE PersonID = @PersonID";

                SqlCommand cmd = new SqlCommand(query, Connect);
                cmd.Parameters.AddWithValue("@PersonID", personId);

                try
                {
                    Connect.Open();

                int? count = cmd.ExecuteScalar() as int?;

                if (count.HasValue)
                {
                    exists = (count.Value > 0);
                }
                else
                {
                    // Handle the case when the result is null
                    exists = false;
                }

            }
                catch (Exception ex)
                {
                   // Console.WriteLine(ex.Message); // Log or handle the exception
                }
                finally
                {
                    Connect.Close();
                }
            

            return exists;
        }

        public static bool IsPersonIDRelatedToUser(int personId)
        {
            bool isRelated = false;

            using (SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection))
            {
                string query = @"SELECT found=1 FROM Users WHERE PersonID = @PersonID";

                SqlCommand cmd = new SqlCommand(query, Connect);
                cmd.Parameters.AddWithValue("@PersonID", personId);

                try
                {
                    Connect.Open();

                    int? count = cmd.ExecuteScalar() as int?;

                    if (count.HasValue)
                    {
                        isRelated = (count.Value > 0);
                    }
                    else
                    {
                        // Handle the case when the result is null
                        isRelated = false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message); // Log or handle the exception
                }
                finally
                {
                    Connect.Close();

                }
            }

            return isRelated;
        }

        public static DataTable GetAllPeople()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.Connection);

            string query =
              @"SELECT People.PersonID, People.NationalNo,
              People.FirstName, People.SecondName, People.ThirdName, People.LastName,
			  People.DateOfBirth,  
				  CASE
                  WHEN People.Gendor = 0 THEN 'Male'

                  ELSE 'Female'

                  END as GendorCaption ,
			  People.Address, People.Phone, People.Email, 
              People.NationalityCountryID, Countries.CountryName, People.ImagePath
              FROM            People INNER JOIN
                         Countries ON People.NationalityCountryID = Countries.CountryID
                ORDER BY People.FirstName";


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
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }



    }
}
