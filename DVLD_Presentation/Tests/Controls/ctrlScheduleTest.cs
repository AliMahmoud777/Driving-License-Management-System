using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Presentation.Properties;

namespace DVLD_Presentation
{
    public partial class ctrlScheduleTest : UserControl
    {
        public delegate void ClosingParentFormEvent();
        public event ClosingParentFormEvent ClosingParentForm;
        private enum enMode { AddNew, Update };
        private enMode _Mode = enMode.AddNew;

        private enum enTestMode { FirstTime, RetakeTest };
        private enTestMode _TestMode = enTestMode.FirstTime;

        private int _TestAppointmentID = -1;
        private clsTestAppointmentsBusiness _TestAppointment;
        private clsLocalDrivingLicenseApplicationsBusiness _LDLApp;
        private clsTestTypesBusiness.enTestType _TestType = clsTestTypesBusiness.enTestType.Vision;

        public clsTestTypesBusiness.enTestType TestType
        {
            get
            {
                return _TestType;
            }

            set
            {
                _TestType = value;

                switch (_TestType)
                {
                    case clsTestTypesBusiness.enTestType.Vision:
                        gbTestType.Text = "Vision Test";
                        pbTestImage.Image = Resources.Vision_512;
                        break;

                    case clsTestTypesBusiness.enTestType.Written:
                        gbTestType.Text = "Written Test";
                        pbTestImage.Image = Resources.Written_Test_512;
                        break;

                    case clsTestTypesBusiness.enTestType.Street:
                        gbTestType.Text = "Street Test";
                        pbTestImage.Image = Resources.driving_test_512;
                        break;
                }
            }
        }

        public ctrlScheduleTest()
        {
            InitializeComponent();
        }

