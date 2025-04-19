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
using DVLD_Presentation.Licenses;

namespace DVLD_Presentation
{
    public partial class ctrlLocalDrivingLicenseApplicationDetails : UserControl
    {
        private int _LDLAppID = -1;
        private clsLocalDrivingLicenseApplicationsBusiness _LDLApp;
        private int _LicenseID = -1;

        public int LocalDrivingLicenseApplicationID
        {
            get { return _LDLAppID; }
        }
        public ctrlLocalDrivingLicenseApplicationDetails()
        {
            InitializeComponent();
        }

        private void _FillLocalDrivingLicenseApplicationData()
        {
            _LicenseID  = clsLicensesBusiness.GetActiveLicenseIDByPersonID(_LDLApp.PersonID, _LDLApp.LicenseClassID);
            llLicenseInfo.Enabled = (_LicenseID != -1);

            ctrlApplicationDetails1.LoadApplicationInfo(_LDLApp.ApplicationID);
            lblDLAppID.Text = _LDLApp.LocalDrivingLicenseApplicationID.ToString();
            lblClass.Text = _LDLApp.LicenseClassesInfo.Name;
            lblPassedTests.Text = _LDLApp.GetPassedTestsCount().ToString() + "/3";
        }

        public void LoadLocalDrivingLicenseApplicationInfo(int LocalDrivingLicenseApplicationID)
        {
            _LDLApp = clsLocalDrivingLicenseApplicationsBusiness.Find(LocalDrivingLicenseApplicationID);

            if(_LDLApp == null)
            {
                MessageBox.Show("No Local Driving License Application with ID = " + LocalDrivingLicenseApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillLocalDrivingLicenseApplicationData();
        }

        public void LoadLocalDrivingLicenseApplicationInfoByApplicationID(int ApplicationID)
        {
            _LDLApp = clsLocalDrivingLicenseApplicationsBusiness.FindByApplicationID(ApplicationID);

            if (_LDLApp == null)
            {
                MessageBox.Show("No Application with ID = " + ApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillLocalDrivingLicenseApplicationData();
        }

        private void ctrlLocalDrivingLicenseApplicationDetails_Load(object sender, EventArgs e)
        {

        }

        private void llLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowLicenseForm form = new ShowLicenseForm(_LicenseID);
            form.ShowDialog();
        }
    }
}
