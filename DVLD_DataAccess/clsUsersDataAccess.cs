using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsUsersDataAccess
    {
        public static DataTable ListUsers()
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"SELECT Users.UserID, 
                             Users.PersonID, CONCAT(People.FirstName, ' ', People.SecondName, ' ', 
                             ISNULL(People.ThirdName + ' ', ''), People.LastName) as FullName,
                             Users.UserName, Users.IsActive FROM Users INNER JOIN 
                             People ON Users.PersonID = People.PersonID;";

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
        public static bool GetUserByID(int ID, ref int PersonID, ref string Username, ref string Password,
            ref bool IsActive)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"select * from Users where UserID = @UserID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@UserID", ID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;
                    PersonID = (int)Reader["PersonID"];
                    Username = (string)Reader["UserName"];
                    Password = (string)Reader["Password"];
                    IsActive = Convert.ToBoolean(Reader["IsActive"]);
                }
                Reader.Close();
            }
            finally
            {
                Connection.Close();
            }

            return IsFound;
        }

        public static bool GetUserByPersonID(int PersonID, ref int UserID, ref string Username, ref string Password,
            ref bool IsActive)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"select * from Users where PersonID = @PersonID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;
                    UserID = (int)Reader["UserID"];
                    Username = (string)Reader["UserName"];
                    Password = (string)Reader["Password"];
                    IsActive = Convert.ToBoolean(Reader["IsActive"]);
                }
                Reader.Close();
            }
            finally
            {
                Connection.Close();
            }

            return IsFound;
        }

        public static bool GetUserByUsernameAndPassword(ref string Username, ref string Password, ref int UserID, ref int PersonID,
            ref bool IsActive)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"select * from Users where UserName = @Username and Password = @Password";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@Username", Username);
            Command.Parameters.AddWithValue("@Password", Password);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;
                    UserID = (int)Reader["UserID"];
                    PersonID = (int)Reader["PersonID"];
                    Username = (string)Reader["UserName"];
                    Password = (string)Reader["Password"];
                    IsActive = Convert.ToBoolean(Reader["IsActive"]);
                }
                Reader.Close();
            }
            finally
            {
                Connection.Close();
            }

            return IsFound;
        }

        public static int AddNewUser(int PersonID, string Username, string Password, bool IsActive)
        {
            int UserID = -1;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"insert into Users values(@PersonID, @UserName, @Password, @IsActive);
                             select scope_identity();";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@UserName", Username);
            Command.Parameters.AddWithValue("@Password", Password);
            Command.Parameters.AddWithValue("@IsActive", IsActive);

            try
            {
                Connection.Open();

                object result = Command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                    UserID = InsertedID;
            }
            finally
            {
                Connection.Close();
            }

            return UserID;
        }

        public static bool UpdateUser(int ID, int PersonID, string Username, string Password, bool IsActive)
        {
            int RowsAffected = 0;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"update Users set PersonID = @PersonID, UserName = @UserName, Password = @Password, IsActive = @IsActive
                             where UserID = @UserID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@UserID", ID);
            Command.Parameters.AddWithValue("@PersonID", PersonID);
            Command.Parameters.AddWithValue("@UserName", Username);
            Command.Parameters.AddWithValue("@Password", Password);
            Command.Parameters.AddWithValue("@IsActive", IsActive);

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

        public static bool DeleteUser(int ID)
        {
            int RowsAffected = 0;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"delete from Users where UserID = @UserID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("UserID", ID);

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

        public static bool IsUserExist(int ID)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"select Found = 1 from Users where UserID = @UserID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@UserID", ID);

            try
            {
                Connection.Open();
                object result = Command.ExecuteScalar();

                if (result != null)
                    IsFound = true;
            }
            catch (Exception e)
            {

            }
            finally
            {
                Connection.Close();
            }

            return IsFound;
        }

        public static bool IsUserExist(string Username)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"select Found = 1 from Users where UserName = @UserName";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@UserName", Username);

            try
            {
                Connection.Open();
                object result = Command.ExecuteScalar();

                if (result != null)
                    IsFound = true;
            }
            catch (Exception e)
            {

            }
            finally
            {
                Connection.Close();
            }

            return IsFound;
        }

        public static bool IsUserExistByPersonID(int PersonID)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"select Found = 1 from Users where PersonID = @PersonID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                Connection.Open();
                object result = Command.ExecuteScalar();

                if (result != null)
                    IsFound = true;
            }
            catch (Exception e)
            {

            }
            finally
            {
                Connection.Close();
            }

            return IsFound;
        }
    }
}