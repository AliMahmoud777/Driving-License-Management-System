using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsApplicationsBusiness
    {
        protected enum enMode { AddNew, Update };
        public enum enAppliactionType {
            NewLocalDrivingLicense = 1, RenewDrivingLicense = 2, ReplaceLostDrivingLicense = 3,
            ReplaceDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicsense = 5, NewInternationalLicense = 6, RetakeTest = 7
        };
        public enum enStatus { New = 1, Cancelled = 2, Completed = 3 };

        protected enMode _BaseMode = enMode.AddNew;
        public int ApplicationID { get; set; }
        public int PersonID { get; set; }
        public clsPeopleBusiness PersonInfo;
        public DateTime Date { get; set; }
        public int TypeID { get; set; }
        public clsApplicationTypesBusiness TypeInfo;
        public enStatus Status { get; set; }
        public string StatusText
        {
            get
            {
                switch (Status)
                {
                    case enStatus.New:
                        return "New";
                    case enStatus.Cancelled:
                        return "Cancelled";
                    case enStatus.Completed:
                        return "Completed";

                    default:
                        return "Unknown";
                }
            }
        }
        public DateTime LastStatusDate { get; set; }
        public float PaidFees { get; set; }
        public int UserID { get; set; }
        public clsUsersBusiness UserInfo;

        public clsApplicationsBusiness()
        {
            this.ApplicationID = -1;
            this.PersonID = -1;
            this.Date = DateTime.Now;
            this.TypeID = -1;
            this.Status = enStatus.New;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = default;
            this.UserID = -1;

            _BaseMode = enMode.AddNew;
        }

        protected clsApplicationsBusiness(int ID, int PersonID, DateTime Date, int TypeID, enStatus Status,
             DateTime LastStatusDate, float PaidFees, int UserID)
        {
            this.ApplicationID = ID;
            this.PersonID = PersonID;
            this.PersonInfo = clsPeopleBusiness.Find(this.PersonID);
            this.Date = Date;
            this.TypeID = TypeID;
            this.TypeInfo = clsApplicationTypesBusiness.Find(this.TypeID);
            this.Status = Status;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.UserID = UserID;
            this.UserInfo = clsUsersBusiness.Find(this.UserID);

            _BaseMode = enMode.Update;
        }

        public static clsApplicationsBusiness FindBaseApplication(int ID)
        {
            int PersonID = -1, TypeID = -1, UserID = -1;
            byte Status = 0;
            DateTime Date = DateTime.Now, LastStatusDate = DateTime.Now;
            float PaidFees = default;

            if(clsApplicationsDataAccess.GetApplicationByID(ID, ref PersonID, ref Date, ref TypeID, ref Status,
                 ref LastStatusDate, ref PaidFees, ref UserID))
            {
                return new clsApplicationsBusiness(ID, PersonID, Date, TypeID, (enStatus)Status, LastStatusDate, PaidFees, UserID);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewApplication()
        {
            this.ApplicationID = clsApplicationsDataAccess.AddNewApplication(this.PersonID, this.Date, this.TypeID, (byte)this.Status,
                 this.LastStatusDate, this.PaidFees, this.UserID);

            return (this.ApplicationID != -1);
        }

        private bool _UpdateApplication()
        {
            return clsApplicationsDataAccess.UpdateApplication(this.ApplicationID, this.PersonID, this.Date, this.TypeID, (byte)this.Status,
                 this.LastStatusDate, this.PaidFees, this.UserID);
        }

        public static bool DeleteApplication(int ID)
        {
            return clsApplicationsDataAccess.DeleteApplication(ID);
        }

        public static DataTable GetAllApplications()
        {
            return clsApplicationsDataAccess.ListApplications();
        }

        public static bool IsApplicationExist(int ID)
        {
            return clsApplicationsDataAccess.IsApplicationExist(ID);
        }

        public bool Cancel()
        {
            return clsApplicationsDataAccess.UpdateStatus(this.ApplicationID, 2);
        }

        public bool Complete()
        {
            return clsApplicationsDataAccess.UpdateStatus(this.ApplicationID, 3);
        }

        public static int GetActiveApplicationID(int PersonID, int TypeID, int LicenseClassID)
        {
            return clsApplicationsDataAccess.GetActiveApplicationID(PersonID, TypeID, LicenseClassID);
        }

        public static bool IsActiveApplicationExist(int PersonID, int TypeID, int LicenseClassID)
        {
            return clsApplicationsDataAccess.IsActiveApplicationExist(PersonID, TypeID, LicenseClassID);
        }

        public bool BaseSave()
        {
            switch (_BaseMode)
            {
                case enMode.AddNew:
                    if (_AddNewApplication())
                    {
                        _BaseMode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateApplication();

                default:
                    return false;
            }
        }
    }
}