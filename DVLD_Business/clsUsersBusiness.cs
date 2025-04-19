using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsUsersBusiness
    {
        enum enMode { AddNew, Update };

        private enMode Mode = enMode.AddNew;

        public int ID { get; set; }
        public int PersonID { get; set; }

        public clsPeopleBusiness Person;
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public clsUsersBusiness()
        {
            this.ID = -1;
            this.PersonID = -1;
            this.Username = "";
            this.Password = "";
            this.IsActive = default;

            this.Mode = enMode.AddNew;
        }
        private clsUsersBusiness(int ID, int PersonID, string Username, string Password, bool IsActive)
        {
            this.ID = ID;
            this.PersonID = PersonID;
            this.Person = clsPeopleBusiness.Find(this.PersonID);
            this.Username = Username;
            this.Password = Password;
            this.IsActive = IsActive;

            this.Mode = enMode.Update;
        }

        public static clsUsersBusiness Find(int ID)
        {
            string Username = "", Password = "";
            int PersonID = -1;
            bool IsActive = default;

            if (clsUsersDataAccess.GetUserByID(ID, ref PersonID, ref Username, ref Password, ref IsActive))
            {
                return new clsUsersBusiness(ID, PersonID, Username, Password, IsActive);
            }
            else
            {
                return null;
            }
        }

        public static clsUsersBusiness FindByPersonID(int PersonID)
        {
            string Username = "", Password = "";
            int UserID = -1;
            bool IsActive = default;

            if (clsUsersDataAccess.GetUserByPersonID(PersonID, ref UserID, ref Username, ref Password, ref IsActive))
            {
                return new clsUsersBusiness(UserID, PersonID, Username, Password, IsActive);
            }
            else
            {
                return null;
            }
        }

        public static clsUsersBusiness FindByUsernameAndPassword(string Username, string Password)
        {
            int UserID =-1, PersonID = -1;
            bool IsActive = default;

            if (clsUsersDataAccess.GetUserByUsernameAndPassword(ref Username, ref Password, ref UserID, ref PersonID, ref IsActive))
            {
                return new clsUsersBusiness(UserID, PersonID, Username, Password, IsActive);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewUser()
        {
            this.ID = clsUsersDataAccess.AddNewUser(this.PersonID, this.Username, this.Password, this.IsActive);

            return (this.ID != -1);
        }

        private bool _UpdateUser()
        {
            return clsUsersDataAccess.UpdateUser(this.ID, this.PersonID, this.Username, this.Password, this.IsActive);
        }

        public static bool DeleteUser(int ID)
        {
            return clsUsersDataAccess.DeleteUser(ID);
        }

        public static DataTable GetAllUsers()
        {
            return clsUsersDataAccess.ListUsers();
        }

        public static bool IsUserExist(int ID)
        {
            return clsUsersDataAccess.IsUserExist(ID);
        }

        public static bool IsUserExist(string Username)
        {
            return clsUsersDataAccess.IsUserExist(Username);
        }

        public static bool IsUserExistByPersonID(int PersonID)
        {
            return clsUsersDataAccess.IsUserExistByPersonID(PersonID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateUser();
            }

            return false;
        }
    }
}