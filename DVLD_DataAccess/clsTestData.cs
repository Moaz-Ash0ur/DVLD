using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsTestData
    {

        public static int AddTest(int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int TestID = -1;

            SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"Insert Into Tests Values (@TestAppointmentID,@TestResult,@Notes,@CreatedByUserID);
                                UPDATE TestAppointments 
                                SET IsLocked=1 where TestAppointmentID = @TestAppointmentID;

                                SELECT SCOPE_IDENTITY()";

            SqlCommand cmd = new SqlCommand(query, Connect);

            cmd.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            cmd.Parameters.AddWithValue("@TestResult", TestResult);

            if (Notes != "" && Notes != null)
                cmd.Parameters.AddWithValue("@Notes", Notes);
            else
                cmd.Parameters.AddWithValue("@Notes", System.DBNull.Value);



                cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                Connect.Open();

                object result = cmd.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    TestID = InsertedID;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                Connect.Close();
            }

            return TestID;
        }

        public static bool FindTest(int TestID, ref int TestAppointmentID, ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {
            bool IsFound = false;

            SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"SELECT * FROM Tests WHERE TestID = @TestID";

            SqlCommand cmd = new SqlCommand(query, Connect);

            cmd.Parameters.AddWithValue("@TestID", TestID);

            try
            {
                Connect.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    IsFound = true;
                    TestAppointmentID = Convert.ToInt32(reader["TestAppointmentID"]);
                    TestResult = Convert.ToBoolean(reader["TestResult"]);
                    Notes = reader["Notes"].ToString();
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
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

        public static bool UpdateTest(int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            bool Updated = false;

            using (SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection))
            {
                string query = @"UPDATE Tests
                         SET TestAppointmentID = @TestAppointmentID,
                             TestResult = @TestResult,
                             Notes = @Notes,
                             CreatedByUserID = @CreatedByUserID
                         WHERE TestID = @TestID";

                SqlCommand cmd = new SqlCommand(query, Connect);

                cmd.Parameters.AddWithValue("@TestID", TestID);
                cmd.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                cmd.Parameters.AddWithValue("@TestResult", TestResult);
                cmd.Parameters.AddWithValue("@Notes", Notes);
                cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

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

        public static DataTable GetAllTests()
        {
            DataTable DT = new DataTable();

            SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"SELECT * FROM Tests order by TestID";

            SqlCommand cmd = new SqlCommand(query, Connect);

            try
            {
                Connect.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    DT.Load(reader);
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

            return DT;
        }

        public static byte GetPassedTestCount(int LocalDrivingAppID)
        {
            byte PassedTestCount = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"SELECT PassedTestCount = count(TestTypeID)
                         FROM Tests INNER JOIN TestAppointments 
                         ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
						 where LocalDrivingLicenseApplicationID =@LocalDrivingLicenseApplicationID and TestResult=1";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingAppID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && byte.TryParse(result.ToString(), out byte ptCount))
                {
                    PassedTestCount = ptCount;
                }
            }

            catch (Exception ex)
            {
            }

            finally
            {
                connection.Close();
            }

            return PassedTestCount;
        }

        public static bool GetLastTestByPersonAndTestTypeAndLicenseClass
       (int PersonID, int LicenseClassID, int TestTypeID, ref int TestID,
         ref int TestAppointmentID, ref bool TestResult,
         ref string Notes, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"SELECT  top 1 Tests.TestID, 
                Tests.TestAppointmentID, Tests.TestResult, 
			    Tests.Notes, Tests.CreatedByUserID, Applications.ApplicantPersonID
                FROM            LocalDrivingLicenseApplications INNER JOIN
                                         Tests INNER JOIN
                                         TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                         Applications ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
                WHERE        (Applications.ApplicantPersonID = @PersonID) 
                        AND (LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID)
                        AND ( TestAppointments.TestTypeID=@TestTypeID)
                ORDER BY Tests.TestAppointmentID DESC";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;
                    TestID = (int)reader["TestID"];
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestResult = (bool)reader["TestResult"];
                    if (reader["Notes"] == DBNull.Value)

                        Notes = "";
                    else
                        Notes = (string)reader["Notes"];

                    CreatedByUserID = (int)reader["CreatedByUserID"];

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
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }







    }
}
