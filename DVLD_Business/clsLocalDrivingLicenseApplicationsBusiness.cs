using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsLocalDrivingLicenseApplicationsBusiness : clsApplicationsBusiness
    {
        private enMode _Mode = enMode.AddNew;
        public int LocalDrivingLicenseApplicationID { get; set; }
        public int LicenseClassID { get; set; }
        public clsLicenseClassesBusiness LicenseClassesInfo;

        public clsLocalDrivingLicenseApplicationsBusiness()
        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.LicenseClassID = -1;

            _Mode = enMode.AddNew;
        }

        private clsLocalDrivingLicenseApplicationsBusiness(int LocalDrivingLicenseApplicationID, int LicenseClassID, int ApplicationID, int PersonID, DateTime Date, int TypeID, enStatus Status,
             DateTime LastStatusDate, float PaidFees, int UserID) : base(ApplicationID, PersonID, Date, TypeID, Status,
             LastStatusDate, PaidFees, UserID)
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.LicenseClassID = LicenseClassID;
            this.LicenseClassesInfo = clsLicenseClassesBusiness.Find(this.LicenseClassID);

            _Mode = enMode.Update;
        }

        public static clsLocalDrivingLicenseApplicationsBusiness Find(int ID)
        {
            int ApplicationID = -1, LicenseClassID = -1;

            if(clsLocalDrivingLicenseApplicationsDataAccess.GetLDLApplicationByID(ID, ref ApplicationID, ref LicenseClassID))
            {
                clsApplicationsBusiness Application = FindBaseApplication(ApplicationID);

                return new clsLocalDrivingLicenseApplicationsBusiness(ID, LicenseClassID, Application.ApplicationID,
                    Application.PersonID, Application.Date, Application.TypeID, Application.Status, Application.LastStatusDate,
                    Application.PaidFees, Application.UserID);
            }
            else
            {
                return null;
            }
        }

        public static clsLocalDrivingLicenseApplicationsBusiness FindByApplicationID(int ApplicationID)
        {
            int LocalDrivingLicenseApplicationID = -1, LicenseClassID = -1;

            if (clsLocalDrivingLicenseApplicationsDataAccess.GetLDLApplicationByApplicationID(ApplicationID, ref LocalDrivingLicenseApplicationID, ref LicenseClassID))
            {
                clsApplicationsBusiness Application = FindBaseApplication(ApplicationID);

                return new clsLocalDrivingLicenseApplicationsBusiness(LocalDrivingLicenseApplicationID, LicenseClassID, Application.ApplicationID,
                    Application.PersonID, Application.Date, Application.TypeID, Application.Status, Application.LastStatusDate,
                    Application.PaidFees, Application.UserID);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewLDLApplication()
        {
            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationsDataAccess.AddNewLDLApplication(this.ApplicationID, this.LicenseClassID);

            return (this.LocalDrivingLicenseApplicationID != -1);
        }

        private bool _UpdateLDLApplication()
        {
            return clsLocalDrivingLicenseApplicationsDataAccess.UpdateLDLApplication(this.LocalDrivingLicenseApplicationID, this.ApplicationID, this.LicenseClassID);
        }

        public bool DeleteLDLApplication()
        {
            if (clsLocalDrivingLicenseApplicationsDataAccess.DeleteLDLApplication(this.LocalDrivingLicenseApplicationID))
                return DeleteApplication(this.ApplicationID);
            else
                return false;
        }

        public static DataTable GetAllLDLApplications()
        {
            return clsLocalDrivingLicenseApplicationsDataAccess.ListLDLApplications();
        }

        public byte GetPassedTestsCount()
        {
            return clsTestsDataAccess.GetPassedTestsCount(this.LocalDrivingLicenseApplicationID);
        }

        public bool IsTestTypePassed(clsTestTypesBusiness.enTestType enTestType)
        {
            return clsLocalDrivingLicenseApplicationsDataAccess.IsTestTypePassed(this.LocalDrivingLicenseApplicationID,
                (int)enTestType);
        }

        public static bool IsTestTypePassed(int LocalDrivingLicenseApplicationID, clsTestTypesBusiness.enTestType enTestType)
        {
            return clsLocalDrivingLicenseApplicationsDataAccess.IsTestTypePassed(LocalDrivingLicenseApplicationID,
                (int)enTestType);
        }

        public int IssueLicenseForFirstTime(string Notes, int UserID)
        {
            clsLicensesBusiness License = new clsLicensesBusiness();

            clsDriversBusiness Driver = clsDriversBusiness.FindByPersonID(this.PersonID);

            if (Driver == null)
            {
                Driver = new clsDriversBusiness();
                Driver.PersonID = this.PersonID;
                Driver.UserID = UserID;
                
                if(Driver.Save())
                    License.DriverID = Driver.ID;
                else
                    return -1;
            }
            else
            {
                License.DriverID = Driver.ID;
            }

            License.ApplicationID = this.ApplicationID;
            License.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassesInfo.DefaultValidityPeriod);
            License.IsActive = true;
            License.IssueDate = DateTime.Now;
            License.IssueReason = clsLicensesBusiness.enIssueReason.FirstTime;
            License.LicenseClassID = this.LicenseClassID;
            License.Notes = Notes;
            License.PaidFees = this.LicenseClassesInfo.ClassFees;
            License.UserID = UserID;

            if (License.Save())
            {
                this.Complete();

                return License.ID;
            }
            else
            {
                return -1;
            }
        }

        public bool Save()
        {

            _BaseMode = _Mode;
            if (!BaseSave())
                return false;

            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLDLApplication())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateLDLApplication();

                default:
                    return false;
            }
        }
    }
}