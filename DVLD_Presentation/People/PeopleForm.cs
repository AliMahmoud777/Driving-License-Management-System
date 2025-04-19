using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DVLD_Business;

namespace DVLD_Presentation
{
    public partial class PeopleForm : Form
    {
        private static DataTable _dtAllPeople = clsPeopleBusiness.GetAllPeople();

        private DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo", "FirstName",
                                                                              "SecondName", "ThirdName", "LastName",
                                                                              "GenderCaption", "DateOfBirth", "CountryName",
                                                                              "Phone", "Email");

        public PeopleForm()
        {
            InitializeComponent();
        }

        private void _RefreshPeopleList()
        {
            _dtAllPeople = clsPeopleBusiness.GetAllPeople();
            _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo", "FirstName",
                                                                              "SecondName", "ThirdName", "LastName",
                                                                              "GenderCaption", "DateOfBirth", "CountryName",
                                                                              "Phone", "Email");
            dgvPeople.DataSource = _dtPeople;
            lblCount.Text = dgvPeople.Rows.Count.ToString();
        }
        private void PeopleForm_Load(object sender, EventArgs e)
        {
            _RefreshPeopleList();

            cbFilterBy.SelectedIndex = 0;

            if(dgvPeople.Rows.Count > 0)
            {
                dgvPeople.Columns[0].HeaderText = "Person ID";

                dgvPeople.Columns[1].HeaderText = "National No";


                dgvPeople.Columns[2].HeaderText = "First Name";

                dgvPeople.Columns[3].HeaderText = "Second Name";


                dgvPeople.Columns[4].HeaderText = "Third Name";

                dgvPeople.Columns[5].HeaderText = "Last Name";

                dgvPeople.Columns[6].HeaderText = "Gender";

                dgvPeople.Columns[7].HeaderText = "Date Of Birth";

                dgvPeople.Columns[8].HeaderText = "Country";


                dgvPeople.Columns[9].HeaderText = "Phone";


                dgvPeople.Columns[10].HeaderText = "Email";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EditPersonForm form = new EditPersonForm();
            form.ShowDialog();
            _RefreshPeopleList();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditPersonForm form = new EditPersonForm();
            form.ShowDialog();
            _RefreshPeopleList();
        }

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EditPersonForm form = new EditPersonForm((int)dgvPeople.CurrentRow.Cells[0].Value);
            form.ShowDialog();
            _RefreshPeopleList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this person with ID :- " + dgvPeople.CurrentRow.Cells[0].Value.ToString(), "Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                string PersonImagePath = clsPeopleBusiness.Find((int)dgvPeople.CurrentRow.Cells[0].Value).ImagePath;

                if (clsPeopleBusiness.DeletePerson((int)dgvPeople.CurrentRow.Cells[0].Value))
                {

                    if (PersonImagePath != "")
                        File.Delete(PersonImagePath);

                    MessageBox.Show("Person deleted successfully");
                    _RefreshPeopleList();
                }
                else
                {
                    MessageBox.Show("Person wasn't deleted");
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

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PersonDetailsForm form = new PersonDetailsForm((int)dgvPeople.CurrentRow.Cells[0].Value);
            form.ShowDialog();
            _RefreshPeopleList();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No":
                    FilterColumn = "NationalNo";
                    break;

                case "First Name":
                    FilterColumn = "FirstName";
                    break;

                case "Second Name":
                    FilterColumn = "SecondName";
                    break;

                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;

                case "Last Name":
                    FilterColumn = "LastName";
                    break;

                case "Country":
                    FilterColumn = "CountryName";
                    break;

                case "Gender":
                    FilterColumn = "GenderCaption";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    break;

                case "Email":
                    FilterColumn = "Email";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllPeople.DefaultView.RowFilter = "";
                lblCount.Text = dgvPeople.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "PersonID")
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] like '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblCount.Text = dgvPeople.Rows.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");

            _RefreshPeopleList();

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PersonDetailsForm form = new PersonDetailsForm((int)dgvPeople.CurrentRow.Cells[0].Value);
            form.ShowDialog();
            _RefreshPeopleList();
        }

        private void dgvPeople_DoubleClick(object sender, EventArgs e)
        {
            PersonDetailsForm form = new PersonDetailsForm((int)dgvPeople.CurrentRow.Cells[0].Value);
            form.ShowDialog();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}