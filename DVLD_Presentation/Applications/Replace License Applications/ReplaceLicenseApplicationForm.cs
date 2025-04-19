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
    public partial class ReplaceLicenseApplicationForm : Form
    {
        private clsLicensesBusiness _ReplacedLicense;
        private clsApplicationsBusiness _App;
        private clsApplicationsBusiness.enAppliactionType _AppliactionType
        {
            get
            {
                if (rbDamaged.Checked)
                    return clsApplicationsBusiness.enAppliactionType.ReplaceDamagedDrivingLicense;
                else
                    return clsApplicationsBusiness.enAppliactionType.ReplaceLostDrivingLicense;
            }
        }

        public ReplaceLicenseApplicationForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool _CreateReplacedLicense()
        {
            _App = new clsApplicationsBusiness();

            _App.Date = DateTime.Now;
            _App.LastStatusDate = DateTime.Now;
            _App.PaidFees = clsApplicationTypesBusiness.Find((int)_AppliactionType).Fees;
            _App.PersonID = ctrlLicenseDetailsWithFilter1.License.ApplicationInfo.PersonID;
            _App.Status = clsApplicationsBusiness.enStatus.Completed;
            _App.TypeID = (int)_AppliactionType;
            _App.UserID = clsGlobal.CurrentUser.ID;

            if (!_App.BaseSave())
            {
                MessageBox.Show("Couldn't create a replaced license application", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            _ReplacedLicense = new clsLicensesBusiness();
            _ReplacedLicense.ApplicationID = _App.ApplicationID;
            _ReplacedLicense.DriverID = ctrlLicenseDetailsWithFilter1.License.DriverID;
            _ReplacedLicense.ExpirationDate = DateTime.Now.AddYears(ctrlLicenseDetailsWithFilter1.License.LicenseClassInfo.DefaultValidityPeriod);
            _ReplacedLicense.IsActive = true;
            _ReplacedLicense.IssueDate = DateTime.Now;

            if(_AppliactionType == clsApplicationsBusiness.enAppliactionType.ReplaceDamagedDrivingLicense)
               _ReplacedLicense.IssueReason = clsLicensesBusiness.enIssueReason.DamagedReplacement;
            else
                _ReplacedLicense.IssueReason = clsLicensesBusiness.enIssueReason.LostReplacement;

            _ReplacedLicense.LicenseClassID = ctrlLicenseDetailsWithFilter1.License.LicenseClassID;
            _ReplacedLicense.Notes = txtNotes.Text;
            _ReplacedLicense.PaidFees = ctrlLicenseDetailsWithFilter1.License.LicenseClassInfo.ClassFees;
            _ReplacedLicense.UserID = clsGlobal.CurrentUser.ID;

            if (!ctrlLicenseDetailsWithFilter1.License.Deactivate())
            {
                MessageBox.Show("Couldn't deactivate the selected license", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (_ReplacedLicense.Save())
            {
                MessageBox.Show("License replaced successfully", "Successful Process", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else
            {
                MessageBox.Show("Couldn't replace the license", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void _LoadReplacedLicenseData()
        {
            if (!_CreateReplacedLicense())
                return;

            lblApplicationID.Text = _App.ApplicationID.ToString();
            lblApplicationDate.Text = _App.Date.ToShortDateString();
            lblIssueDate.Text = _ReplacedLicense.IssueDate.ToShortDateString();
            lblApplicationFees.Text = _App.PaidFees.ToString();
            lblLicenseFees.Text = _ReplacedLicense.PaidFees.ToString();
            lblReplacedLicenseID.Text = _ReplacedLicense.ID.ToString();
            lblOldLicenseID.Text = ctrlLicenseDetailsWithFilter1.LicenseID.ToString();
            lblExpirationDate.Text = _ReplacedLicense.ExpirationDate.ToShortDateString();
            lblCreatedBy.Text = _ReplacedLicense.UserInfo.Username;
            lblTotalFees.Text = (_ReplacedLicense.PaidFees + _App.PaidFees).ToString();

            btnReplace.Enabled = false;
            llShowNewLicense.Enabled = true;
            ctrlLicenseDetailsWithFilter1.FilterEnabled = false;
            txtNotes.Enabled = false;
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (ctrlLicenseDetailsWithFilter1.LicenseID == -1)
            {
                MessageBox.Show("Select a license to replace", "No License Selected", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            if (!ctrlLicenseDetailsWithFilter1.License.IsActive)
            {
                MessageBox.Show("This License isn't actve to be replaced", "Inactive License", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            _LoadReplacedLicenseData();
        }

        private void llShowNewLicense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowLicenseForm form = new ShowLicenseForm(_ReplacedLicense.ID);
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