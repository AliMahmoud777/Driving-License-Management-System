using System;
using System.Configuration;

namespace DVLD_DataAccess
{
    static class clsDataAccessSettings
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["DVLD_Connection"].ConnectionString;
    }
}