using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsPeopleBusiness
    {
        enum enMode { AddNew, Update };

        private enMode Mode = enMode.AddNew;
        public int ID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get { return FirstName + " " + SecondName + " " + ((ThirdName != "") ? ThirdName + " " + LastName : LastName); }
        }
        public short Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int CountryID { get; set; }

        public clsCountriesBusiness Country;
        public DateTime DateOfBirth { get; set; }
        public string ImagePath { get; set; }

        public clsPeopleBusiness()
        {
            this.ID = -1;
            this.NationalNo = "";
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.Gender = -1;
            this.Email = "";
            this.Phone = "";
            this.Address = "";
            this.DateOfBirth = DateTime.Now;
            this.CountryID = -1;
            this.ImagePath = "";

            this.Mode = enMode.AddNew;
        }

        private clsPeopleBusiness(int ID, string NationalNo, string FirstName, string SecondName, string ThirdName,
            string LastName, short Gender, string Email,
            string Phone, string Address, DateTime DateOfBirth,
            int CountryID, string ImagePath)
        {
            this.ID = ID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.Gender = Gender;
            this.Email = Email;
            this.Phone = Phone;
            this.Address = Address;
            this.DateOfBirth = DateOfBirth;
            this.CountryID = CountryID;
            this.Country = clsCountriesBusiness.Find(this.CountryID);
            this.ImagePath = ImagePath;

            this.Mode = enMode.Update;
        }

        public static clsPeopleBusiness Find(int ID)
        {
            string NationalNo = "", FirstName = "", SecondName = "", ThirdName = "",
            LastName = "", Email = "", Phone = "", Address = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            short Gender = -1;
            int CountryID = -1;

            if (clsPeopleDataAccess.GetPersonByID(ID, ref NationalNo, ref FirstName, ref SecondName, ref ThirdName,
            ref LastName, ref Gender, ref Email,
            ref Phone, ref Address, ref DateOfBirth,
            ref CountryID, ref ImagePath))
            {
                return new clsPeopleBusiness(ID, NationalNo, FirstName, SecondName, ThirdName, LastName,
                    Gender, Email, Phone, Address, DateOfBirth,
                    CountryID, ImagePath);
            }
            else
            {
                return null;
            }
        }

        public static clsPeopleBusiness Find(string NationalNo)
        {
            string FirstName = "", SecondName = "", ThirdName = "",
            LastName = "", Email = "", Phone = "", Address = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            short Gender = -1;
            int ID = -1, CountryID = -1;

            if (clsPeopleDataAccess.GetPersonByNationalNo(NationalNo, ref ID, ref FirstName, ref SecondName, ref ThirdName,
            ref LastName, ref Gender, ref Email,
            ref Phone, ref Address, ref DateOfBirth,
            ref CountryID, ref ImagePath))
            {
                return new clsPeopleBusiness(ID, NationalNo, FirstName, SecondName, ThirdName, LastName,
                    Gender, Email, Phone, Address, DateOfBirth,
                    CountryID, ImagePath);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewPerson()
        {
            this.ID = clsPeopleDataAccess.AddNewPerson(this.NationalNo, this.FirstName, this.SecondName,
                this.ThirdName, this.LastName, this.Gender, this.Email,
                this.Phone, this.Address, this.DateOfBirth, this.CountryID, this.ImagePath);

            return (this.ID != -1);
        }

        private bool _UpdatePerson()
        {
            return clsPeopleDataAccess.UpdatePerson(this.ID, this.NationalNo, this.FirstName, this.SecondName,
                this.ThirdName, this.LastName, this.Gender, this.Email,
                this.Phone, this.Address, this.DateOfBirth, this.CountryID, this.ImagePath);
        }

        public static bool DeletePerson(int ID)
        {
            return clsPeopleDataAccess.DeletePerson(ID);
        }

        public static DataTable GetAllPeople()
        {
            return clsPeopleDataAccess.ListPeople();
        }

        public static bool IsPersonExist(int ID)
        {
            return clsPeopleDataAccess.IsPersonExist(ID);
        }
        public static bool IsPersonExist(string NationalNo)
        {
            return clsPeopleDataAccess.IsPersonExist(NationalNo);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdatePerson();
            }

            return false;
        }
    }
}