using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsTestsBusiness
    {
        private enum enMode { AddNew, Update };
        private enMode _Mode = enMode.AddNew;

        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public clsTestAppointmentsBusiness TestAppointmentInfo;
        public bool TestResult { get; set; }
        public string Notes { get; set; }
        public int UserID { get; set; }
        public clsUsersBusiness UserInfo;

        public clsTestsBusiness()
        {
            this.TestID = -1;
            this.TestAppointmentID = -1;
            this.TestAppointmentInfo = new clsTestAppointmentsBusiness();
            this.TestResult = false;
            this.Notes = "";
            this.UserID = -1;
            this.UserInfo = new clsUsersBusiness();

            _Mode = enMode.AddNew;
        }

        private clsTestsBusiness(int TestID, int TestAppointmentID, bool TestResult, string Notes, int UserID)
        {
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            this.TestAppointmentInfo = clsTestAppointmentsBusiness.Find(this.TestAppointmentID);
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.UserID = UserID;
            this.UserInfo = clsUsersBusiness.Find(this.UserID);

            _Mode = enMode.Update;
        }

        public static clsTestsBusiness Find(int TestID)
        {
            int TestAppointmentID = -1, UserID = -1;
            bool TestResult = false;
            string Notes = "";

            if (clsTestsDataAccess.GetTestByID(TestID, ref TestAppointmentID, ref TestResult, ref Notes, ref UserID))
            {
                return new clsTestsBusiness(TestID, TestAppointmentID, TestResult, Notes, UserID);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewTest()
        {
            this.TestID = clsTestsDataAccess.AddNewTest(this.TestAppointmentID, this.TestResult, this.Notes, this.UserID);

            return (this.TestID != -1);
        }

        private bool _UpdateTest()
        {
            return clsTestsDataAccess.UpdateTest(this.TestID, this.TestAppointmentID, this.TestResult, this.Notes, this.UserID);
        }

        public static byte GetPassedTestsCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTestsDataAccess.GetPassedTestsCount(LocalDrivingLicenseApplicationID);
        }

        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTest())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateTest();
            }

            return false;
        }
    }
}