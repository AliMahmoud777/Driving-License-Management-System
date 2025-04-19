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
    public partial class UsersForm : Form
    {
        private static DataTable _dtAllUsers = clsUsersBusiness.GetAllUsers();
        public UsersForm()
        {
            InitializeComponent();
        }

        private void _RefreshUsersList()
        {
            _dtAllUsers = clsUsersBusiness.GetAllUsers();
            dgvUsers.DataSource = _dtAllUsers;
            lblCount.Text = dgvUsers.Rows.Count.ToString();
        }

        private void UsersForm_Load(object sender, EventArgs e)
        {
            _RefreshUsersList();

            cbFilterBy.SelectedIndex = 0;

            if (dgvUsers.Rows.Count > 0)
            {
                dgvUsers.Columns[0].HeaderText = "User ID";

                dgvUsers.Columns[1].HeaderText = "Person ID";

                dgvUsers.Columns[2].HeaderText = "Full Name";

                dgvUsers.Columns[3].HeaderText = "Username";

                dgvUsers.Columns[4].HeaderText = "Is Active";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _RefreshUsersList();

            if(cbFilterBy.Text == "Is Active")
            {
                txtFilterValue.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.SelectedIndex = 0;
                return;
            }

            txtFilterValue.Visible = (cbFilterBy.Text != "None");
            cbIsActive.Visible = false;

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "User ID":
                    FilterColumn = "UserID";
                    break;

                case "Username":
                    FilterColumn = "UserName";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
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
                _dtAllUsers.DefaultView.RowFilter = "";
                lblCount.Text = dgvUsers.Rows.Count.ToString();
                return;
            }

            if (cbFilterBy.Text != "Username" && cbFilterBy.Text != "Full Name")
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblCount.Text = dgvUsers.Rows.Count.ToString();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
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

            if(FilterValue == "All")
                _dtAllUsers.DefaultView.RowFilter = "";
            else
                _dtAllUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", "IsActive", FilterValue);

            lblCount.Text = dgvUsers.Rows.Count.ToString();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.SelectedIndex == 1 || cbFilterBy.SelectedIndex == 2)
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this user with ID :- " + dgvUsers.CurrentRow.Cells[0].Value.ToString(), "Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (clsUsersBusiness.DeleteUser((int)dgvUsers.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("User deleted successfully");
                    _RefreshUsersList();
                }
                else
                {
                    MessageBox.Show("User wasn't deleted");
                }
            }
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is coming soon");
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is coming soon");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EditUserForm form = new EditUserForm();
            form.ShowDialog();
            _RefreshUsersList();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditUserForm form = new EditUserForm();
            form.ShowDialog();
            _RefreshUsersList();
        }

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EditUserForm form = new EditUserForm((int)dgvUsers.CurrentRow.Cells[0].Value);
            form.ShowDialog();
            _RefreshUsersList();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePasswordForm form = new ChangePasswordForm((int)dgvUsers.CurrentRow.Cells[0].Value);
            form.ShowDialog();
            _RefreshUsersList();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserDetailsForm form = new UserDetailsForm((int)dgvUsers.CurrentRow.Cells[0].Value);
            form.ShowDialog();
            _RefreshUsersList();
        }

        private void dgvUsers_DoubleClick(object sender, EventArgs e)
        {
            UserDetailsForm form = new UserDetailsForm((int)dgvUsers.CurrentRow.Cells[0].Value);
            form.ShowDialog();
            _RefreshUsersList();
        }
    }
}