using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsDriversBusiness
    {
        private enum enMode { AddNew, Update };

        private enMode _Mode;
        public int ID { get; set; }
        public int PersonID { get; set; }
        public int UserID { get; set; }
        public DateTime CreationDate { get; set; }

        public clsDriversBusiness()
        {
            this.ID = -1;
            this.PersonID = -1;
            this.UserID = -1;
            this.CreationDate = DateTime.Now;

            _Mode = enMode.AddNew;
        }

        private clsDriversBusiness(int ID, int PersonID, int UserID, DateTime CreationDate)
        {
            this.ID = ID;
            this.PersonID = PersonID;
            this.UserID = UserID;
            this.CreationDate = CreationDate;

            _Mode = enMode.Update;
        }

        public static clsDriversBusiness Find(int ID)
        {
            int PersonID = -1, UserID = -1;
            DateTime CreationDate = DateTime.Now;

            if(clsDriversDataAccess.GetDriverByID(ID, ref PersonID, ref UserID, ref CreationDate))
            {
                return new clsDriversBusiness(ID, PersonID, UserID, CreationDate);
            }
            else
            {
                return null;
            }
        }

        public static clsDriversBusiness FindByPersonID(int PersonID)
        {
            int DriverID = -1, UserID = -1;
            DateTime CreationDate = DateTime.Now;

            if (clsDriversDataAccess.GetDriverByPersonID(PersonID, ref DriverID, ref UserID, ref CreationDate))
            {
                return new clsDriversBusiness(DriverID, PersonID, UserID, CreationDate);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewDriver()
        {
            this.ID = clsDriversDataAccess.AddNewDriver(this.PersonID, this.UserID);

            return (this.ID != -1);
        }

        public static DataTable GetAllDrivers()
        {
            return clsDriversDataAccess.ListDrivers();
        }

        public static int NumberOfDrivers()
        {
            return clsDriversDataAccess.GetNumberOfDrivers();
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDriver())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return true;
            }

            return false;
        }
    }
}