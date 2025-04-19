using System;
using DVLD_Business;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Presentation.Licenses.Detained_Licenses
{
    public partial class DetainLicenseForm : Form
    {
        private clsDetainedLicensesBusiness _DetainedLicense;
        public DetainLicenseForm()
        {
            InitializeComponent();
            this.ActiveControl = ctrlLicenseDetailsWithFilter1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool _CreateDetainedLicense()
        {
            _DetainedLicense = new clsDetainedLicensesBusiness();

            _DetainedLicense.LicenseID = ctrlLicenseDetailsWithFilter1.LicenseID;
            _DetainedLicense.DetainDate = DateTime.Now;
            _DetainedLicense.FineFees = Convert.ToSingle(txtFineFees.Text);
            _DetainedLicense.CreatedByUserID = clsGlobal.CurrentUser.ID;

            if (_DetainedLicense.Save())
            {
                MessageBox.Show("License detained successfully", "Successful Process", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            else
            {
                MessageBox.Show("Couldn't detain the license", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void _LoadDetainedLicenseInfo()
        {
            if(!_CreateDetainedLicense())
                return;

            lblDetainID.Text = _DetainedLicense.DetainID.ToString();
            lblDetainDate.Text = _DetainedLicense.DetainDate.ToShortDateString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.Username;

            btnDetain.Enabled = false;
            txtFineFees.Enabled = false;
            ctrlLicenseDetailsWithFilter1.FilterEnabled = false;
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if(ctrlLicenseDetailsWithFilter1.LicenseID == -1)
            {
                MessageBox.Show("Select a license to detain", "No License Selected", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            if (ctrlLicenseDetailsWithFilter1.License.IsDetained)
            {
                MessageBox.Show("The selected license is already detained", "Detained License Selected", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }

            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields aren't valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LoadDetainedLicenseInfo();
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

        private void txtFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFineFees.Text))
            {
                e.Cancel = true;
                txtFineFees.Focus();
                errorProvider1.SetError(txtFineFees, "Fees field cannot be empty");
            }
            else
            {
                e.Cancel = false;
            }
        }

        private void DetainLicenseForm_Load(object sender, EventArgs e)
        {
            
        }

        private void ctrlLicenseDetailsWithFilter1_OnLicenseSelected(int obj)
        {

        }

        private void DetainLicenseForm_Activated(object sender, EventArgs e)
        {
        }

        private void DetainLicenseForm_Shown(object sender, EventArgs e)
        {

        }
    }
}