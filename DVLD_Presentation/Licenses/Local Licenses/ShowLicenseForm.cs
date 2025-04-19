using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Presentation.Licenses
{
    public partial class ShowLicenseForm : Form
    {
        private int _LicenseID = -1;
        public ShowLicenseForm(int LicenseID)
        {
            InitializeComponent();

            _LicenseID = LicenseID;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowLicenseForm_Load(object sender, EventArgs e)
        {
            ctrlLicenseDetails1.LoadLicenseInfo(_LicenseID);
        }
    }
}