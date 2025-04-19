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
using DVLD_Presentation.Applications.Detained_License_Applications;
using DVLD_Presentation.Licenses.Detained_Licenses;

namespace DVLD_Presentation
{
    public partial class MainForm : Form
    {
        private LoginForm _LoginForm;
        public MainForm(LoginForm loginForm)
        {
            InitializeComponent();

            _LoginForm = loginForm;
        }

        private void _RefreshMainFormInfo()
        {
            lblUsername.Text = clsGlobal.CurrentUser.Username;
            lblNumberOfDrivers.Text = clsDriversBusiness.NumberOfDrivers().ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _RefreshMainFormInfo();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PeopleForm form = new PeopleForm();
            form.ShowDialog();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            UsersForm form = new UsersForm();
            form.ShowDialog();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {

        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationTypesForm form = new ApplicationTypesForm();
            form.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestTypesForm form = new TestTypesForm();
            form.ShowDialog();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserDetailsForm form = new UserDetailsForm(clsGlobal.CurrentUser.ID);
            form.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePasswordForm form = new ChangePasswordForm(clsGlobal.CurrentUser.ID);
            form.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            _LoginForm.Show();
            _LoginForm.TriggerLoadEvent();
            this.Close();
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditLocalDrivingLicenseApplicationForm form = new EditLocalDrivingLicenseApplicationForm();
            form.ShowDialog();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LocalDrivingLicenseApplicationsForm form = new LocalDrivingLicenseApplicationsForm();
            form.ShowDialog();
            _RefreshMainFormInfo();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DriversForm form = new DriversForm();
            form.ShowDialog();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (clsGlobal.CurrentUser != null)
                Application.Exit();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RenewLicenseApplicationForm form = new RenewLicenseApplicationForm();
            form.ShowDialog();
        }

        private void replacementForLostOrDamagedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReplaceLicenseApplicationForm form = new ReplaceLicenseApplicationForm();
            form.ShowDialog();
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LocalDrivingLicenseApplicationsForm form = new LocalDrivingLicenseApplicationsForm();
            form.ShowDialog();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewInternationalLicenseApplicationForm form = new NewInternationalLicenseApplicationForm();
            form.ShowDialog();
        }

        private void internationalDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InternationalLicenseApplicationsForm form = new InternationalLicenseApplicationsForm();
            form.ShowDialog();
        }

        private void detainLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void manageDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DetainedLicenseApplicationsForm form = new DetainedLicenseApplicationsForm();
            form.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DetainLicenseForm form = new DetainLicenseForm();
            form.ShowDialog();
        }

        private void releaseLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReleaseDetainedLicenseForm form = new ReleaseDetainedLicenseForm();
            form.ShowDialog();
        }

        private void releaseDetaindDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReleaseDetainedLicenseForm form = new ReleaseDetainedLicenseForm();
            form.ShowDialog();
        }
    }
}