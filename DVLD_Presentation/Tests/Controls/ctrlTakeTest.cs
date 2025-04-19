using DVLD_Business;
using DVLD_Presentation.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Presentation
{
    public partial class ctrlTakeTest : UserControl
    {
        public delegate void ClosingParentFormEvent();
        public event ClosingParentFormEvent ClosingParentForm;

        private int _TestID = -1;
        public int TestID
        {
            get
            {
                return _TestID;
            }
        }
        private clsTestsBusiness _Test;
        private clsLocalDrivingLicenseApplicationsBusiness _LDLApp;
        private clsTestAppointmentsBusiness _TestAppointment;

        private clsTestTypesBusiness.enTestType _TestType;
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
        public ctrlTakeTest()
        {
            InitializeComponent();
        }

        public void LoadTestData(int TestAppointmentID)
        {
            _TestAppointment = clsTestAppointmentsBusiness.Find(TestAppointmentID);

            if(_TestAppointment == null)
            {
                MessageBox.Show("Error: No Appointment ID = " + TestAppointmentID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _TestID = _TestAppointment.TestID;
            _LDLApp = clsLocalDrivingLicenseApplicationsBusiness.Find(_TestAppointment.LocalDrivingLicenseApplicationID);
            lblLDLAppID.Text = _LDLApp.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = _LDLApp.LicenseClassesInfo.Name;
            lblName.Text = _LDLApp.PersonInfo.FullName;
            lblTrials.Text = clsTestAppointmentsBusiness.TotalTrialsPerTestType(_LDLApp.LocalDrivingLicenseApplicationID, _TestType).ToString();
            lblDate.Text = _TestAppointment.Date.ToShortDateString();
            lblFees.Text = _TestAppointment.PaidFees.ToString();

            if(_TestID != -1)
            {
                _Test = clsTestsBusiness.Find(_TestID);

                if (_Test.TestResult)
                    rbPassed.Checked = true;
                else
                    rbFailed.Checked = true;

                lblTestID.Text = _Test.TestID.ToString();
                lblNotes.Text = _Test.Notes;
                lblUserMessage.Visible = true;
                rbPassed.Enabled = false;
                rbFailed.Enabled = false;
            }
            else
            {
                _Test = new clsTestsBusiness();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClosingParentForm?.Invoke();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _Test.TestAppointmentID = _TestAppointment.TestAppointmentID;
            _Test.TestResult = rbPassed.Checked;
            _Test.Notes = lblNotes.Text;
            _Test.UserID = clsGlobal.CurrentUser.ID;

            if (_Test.Save())
            {
                lblTestID.Text = _Test.TestID.ToString();

                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClosingParentForm?.Invoke();
            }
            else
            {
                MessageBox.Show("Error: Data isn't Saved Successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
