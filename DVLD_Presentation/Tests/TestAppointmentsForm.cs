using DVLD_Business;
using DVLD_Presentation.Properties;
using DVLD_Presentation.Tests;
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
    public partial class TestAppointmentsForm : Form
    {
        private DataTable _dtTestAppointments;
        private int _LDLAppID = -1;
        private clsTestTypesBusiness.enTestType _TestType;

        public TestAppointmentsForm(int LocalDrivingLicenseApplicationID, clsTestTypesBusiness.enTestType TestType)
        {
            InitializeComponent();

            _TestType = TestType;
            _LDLAppID = LocalDrivingLicenseApplicationID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _RefreshTestAppointmentsList()
        {
            _dtTestAppointments = clsTestAppointmentsBusiness.GetApplicationTestAppointmentsPerTestType(_LDLAppID, (int)_TestType);
            dgvTestAppointments.DataSource = _dtTestAppointments;
            lblCount.Text = dgvTestAppointments.Rows.Count.ToString();
        }

        private void _LoadTestTypeData()
        {
            switch (_TestType)
            {

                case clsTestTypesBusiness.enTestType.Vision:
                    lblTitle.Text = "Vision Test Appointments";
                    this.Text = lblTitle.Text;
                    pbTestImage.Image = Resources.Vision_512;
                    break;

                case clsTestTypesBusiness.enTestType.Written:
                    lblTitle.Text = "Written Test Appointments";
                    this.Text = lblTitle.Text;
                    pbTestImage.Image = Resources.Written_Test_512;
                    break;

                case clsTestTypesBusiness.enTestType.Street:
                    lblTitle.Text = "Street Test Appointments";
                    this.Text = lblTitle.Text;
                    pbTestImage.Image = Resources.driving_test_512;
                    break;
            }
        }

        private void TestAppointmentsForm_Load(object sender, EventArgs e)
        {
            _RefreshTestAppointmentsList();

            _LoadTestTypeData();

            ctrlLocalDrivingLicenseApplicationDetails1.LoadLocalDrivingLicenseApplicationInfo(_LDLAppID);

            if (dgvTestAppointments.Rows.Count > 0)
            {
                dgvTestAppointments.Columns[0].HeaderText = "Appointment ID";

                dgvTestAppointments.Columns[1].HeaderText = "Appointment Date";

                dgvTestAppointments.Columns[2].HeaderText = "Paid Fees";

                dgvTestAppointments.Columns[3].HeaderText = "Is Locked";
            }
        }

        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {
            if(clsTestAppointmentsBusiness.DoesActiveTestAppointmentExist(_LDLAppID, _TestType))
            {
                MessageBox.Show("Person Already has an active appointment for this test, You cannot add new appointment", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(clsLocalDrivingLicenseApplicationsBusiness.IsTestTypePassed(_LDLAppID, _TestType))
            {
                MessageBox.Show("This person already passed this test before, you can only retake failed test", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            EditTestAppointmentForm form = new EditTestAppointmentForm(_LDLAppID, _TestType);
            form.ShowDialog();
            _RefreshTestAppointmentsList();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditTestAppointmentForm form = new EditTestAppointmentForm(_LDLAppID, _TestType, (int)dgvTestAppointments.CurrentRow.Cells[0].Value);
            form.ShowDialog();
            _RefreshTestAppointmentsList();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TakeTestForm form = new TakeTestForm((int)dgvTestAppointments.CurrentRow.Cells[0].Value, _TestType);
            form.ShowDialog();
            _RefreshTestAppointmentsList();
        }
    }
}
