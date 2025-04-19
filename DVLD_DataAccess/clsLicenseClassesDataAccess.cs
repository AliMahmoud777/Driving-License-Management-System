using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsLicenseClassesDataAccess
    {
        public static DataTable GetAllLicenseClasses()
        {
            DataTable DT = new DataTable();

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"select * from LicenseClasses order by ClassName asc";

            SqlCommand Command = new SqlCommand(Query, Connection);

            try
            {
                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.HasRows)
                {
                    DT.Load(Reader);
                }

                Reader.Close();
            }
            finally
            {
                Connection.Close();
            }

            return DT;
        }

        public static bool GetLicenseClassByName(ref int ID, string ClassName, ref string ClassDescription, ref byte MinimumAllowedAge,
            ref byte DefaultValidityPeriod, ref float ClassFees)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"select * from LicenseClasses where ClassName = @ClassName";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ClassName", ClassName);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();


                if (Reader.Read())
                {
                    IsFound = true;
                    ID = (int)Reader["LicenseClassID"];
                    ClassDescription = (string)Reader["ClassDescription"];
                    MinimumAllowedAge = (byte)Reader["MinimumAllowedAge"];
                    DefaultValidityPeriod = (byte)Reader["DefaultValidityLength"];
                    ClassFees = Convert.ToSingle(Reader["ClassFees"]);
                }
                Reader.Close();
            }
            finally
            {
                Connection.Close();
            }

            return IsFound;
        }

        public static bool GetLicenseClassByID(int ID, ref string ClassName, ref string ClassDescription, ref byte MinimumAllowedAge,
            ref byte DefaultValidityPeriod, ref float ClassFees)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"select * from LicenseClasses where LicenseClassID = @ID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ID", ID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();


                if (Reader.Read())
                {
                    IsFound = true;
                    ClassName = (string)Reader["ClassName"];
                    ClassDescription = (string)Reader["ClassDescription"];
                    MinimumAllowedAge = (byte)Reader["MinimumAllowedAge"];
                    DefaultValidityPeriod = (byte)Reader["DefaultValidityLength"];
                    ClassFees = Convert.ToSingle(Reader["ClassFees"]);
                }
                Reader.Close();
            }
            finally
            {
                Connection.Close();
            }

            return IsFound;
        }
    }
}