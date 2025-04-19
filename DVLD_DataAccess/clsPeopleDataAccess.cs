using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsPeopleDataAccess
    {
        public static DataTable ListPeople()
        {
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"SELECT People.PersonID, People.NationalNo,
              People.FirstName, People.SecondName, People.ThirdName, People.LastName,
			  People.DateOfBirth, People.Gender,  
				  CASE
                  WHEN People.Gender = 0 THEN 'Male'

                  ELSE 'Female'

                  END as GenderCaption ,
			  People.Address, People.Phone, People.Email, 
              People.NationalityCountryID, Countries.CountryName, People.ImagePath
              FROM            People INNER JOIN
                         Countries ON People.NationalityCountryID = Countries.CountryID
                ORDER BY People.FirstName";

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

        public static bool GetPersonByID(int ID, ref string NationalNum, ref string FirstName, ref string SecondName, ref string ThirdName, 
            ref string LastName, ref short Gender, ref string Email,
            ref string Phone, ref string Address, ref DateTime DateOfBirth,
            ref int CountryID, ref string ImagePath)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"select * from People where PersonID = @PersonID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", ID);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;
                    NationalNum = (string)Reader["NationalNo"];
                    FirstName = (string)Reader["FirstName"];
                    SecondName = (string)Reader["SecondName"];
                    ThirdName = Convert.ToString(Reader["ThirdName"]);
                    LastName = (string)Reader["LastName"];
                    Email = Convert.ToString(Reader["Email"]);
                    Gender = Convert.ToInt16(Reader["Gender"]);
                    Phone = (string)Reader["Phone"];
                    Address = (string)Reader["Address"];
                    DateOfBirth = (DateTime)Reader["DateOfBirth"];
                    CountryID = (int)Reader["NationalityCountryID"];
                    ImagePath = Convert.ToString(Reader["ImagePath"]);
                }
                Reader.Close();
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

        public static bool GetPersonByNationalNo(string NationalNo, ref int ID, ref string FirstName, ref string SecondName, ref string ThirdName,
            ref string LastName, ref short Gender, ref string Email,
            ref string Phone, ref string Address, ref DateTime DateOfBirth,
            ref int CountryID, ref string ImagePath)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string Query = @"select * from People where NationalNo = @NationalNo";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    IsFound = true;
                    ID = (int)Reader["PersonID"];
                    FirstName = (string)Reader["FirstName"];
                    SecondName = (string)Reader["SecondName"];
                    ThirdName = Convert.ToString(Reader["ThirdName"]);
                    LastName = (string)Reader["LastName"];
                    Email = Convert.ToString(Reader["Email"]);
                    Gender = Convert.ToInt16(Reader["Gender"]);
                    Phone = (string)Reader["Phone"];
                    Address = (string)Reader["Address"];
                    DateOfBirth = (DateTime)Reader["DateOfBirth"];
                    CountryID = (int)Reader["NationalityCountryID"];
                    ImagePath = Convert.ToString(Reader["ImagePath"]);
                }
                Reader.Close();
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

        public static int AddNewPerson(string NationalNum, string FirstName, string SecondName, string ThirdName,
            string LastName, short Gender, string Email,
            string Phone, string Address, DateTime DateOfBirth,
            int CountryID, string ImagePath)
        {
            int PersonID = -1;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"insert into People values(@NationalNo, @FirstName, @SecondName, @ThirdName,
                             @LastName, @DateOfBirth, @Gender, @Address, @Phone,
                             @Email, @NationalityCountryID, @ImagePath);
                             select scope_identity();";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@NationalNo", NationalNum);
            Command.Parameters.AddWithValue("@FirstName", FirstName);
            Command.Parameters.AddWithValue("@SecondName", SecondName);
            if (ThirdName != "" && ThirdName != null)
            {
                Command.Parameters.AddWithValue("@ThirdName", ThirdName);
            }
            else
            {
                Command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);
            }
            Command.Parameters.AddWithValue("@LastName", LastName);
            Command.Parameters.AddWithValue("@Gender", Convert.ToInt16(Gender));
            if (Email != "" && Email != null)
            {
                Command.Parameters.AddWithValue("@Email", Email);
            }
            else
            {
                Command.Parameters.AddWithValue("@Email", System.DBNull.Value);
            }
            Command.Parameters.AddWithValue("@Phone", Phone);
            Command.Parameters.AddWithValue("@Address", Address);
            Command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            Command.Parameters.AddWithValue("@NationalityCountryID", CountryID);
            if (ImagePath != "" && ImagePath != null)
            {
                Command.Parameters.AddWithValue("@ImagePath", ImagePath);
            }
            else
            {
                Command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
            }

            try
            {
                Connection.Open();

                object result = Command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int InsertedID))
                    PersonID = InsertedID;
            }
            finally
            {
                Connection.Close();
            }

            return PersonID;
        }

        public static bool UpdatePerson(int ID, string NationalNum, string FirstName, string SecondName, string ThirdName,
            string LastName, short Gender, string Email,
            string Phone, string Address, DateTime DateOfBirth,
            int CountryID, string ImagePath)
        {
            int RowsAffected = 0;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"update People set NationalNo = @NationalNo, FirstName = @FirstName, SecondName = @SecondName,
                             ThirdName = @ThirdName, LastName = @LastName, Gender = @Gender, Email= @Email,
                             Phone = @Phone, Address = @Address,
                             DateOfBirth = @DateOfBirth, 
                             NationalityCountryID = @NationalityCountryID, ImagePath = @ImagePath
                             where PersonID = @PersonID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", ID);
            Command.Parameters.AddWithValue("@NationalNo", NationalNum);
            Command.Parameters.AddWithValue("@FirstName", FirstName);
            Command.Parameters.AddWithValue("@SecondName", SecondName);
            if (ThirdName != "" && ThirdName != null)
            {
                Command.Parameters.AddWithValue("@ThirdName", ThirdName);
            }
            else
            {
                Command.Parameters.AddWithValue("@ThirdName", System.DBNull.Value);
            }
            Command.Parameters.AddWithValue("@LastName", LastName);
            Command.Parameters.AddWithValue("@Gender", Gender);
            if (Email != "" && Email != null)
            {
                Command.Parameters.AddWithValue("@Email", Email);
            }
            else
            {
                Command.Parameters.AddWithValue("@Email", System.DBNull.Value);
            }
            Command.Parameters.AddWithValue("@Phone", Phone);
            Command.Parameters.AddWithValue("@Address", Address);
            Command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            Command.Parameters.AddWithValue("@NationalityCountryID", CountryID);
            if (ImagePath != "" && ImagePath != null)
            {
                Command.Parameters.AddWithValue("@ImagePath", ImagePath);
            }
            else
            {
                Command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
            }

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

        public static bool DeletePerson(int ID)
        {
            int RowsAffected = 0;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"delete from People where PersonID = @PersonID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", ID);

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

        public static bool IsPersonExist(int ID)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"select Found = 1 from People where PersonID = @PersonID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@PersonID", ID);

            try
            {
                Connection.Open();
                object result = Command.ExecuteScalar();

                if (result != null)
                    IsFound = true;
            }
            finally
            {
                Connection.Close();
            }

            return IsFound;
        }

        public static bool IsPersonExist(string NationalNo)
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string Query = @"select Found = 1 from People where NationalNo = @NationalNo";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@NationalNo", NationalNo);

            try
            {
                Connection.Open();
                object result = Command.ExecuteScalar();

                if (result != null)
                    IsFound = true;
            }
            finally
            {
                Connection.Close();
            }

            return IsFound;
        }
    }
}