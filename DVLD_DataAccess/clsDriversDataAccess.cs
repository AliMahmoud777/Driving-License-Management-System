using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsDriversDataAccess
    {
        public static DataTable ListDrivers()
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"SELECT Drivers.DriverID, People.PersonID, People.NationalNo, CONCAT(People.FirstName, ' ', People.SecondName, ' ', ISNULL(People.ThirdName + ' ', ''), People.LastName) as FullName, Drivers.CreatedDate, COUNT(case when Licenses.IsActive = 1 then 1 end) as ActiveLicenses FROM Drivers INNER JOIN People ON Drivers.PersonID = People.PersonID INNER JOIN Licenses ON Drivers.DriverID = Licenses.DriverID
                 group by Drivers.DriverID, People.PersonID, People.NationalNo, People.FirstName, People.SecondName, People.ThirdName, People.LastName, Drivers.CreatedDate;";

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

        public static bool GetDriverByID(int ID, ref int PersonID, ref int UserID, ref DateTime CreatedDate)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"select * from Drivers where DriverID = @ID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ID", ID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;
                    PersonID = (int)Reader["PersonID"];
                    UserID = (int)Reader["CreatedByUserID"];
                    CreatedDate = (DateTime)(Reader["CreatedDate"]);
                }
                Reader.Close();

            }
            finally
            {
                Connection.Close();
            }

            return IsFound;
        }

        public static bool GetDriverByPersonID(int PersonID, ref int DriverID, ref int UserID, ref DateTime CreatedDate)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"select * from Drivers where PersonID = @PersonID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;
                    DriverID = (int)Reader["DriverID"];
                    UserID = (int)Reader["CreatedByUserID"];
                    CreatedDate = (DateTime)(Reader["CreatedDate"]);
                }
                Reader.Close();

            }
            finally
            {
                Connection.Close();
            }

            return IsFound;
        }

        public static int AddNewDriver(int PersonID, int UserID)
        {
            int DriverID = -1;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"insert into Drivers values(@PersonID, @UserID, @CreatedDate);

                             select scope_identity();";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@UserID", UserID);
            Command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

            try
            {
                Connection.Open();

                object result = Command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                    DriverID = InsertedID;
            }
            finally
            {
                Connection.Close();
            }

            return DriverID;
        }

        public static int GetNumberOfDrivers()
        {
            int NumberOfDrivers = 0;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "Select Count(*) as NumberOfDrivers from Drivers;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            try
            {
                Connection.Open();
                object result = Command.ExecuteScalar();

                NumberOfDrivers = Convert.ToInt32(result);
            }
            finally
            {
                Connection.Close();
            }

            return NumberOfDrivers;
        }
    }
}