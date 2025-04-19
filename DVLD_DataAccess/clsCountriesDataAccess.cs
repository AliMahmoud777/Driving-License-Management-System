using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsCountriesDataAccess
    {
        public static DataTable GetAllCountries()
        {
            DataTable DT = new DataTable();

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"select * from Countries order by CountryName asc";

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

        public static bool GetCountryByName(string CountryName, ref int ID)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"select * from Countries where CountryName = @CountryName";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();


                if (Reader.Read())
                {
                    IsFound = true;
                    ID = (int)Reader["CountryID"];
                }
                Reader.Close();
            }
            finally
            {
                Connection.Close();
            }

            return IsFound;
        }

        public static bool GetCountryByID(int ID, ref string CountryName)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"select * from Countries where CountryID = @CountryID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@CountryID", ID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();


                if (Reader.Read())
                {
                    IsFound = true;
                    CountryName = (string)Reader["CountryName"];
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