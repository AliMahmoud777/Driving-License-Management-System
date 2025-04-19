using System;
using System.Data;
using DVLD_Business;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsDetainedLicensesBusiness
    {
        private enum enMode { AddNew, Update };
        private enMode _Mode = enMode.AddNew;

        public int DetainID { get; set; }
        public int LicenseID { get; set; }
        public clsLicensesBusiness LicenseInfo;
        public DateTime DetainDate { get; set; }
        public float FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUsersBusiness CreatedByUserInfo;
        public bool IsReleased { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ReleasedByUserID { get; set; }
        public clsUsersBusiness ReleasedByUserInfo;
        public int ReleaseApplicationID { get; set; }
        public clsApplicationsBusiness ReleaseApplicationInfo;

        public clsDetainedLicensesBusiness()
        {
            this.DetainID = -1;
            this.LicenseID = -1;
            this.LicenseInfo = new clsLicensesBusiness();
            this.DetainDate = DateTime.Now;
            this.FineFees = default;
            this.CreatedByUserID = -1;
            this.CreatedByUserInfo = new clsUsersBusiness();
            this.IsReleased = false;
            this.ReleaseDate = DateTime.MaxValue;
            this.ReleasedByUserID = -1;
            this.ReleasedByUserInfo = new clsUsersBusiness();
            this.ReleaseApplicationID = -1;
            this.ReleaseApplicationInfo = new clsApplicationsBusiness();

            _Mode = enMode.AddNew;
        }

        public clsDetainedLicensesBusiness(int DetainID,
            int LicenseID, DateTime DetainDate,
            float FineFees, int CreatedByUserID,
            bool IsReleased, DateTime ReleaseDate,
            int ReleasedByUserID, int ReleaseApplicationID)
        {
            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.LicenseInfo = clsLicensesBusiness.Find(this.LicenseID);
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedByUserInfo = clsUsersBusiness.Find(this.CreatedByUserID);
            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleasedByUserInfo = clsUsersBusiness.Find(this.ReleasedByUserID);
            this.ReleaseApplicationID = ReleaseApplicationID;
            this.ReleaseApplicationInfo = clsApplicationsBusiness.FindBaseApplication(this.ReleaseApplicationID);

            _Mode = enMode.Update;
        }

        public static clsDetainedLicensesBusiness Find(int DetainID)
        {
            int LicenseID = -1, CreatedByUserID = -1, ReleasedByUserID = -1, ReleaseApplicationID = -1;
            DateTime DetainDate = DateTime.Now, ReleaseDate = DateTime.MaxValue;
            bool IsReleased = false;
            float FineFees = default;

            if (clsDetainedLicensesDataAccess.GetDetainedLicenseInfo(DetainID,
            ref LicenseID, ref DetainDate,
            ref FineFees, ref CreatedByUserID,
            ref IsReleased, ref ReleaseDate,
            ref ReleasedByUserID, ref ReleaseApplicationID))

                return new clsDetainedLicensesBusiness(DetainID,
                     LicenseID, DetainDate,
                     FineFees, CreatedByUserID,
                     IsReleased, ReleaseDate,
                     ReleasedByUserID, ReleaseApplicationID);
            else

                return null;
        }

        public static clsDetainedLicensesBusiness FindByLicenseID(int LicenseID)
        {
            int DetainID = -1, CreatedByUserID = -1, ReleasedByUserID = -1, ReleaseApplicationID = -1;
            DateTime DetainDate = DateTime.Now, ReleaseDate = DateTime.MaxValue;
            bool IsReleased = false;
            float FineFees = default;

            if (clsDetainedLicensesDataAccess.GetDetainedLicenseInfoByLicenseID(LicenseID,
            ref DetainID, ref DetainDate,
            ref FineFees, ref CreatedByUserID,
            ref IsReleased, ref ReleaseDate,
            ref ReleasedByUserID, ref ReleaseApplicationID))

                return new clsDetainedLicensesBusiness(DetainID,
                     LicenseID, DetainDate,
                     FineFees, CreatedByUserID,
                     IsReleased, ReleaseDate,
                     ReleasedByUserID, ReleaseApplicationID);
            else

                return null;
        }

        private bool _AddNewDetainedLicense()
        {
            this.DetainID = clsDetainedLicensesDataAccess.AddNewDetainedLicense(this.LicenseID, this.DetainDate, this.FineFees,
                this.CreatedByUserID);

            return (this.DetainID != -1);
        }

        private bool _UpdateDetainedLicense()
        {
            return clsDetainedLicensesDataAccess.UpdateDetainedLicense(this.DetainID, this.LicenseID, this.DetainDate,
                this.FineFees, this.CreatedByUserID);
        }

        public static DataTable GetAllDetainedLicenses()
        {
            return clsDetainedLicensesDataAccess.GetAllDetainedLicenses();
        }

        public bool Release(int ReleasedByUserID, int ReleaseApplicationID)
        {
            return clsDetainedLicensesDataAccess.ReleaseDetainedLicense(this.DetainID, ReleasedByUserID, ReleaseApplicationID);
        }

        public static bool IsLicenseDetained(int LicenseID)
        {
            return clsDetainedLicensesDataAccess.IsLicenseDetained(LicenseID);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDetainedLicense())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateDetainedLicense();
            }

            return false;
        }
    }
}