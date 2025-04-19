using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Presentation.Properties;
using System.Diagnostics;
using Microsoft.Win32;

namespace DVLD_Presentation
{
    public partial class ChangePasswordForm : Form
    {
        private int _UserID = -1;

        private clsUsersBusiness _User;
        public ChangePasswordForm(int UserID)
        {
            InitializeComponent();

            _UserID = UserID;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _ResetDefaultValues()
        {
            txtCurrentPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfirmPassword.Text = "";
            txtCurrentPassword.Focus();
        }

        private void WriteToEventLog()
        {
            if (!EventLog.SourceExists("DVLD"))
            {
                EventLog.CreateEventSource("DVLD", "Application");
            }

            EventLog.WriteEntry("DVLD", $"User: {clsGlobal.CurrentUser.Username}, has changed their password");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid! Put the mouse over the red icon(s) to see the errors", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _User.Password = txtNewPassword.Text;

            if (_User.Save())
            {
                MessageBox.Show("Password Changed Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                WriteToEventLog();
                try
                {
                    using (RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
                    {
                        using (RegistryKey subKey = key.OpenSubKey(@"SOFTWARE\DVLD", true))
                        {
                            if (subKey != null)
                            {
                                subKey.SetValue("Password", _User.Password, RegistryValueKind.String);
                            }
                        }
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show($"UnauthorizedAccessException: Run the program with administrative privileges");
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                    return;
                }

                _ResetDefaultValues();
            }
            else
            {
                MessageBox.Show("An Error Occured, Password wasn't change", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChangePasswordForm_Load(object sender, EventArgs e)
        {
            pbEye1.Image = Resources.Eye_Closed;
            pbEye1.PressedState.ImageSize = pbEye1.ImageSize;

            pbEye2.Image = Resources.Eye_Closed;
            pbEye2.PressedState.ImageSize = pbEye2.ImageSize;

            pbEye3.Image = Resources.Eye_Closed;
            pbEye3.PressedState.ImageSize = pbEye3.ImageSize;

            _ResetDefaultValues();

            _User = clsUsersBusiness.Find(_UserID);

            if(_User == null)
            {
                MessageBox.Show("Could not Find User with ID = " + _UserID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctrlUserDetails1.LoadUserInfo(_User.ID);
        }

        private void txtCurrentPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void pbEye1_Click(object sender, EventArgs e)
        {
            txtCurrentPassword.UseSystemPasswordChar = !txtCurrentPassword.UseSystemPasswordChar;

            if (txtCurrentPassword.UseSystemPasswordChar)
                pbEye1.Image = Resources.Eye_Closed;
            else
                pbEye1.Image = Resources.Eye_Open;
        }

        private void pbEye2_Click(object sender, EventArgs e)
        {
            txtNewPassword.UseSystemPasswordChar = !txtNewPassword.UseSystemPasswordChar;

            if (txtNewPassword.UseSystemPasswordChar)
                pbEye2.Image = Resources.Eye_Closed;
            else
                pbEye2.Image = Resources.Eye_Open;
        }

        private void pbEye3_Click(object sender, EventArgs e)
        {
            txtConfirmPassword.UseSystemPasswordChar = !txtConfirmPassword.UseSystemPasswordChar;

            if (txtConfirmPassword.UseSystemPasswordChar)
                pbEye3.Image = Resources.Eye_Closed;
            else
                pbEye3.Image = Resources.Eye_Open;
        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCurrentPassword.Text.Trim()))
            {
                e.Cancel = true;
                txtCurrentPassword.Focus();
                errorProvider1.SetError(txtCurrentPassword, "Current Password field cannot be empty");
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, null);
            }

            if (_User.Password != txtCurrentPassword.Text.Trim())
            {
                e.Cancel = true;
                txtCurrentPassword.Focus();
                errorProvider1.SetError(txtCurrentPassword, "Current Password field isn't correct");
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, null);
            }
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNewPassword.Text.Trim()))
            {
                e.Cancel = true;
                txtNewPassword.Focus();
                errorProvider1.SetError(txtNewPassword, "New Password field cannot be empty");
            }
            else
            {
                errorProvider1.SetError(txtNewPassword, null);
            }

            if (txtNewPassword.Text.Trim() == txtCurrentPassword.Text.Trim())
            {
                e.Cancel = true;
                txtNewPassword.Focus();
                errorProvider1.SetError(txtNewPassword, "New Password field cannot be equivalent to Current Password field");
            }
            else
            {
                errorProvider1.SetError(txtNewPassword, null);
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmPassword.Text.Trim() != txtNewPassword.Text.Trim())
            {
                e.Cancel = true;
                txtConfirmPassword.Focus();
                errorProvider1.SetError(txtConfirmPassword, "Confirm Password field doesn't match New Password field");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            }
        }
    }
}