using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using DVLD_Business;

namespace DVLD_Presentation
{
    public static class clsGlobal
    {
        public static clsUsersBusiness CurrentUser;
        private static readonly string _keyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD";

        public static bool GetStoredCredentials(ref string Username, ref string Password)
        {
            try
            {
                Username = Registry.GetValue(_keyPath, "Username", null) as string;
                Password = Registry.GetValue(_keyPath, "Password", null) as string;

                if (Username != null && Password != null)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }

        }

        public static bool RememberMe(string Username, string Password)
        {
            try
            {
                if (Username == "" && Password == "")
                {
                    using(RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
                    {
                        using (RegistryKey subKey = key.OpenSubKey(@"SOFTWARE\DVLD", true))
                        {
                            if (subKey != null)
                            {
                                key.DeleteSubKey(@"SOFTWARE\DVLD");
                            }
                        }
                    }

                    return true;
                }

                Registry.SetValue(_keyPath, "Username", Username, RegistryValueKind.String);
                Registry.SetValue(_keyPath, "Password", Password, RegistryValueKind.String);

                return true;
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show($"UnauthorizedAccessException: Run the program with administrative privileges");
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }
        }
    }
}