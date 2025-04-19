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
    public partial class UserDetailsForm : Form
    {
        private int _UserID = -1;
        public UserDetailsForm(int UserID)
        {
            InitializeComponent();

            _UserID = UserID;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UserDetailsForm_Load(object sender, EventArgs e)
        {
            ctrlUserDetails1.LoadUserInfo(_UserID);
        }
    }
}