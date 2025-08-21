using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsApplicationTypeData
    {

        public static DataTable GetAllApplicationTypes()
        {
            DataTable DT = new DataTable();

            using (SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection))
            {
                string query = @" select * from ApplicationTypes";

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
    
        public static bool FindApplicationType(int ApplicationTypeID, ref string ApplicationTypeTitle, ref float Fees)
        {
            bool IsFound = false;

            using (SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection))
            {
                string query = @"select * from ApplicationTypes where ApplicationTypeID = @ApplicationTypeID";

                SqlCommand cmd = new SqlCommand(query, Connect);
                cmd.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

                try
                {
                    Connect.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        IsFound = true;
                        ApplicationTypeTitle = reader["ApplicationTypeTitle"].ToString();
                        Fees = Convert.ToInt64(reader["ApplicationFees"]);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                   // Console.WriteLine($"Error: {ex.Message}");
                }
            }

            return IsFound;
        }

        public static bool UpdateApplicationFees(int ApplicationTypeID, float Fees)
        {
            bool IsUpdated = false;

            using (SqlConnection Connect = new SqlConnection(clsDataAccessSetting.Connection))
            {
                string query = @"UPDATE ApplicationTypes 
                         SET ApplicationFees = @ApplicationFees 
                         WHERE ApplicationTypeID = @ApplicationTypeID";

                SqlCommand cmd = new SqlCommand(query, Connect);
                cmd.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
                cmd.Parameters.AddWithValue("@ApplicationFees", Fees);

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