        private bool _FillTestAppointmentData()
        {
            _TestAppointment = clsTestAppointmentsBusiness.Find(_TestAppointmentID);

            if(_TestAppointment == null)
            {
                MessageBox.Show("Error: No Appointment with ID = " + _TestAppointmentID.ToString(),
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return false;
            }

            if(_TestAppointment.RetakeTestApplicationID == -1)
            {
                gbRetakeTestInfo.Enabled = false;
                lblRetakeTestAppFees.Text = "??";
                lblTotalFees.Text = "??";
                lblRetakeTestAppID.Text = "??";
                lblTitle.Text = "Schedule Test";
            }
            else
            {
                gbRetakeTestInfo.Enabled = true;
                lblRetakeTestAppFees.Text = _TestAppointment.RetakeTestApplicationInfo.PaidFees.ToString();
                lblTotalFees.Text = (Convert.ToSingle(lblFees.Text) + Convert.ToSingle(lblRetakeTestAppFees.Text)).ToString();
                lblRetakeTestAppID.Text = _TestAppointment.RetakeTestApplicationID.ToString();
                lblTitle.Text = "Schedule Retake Test";
            }

            if (_TestAppointment.IsLocked)
                dtpTestAppointmentDate.MinDate = _TestAppointment.Date;

            if (_TestAppointment.Date > DateTime.Now)
                dtpTestAppointmentDate.Value = _TestAppointment.Date;

            return true;
        }

        private bool _HandleActiveTestAppointment()
        {
            if(_Mode == enMode.AddNew && clsTestAppointmentsBusiness.DoesActiveTestAppointmentExist(_LDLApp.LocalDrivingLicenseApplicationID, _TestType))
            {
                lblUserMessage.Visible = true;
                lblUserMessage.Text = "Person already has an active appointment for this test";
                btnSave.Enabled = false;
                dtpTestAppointmentDate.Enabled = false;
                return false;
            }
            return true;
        }

        private bool _HandleLockedTestAppointment()
        {
            if(_Mode == enMode.Update && _TestAppointment.IsLocked)
            {
                lblUserMessage.Visible = true;
                lblUserMessage.Text = "Person already took this test, Appointment is locked";
                btnSave.Enabled = false;
                dtpTestAppointmentDate.Enabled = false;
                return false;
            }
            return true;
        }

        public void LoadTestAppointmentInfo(int LocalDrivingLicenseApplicationID, int TestAppointmentID = -1)
        {
            if (TestAppointmentID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;

            _TestAppointmentID = TestAppointmentID;
            _LDLApp = clsLocalDrivingLicenseApplicationsBusiness.Find(LocalDrivingLicenseApplicationID);

            if(_LDLApp == null)
            {
                MessageBox.Show("Error: No Local Driving License Application with ID = " + LocalDrivingLicenseApplicationID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;
            }

            lblLDLAppID.Text = _LDLApp.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = _LDLApp.LicenseClassesInfo.Name;
            lblName.Text = _LDLApp.PersonInfo.FullName;
            lblTrials.Text = clsTestAppointmentsBusiness.TotalTrialsPerTestType(_LDLApp.LocalDrivingLicenseApplicationID, _TestType).ToString();

            dtpTestAppointmentDate.MinDate = DateTime.Now;

            lblFees.Text = clsTestTypesBusiness.Find(_TestType).Fees.ToString();

            if (clsTestAppointmentsBusiness.TotalTrialsPerTestType(_LDLApp.LocalDrivingLicenseApplicationID, _TestType) > 0)
                _TestMode = enTestMode.RetakeTest;
            else
                _TestMode = enTestMode.FirstTime;

            if(_TestMode == enTestMode.RetakeTest)
            {
                lblTitle.Text = "Schedule Retake Test";
                gbRetakeTestInfo.Enabled = true;
                lblRetakeTestAppFees.Text = clsApplicationTypesBusiness.Find((int)clsApplicationsBusiness.enAppliactionType.RetakeTest).Fees.ToString();
                lblTotalFees.Text = lblTotalFees.Text = (Convert.ToSingle(lblFees.Text) + Convert.ToSingle(lblRetakeTestAppFees.Text)).ToString();
                dtpTestAppointmentDate.MinDate = clsTestAppointmentsBusiness.GetLastTestAppointment(LocalDrivingLicenseApplicationID, _TestType).Date;
            }
            else
            {
                lblTitle.Text = "Schedule Test";
                gbRetakeTestInfo.Enabled = false;
            }

            if (_Mode == enMode.Update)
            {
                if(!_FillTestAppointmentData())
                    return;
            }
            else
            {
                _TestAppointment = new clsTestAppointmentsBusiness();
            }

            if (_HandleActiveTestAppointment())
            {
                _HandleLockedTestAppointment();
            }
        }

        private bool _HandleRetakeTestApplication()
        {
            if(_Mode == enMode.AddNew && _TestMode == enTestMode.RetakeTest)
            {
                clsApplicationsBusiness RetakeTestApplication = new clsApplicationsBusiness();

                RetakeTestApplication.Date = DateTime.Now;
                RetakeTestApplication.LastStatusDate = DateTime.Now;
                RetakeTestApplication.PaidFees = clsApplicationTypesBusiness.Find((int)clsApplicationsBusiness.enAppliactionType.RetakeTest).Fees;
                RetakeTestApplication.PersonID = _LDLApp.PersonID;
                RetakeTestApplication.Status = clsApplicationsBusiness.enStatus.Completed;
                RetakeTestApplication.TypeID = (int)clsApplicationsBusiness.enAppliactionType.RetakeTest;
                RetakeTestApplication.UserID = clsGlobal.CurrentUser.ID;

                if (!RetakeTestApplication.BaseSave())
                {
                    MessageBox.Show("Failed to Create Application", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                _TestAppointment.RetakeTestApplicationID = RetakeTestApplication.ApplicationID;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_HandleRetakeTestApplication())
                return;

            _TestAppointment.Date = dtpTestAppointmentDate.Value;
            _TestAppointment.IsLocked = false;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LDLApp.LocalDrivingLicenseApplicationID;
            _TestAppointment.PaidFees = Convert.ToSingle(lblFees.Text);
            _TestAppointment.TestType = _TestType;
            _TestAppointment.UserID = clsGlobal.CurrentUser.ID;

            if (_TestAppointment.Save())
            {
                _Mode = enMode.Update;
                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClosingParentForm?.Invoke();
            }
            else
            {
                MessageBox.Show("Error: Data isn't Saved Successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClosingParentForm?.Invoke();
        }
    }
}
