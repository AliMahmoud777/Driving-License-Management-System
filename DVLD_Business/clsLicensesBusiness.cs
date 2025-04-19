using DVLD_DataAccess;
using System;
using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace DVLD_Business
{
    public class clsLicensesBusiness
    {
        private enum enMode { AddNew, Update };
        public enum enIssueReason { FirstTime = 1, Renew = 2, DamagedReplacement = 3, LostReplacement = 4 };

        private enMode _Mode = enMode.AddNew;

        public int ID { get; set; }
        public int ApplicationID { get; set; }
        public clsApplicationsBusiness ApplicationInfo;

        public int DriverID { get; set; }
        public clsDriversBusiness DriverInfo;
        public int LicenseClassID { get; set; }
        public clsLicenseClassesBusiness LicenseClassInfo;

        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public float PaidFees { get; set; }
        public bool IsActive { get; set; }
        public enIssueReason IssueReason { get; set; }
        public string IssueReasonText
        {
            get
            {
                switch (IssueReason)
                {
                    case enIssueReason.FirstTime:
                        return "First Time";

                    case enIssueReason.Renew:
                        return "Renew";

                    case enIssueReason.DamagedReplacement:
                        return "Replacement for Damaged";

                    case enIssueReason.LostReplacement:
                        return "Replacement for Lost";

                    default:
                        return "First Time";
                }
            }
        }
        public int UserID { get; set; }
        public clsUsersBusiness UserInfo;

        public bool IsDetained
        {
            get
            {
                return clsDetainedLicensesBusiness.IsLicenseDetained(this.ID);
            }
        }
        
        public clsLicensesBusiness()
        {
            this.ID = -1;
            this.ApplicationID = -1;
            this.ApplicationInfo = new clsApplicationsBusiness();
            this.DriverID = -1;
            this.DriverInfo = new clsDriversBusiness();
            this.LicenseClassID = -1;
            this.LicenseClassInfo = new clsLicenseClassesBusiness();
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = "";
            this.PaidFees = default;
            this.IsActive = default;
            this.IssueReason = enIssueReason.FirstTime;
            this.UserID = -1;
            this.UserInfo = new clsUsersBusiness();

            _Mode = enMode.AddNew;
        }
        public clsLicensesBusiness(int ID, int ApplicationID, int DriverID, int LicenseClassID, DateTime IssueDate, 
            DateTime ExpirationDate, string Notes, float PaidFees, bool IsActive, enIssueReason issueReason, int UserID)
        {
            this.ID = ID;
            this.ApplicationID = ApplicationID;
            this.ApplicationInfo = clsApplicationsBusiness.FindBaseApplication(this.ApplicationID);
            this.DriverID = DriverID;
            this.DriverInfo = clsDriversBusiness.Find(this.DriverID);
            this.LicenseClassID = LicenseClassID;
            this.LicenseClassInfo = clsLicenseClassesBusiness.Find(this.LicenseClassID);
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = issueReason;
            this.UserID = UserID;
            this.UserInfo = clsUsersBusiness.Find(this.UserID);

            _Mode = enMode.Update;
        }

        public static clsLicensesBusiness Find(int ID)
        {
            int ApplicationID = -1, DriverID = -1, LicenseClass = -1, CreatedByUserID = -1;
            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            string Notes = "";
            float PaidFees = default;
            bool IsActive = default;
            byte IssueReason = 0;

            if(clsLicensesDataAccess.GetLicenseByID(ID, ref ApplicationID, ref DriverID, ref LicenseClass,
            ref IssueDate, ref ExpirationDate, ref Notes,
            ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))
            {
                return new clsLicensesBusiness(ID, ApplicationID, DriverID, LicenseClass,
                        IssueDate, ExpirationDate, Notes,
                        PaidFees, IsActive, (enIssueReason)IssueReason, CreatedByUserID);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewLicense()
        {
            this.ID = clsLicensesDataAccess.AddNewLicense(this.ApplicationID, this.DriverID, this.LicenseClassID,
               this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees,
               this.IsActive, (byte)this.IssueReason, this.UserID);

            return (this.ID != -1);
        }

        private bool _UpdateLicense()
        {
            return clsLicensesDataAccess.UpdateLicense(this.ID, this.ApplicationID, this.DriverID, this.LicenseClassID,
               this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees,
               this.IsActive, (byte)this.IssueReason, this.UserID);
        }

        public static DataTable ListDriverLicenses(int DriverID)
        {
            return clsLicensesDataAccess.GetDriverLicenses(DriverID);
        }

        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {
            return clsLicensesDataAccess.GetActiveLicenseIDByPersonID(PersonID, LicenseClassID);
        }

        public static bool IsActiveLicenseExist(int PersonID, int LicenseClassID)
        {
            return clsLicensesDataAccess.IsActiveLicenseExist(PersonID, LicenseClassID);
        }

        public bool Deactivate()
        {
            return clsLicensesDataAccess.DeactivateLicense(this.ID);
        }

        public bool IsExpired()
        {
            return (this.ExpirationDate < DateTime.Now);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicense())
                    {

                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateLicense();
            }

            return false;
        }
    }
}