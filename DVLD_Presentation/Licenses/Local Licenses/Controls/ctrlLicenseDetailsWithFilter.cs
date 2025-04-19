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

namespace DVLD_Presentation.Licenses
{
    public partial class ctrlLicenseDetailsWithFilter : UserControl
    {
        public event Action<int> OnLicenseSelected;
        protected virtual void LicenseSelected(int LicenseID)
        {
            Action<int> handler = OnLicenseSelected;
            if (handler != null)
            {
                handler(LicenseID);
            }
        }
        public int LicenseID
        {
            get
            {
                return ctrlLicenseDetails1.LicenseID;
            }
        }
        public clsLicensesBusiness License
        {
            get
            {
                return ctrlLicenseDetails1.License;
            }
        }

        private bool _FilterEnabled = true;

        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                gbFilters.Enabled = _FilterEnabled;
            }
        }
        public ctrlLicenseDetailsWithFilter()
        {
            InitializeComponent();
        }

        public void LoadLicenseWithFilterInfo(int pLicenseID)
        {
            txtLicenseID.Text = pLicenseID.ToString();
            ctrlLicenseDetails1.LoadLicenseInfo(pLicenseID);

            if (OnLicenseSelected != null && FilterEnabled && LicenseID != -1)
                OnLicenseSelected(LicenseID);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid! Put the mouse over the red icon(s) to see the errors", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLicenseID.Focus();
                return;
            }

            LoadLicenseWithFilterInfo(int.Parse(txtLicenseID.Text));
        }

        private void txtLicenseID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            if (e.KeyChar == (char)13)
            {
                btnFind.PerformClick();
            }
        }

        private void txtLicenseID_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLicenseID.Text.Trim()))
            {
                e.Cancel = true;
                txtLicenseID.Focus();
                errorProvider1.SetError(txtLicenseID, "This field isn't valid!");
            }
            else
            {
                errorProvider1.SetError(txtLicenseID, null);
            }
        }

        public void LicenseTextBoxFocus()
        {
            txtLicenseID.Focus();
        }
    }
}