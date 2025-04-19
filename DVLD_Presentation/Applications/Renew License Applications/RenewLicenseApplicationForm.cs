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
using DVLD_Presentation.Licenses;

namespace DVLD_Presentation
{
    public partial class RenewLicenseApplicationForm : Form
    {
        private clsApplicationsBusiness _App;
        private clsLicensesBusiness _RenewedLicense;
        public RenewLicenseApplicationForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool _CreateRenewedLicense()
        {
            _App = new clsApplicationsBusiness();

            _App.Date = DateTime.Now;
            _App.LastStatusDate = DateTime.Now;
            _App.PaidFees = clsApplicationTypesBusiness.Find((int)clsApplicationsBusiness.enAppliactionType.RenewDrivingLicense).Fees;
            _App.PersonID = ctrlLicenseDetailsWithFilter1.License.ApplicationInfo.PersonID;
            _App.Status = clsApplicationsBusiness.enStatus.Completed;
            _App.TypeID = (int)clsApplicationsBusiness.enAppliactionType.RenewDrivingLicense;
            _App.UserID = clsGlobal.CurrentUser.ID;

            if (!_App.BaseSave())
            {
                MessageBox.Show("Couldn't create a renewed license application", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            _RenewedLicense = new clsLicensesBusiness();
            _RenewedLicense.ApplicationID = _App.ApplicationID;
            _RenewedLicense.DriverID = ctrlLicenseDetailsWithFilter1.License.DriverID;
            _RenewedLicense.ExpirationDate = DateTime.Now.AddYears(ctrlLicenseDetailsWithFilter1.License.LicenseClassInfo.DefaultValidityPeriod);
            _RenewedLicense.IsActive = true;
            _RenewedLicense.IssueDate = DateTime.Now;
            _RenewedLicense.IssueReason = clsLicensesBusiness.enIssueReason.Renew;
            _RenewedLicense.LicenseClassID = ctrlLicenseDetailsWithFilter1.License.LicenseClassID;
            _RenewedLicense.Notes = txtNotes.Text;
            _RenewedLicense.PaidFees = ctrlLicenseDetailsWithFilter1.License.LicenseClassInfo.ClassFees;
            _RenewedLicense.UserID = clsGlobal.CurrentUser.ID;

            if (!ctrlLicenseDetailsWithFilter1.License.Deactivate())
            {
                MessageBox.Show("Couldn't deactivate the selected license", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (_RenewedLicense.Save())
            {
                MessageBox.Show("License renewed successfully", "Successful Process", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else
            {
                MessageBox.Show("Couldn't renew the license", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        } 

        private void _LoadRenewedLicenseData()
        {
            if (!_CreateRenewedLicense())
                return;

            lblApplicationID.Text = _App.ApplicationID.ToString();
            lblApplicationDate.Text = _App.Date.ToShortDateString();
            lblIssueDate.Text = _RenewedLicense.IssueDate.ToShortDateString();
            lblApplicationFees.Text = _App.PaidFees.ToString();
            lblLicenseFees.Text = _RenewedLicense.PaidFees.ToString();
            lblRenewedLicenseID.Text = _RenewedLicense.ID.ToString();
            lblOldLicenseID.Text = ctrlLicenseDetailsWithFilter1.LicenseID.ToString();
            lblExpirationDate.Text = _RenewedLicense.ExpirationDate.ToShortDateString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.Username;
            lblTotalFees.Text = (_RenewedLicense.PaidFees + _App.PaidFees).ToString();

            btnRenew.Enabled = false;
            llShowNewLicense.Enabled = true;
            ctrlLicenseDetailsWithFilter1.FilterEnabled = false;
            txtNotes.Enabled = false;
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            if (ctrlLicenseDetailsWithFilter1.LicenseID == -1)
            {
                MessageBox.Show("Select a license to renew", "No License Selected", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            if (!ctrlLicenseDetailsWithFilter1.License.IsActive)
            {
                MessageBox.Show("This License isn't actve to be renewed", "Inactive License", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            if (!ctrlLicenseDetailsWithFilter1.License.IsExpired())
            {
                MessageBox.Show("The selected license must be expired", "Not Expired", MessageBoxButtons.OK);
                return;
            }

            _LoadRenewedLicenseData();
        }

        private void llShowNewLicense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowLicenseForm form = new ShowLicenseForm(_RenewedLicense.ID);
            form.ShowDialog();
        }

        private void llShowLicensesHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(ctrlLicenseDetailsWithFilter1.LicenseID != -1)
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