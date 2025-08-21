using DVLD_Business;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace DVLD_Project
{
    public static class clsGlobal
    {
        public static clsUser CurrentUser;

        public static bool RememberUsernameAndPassword(string Username, string Password)
        {
            try
            {
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();

                string filePath = currentDirectory + "\\LoginData.txt";

                if (Username == "" && File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;

                }
                string dataToSave = Username + "#//#" + Password;

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine(dataToSave);
                    return true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }

        }

        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            try
            {
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();

                string filePath = currentDirectory + "\\LoginData.txt";

                if (File.Exists(filePath))
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            Console.WriteLine(line); 
                            string[] result = line.Split(new string[] { "#//#" }, StringSplitOptions.None);

                            Username = result[0];
                            Password = result[1];
                        }
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }

        }


        //Store LoginInfo On Windos Registry

        
        public static bool RememberUsernameAndPasswordUsingRegisty(string Username, string Password)
        {

            const string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD_LoginData";
            const string UserNameValue = "UserName";
            const string PasswordValue = "Password";

            try
            {
                Registry.SetValue(keyPath, UserNameValue, Username, RegistryValueKind.String);
                Registry.SetValue(keyPath, PasswordValue, Password, RegistryValueKind.String);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }



        }

        public static bool GetStoredCredentialUsingRegisty(ref string Username, ref string Password)
        {
            const string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD_LoginData";
            const string UserNameValue = "UserName";
            const string PasswordValue = "Password";

            try
            {
                Username = Registry.GetValue(keyPath, UserNameValue, null) as string;
                Password = Registry.GetValue(keyPath, PasswordValue, null) as string;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }


        }



    }

}
