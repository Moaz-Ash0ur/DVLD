using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public static class clsTestTypeData
    {
        public static DataTable GetAllTestTypes()
        {
            DataTable DT = new DataTable();

            using (SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection))
            {
                string query = @"SELECT * FROM TestTypes";

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
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            return DT;
        }

        public static bool FindTestType(int TestTypeID, ref string TestTypeTitle, ref string TestTypeDescription, ref float TestTypeFees)
        {
            bool IsFound = false;

            using (SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection))
            {
                string query = @"SELECT * FROM TestTypes WHERE TestTypeID = @TestTypeID";

                SqlCommand cmd = new SqlCommand(query, Connect);
                cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);

                try
                {
                    Connect.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        IsFound = true;
                        TestTypeTitle = reader["TestTypeTitle"].ToString();
                        TestTypeDescription = reader["TestTypeDescription"].ToString();
                        TestTypeFees = Convert.ToSingle(reader["TestTypeFees"]);
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

        public static bool UpdateTestType(int TestTypeID, string TestTypeTitle, string TestTypeDescription, float TestTypeFees)
        {
            bool IsUpdated = false;

            using (SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection))
            {
                string query = @"UPDATE TestTypes 
                         SET TestTypeTitle = @TestTypeTitle, 
                             TestTypeDescription = @TestTypeDescription, 
                             TestTypeFees = @TestTypeFees 
                         WHERE TestTypeID = @TestTypeID";

                SqlCommand cmd = new SqlCommand(query, Connect);
                cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                cmd.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
                cmd.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
                cmd.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);

                try
                {
                    Connect.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    IsUpdated = rowsAffected > 0; // Check if any row was updated
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            return IsUpdated;
        }



    }
}
