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
    public partial class InternationalLicenseApplicationsForm : Form
    {
        private DataTable _dtAllILApplications = clsInternationalLicensesBusiness.ListInternationalLicenseApplications();

        public InternationalLicenseApplicationsForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _RefreshILApplicationsList()
        {
            _dtAllILApplications = clsInternationalLicensesBusiness.ListInternationalLicenseApplications();
            dgvILApplications.DataSource = _dtAllILApplications;
            lblCount.Text = dgvILApplications.Rows.Count.ToString();
        }

        private void InternationalLicenseApplicationsForm_Load(object sender, EventArgs e)
        {
            _RefreshILApplicationsList();

            cbFilterBy.SelectedIndex = 0;

            if(dgvILApplications.Rows.Count > 0)
            {
                dgvILApplications.Columns[0].HeaderText = "International License ID";

                dgvILApplications.Columns[1].HeaderText = "Application ID";

                dgvILApplications.Columns[2].HeaderText = "Driver ID";

                dgvILApplications.Columns[3].HeaderText = "L.License ID";

                dgvILApplications.Columns[4].HeaderText = "Issue Date";

                dgvILApplications.Columns[5].HeaderText = "Expiration Date";

                dgvILApplications.Columns[6].HeaderText = "Is Active";
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            _RefreshILApplicationsList();

            if (cbFilterBy.Text == "Is Active")
            {
                txtFilterValue.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.SelectedIndex = 0;
                return;
            }

            txtFilterValue.Visible = (cbFilterBy.Text != "None");
            cbIsActive.Visible = false;

            txtFilterValue.Text = "";
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "International License ID":
                    FilterColumn = "InternationalLicenseID";
                    break;

                case "Application ID":
                    FilterColumn = "ApplicationID";
                    break;

                case "Local License ID":
                    FilterColumn = "IssuedUsingLocalLicenseID";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllILApplications.DefaultView.RowFilter = "";
                lblCount.Text = dgvILApplications.Rows.Count.ToString();
                return;
            }

            _dtAllILApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());

            lblCount.Text = dgvILApplications.Rows.Count.ToString();
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterValue = cbIsActive.Text;

            switch (cbIsActive.Text)
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

            if (FilterValue == "All")
                _dtAllILApplications.DefaultView.RowFilter = "";
            else
                _dtAllILApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", "IsActive", FilterValue);

            lblCount.Text = dgvILApplications.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            NewInternationalLicenseApplicationForm form = new NewInternationalLicenseApplicationForm();
            form.ShowDialog();
            _RefreshILApplicationsList();
        }

        private void PesonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsDriversBusiness.Find((int)dgvILApplications.CurrentRow.Cells[2].Value).PersonID;

            PersonDetailsForm form = new PersonDetailsForm(PersonID);
            form.ShowDialog();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InternationalLicenseDetailsForm form = new InternationalLicenseDetailsForm((int)dgvILApplications.CurrentRow.Cells[0].Value);
            form.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsDriversBusiness.Find((int)dgvILApplications.CurrentRow.Cells[2].Value).PersonID;

            ShowPersonLicensesHistoryForm form = new ShowPersonLicensesHistoryForm(PersonID);
            form.ShowDialog();
        }
    }
}