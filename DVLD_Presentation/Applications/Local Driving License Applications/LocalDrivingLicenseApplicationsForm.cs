using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Business;
using DVLD_Presentation.Licenses;

namespace DVLD_Presentation
{
    public partial class LocalDrivingLicenseApplicationsForm : Form
    {
        private DataTable _dtAllLocalDrivingLicenseApplications = clsLocalDrivingLicenseApplicationsBusiness.GetAllLDLApplications();
        public LocalDrivingLicenseApplicationsForm()
        {
            InitializeComponent();
        }

        private void _RefreshLDLApplicationsList()
        {
            _dtAllLocalDrivingLicenseApplications = clsLocalDrivingLicenseApplicationsBusiness.GetAllLDLApplications();
            dgvLDLApplications.DataSource = _dtAllLocalDrivingLicenseApplications;
            lblCount.Text = dgvLDLApplications.Rows.Count.ToString();
        }

        private void LocalDrivingLicenseApplicationsForm_Load(object sender, EventArgs e)
        {
            _RefreshLDLApplicationsList();

            cbFilterBy.SelectedIndex = 0;

            if(dgvLDLApplications.Rows.Count > 0)
            {
                dgvLDLApplications.Columns[0].HeaderText = "L.D.L.AppID";

                dgvLDLApplications.Columns[1].HeaderText = "Driving Class";

                dgvLDLApplications.Columns[2].HeaderText = "National No";

                dgvLDLApplications.Columns[3].HeaderText = "Full Name";

                dgvLDLApplications.Columns[4].HeaderText = "Application Date";

                dgvLDLApplications.Columns[5].HeaderText = "Passed Tests";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            EditLocalDrivingLicenseApplicationForm form = new EditLocalDrivingLicenseApplicationForm();
            form.ShowDialog();
            _RefreshLDLApplicationsList();
        }

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EditLocalDrivingLicenseApplicationForm form = new EditLocalDrivingLicenseApplicationForm((int)dgvLDLApplications.CurrentRow.Cells[0].Value);
            form.ShowDialog();
            _RefreshLDLApplicationsList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure that you want to delete this Application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                clsLocalDrivingLicenseApplicationsBusiness LDLApp =
                    clsLocalDrivingLicenseApplicationsBusiness.Find((int)dgvLDLApplications.CurrentRow.Cells[0].Value);

                if (LDLApp.DeleteLDLApplication())
                {
                    MessageBox.Show("Application Deleted Successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshLDLApplicationsList();
                }
                else
                {
                    MessageBox.Show("Couldn't delete the Application, other data depends on it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            clsLocalDrivingLicenseApplicationsBusiness LDLApp = 
                clsLocalDrivingLicenseApplicationsBusiness.Find((int)dgvLDLApplications.CurrentRow.Cells[0].Value);

            int PassedTests = (int)dgvLDLApplications.CurrentRow.Cells[5].Value;

            bool LicenseExists = clsLicensesBusiness.IsActiveLicenseExist(LDLApp.PersonID, LDLApp.LicenseClassID);

            showLicenseToolStripMenuItem.Enabled = LicenseExists;
            editToolStripMenuItem1.Enabled = !LicenseExists && (LDLApp.Status == clsApplicationsBusiness.enStatus.New);
            scheduleTestToolStripMenuItem.Enabled = !LicenseExists && PassedTests != 3 && LDLApp.Status == clsApplicationsBusiness.enStatus.New;

            cancelToolStripMenuItem.Enabled = (LDLApp.Status == clsApplicationsBusiness.enStatus.New);
            deleteToolStripMenuItem.Enabled = (LDLApp.Status == clsApplicationsBusiness.enStatus.New);

            issueDrivingLicenseToolStripMenuItem.Enabled = !LicenseExists && (PassedTests == 3);

            bool IsVisionTestPassed = LDLApp.IsTestTypePassed(clsTestTypesBusiness.enTestType.Vision),
                IsWrittenTestPassed = LDLApp.IsTestTypePassed(clsTestTypesBusiness.enTestType.Written),
                IsStreetTestPassed = LDLApp.IsTestTypePassed(clsTestTypesBusiness.enTestType.Street);

            if (scheduleTestToolStripMenuItem.Enabled)
            {
                scheduleVisionTestToolStripMenuItem.Enabled = !IsVisionTestPassed;
                scheduleWrittenTestToolStripMenuItem.Enabled = IsVisionTestPassed && !IsWrittenTestPassed;
                scheduleStreetTestToolStripMenuItem.Enabled = IsVisionTestPassed && IsWrittenTestPassed && !IsStreetTestPassed;
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");

            _RefreshLDLApplicationsList();

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "L.D.L.AppID":
                    FilterColumn = "LocalDrivingLicenseApplicationID";
                    break;

                case "National No":
                    FilterColumn = "NationalNo";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Status":
                    FilterColumn = "Status";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            if(txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = "";
                lblCount.Text = dgvLDLApplications.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "LocalDrivingLicenseApplicationID")
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtAllLocalDrivingLicenseApplications.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblCount.Text = dgvLDLApplications.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "L.D.L.AppID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LocalDrivingLicenseApplicationDetailsForm form = new LocalDrivingLicenseApplicationDetailsForm((int)dgvLDLApplications.CurrentRow.Cells[0].Value);
            form.ShowDialog();
            _RefreshLDLApplicationsList();
        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure that you want to cancel this Application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                clsLocalDrivingLicenseApplicationsBusiness LDLApp =
                    clsLocalDrivingLicenseApplicationsBusiness.Find((int)dgvLDLApplications.CurrentRow.Cells[0].Value);

                if (LDLApp.Cancel())
                {
                    MessageBox.Show("Application Cancelled Successfully", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshLDLApplicationsList();
                }
                else
                {
                    MessageBox.Show("An error occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void _OpenTestAppointmentsPerTestType(clsTestTypesBusiness.enTestType TestType)
        {
            TestAppointmentsForm form = new TestAppointmentsForm((int)dgvLDLApplications.CurrentRow.Cells[0].Value, TestType);
            form.ShowDialog();
            _RefreshLDLApplicationsList();
        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _OpenTestAppointmentsPerTestType(clsTestTypesBusiness.enTestType.Vision);
        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _OpenTestAppointmentsPerTestType(clsTestTypesBusiness.enTestType.Written);
        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _OpenTestAppointmentsPerTestType(clsTestTypesBusiness.enTestType.Street);
        }

        private void issueDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IssueDrivingLicenseForFirstTimeForm form = new IssueDrivingLicenseForFirstTimeForm((int)dgvLDLApplications.CurrentRow.Cells[0].Value);
            form.ShowDialog();
            _RefreshLDLApplicationsList();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplicationsBusiness LDLApp = clsLocalDrivingLicenseApplicationsBusiness.Find((int)dgvLDLApplications.CurrentRow.Cells[0].Value);

            int LicenseID = clsLicensesBusiness.GetActiveLicenseIDByPersonID(LDLApp.PersonID, LDLApp.LicenseClassID);

            ShowLicenseForm form = new ShowLicenseForm(LicenseID);
            form.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplicationsBusiness LDLApp = clsLocalDrivingLicenseApplicationsBusiness.Find((int)dgvLDLApplications.CurrentRow.Cells[0].Value);
            ShowPersonLicensesHistoryForm form = new ShowPersonLicensesHistoryForm(LDLApp.PersonID);
            form.ShowDialog();
        }
    }
}