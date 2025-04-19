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

namespace DVLD_Presentation
{
    public partial class ReleaseDetainedLicenseForm : Form
    {
        private clsDetainedLicensesBusiness _DetainedLicense;
        private clsApplicationsBusiness _ReleaseApplication;
        public ReleaseDetainedLicenseForm()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ctrlLicenseDetailsWithFilter1_OnLicenseSelected(int obj)
        {

        }

        private bool _ReleaseDetainedLicense()
        {
            _ReleaseApplication = new clsApplicationsBusiness();

            _ReleaseApplication.Date = DateTime.Now;
            _ReleaseApplication.LastStatusDate = DateTime.Now;
            _ReleaseApplication.PaidFees = clsApplicationTypesBusiness.Find((int)clsApplicationsBusiness.enAppliactionType.ReleaseDetainedDrivingLicsense).Fees;
            _ReleaseApplication.PersonID = ctrlLicenseDetailsWithFilter1.License.DriverInfo.PersonID;
            _ReleaseApplication.Status = clsApplicationsBusiness.enStatus.Completed;
            _ReleaseApplication.TypeID = (int)clsApplicationsBusiness.enAppliactionType.ReleaseDetainedDrivingLicsense;
            _ReleaseApplication.UserID = clsGlobal.CurrentUser.ID;

            if (!_ReleaseApplication.BaseSave())
            {
                MessageBox.Show("Couldn't create a release license application", "Error", MessageBoxButtons.OK);
                return false;
            }

            _DetainedLicense = clsDetainedLicensesBusiness.FindByLicenseID(ctrlLicenseDetailsWithFilter1.LicenseID);

            if(_DetainedLicense.Release(clsGlobal.CurrentUser.ID, _ReleaseApplication.ApplicationID))
            {
                MessageBox.Show("License released successfully", "Successful Process", MessageBoxButtons.OK);
                return true;
            }
            else
            {
                MessageBox.Show("Couldn't release the license", "Error", MessageBoxButtons.OK);
                return false;
            }
        }

        private void _LoadReleasedLicenseInfo()
        {
            if (!_ReleaseDetainedLicense())
                return;

            lblApplicationID.Text = _ReleaseApplication.ApplicationID.ToString();
            lblDetainID.Text = _DetainedLicense.DetainID.ToString();
            lblApplicationFees.Text = clsApplicationTypesBusiness.Find((int)clsApplicationsBusiness.enAppliactionType.ReleaseDetainedDrivingLicsense).Fees.ToString();
            lblDetainDate.Text = _DetainedLicense.DetainDate.ToShortDateString();
            lblFineFees.Text = _DetainedLicense.FineFees.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblFineFees.Text) + Convert.ToSingle(lblApplicationFees.Text)).ToString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.Username;

            btnRelease.Enabled = false;
            ctrlLicenseDetailsWithFilter1.FilterEnabled = false;
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if(ctrlLicenseDetailsWithFilter1.LicenseID == -1)
            {
                MessageBox.Show("Select a license to release", "No License Selected", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            if (!ctrlLicenseDetailsWithFilter1.License.IsDetained)
            {
                MessageBox.Show("This license isn't detained to be release", "License isn't Detained", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            _LoadReleasedLicenseInfo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ctrlLicenseDetailsWithFilter1.LicenseID != -1)
            {
                ShowPersonLicensesHistoryForm form = new ShowPersonLicensesHistoryForm(ctrlLicenseDetailsWithFilter1.License.DriverInfo.PersonID);
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("No License selected", "Select a License", MessageBoxButtons.OK);
            }
        }
    }
}