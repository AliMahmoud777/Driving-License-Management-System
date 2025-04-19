using System;
using System.Data;
using DVLD_Business;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsTestAppointmentsBusiness
    {
        private enum enMode { AddNew, Update };
        private enMode _Mode = enMode.AddNew;

        public int TestAppointmentID { get; set; }
        public clsTestTypesBusiness.enTestType TestType { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }
        public DateTime Date { get; set; }
        public float PaidFees { get; set; }
        public int UserID { get; set; }
        public clsUsersBusiness UserInfo;
        public bool IsLocked { get; set; }
        public int RetakeTestApplicationID { get; set; }
        public clsApplicationsBusiness RetakeTestApplicationInfo;
        public int TestID
        {
            get
            {
                return _GetTestID();
            }
        }

        public clsTestAppointmentsBusiness()
        {
            this.TestAppointmentID = -1;
            this.TestType = clsTestTypesBusiness.enTestType.Vision;
            this.LocalDrivingLicenseApplicationID = -1;
            this.Date = DateTime.Now;
            this.PaidFees = default;
            this.UserID = -1;
            this.UserInfo = new clsUsersBusiness();
            this.IsLocked = default;
            this.RetakeTestApplicationID = -1;
            this.RetakeTestApplicationInfo = new clsApplicationsBusiness();

            _Mode = enMode.AddNew;
        }

        private clsTestAppointmentsBusiness(int TestAppointmentID, clsTestTypesBusiness.enTestType TestType, 
            int LocalDrivingLicenseApplicationID, DateTime Date, float PaidFees, int UserID, bool IsLocked, 
            int RetakeTestApplicationID)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestType = TestType;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.Date = Date;
            this.PaidFees = PaidFees;
            this.UserID = UserID;
            this.UserInfo = clsUsersBusiness.Find(this.UserID);
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID = RetakeTestApplicationID;
            this.RetakeTestApplicationInfo = clsApplicationsBusiness.FindBaseApplication(this.RetakeTestApplicationID);

            _Mode = enMode.Update;
        }

        public static clsTestAppointmentsBusiness Find(int TestAppointmentID)
        {
            int LocalDrivingLicenseApplicationID = -1, UserID = -1, RetakeTestApplicationID = -1, TestTypeID = -1;
            DateTime Date = DateTime.Now;
            float PaidFees = default;
            bool IsLocked = false;

            if (clsTestAppointmentsDataAccess.GetTestAppointmentByID(TestAppointmentID, ref TestTypeID,
                ref LocalDrivingLicenseApplicationID, ref Date, ref PaidFees, ref UserID, ref IsLocked, ref RetakeTestApplicationID))

                return new clsTestAppointmentsBusiness(TestAppointmentID, (clsTestTypesBusiness.enTestType)TestTypeID,
                  LocalDrivingLicenseApplicationID, Date, PaidFees, UserID, IsLocked, RetakeTestApplicationID);

            else

                return null;
        }

        public static clsTestAppointmentsBusiness GetLastTestAppointment(int LocalDrivingLicenseApplicationID, clsTestTypesBusiness.enTestType TestType)
        {
            int TestAppointmentID = -1, UserID = -1, RetakeTestApplicationID = -1;
            DateTime Date = DateTime.Now;
            float PaidFees = default;
            bool IsLocked = false;

            if(clsTestAppointmentsDataAccess.GetLastTestAppointment(LocalDrivingLicenseApplicationID, (int)TestType, 
                ref TestAppointmentID, ref Date, ref PaidFees, ref UserID, ref IsLocked, ref RetakeTestApplicationID))

                return new clsTestAppointmentsBusiness(TestAppointmentID, TestType,
                      LocalDrivingLicenseApplicationID, Date, PaidFees, UserID, IsLocked, RetakeTestApplicationID);
            else

                return null;
        }

        private bool _AddNewTestAppointment()
        {
            this.TestAppointmentID = clsTestAppointmentsDataAccess.AddNewTestAppointment((int)this.TestType, this.LocalDrivingLicenseApplicationID,
                this.Date, this.PaidFees, this.UserID, this.RetakeTestApplicationID);

            return (this.TestAppointmentID != -1);
        }

        private bool _UpdateTestAppointment()
        {
            return clsTestAppointmentsDataAccess.UpdateTestAppointment(this.TestAppointmentID, (int)this.TestType, this.LocalDrivingLicenseApplicationID,
                this.Date, this.PaidFees, this.UserID, this.IsLocked, this.RetakeTestApplicationID);
        }

        public static DataTable GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return clsTestAppointmentsDataAccess.GetApplicationTestAppointmentsPerTestType(LocalDrivingLicenseApplicationID, TestTypeID);
        }

        private int _GetTestID()
        {
            return clsTestAppointmentsDataAccess.GetTestID(this.TestAppointmentID);
        }

        public static byte TotalTrialsPerTestType(int LocalDrivingLicenseApplicationID, clsTestTypesBusiness.enTestType TestTypeID)
        {
            return clsTestAppointmentsDataAccess.TotalTrialsPerTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public static bool DoesActiveTestAppointmentExist(int LocalDrivingLicenseApplicationID, clsTestTypesBusiness.enTestType TestTypeID)
        {
            return clsTestAppointmentsDataAccess.DoesActiveTestAppointmentExist(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestAppointment())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateTestAppointment();
            }

            return false;
        }
    }
}