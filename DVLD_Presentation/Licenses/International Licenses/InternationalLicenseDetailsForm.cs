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
    public partial class InternationalLicenseDetailsForm : Form
    {
        private int _InternationalLicenseID = -1;
        public InternationalLicenseDetailsForm(int InternationalLicenseID)
        {
            InitializeComponent();

            _InternationalLicenseID = InternationalLicenseID;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InternationalLicenseDetailsForm_Load(object sender, EventArgs e)
        {
            ctrlInternationalLicenseDetails1.LoadInternationalLicenseInfo(_InternationalLicenseID);
        }
    }
}