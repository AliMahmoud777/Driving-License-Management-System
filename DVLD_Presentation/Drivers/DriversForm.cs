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

namespace DVLD_Presentation
{
    public partial class DriversForm : Form
    {
        private DataTable _dtAllDrivers = clsDriversBusiness.GetAllDrivers();
        public DriversForm()
        {
            InitializeComponent();
        }

        private void _RefreshDriversList()
        {
            _dtAllDrivers = clsDriversBusiness.GetAllDrivers();
            dgvDrivers.DataSource = clsDriversBusiness.GetAllDrivers();
            lblCount.Text = dgvDrivers.Rows.Count.ToString();
        }
        private void DriversForm_Load(object sender, EventArgs e)
        {
            _RefreshDriversList();

            cbFilterBy.SelectedIndex = 0;

            if(dgvDrivers.Rows.Count > 0)
            {
                dgvDrivers.Columns[0].HeaderText = "Driver ID";

                dgvDrivers.Columns[1].HeaderText = "Person ID";

                dgvDrivers.Columns[2].HeaderText = "National No";

                dgvDrivers.Columns[3].HeaderText = "Full Name";

                dgvDrivers.Columns[4].HeaderText = "Date";

                dgvDrivers.Columns[5].HeaderText = "Active Licenses";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _RefreshDriversList();

            txtFilterValue.Visible = (cbFilterBy.Text != "None");

            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No":
                    FilterColumn = "NationalNo";
                    break;

                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            if(txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllDrivers.DefaultView.RowFilter = "";
                lblCount.Text = dgvDrivers.Rows.Count.ToString();
                return;
            }

            if (cbFilterBy.Text == "Person ID" || cbFilterBy.Text == "Driver ID")
                _dtAllDrivers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtAllDrivers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblCount.Text = dgvDrivers.Rows.Count.ToString();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.SelectedIndex == 1 || cbFilterBy.SelectedIndex == 2)
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PersonDetailsForm form = new PersonDetailsForm(((int)dgvDrivers.CurrentRow.Cells[1].Value));
            form.ShowDialog();
            _RefreshDriversList();
        }

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowPersonLicensesHistoryForm form = new ShowPersonLicensesHistoryForm((int)dgvDrivers.CurrentRow.Cells[1].Value);
            form.ShowDialog();
            _RefreshDriversList();
        }
    }
}