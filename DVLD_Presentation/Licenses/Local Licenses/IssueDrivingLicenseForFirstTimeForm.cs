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
    public partial class IssueDrivingLicenseForFirstTimeForm : Form
    {
        private int _LDLAppID = -1;
        private clsLocalDrivingLicenseApplicationsBusiness _LDLApp;
        
        public IssueDrivingLicenseForFirstTimeForm(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();

            _LDLAppID = LocalDrivingLicenseApplicationID;
        }

        private void lblNotes_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void IssueDrivingLicenseForFirstTimeForm_Load(object sender, EventArgs e)
        {
            _LDLApp = clsLocalDrivingLicenseApplicationsBusiness.Find(_LDLAppID);

            if(_LDLApp == null)
            {
                MessageBox.Show("No Applicaiton with ID =" + _LDLAppID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            ctrlLocalDrivingLicenseApplicationDetails1.LoadLocalDrivingLicenseApplicationInfo(_LDLApp.LocalDrivingLicenseApplicationID);
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            int LicenseID = _LDLApp.IssueLicenseForFirstTime(lblNotes.Text.Trim(), clsGlobal.CurrentUser.ID);

            if(LicenseID != -1)
            {
                MessageBox.Show("License Issued Successfully with License ID = " + LicenseID.ToString(),
                    "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("License wasn't Issued",
                 "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}