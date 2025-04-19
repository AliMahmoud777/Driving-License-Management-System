using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsApplicationTypesDataAccess
    {
        public static DataTable ListApplicationTypes()
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT * from ApplicationTypes;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            DataTable DT = new DataTable();

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

        public static bool GetApplicationTypeByID(int ID, ref string Title, ref float Fees)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"select * from ApplicationTypes where ApplicationTypeID = @ID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ID", ID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;
                    Title = (string)Reader["ApplicationTypeTitle"];
                    Fees = Convert.ToSingle(Reader["ApplicationFees"]);
                }
                Reader.Close();
            }
            finally
            {
                Connection.Close();
            }

            return IsFound;
        }

        public static bool UpdateApplicationType(int ID, string Title, float Fees)
        {
            int RowsAffected = 0;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"update ApplicationTypes set ApplicationTypeTitle = @Title, ApplicationFees = @Fees
                             where ApplicationTypeID = @ID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ID", ID);
            Command.Parameters.AddWithValue("@Title", Title);
            Command.Parameters.AddWithValue("@Fees", Fees);

            try
            {
                Connection.Open();

                RowsAffected = Command.ExecuteNonQuery();
            }
            finally
            {
                Connection.Close();
            }

            return (RowsAffected > 0);
        }
    }
}