using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsTestTypesDataAccess
    {
        public static DataTable ListTestTypes()
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "SELECT * from TestTypes;";

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

        public static bool GetTestTypeByID(int ID, ref string Title, ref string Description, ref float Fees)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"select * from TestTypes where TestTypeID = @ID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ID", ID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;
                    Title = (string)Reader["TestTypeTitle"];
                    Description = (string)Reader["TestTypeDescription"];
                    Fees = Convert.ToSingle(Reader["TestTypeFees"]);
                }
                Reader.Close();
            }
            finally
            {
                Connection.Close();
            }

            return IsFound;
        }

        public static bool UpdateTestType(int ID, string Title, string Description, float Fees)
        {
            int RowsAffected = 0;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"update TestTypes set TestTypeTitle = @Title, TestTypeDescription = @Description, TestTypeFees = @Fees
                             where TestTypeID = @ID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ID", ID);
            Command.Parameters.AddWithValue("@Title", Title);
            Command.Parameters.AddWithValue("@Description", Description);
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