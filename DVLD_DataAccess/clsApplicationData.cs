using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_DataAccess
{
    public class clsApplicationData
    {
            
        public static int GetApplicationIDByLocalApplicationID(int applicationId)
        {
            int resultApplicationID = -1; // Default value if no match is found

            string query = @"SELECT ApplicationID FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSetting.Connection))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", applicationId);

                try
                {

                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int applicationID))
                    {
                        resultApplicationID = applicationID;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            return resultApplicationID;
        }


        public static DataTable GetLicenseDetailsByApplicationID(int applicationID)
        {
            DataTable dt = new DataTable();

            string query = @"
        SELECT LicenseID, 
               ApplicationID, 
               (SELECT ClassName FROM LicenseClasses WHERE LicenseClassID = LicenseClass) AS ClassName, 
               IssueDate, 
               ExpirationDate, 
               IsActive
        FROM Licenses
        WHERE ApplicationID = @ApplicationID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSetting.Connection))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ApplicationID", applicationID);

                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt); // Fill the DataTable with the query results
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            return dt;
        }


        public static DataTable GetInternationalLicenseDetailsByApplicationID(int applicationID)
        {
            DataTable dt = new DataTable();

            string query = @"
        SELECT InternationalLicenseID, 
               ApplicationID, 
               IssuedUsingLocalLicenseID AS LicenseID, 
               IssueDate, 
               ExpirationDate, 
               IsActive
        FROM InternationalLicenses
        WHERE IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID";           

            using (SqlConnection connection = new SqlConnection(clsDataAccessSetting.Connection))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", applicationID);

                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt); // Fill the DataTable with the query results
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            return dt;
        }


        //=====================================================
        public static int AddApplication(int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID,
                                 int ApplicationStatus, DateTime LastStatusDate, float PaidFees,
                                 int CreatedByUserID)
        {
            int ApplicationID = -1;

            SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"INSERT INTO Applications (ApplicantPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus, 
                     LastStatusDate, PaidFees, CreatedByUserID) 
                     VALUES (@ApplicantPersonID, @ApplicationDate, @ApplicationTypeID, @ApplicationStatus, 
                             @LastStatusDate, @PaidFees, @CreatedByUserID);
                     SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(query, Connect);

            cmd.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            cmd.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            cmd.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            cmd.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            cmd.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            cmd.Parameters.AddWithValue("@PaidFees", PaidFees);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                Connect.Open();

                object result = cmd.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    ApplicationID = InsertedID;
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

            return ApplicationID;
        }


        public static DataTable GetAllApplications()
        {
            DataTable applicationsTable = new DataTable();

            using (SqlConnection conn = new SqlConnection(clsDataAccessSetting.Connection))
            {
                string query = "SELECT ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID, " +
                               "ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID FROM Applications";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);

                dataAdapter.Fill(applicationsTable);
            }

            return applicationsTable;
        }


        public static bool FindApplicationInfoByID(int ApplicationID,
           ref int ApplicantPersonID, ref DateTime ApplicationDate, ref int ApplicationTypeID,
           ref byte ApplicationStatus, ref DateTime LastStatusDate,
           ref float PaidFees, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.Connection);

            string query = "SELECT * FROM Applications WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    ApplicantPersonID = (int)reader["ApplicantPersonID"];
                    ApplicationDate = (DateTime)reader["ApplicationDate"];
                    ApplicationTypeID = (int)reader["ApplicationTypeID"];
                    ApplicationStatus = (byte)reader["ApplicationStatus"];
                    LastStatusDate = (DateTime)reader["LastStatusDate"];
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];


                }
                else
                {
                    isFound = false;
                }

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

        public static bool UpdateApplication(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID,
           byte ApplicationStatus, DateTime LastStatusDate,
           float PaidFees, int CreatedByUserID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"Update  Applications  
                            set ApplicantPersonID = @ApplicantPersonID,
                                ApplicationDate = @ApplicationDate,
                                ApplicationTypeID = @ApplicationTypeID,
                                ApplicationStatus = @ApplicationStatus, 
                                LastStatusDate = @LastStatusDate,
                                PaidFees = @PaidFees,
                                CreatedByUserID=@CreatedByUserID
                            where ApplicationID=@ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("ApplicantPersonID", @ApplicantPersonID);
            command.Parameters.AddWithValue("ApplicationDate", @ApplicationDate);
            command.Parameters.AddWithValue("ApplicationTypeID", @ApplicationTypeID);
            command.Parameters.AddWithValue("ApplicationStatus", @ApplicationStatus);
            command.Parameters.AddWithValue("LastStatusDate", @LastStatusDate);
            command.Parameters.AddWithValue("PaidFees", @PaidFees);
            command.Parameters.AddWithValue("CreatedByUserID", @CreatedByUserID);


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


        public static bool DeleteApplication(int ApplicationID)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"Delete Applications 
                                where ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);

        }


        //Now after created CURD Opertion ,need the basic fun for project rules

        public static bool IsApplicationExist(int ApplicationID)
        {
            bool IsFound = true;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.Connection);

            string Query = "select found=1 from Applications where ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);


            try
            {
                connection.Open();

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                IsFound = reader.HasRows;

                reader.Close();

            }
            catch (Exception ex)
            {
                IsFound = false;
            }
            finally
            {
              connection.Close();
            }

            return IsFound; 



        }

        public static int GetActiveApplicationID(int ApplicationTypeID, int PeronsID)
        {

            int AcitveApplicationIDAcitve = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.Connection);

            string Query = "select ActiveApplicationID = ApplicationID from Applications where ApplicantPersonID = @ApplicantPersonID and ApplicationTypeID = @ApplicationTypeID and ApplicationStatus = 1";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", PeronsID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);


            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int applicationID))
                {
                    AcitveApplicationIDAcitve = applicationID;
                }

            }
            catch (Exception)
            {              
            }
            finally
            {
                connection.Close();
            }

            return AcitveApplicationIDAcitve;   
        }

        public static bool IsPersonHaveActiveApplication(int ApplicationTypeID, int PeronsID)
        {
         return GetActiveApplicationID(ApplicationTypeID, PeronsID)!= -1 ;
        }

        public static bool UpdateStatus(int ApplicationID, int NewStatus)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"Update Applications  
                            set ApplicationStatus = @ApplicationStatus, 
                                LastStatusDate = @LastStatusDate
                            where ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@ApplicationStatus", NewStatus);
            command.Parameters.AddWithValue("LastStatusDate", DateTime.Now);


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

            return rowsAffected > 0;
        }

        public static int GetActiveApplicationIDForLicenseClass(int PersonID, int ApplicationTypeID, int LicenseClassID)
        {
            int ActiveApplicationID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"SELECT ActiveApplicationID=Applications.ApplicationID  
                            From
                            Applications INNER JOIN
                            LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                            WHERE ApplicantPersonID = @ApplicantPersonID 
                            and ApplicationTypeID=@ApplicationTypeID 
							and LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID
                            and ApplicationStatus=1";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int AppID))
                {
                    ActiveApplicationID = AppID;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return ActiveApplicationID;
        }

        //---------------------------------

        public static Dictionary<string, int> GetApplicationStatistics()
        {

            var statistics = new Dictionary<string, int>();
          
            using (SqlConnection conn = new SqlConnection(clsDataAccessSetting.Connection))
            
            using (SqlCommand cmd = new SqlCommand("SP_GetApplicationStatistics", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                string[] parameterNames =
                {
                "@TotalDrivers",
                "@TotalUsers",
                "@TotalInternationalLicenses",
                "@TotalLocalLicenses",
                "@TotalDetainedAndReleaseLicenses"
            };

                foreach (string paramName in parameterNames)
                {
                    cmd.Parameters.Add(new SqlParameter(paramName, SqlDbType.Int) { Direction = ParameterDirection.Output });
                }

                conn.Open();
                cmd.ExecuteNonQuery();

                foreach (string paramName in parameterNames)
                {
                    statistics[paramName] = (int)cmd.Parameters[paramName].Value;
                }
            }

            return statistics;
        }


        public static Dictionary<string, int> GetApplicationStatusCounts()
        {
            var statusCounts = new Dictionary<string, int>();

            using (SqlConnection conn = new SqlConnection(clsDataAccessSetting.Connection))
            using (SqlCommand cmd = new SqlCommand("SP_GetApplicationStatusCounts", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string status = reader.GetString(0);  
                        int count = reader.GetInt32(1); 
                        statusCounts[status] = count;
                    }
                }
            }

            return statusCounts;
        }

        public static Dictionary<string, int> GetTestResultsCounts()
        {
            var resultsCounts = new Dictionary<string, int>();

            using (SqlConnection conn = new SqlConnection(clsDataAccessSetting.Connection))
            using (SqlCommand cmd = new SqlCommand("SP_GetTestResultsCounts", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string resultType = reader.GetString(0); 
                        int count = reader.GetInt32(1);          
                        resultsCounts[resultType] = count;
                    }
                }
            }

            return resultsCounts;
        }


    }
}
