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

namespace DVLD_Presentation
{
    public partial class ctrlUserDetails : UserControl
    {

        private clsUsersBusiness _User;
        public ctrlUserDetails()
        {
            InitializeComponent();
        }

        private void _FillUserInfo()
        {
            lblID.Text = _User.ID.ToString();
            lblUserName.Text = _User.Username;
            lblIsActive.Text = (_User.IsActive) ? "Yes" : "No";
            ctrlPersonDetails1.LoadPersonInfo(_User.PersonID);
        }

        public void LoadUserInfo(int UserID)
        {
            _User = clsUsersBusiness.Find(UserID);

            if(_User == null)
            {
                MessageBox.Show("No User with UserID = " + UserID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillUserInfo();
        }
    }
}