using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using DVLD_Business;
using DVLD_Presentation.Properties;
using System.Diagnostics;

namespace DVLD_Presentation
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {

        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {

        }

        private void WriteToEventLog()
        {
            if (!EventLog.SourceExists("DVLD"))
            {
                EventLog.CreateEventSource("DVLD", "Application");
            }

            EventLog.WriteEntry("DVLD", $"User: {clsGlobal.CurrentUser.Username}, has logged in the system");
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            clsUsersBusiness User = clsUsersBusiness.FindByUsernameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());

            if (User != null)
            {
                if (!User.IsActive)
                {
                    txtUserName.Focus();
                    MessageBox.Show("Your account isn't Active", "Inactive Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (chkRememberMe.Checked)
                {
                    clsGlobal.RememberMe(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                }
                else
                {
                    clsGlobal.RememberMe("", "");
                }

                clsGlobal.CurrentUser = User;
                WriteToEventLog();
                this.Hide();
                MainForm form = new MainForm(this);
                form.ShowDialog();

            }
            else
            {
                txtUserName.Focus();
                MessageBox.Show("Invalid Username/Password", "Incorrect Credintials", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            pbEye.Image = Resources.Eye_Closed;
            pbEye.PressedState.ImageSize = pbEye.ImageSize;

            string Username = "", Password = "";

            if (clsGlobal.GetStoredCredentials(ref Username, ref Password))
            {
                txtUserName.Text = Username;
                txtPassword.Text = Password;
                chkRememberMe.Checked = true;
            }
            else
            {
                txtUserName.Text = "";
                txtPassword.Text = "";
                chkRememberMe.Checked = false;
            }               
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void TriggerLoadEvent()
        {
            LoginForm_Load(this, EventArgs.Empty);
        }

        private void pbEye_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !txtPassword.UseSystemPasswordChar;

            if (txtPassword.UseSystemPasswordChar)
                pbEye.Image = Resources.Eye_Closed;
            else
                pbEye.Image = Resources.Eye_Open;
        }
    }
}