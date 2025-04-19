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
using DVLD_Presentation.Properties;

namespace DVLD_Presentation
{
    
    public partial class EditUserForm : Form
    {
        enum enMode { AddNew, Update };

        private enMode _Mode;

        private int _UserID = -1;

        private clsUsersBusiness _User;

        public EditUserForm()
        {
            InitializeComponent();

            _Mode = enMode.AddNew;
        }
        public EditUserForm(int UserID)
        {
            InitializeComponent();

            _Mode = enMode.Update;
            _UserID = UserID;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void _ResetDefaultValue()
        {
            if(_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New User";
                _User = new clsUsersBusiness();
                tpLoginInfo.Enabled = false;
            }
            else
            {
                lblTitle.Text = "Update User";
                tpLoginInfo.Enabled = true;
                btnSave.Enabled = true;
            }

            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            chkIsActive.Checked = false;
        }

        private void _LoadData(int UserID)
        {
            _User = clsUsersBusiness.Find(UserID);
            ctrlPersonDetailsWithFilter1.FilterEnabled = false;

            if (_User == null)
            {
                MessageBox.Show("No User with ID = " + UserID, "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            lblUserID.Text = _User.ID.ToString();
            txtUserName.Text = _User.Username;
            txtPassword.Text = _User.Password;
            txtConfirmPassword.Text = _User.Password;
            chkIsActive.Checked = _User.IsActive;
            ctrlPersonDetailsWithFilter1.LoadPersonInfo(_User.PersonID);
        }

        private void EditUserForm_Load(object sender, EventArgs e)
        {
            pbEye1.Image = Resources.Eye_Closed;
            pbEye1.PressedState.ImageSize = pbEye1.ImageSize;

            pbEye2.Image = Resources.Eye_Closed;
            pbEye2.PressedState.ImageSize = pbEye2.ImageSize;

            _ResetDefaultValue();

            if (_Mode == enMode.Update)
                _LoadData(_UserID);
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void RefillPersonDetails(int PersonID)
        {

        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if(_Mode == enMode.Update)
            {
                tcUserInfo.SelectedIndex = 1;
                return;
            }

            if(ctrlPersonDetailsWithFilter1.PersonID != -1)
            {
                if (clsUsersBusiness.IsUserExistByPersonID(ctrlPersonDetailsWithFilter1.PersonID))
                {
                    MessageBox.Show("Selected Person already is a user, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    btnSave.Enabled = true;
                    tpLoginInfo.Enabled = true;
                    tcUserInfo.SelectedIndex = 1;
                }
            }
            else
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid! Put the mouse over the red icon(s) to see the error",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _User.PersonID = ctrlPersonDetailsWithFilter1.PersonID;
            _User.Username = txtUserName.Text.Trim();
            _User.Password = txtPassword.Text.Trim();
            _User.IsActive = chkIsActive.Checked;

            if (_User.Save())
            {
                lblUserID.Text = _User.ID.ToString();
                lblTitle.Text = "Update User";
                _Mode = enMode.Update;

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void ctrlPersonDetailsWithFilter1_Load(object sender, EventArgs e)
        {

        }

        private void pbEye1_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !txtPassword.UseSystemPasswordChar;

            if (txtPassword.UseSystemPasswordChar)
                pbEye1.Image = Resources.Eye_Closed;
            else
                pbEye1.Image = Resources.Eye_Open;
        }

        private void pbEye2_Click(object sender, EventArgs e)
        {
            txtConfirmPassword.UseSystemPasswordChar = !txtConfirmPassword.UseSystemPasswordChar;

            if (txtConfirmPassword.UseSystemPasswordChar)
                pbEye2.Image = Resources.Eye_Closed;
            else
                pbEye2.Image = Resources.Eye_Open;
        }

        private void txtUserName_Validating_1(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text.Trim()))
            {
                e.Cancel = true;
                txtUserName.Focus();
                errorProvider1.SetError(txtUserName, "Username field cannot be empty");
            }
            else
            {
                errorProvider1.SetError(txtUserName, null);
            }

            if (_Mode == enMode.AddNew)
            {
                if (clsUsersBusiness.IsUserExist(txtUserName.Text.Trim()))
                {
                    e.Cancel = true;
                    txtUserName.Focus();
                    errorProvider1.SetError(txtUserName, "Username is used by another user");
                }
                else
                {
                    errorProvider1.SetError(txtUserName, null);
                }
            }
            else
            {
                if (_User.Username != txtUserName.Text.Trim())
                {
                    if (clsUsersBusiness.IsUserExist(txtUserName.Text.Trim()))
                    {
                        e.Cancel = true;
                        txtUserName.Focus();
                        errorProvider1.SetError(txtUserName, "Username is used by another user");
                    }
                    else
                    {
                        errorProvider1.SetError(txtUserName, null);
                    }
                }
            }
        }

        private void txtPassword_Validating_1(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPassword.Text.Trim()))
            {
                e.Cancel = true;
                txtPassword.Focus();
                errorProvider1.SetError(txtPassword, "Password field cannot be empty");
            }
            else
            {
                errorProvider1.SetError(txtUserName, null);
            }
        }

        private void txtConfirmPassword_Validating_1(object sender, CancelEventArgs e)
        {
            if (txtConfirmPassword.Text.Trim() != txtPassword.Text.Trim())
            {
                e.Cancel = true;
                txtConfirmPassword.Focus();
                errorProvider1.SetError(txtConfirmPassword, "Confirm Password field doesn't match Password field");
            }
            else
            {
                errorProvider1.SetError(txtUserName, null);
            }
        }
    }
}