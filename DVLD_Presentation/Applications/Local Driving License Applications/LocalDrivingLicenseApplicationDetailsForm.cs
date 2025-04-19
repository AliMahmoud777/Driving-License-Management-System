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
    public partial class LocalDrivingLicenseApplicationDetailsForm : Form
    {
        private int _LDLAppID = -1;
        public LocalDrivingLicenseApplicationDetailsForm(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();

            _LDLAppID = LocalDrivingLicenseApplicationID;
        }

        private void LocalDrivingLicenseApplicationDetailsForm_Load(object sender, EventArgs e)
        {
            ctrlLocalDrivingLicenseApplicationDetails1.LoadLocalDrivingLicenseApplicationInfo(_LDLAppID);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}