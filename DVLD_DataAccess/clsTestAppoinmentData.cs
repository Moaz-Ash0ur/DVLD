using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsTestAppoinmentData
    {

        public static int AddTestAppoinment(int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, float PaidFees, 
            int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
        {
            int TestAppoinmentID = -1;

            SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"INSERT INTO TestAppointments 
                     VALUES (@TestTypeID, @LocalDrivingLicenseApplicationID, @AppointmentDate, @PaidFees, @CreatedByUserID, @IsLocked, @RetakeTestApplicationID);
                     SELECT SCOPE_IDENTITY();";

            SqlCommand cmd = new SqlCommand(query, Connect);

            cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            cmd.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            cmd.Parameters.AddWithValue("@PaidFees", PaidFees);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            cmd.Parameters.AddWithValue("@IsLocked", IsLocked);

            if (RetakeTestApplicationID != 0)
            {
                cmd.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RetakeTestApplicationID", System.DBNull.Value);
            }





            try
            {
                Connect.Open();

                object result = cmd.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                {
                    TestAppoinmentID = InsertedID;
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

            return TestAppoinmentID;
        }


        public static bool FindTestAppointment(int testAppointmentID, ref int testTypeID, ref int localDrivingLicenseApplicationID,
                                         ref DateTime appointmentDate, ref float paidFees, ref int createdByUserID,
                                         ref bool isLocked, ref int retakeTestApplicationID)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSetting.Connection))
            {
                string query = @"SELECT * FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@TestAppointmentID", testAppointmentID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        isFound = true;
                        testTypeID = Convert.ToInt32(reader["TestTypeID"]);
                        localDrivingLicenseApplicationID = Convert.ToInt32(reader["LocalDrivingLicenseApplicationID"]);
                        appointmentDate = Convert.ToDateTime(reader["AppointmentDate"]);
                        paidFees = Convert.ToInt64(reader["PaidFees"]);
                        createdByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                        isLocked = Convert.ToBoolean(reader["IsLocked"]);
                        retakeTestApplicationID = reader["RetakeTestApplicationID"] == DBNull.Value ? -1 : Convert.ToInt32(reader["RetakeTestApplicationID"]);
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



        public static bool UpdateTestAppointment(int testAppointmentID, int testTypeID, int localDrivingLicenseApplicationID,
                                         DateTime appointmentDate, float paidFees, int createdByUserID,
                                         bool isLocked, int retakeTestApplicationID) 
        {
            bool updated = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSetting.Connection))
            {
                string query = @"UPDATE TestAppointments
                         SET TestTypeID = @TestTypeID,
                             LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID,
                             AppointmentDate = @AppointmentDate,
                             PaidFees = @PaidFees,
                             CreatedByUserID = @CreatedByUserID,
                             IsLocked = @IsLocked,
                             RetakeTestApplicationID = @RetakeTestApplicationID
                         WHERE TestAppointmentID = @TestAppointmentID";

                SqlCommand cmd = new SqlCommand(query, connection);

                // Add parameters
                cmd.Parameters.AddWithValue("@TestAppointmentID", testAppointmentID);
                cmd.Parameters.AddWithValue("@TestTypeID", testTypeID);
                cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", localDrivingLicenseApplicationID);
                cmd.Parameters.AddWithValue("@AppointmentDate", appointmentDate);
                cmd.Parameters.AddWithValue("@PaidFees", paidFees);
                cmd.Parameters.AddWithValue("@CreatedByUserID", createdByUserID);
                cmd.Parameters.AddWithValue("@IsLocked", isLocked);

                if (retakeTestApplicationID != 0)
                {
                    cmd.Parameters.AddWithValue("@RetakeTestApplicationID", retakeTestApplicationID);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@RetakeTestApplicationID", System.DBNull.Value);
                }

                try
                {
                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    updated = rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            return updated;
        }

        public static DataTable GetAllTestAppointments()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"select * from TestAppointments_View
                                  order by AppointmentDate Desc";


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


        public static bool GetLastTestAppointment(
            int LocalDrivingLicenseApplicationID, int TestTypeID,
           ref int TestAppointmentID, ref DateTime AppointmentDate,
           ref float PaidFees, ref int CreatedByUserID, ref bool IsLocked, ref int RetakeTestApplicationID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"SELECT       top 1 *
                FROM            TestAppointments
                WHERE        (TestTypeID = @TestTypeID) 
                AND (LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                order by TestAppointmentID Desc";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;

                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsLocked = (bool)reader["IsLocked"];

                    if (reader["RetakeTestApplicationID"] == DBNull.Value)
                        RetakeTestApplicationID = -1;
                    else
                        RetakeTestApplicationID = (int)reader["RetakeTestApplicationID"];


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


        public static DataTable GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"SELECT TestAppointmentID, AppointmentDate,PaidFees, IsLocked
                        FROM TestAppointments
                        WHERE  
                        (TestTypeID = @TestTypeID) 
                        AND (LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)
                        order by TestAppointmentID desc;";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);


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


        public static int GetTestID(int TestAppointmentID)
        {
            int TestID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSetting.Connection);

            string query = @"select TestID from Tests where TestAppointmentID=@TestAppointmentID;";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestID = insertedID;
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }


            return TestID;

        }


      


    }
}
