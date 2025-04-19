using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsApplicationsDataAccess
    {
        public static DataTable ListApplications()
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = "select * from Applications";

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

        public static bool GetApplicationByID(int ID, ref int PersonID, ref DateTime Date, ref int TypeID, ref byte Status,
            ref DateTime LastStatusDate, ref float PaidFees, ref int UserID)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"select * from Applications where ApplicationID = @ID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ID", ID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;
                    PersonID = (int)Reader["ApplicantPersonID"];
                    Date = (DateTime)Reader["ApplicationDate"];
                    TypeID = (int)Reader["ApplicationTypeID"];
                    Status = (byte)Reader["ApplicationStatus"];
                    LastStatusDate = (DateTime)Reader["LastStatusDate"];
                    PaidFees = Convert.ToSingle(Reader["PaidFees"]);
                    UserID = (int)(Reader["CreatedByUserID"]);
                }
                Reader.Close();
            }
            finally
            {
                Connection.Close();
            }

            return IsFound;
        }

        public static int AddNewApplication(int PersonID, DateTime Date, int TypeID, byte Status,
            DateTime LastStatusDate, float PaidFees, int UserID)
        {
            int ApplicationID = -1;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"insert into Applications values(@PersonID, @Date, @TypeID, @Status,
                             @LastStatusDate, @PaidFees, @UserID);
                             select scope_identity();";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@Date", Date);
            Command.Parameters.AddWithValue("@TypeID", TypeID);
            Command.Parameters.AddWithValue("@Status", Status);
            Command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            Command.Parameters.AddWithValue("@PaidFees", PaidFees);
            Command.Parameters.AddWithValue("@UserID", UserID);

            try
            {
                Connection.Open();

                object result = Command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                    ApplicationID = InsertedID;
            }
            finally
            {
                Connection.Close();
            }

            return ApplicationID;
        }

        public static bool UpdateApplication(int ID, int PersonID, DateTime Date, int TypeID, byte Status,
            DateTime LastStatusDate, float PaidFees, int UserID)
        {
            int RowsAffected = 0;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"update Applications set ApplicantPersonID = @PersonID, ApplicationDate = @Date, ApplicationTypeID = @TypeID,
                             ApplicationStatus = @Status, LastStatusDate = @LastStatusDate, PaidFees = @PaidFees, CreatedByUserID = @UserID
                             where ApplicationID = @ID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ID", ID);
            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@Date", Date);
            Command.Parameters.AddWithValue("@TypeID", TypeID);
            Command.Parameters.AddWithValue("@Status", Status);
            Command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            Command.Parameters.AddWithValue("@PaidFees", PaidFees);
            Command.Parameters.AddWithValue("@UserID", UserID);

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

        public static bool DeleteApplication(int ID)
        {
            int RowsAffected = 0;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"delete from Applications where ApplicationID = @ID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ID", ID);

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

        public static bool IsApplicationExist(int ID)
        {
            bool IsFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found = 1 FROM Applications WHERE ApplicationID = @ID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ID", ID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                    IsFound = true;
            }
            finally
            {
                connection.Close();
            }

            return IsFound;
        }

        public static int GetActiveApplicationID(int PersonID, int ApplicationTypeID, int LicenseClassID)
        {
            int ActiveApplicationID = -1;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"SELECT Applications.ApplicationID  
                            From
                            Applications INNER JOIN
                            LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                            WHERE ApplicantPersonID = @ApplicantPersonID 
                            and ApplicationTypeID = @ApplicationTypeID 
							and LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID
                            and ApplicationStatus = 1";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            Command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            Command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                Connection.Open();
                object result = Command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int ActiveAppID))
                    ActiveApplicationID = ActiveAppID;
            }
            finally
            {
                Connection.Close();
            }

            return ActiveApplicationID;
        }

        public static bool IsActiveApplicationExist(int PersonID, int ApplicationTypeID, int LicenseClassID)
        {
            return (GetActiveApplicationID(PersonID, ApplicationTypeID, LicenseClassID) != -1);
        }

        public static bool UpdateStatus(int ApplicationID, byte ApplicationStatus)
        {
            int RowsAffected = 0;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"Update  Applications  
                            set 
                                ApplicationStatus = @NewStatus, 
                                LastStatusDate = @LastStatusDate
                            where ApplicationID=@ApplicationID;";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            Command.Parameters.AddWithValue("@NewStatus", ApplicationStatus);
            Command.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);

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