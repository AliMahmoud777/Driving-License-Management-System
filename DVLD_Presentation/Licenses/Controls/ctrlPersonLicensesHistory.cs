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
    public partial class ctrlPersonLicensesHistory : UserControl
    {
        private int _DriverID;
        private DataTable _dtLocalLicenses;
        private DataTable _dtInternationalLicenses;
        public ctrlPersonLicensesHistory()
        {
            InitializeComponent();
        }

        private void _LoadLocalLicensesList()
        {
            _dtLocalLicenses = clsLicensesBusiness.ListDriverLicenses(_DriverID);
            dgvLocalLicenses.DataSource = _dtLocalLicenses;
            lblLocalCount.Text = dgvLocalLicenses.Rows.Count.ToString();

            if(dgvLocalLicenses.Rows.Count > 0)
            {
                dgvLocalLicenses.Columns[0].HeaderText = "License ID";

                dgvLocalLicenses.Columns[1].HeaderText = "App.ID";

                dgvLocalLicenses.Columns[2].HeaderText = "Class Name";

                dgvLocalLicenses.Columns[3].HeaderText = "Issue Date";

                dgvLocalLicenses.Columns[4].HeaderText = "Expiration Date";

                dgvLocalLicenses.Columns[5].HeaderText = "Is Active";
            }
        }

        private void _LoadInternationalLicensesList()
        {
            _dtInternationalLicenses = clsInternationalLicensesBusiness.ListDriverInternationalLicenses(_DriverID);
            dgvInternationalLicenses.DataSource = _dtInternationalLicenses;
            lblInternationalCount.Text = dgvInternationalLicenses.Rows.Count.ToString();

            if (dgvInternationalLicenses.Rows.Count > 0)
            {
                dgvInternationalLicenses.Columns[0].HeaderText = "Int.License ID";

                dgvInternationalLicenses.Columns[1].HeaderText = "App.ID";

                dgvInternationalLicenses.Columns[2].HeaderText = "L.License ID";

                dgvInternationalLicenses.Columns[3].HeaderText = "Issue Date";

                dgvInternationalLicenses.Columns[4].HeaderText = "Expiration Date";

                dgvInternationalLicenses.Columns[5].HeaderText = "Is Active";
            }
        }

        public void LoadLicensesInfoByPersonID(int PersonID)
        {
            clsDriversBusiness Driver = clsDriversBusiness.FindByPersonID(PersonID);

            if(Driver == null)
            {
                MessageBox.Show("No Driver with Person ID :- " + PersonID.ToString(), "Error");
                return;
            }

            _DriverID = Driver.ID;
            _LoadLocalLicensesList();
            _LoadInternationalLicensesList();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(tcDriverLicenses.SelectedTab == tpLocalLicenses)
            {
                ShowLicenseForm form = new ShowLicenseForm((int)dgvLocalLicenses.CurrentRow.Cells[0].Value);
                form.ShowDialog();
            }
            else
            {
                InternationalLicenseDetailsForm form = new InternationalLicenseDetailsForm((int)dgvInternationalLicenses.CurrentRow.Cells[0].Value);
                form.ShowDialog();
            }
        }
    }
}