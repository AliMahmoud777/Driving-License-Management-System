using DVLD_Business;
using DVLD_Presentation.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics.Contracts;

namespace DVLD_Presentation
{
    public partial class ctrlPersonInfo : UserControl
    {
        public event Action<bool> OnClose;
        protected virtual void Close(bool Result)
        {
            Action<bool> handler = OnClose;
            if(handler != null)
            {
                handler(Result);
            }
        }

        enum enMode { AddNew, Update };

        enMode _Mode;
        public int _PersonID { get; set; }
        public clsPeopleBusiness _Person;
        string PreviousImagePath = "";

        public ctrlPersonInfo()
        {
            InitializeComponent();        
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void _FillCountriesInComboBox()
        {
            DataTable dtCountries = clsCountriesBusiness.ListCountries();

            foreach (DataRow Row in dtCountries.Rows)
            {
                cbCountry.Items.Add(Row["CountryName"]);
            }
        }

        private void ctrlPersonInfo_Load(object sender, EventArgs e)
        {
            if (_PersonID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;

            rbMale.Checked = true;
            dateTimePicker1.MaxDate = DateTime.Now.AddYears(-18);

            _FillCountriesInComboBox();

            cbCountry.SelectedIndex = 0;

            if (_Mode == enMode.AddNew)
            {
                _Person = new clsPeopleBusiness();
                return;
            }

            _Person = clsPeopleBusiness.Find(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show($"No person with ID :- {_PersonID}");
                if (OnClose != null)
                {
                    Close(true);
                }
                return;
            }

            cbCountry.SelectedIndex = cbCountry.FindString((clsCountriesBusiness.Find(_Person.CountryID)).Name);
            txtNationalNo.Text = _Person.NationalNo;
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            if (_Person.ThirdName != "")
            {
                txtThirdName.Text = _Person.ThirdName;
            }
            txtLastName.Text = _Person.LastName;
            if (_Person.Gender == 0)
            {
                rbMale.Checked = true;
            }
            else
            {
                rbFemale.Checked = true;
            }
            if(_Person.Email != "")
            {
                txtEmail.Text = _Person.Email;
            }
            txtPhone.Text = _Person.Phone;
            txtAddress.Text = _Person.Address;
            dateTimePicker1.Value = _Person.DateOfBirth;
            if (_Person.ImagePath != "")
            {
                pbPersonImage.Load(_Person.ImagePath);
                lblRemove.Visible = true;
            }


            cbCountry.SelectedIndex = cbCountry.FindString((clsCountriesBusiness.Find(_Person.CountryID)).Name);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
           /* if ((clsPeopleBusiness.FindPeopleWithCondition("NationalNo", txtNationalNo.Text)).Rows.Count > 0
                || string.IsNullOrWhiteSpace(txtNationalNo.Text))
            {
                e.Cancel = true;
                txtNationalNo.Focus();
                errorProvider1.SetError(txtNationalNo, "National Number cannot be empty or assigned to more than one person");
            }
            else
            {
                e.Cancel = false;
            }*/
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                e.Cancel = true;
                txtFirstName.Focus();
                errorProvider1.SetError(txtFirstName, "First Name field cannot be empty");

            }
            else
            {
                e.Cancel = false;
            }
        }

        private void txtSecondName_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtSecondName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSecondName.Text))
            {
                e.Cancel = true;
                txtSecondName.Focus();
                errorProvider1.SetError(txtSecondName, "Second Name field cannot be empty");

            }
            else
            {
                e.Cancel = false;
            }
        }

        private void textBox4_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                e.Cancel = true;
                txtLastName.Focus();
                errorProvider1.SetError(txtLastName, "Last Name field cannot be empty");

            }
            else
            {
                e.Cancel = false;
            }
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if(pbPersonImage.ImageLocation == "")
              pbPersonImage.Image = Resources.Male_512;
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if(pbPersonImage.ImageLocation == "")
              pbPersonImage.Image = Resources.Female_512;
        }

        private void textBox6_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                e.Cancel = true;
                txtPhone.Focus();
                errorProvider1.SetError(txtPhone, "Phone field cannot be empty");

            }
            else
            {
                e.Cancel = false;
            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (txtEmail.Text == "")
                return;

            string Pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            if(!Regex.IsMatch(txtEmail.Text, Pattern))
            {
                e.Cancel = true;
                txtEmail.Focus();
                errorProvider1.SetError(txtEmail, "Invalid Email");
            }
            else
            {
                e.Cancel = false;
            }
        }

        private void textBox8_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                e.Cancel = true;
                txtAddress.Focus();
                errorProvider1.SetError(txtAddress, "Address field cannot be empty");

            }
            else
            {
                e.Cancel = false;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            openFileDialog1.Filter = "png file (*.png)|*.png|jpg files (*.jpg)|*.jpg";
            openFileDialog1.DefaultExt = "png";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pbPersonImage.Image = Image.FromFile(openFileDialog1.FileName);
                pbPersonImage.ImageLocation = openFileDialog1.FileName;

                lblRemove.Visible = true;
            }
        }

        private void lblRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (rbMale.Checked)
            {
                pbPersonImage.Image = Resources.Male_512;
            }
            else
            {
                pbPersonImage.Image = Resources.Female_512;
            }

            lblRemove.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(OnClose != null)
            {
                Close(true);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int CountryID = (clsCountriesBusiness.FindByName(cbCountry.Text)).ID;

            _Person.NationalNo = txtNationalNo.Text;
            _Person.FirstName = txtFirstName.Text;
            _Person.SecondName = txtSecondName.Text;
            if(txtThirdName.Text != "")
            {
                _Person.ThirdName = txtThirdName.Text;
            }
            else
            {
                _Person.ThirdName= "";
            }
            _Person.LastName = txtLastName.Text;
            if(rbMale.Checked == true)
            {
                _Person.Gender = 0;
            }
            else
            {
                _Person.Gender = 1;
            }
            if (txtEmail.Text != "")
            {
                _Person.Email = txtEmail.Text;
            }
            else
            {
                _Person.Email = "";
            }
            _Person.Phone = txtPhone.Text;
            _Person.Address = txtAddress.Text;
            _Person.DateOfBirth = dateTimePicker1.Value;
            _Person.CountryID = CountryID;
            if (pbPersonImage.ImageLocation != null)
            {
                if(PreviousImagePath != "")
                {
                    File.Delete(_Person.ImagePath);
                }
                
                
                PreviousImagePath = @"F:\Visual Studio\DVLD_PeopleImages\" + Guid.NewGuid().ToString() + ".jpg";
                
                File.Copy(pbPersonImage.ImageLocation, PreviousImagePath);
                _Person.ImagePath = PreviousImagePath;
            }
            else
            {
                _Person.ImagePath = "";
            }

            if (_Person.Save())
                MessageBox.Show("Person saved successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                MessageBox.Show("Person wasn't saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if(OnClose != null)
            {
                Close(true);
            }
        }
    }
}