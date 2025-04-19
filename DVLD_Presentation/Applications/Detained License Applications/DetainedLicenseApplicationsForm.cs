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

namespace DVLD_Presentation.Applications.Detained_License_Applications
{
    public partial class DetainedLicenseApplicationsForm : Form
    {
        private DataTable _dtAllDLApplications = clsDetainedLicensesBusiness.GetAllDetainedLicenses();
        public DetainedLicenseApplicationsForm()
        {
            InitializeComponent();
        }

        private void _RefreshDLApplicationsList()
        {
            _dtAllDLApplications = clsDetainedLicensesBusiness.GetAllDetainedLicenses();
            dgvDLApplications.DataSource = _dtAllDLApplications;
            lblCount.Text = dgvDLApplications.Rows.Count.ToString();
        }

        private void DetainedLicenseApplicationsForm_Load(object sender, EventArgs e)
        {
            _RefreshDLApplicationsList();

            cbFilterBy.SelectedIndex = 0;

            if(dgvDLApplications.Rows.Count > 0)
            {
                dgvDLApplications.Columns[0].HeaderText = "Detain ID";
                   
                dgvDLApplications.Columns[1].HeaderText = "License ID";
                   
                dgvDLApplications.Columns[2].HeaderText = "Detain Date";
                   
                dgvDLApplications.Columns[3].HeaderText = "Is Released";
                   
                dgvDLApplications.Columns[4].HeaderText = "Fine Fees";
                   
                dgvDLApplications.Columns[5].HeaderText = "Release Date";
                   
                dgvDLApplications.Columns[6].HeaderText = "National No";
                   
                dgvDLApplications.Columns[7].HeaderText = "Full Name";
                   
                dgvDLApplications.Columns[8].HeaderText = "Release App.ID";
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbFilterBy.Text == "Is Released")
            {
                cbIsReleased.Visible = true;
                txtFilterValue.Visible = false;
                cbIsReleased.SelectedIndex = 0;
                return;
            }

            txtFilterValue.Visible = (cbFilterBy.Text != "None");
            cbFilterBy.Visible = false;

            txtFilterValue.Text = "";
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "Detain ID":
                    FilterColumn = "DetainID";
                    break;

                case "National No":
                    FilterColumn = "NationalNo";
                    break;

                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Release Application ID":
                    FilterColumn = "ReleaseApplicationID";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            if(txtFilterValue.Text == "" || FilterColumn == "None")
            {
                _dtAllDLApplications.DefaultView.RowFilter = "";
                lblCount.Text = dgvDLApplications.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "DetainID" || FilterColumn == "ReleaseApplicationID")
                _dtAllDLApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text);
            else
                _dtAllDLApplications.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text);

            lblCount.Text = dgvDLApplications.Rows.Count.ToString();
        }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterValue = cbIsReleased.Text;

            switch (cbIsReleased.Text)
            {
                case "All":
                    break;

                case "Yes":
                    FilterValue = "1";
                    break;

                case "No":
                    FilterValue = "0";
                    break;
            }

            if (FilterValue != "All")
                _dtAllDLApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", "IsReleased", FilterValue);
            else
                _dtAllDLApplications.DefaultView.RowFilter = "";

            lblCount.Text = dgvDLApplications.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Detain ID" || cbFilterBy.Text == "Release Application ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            ReleaseDetainedLicenseForm form = new ReleaseDetainedLicenseForm();
            form.ShowDialog();
            _RefreshDLApplicationsList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PesonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDLApplications.CurrentRow.Cells[1].Value;
            int PersonID = clsLicensesBusiness.Find(LicenseID).DriverInfo.PersonID;

            PersonDetailsForm form = new PersonDetailsForm(PersonID);
            form.ShowDialog();
            _RefreshDLApplicationsList();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowLicenseForm form = new ShowLicenseForm((int)dgvDLApplications.CurrentRow.Cells[1].Value);
            form.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDLApplications.CurrentRow.Cells[1].Value;
            int PersonID = clsLicensesBusiness.Find(LicenseID).DriverInfo.PersonID;

            ShowPersonLicensesHistoryForm form = new ShowPersonLicensesHistoryForm(PersonID);
            form.ShowDialog();
            _RefreshDLApplicationsList();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReleaseDetainedLicenseForm form = new ReleaseDetainedLicenseForm();
            form.ShowDialog();
            _RefreshDLApplicationsList();
        }
    }
}