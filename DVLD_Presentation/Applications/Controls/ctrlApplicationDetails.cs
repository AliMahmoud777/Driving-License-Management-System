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
    public partial class ctrlApplicationDetails : UserControl
    {
        private int _AppID = -1;
        public int selectedApplicationID
        {
            get { return _AppID; }
        }

        private clsApplicationsBusiness _App;
        public ctrlApplicationDetails()
        {
            InitializeComponent();
        }

        private void _FillApplicationInfo()
        {
            _AppID = _App.ApplicationID;
            lblAppID.Text = _App.ApplicationID.ToString();
            lblStatus.Text = _App.StatusText;
            lblFees.Text = _App.PaidFees.ToString();
            lblType.Text = _App.TypeInfo.Title;
            lblApplicant.Text = _App.PersonInfo.FullName;
            lblDate.Text = _App.Date.ToShortDateString();
            lblStatusDate.Text = _App.LastStatusDate.ToShortDateString();
            lblCreatedBy.Text = _App.UserInfo.Username;
        }

        public void LoadApplicationInfo(int ApplicationID)
        {
            _App = clsApplicationsBusiness.FindBaseApplication(ApplicationID);

            if (_App == null)
            {
                MessageBox.Show("No Application with Application ID = " + ApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillApplicationInfo();
        }

        private void llPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PersonDetailsForm form = new PersonDetailsForm(_App.PersonID);
            form.ShowDialog();
            LoadApplicationInfo(_AppID);
        }
    }
}