using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsLicenceData
    {

        public static int AddNewLicense(int applicationID, int driverID, int licenseClass, DateTime issueDate, DateTime expirationDate,
                             string notes, float paidFees, bool isActive, int issueReason, int createdByUserID)
        {
            int licenseID = -1;

            string query = @"INSERT INTO Licenses (ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID) 
                     VALUES (@ApplicationID, @DriverID, @LicenseClass, @IssueDate, @ExpirationDate, @Notes, @PaidFees, @IsActive, @IssueReason, @CreatedByUserID);
                     SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSetting.Connection))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ApplicationID", applicationID);
                command.Parameters.AddWithValue("@DriverID", driverID);
                command.Parameters.AddWithValue("@LicenseClass", licenseClass);
                command.Parameters.AddWithValue("@IssueDate", issueDate);
                command.Parameters.AddWithValue("@ExpirationDate", expirationDate);
                command.Parameters.AddWithValue("@Notes", notes);
                command.Parameters.AddWithValue("@PaidFees", paidFees);
                command.Parameters.AddWithValue("@IsActive", isActive);
                command.Parameters.AddWithValue("@IssueReason", issueReason);
                command.Parameters.AddWithValue("@CreatedByUserID", createdByUserID);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        licenseID = insertedID;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            return licenseID;
        }


        
        public static bool UpdateLicense(int licenseID, int applicationID, int driverID, int licenseClass, DateTime issueDate, DateTime expirationDate,
                                 string notes, float paidFees, bool isActive, int issueReason, int createdByUserID)
        {
            bool updated = false;

            string query = @"UPDATE Licenses
                     SET ApplicationID = @ApplicationID, DriverID = @DriverID, LicenseClass = @LicenseClass, 
                         IssueDate = @IssueDate, ExpirationDate = @ExpirationDate, Notes = @Notes, 
                         PaidFees = @PaidFees, IsActive = @IsActive, IssueReason = @IssueReason, 
                         CreatedByUserID = @CreatedByUserID
                     WHERE LicenseID = @LicenseID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSetting.Connection))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LicenseID", licenseID);
                command.Parameters.AddWithValue("@ApplicationID", applicationID);
                command.Parameters.AddWithValue("@DriverID", driverID);
                command.Parameters.AddWithValue("@LicenseClass", licenseClass);
                command.Parameters.AddWithValue("@IssueDate", issueDate);
                command.Parameters.AddWithValue("@ExpirationDate", expirationDate);
                command.Parameters.AddWithValue("@Notes", notes);
                command.Parameters.AddWithValue("@PaidFees", paidFees);
                command.Parameters.AddWithValue("@IsActive", isActive);
                command.Parameters.AddWithValue("@IssueReason", issueReason);
                command.Parameters.AddWithValue("@CreatedByUserID", createdByUserID);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    updated = rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            return updated;
        }
        
        
        public static bool GetLicenseInfoByID(int licenseID, ref int applicationID, ref int driverID, ref int licenseClass,
                                       ref DateTime issueDate, ref DateTime expirationDate, ref string notes, ref float paidFees,
                                       ref bool isActive, ref int issueReason, ref int createdByUserID)
        {
            bool isFound = false;

            string query = @"SELECT * FROM Licenses WHERE LicenseID = @LicenseID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSetting.Connection))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LicenseID", licenseID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        isFound = true;
                        applicationID = Convert.ToInt32(reader["ApplicationID"]);
                        driverID = Convert.ToInt32(reader["DriverID"]);
                        licenseClass = (int)reader["LicenseClass"];
                        issueDate = Convert.ToDateTime(reader["IssueDate"]);
                        expirationDate = Convert.ToDateTime(reader["ExpirationDate"]);
                        notes = reader["Notes"].ToString();
                        paidFees = Convert.ToSingle(reader["PaidFees"]);
                        isActive = Convert.ToBoolean(reader["IsActive"]);
                        issueReason = Convert.ToInt32(reader["IssueReason"]);
                        createdByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            return isFound;
        }
      
            
        public static DataTable GetAllLicenses()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT * FROM Licenses";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSetting.Connection))
            {
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
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            return dt;
        }

        public static DataTable GetDriverLicenses(int DriverID)
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"SELECT     
                           Licenses.LicenseID,
                           ApplicationID,
		                   LicenseClasses.ClassName, Licenses.IssueDate, 
		                   Licenses.ExpirationDate, Licenses.IsActive
                           FROM Licenses INNER JOIN
                                LicenseClasses ON Licenses.LicenseClass = LicenseClasses.LicenseClassID
                            where DriverID=@DriverID
                            Order By IsActive Desc, ExpirationDate Desc";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

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

        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {
            int LicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"SELECT        Licenses.LicenseID
                            FROM Licenses INNER JOIN
                                                     Drivers ON Licenses.DriverID = Drivers.DriverID
                            WHERE  
                             
                             Licenses.LicenseClass = @LicenseClass 
                              AND Drivers.PersonID = @PersonID
                              And IsActive=1;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClassID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    LicenseID = insertedID;
                }
            }

            catch (Exception ex)
            {

            }

            finally
            {
                connection.Close();
            }


            return LicenseID;
        }

        public static bool DeactivateLicense(int LicenseID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"UPDATE Licenses
                           SET 
                              IsActive = 0
                             
                         WHERE LicenseID=@LicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);


            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static bool ActivateLicense(int LicenseID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"UPDATE Licenses
                           SET 
                              IsActive = 1
                             
                         WHERE LicenseID=@LicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);


            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

      
        public static int GetPersonIDByDriverID(int driverId)
        {
            int personId = -1;
            string query = @"SELECT PersonID FROM Drivers WHERE DriverID = @DriverID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSetting.Connection))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DriverID", driverId);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    personId = (result != null && int.TryParse(result.ToString(), out int id)) ? id : -1;
                }
                catch (Exception ex)
                {
                  //  Console.WriteLine($"Error: {ex.Message}");
                }
            }

            return personId;
        }




    }
}
