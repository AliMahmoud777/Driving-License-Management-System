using DVLD_Business;
using DVLD_Presentation.Licenses;
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
    public partial class NewInternationalLicenseApplicationForm : Form
    {
        private clsApplicationsBusiness _App;
        private clsInternationalLicensesBusiness _InternationalLicense;

        public NewInternationalLicenseApplicationForm()
        {
            InitializeComponent();
            ctrlLicenseDetailsWithFilter1.OnLicenseSelected += ctrlLicenseDetailsWithFilter1_OnLicenseSelected;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool _CreateInternationalLicense()
        {
            _App = new clsApplicationsBusiness();

            _App.Date = DateTime.Now;
            _App.LastStatusDate = DateTime.Now;
            _App.PaidFees = clsApplicationTypesBusiness.Find((int)clsApplicationsBusiness.enAppliactionType.NewInternationalLicense).Fees;
            _App.PersonID = ctrlLicenseDetailsWithFilter1.License.ApplicationInfo.PersonID;
            _App.Status = clsApplicationsBusiness.enStatus.Completed;
            _App.TypeID = (int)clsApplicationsBusiness.enAppliactionType.NewInternationalLicense;
            _App.UserID = clsGlobal.CurrentUser.ID;

            if (!_App.BaseSave())
            {
                MessageBox.Show("Couldn't create an international license application", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            _InternationalLicense = new clsInternationalLicensesBusiness();
            _InternationalLicense.ApplicationID = _App.ApplicationID;
            _InternationalLicense.DriverID = ctrlLicenseDetailsWithFilter1.License.DriverID;
            _InternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);
            _InternationalLicense.IsActive = true;
            _InternationalLicense.IssueDate = DateTime.Now;
            _InternationalLicense.IssuedUsingLocalLicenseID = ctrlLicenseDetailsWithFilter1.LicenseID;
            _InternationalLicense.UserID = clsGlobal.CurrentUser.ID;

            if (_InternationalLicense.Save())
            {
                MessageBox.Show("License saved successfully", "Successful Process", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else
            {
                MessageBox.Show("Couldn't save the license", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void _LoadInternationalLicenseData()
        {
            if (!_CreateInternationalLicense())
                return;

            lblApplicationID.Text = _App.ApplicationID.ToString();
            lblApplicationDate.Text = _App.Date.ToShortDateString();
            lblIssueDate.Text = _InternationalLicense.IssueDate.ToShortDateString();
            lblApplicationFees.Text = _App.PaidFees.ToString();
            lblInternationalLicenseID.Text = _InternationalLicense.InternationalLicenseID.ToString();
            lblLocalLicenseID.Text = _InternationalLicense.IssuedUsingLocalLicenseID.ToString();
            lblExpirationDate.Text = _InternationalLicense.ExpirationDate.ToShortDateString();
            lblCreatedBy.Text = _InternationalLicense.UserInfo.Username;

            btnIssue.Enabled = false;
            ctrlLicenseDetailsWithFilter1.FilterEnabled = false;
            llShowNewLicense.Enabled = true;
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if(ctrlLicenseDetailsWithFilter1.LicenseID == -1)
            {
                MessageBox.Show("Select a license to renew", "No License Selected", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            _LoadInternationalLicenseData();
        }

        private void ctrlLicenseDetailsWithFilter1_OnLicenseSelected(int obj)
        {
            if (ctrlLicenseDetailsWithFilter1.License.LicenseClassID != 3)
            {
                MessageBox.Show("Selected License should be of Class 3, select another one", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                return;
            }

            if(clsInternationalLicensesBusiness.GetActiveInternationalLicenseIDByDriverID(ctrlLicenseDetailsWithFilter1.License.DriverID) != -1)
            {
                MessageBox.Show("This Person already has an active international license", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssue.Enabled = false;
                return;
            }

            if (!ctrlLicenseDetailsWithFilter1.License.IsActive)
            {
                MessageBox.Show("This License isn't actve", "Inactive License", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                btnIssue.Enabled = false;
                return;
            }

            if (ctrlLicenseDetailsWithFilter1.License.IsExpired())
            {
                MessageBox.Show("The selected license mustn't be expired", "Expired", MessageBoxButtons.OK);
                btnIssue.Enabled = false;
                return;
            }

            btnIssue.Enabled = true;
        }

        private void llShowNewLicense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            InternationalLicenseDetailsForm form = new InternationalLicenseDetailsForm(_InternationalLicense.InternationalLicenseID);
            form.ShowDialog();
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