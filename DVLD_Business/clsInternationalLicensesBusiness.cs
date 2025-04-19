using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsInternationalLicensesBusiness
    {
        private enum enMode { AddNew, Update };
        private enMode _Mode = enMode.AddNew;

        public int InternationalLicenseID { get; set; }
        public int ApplicationID { get; set; }
        public clsApplicationsBusiness ApplicationInfo;
        public int DriverID { get; set; }
        public clsDriversBusiness DriverInfo;
        public int IssuedUsingLocalLicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public int UserID { get; set; }
        public clsUsersBusiness UserInfo;


        public clsInternationalLicensesBusiness()
        {
            this.InternationalLicenseID = -1;
            this.ApplicationID = -1;
            this.ApplicationInfo = new clsApplicationsBusiness();
            this.DriverID = -1;
            this.DriverInfo = new clsDriversBusiness();
            this.IssuedUsingLocalLicenseID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.IsActive = true;
            this.UserID = -1;
            this.UserInfo = new clsUsersBusiness();

            _Mode = enMode.AddNew;
        }

        public clsInternationalLicensesBusiness(int InternationalLicenseID, int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID,
            DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int UserID)
        {
            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.ApplicationInfo = clsApplicationsBusiness.FindBaseApplication(this.ApplicationID);
            this.DriverID = DriverID;
            this.DriverInfo = clsDriversBusiness.Find(this.DriverID);
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.UserID = UserID;
            this.UserInfo = clsUsersBusiness.Find(this.UserID);

            _Mode = enMode.Update;
        }

        public static clsInternationalLicensesBusiness Find(int ID)
        {
            int ApplicationID = -1, DriverID = -1, IssuedUsingLocalLicenseID = -1, UserID = -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            bool IsActive = false;

            if(clsInternationalLicensesDataAccess.GetInternationalLicenseInfo(ID, ref ApplicationID, ref DriverID, 
                ref IssuedUsingLocalLicenseID, ref IssueDate, ref ExpirationDate, ref IsActive, ref UserID))
            {
                return new clsInternationalLicensesBusiness(ID, ApplicationID, DriverID, IssuedUsingLocalLicenseID,
                    IssueDate, ExpirationDate, IsActive, UserID);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewInternationalLicense()
        {
            this.InternationalLicenseID = clsInternationalLicensesDataAccess.AddNewInternationalLicense(this.ApplicationID,
                this.DriverID, this.IssuedUsingLocalLicenseID, this.IssueDate, this.ExpirationDate, this.UserID);

            return (this.InternationalLicenseID != -1);
        }

        private bool _UpdateInternationalLicense()
        {
            return clsInternationalLicensesDataAccess.UpdateInternationalLicense(this.InternationalLicenseID, this.ApplicationID,
                this.DriverID, this.IssuedUsingLocalLicenseID, this.IssueDate, this.ExpirationDate, this.IsActive, this.UserID);
        }

        public static DataTable ListInternationalLicenseApplications()
        {
            return clsInternationalLicensesDataAccess.GetAllInternationalLicenseApplications();
        }

        public static DataTable ListDriverInternationalLicenses(int DriverID)
        {
            return clsInternationalLicensesDataAccess.GetDriverInternationalLicenses(DriverID);
        }

        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {
            return clsInternationalLicensesDataAccess.GetActiveInternationalLicenseIDByDriverID(DriverID);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewInternationalLicense())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateInternationalLicense();
            }

            return false;
        }
    }
}