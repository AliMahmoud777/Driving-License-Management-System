using System;
using DVLD_Business;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DVLD_Presentation.Properties;

namespace DVLD_Presentation
{
    public partial class ctrlInternationalLicenseDetails : UserControl
    {
        private clsInternationalLicensesBusiness _InternationalLicense;
        public ctrlInternationalLicenseDetails()
        {
            InitializeComponent();
        }

        private void _LoadPersonImage()
        {
            if (_InternationalLicense.ApplicationInfo.PersonInfo.Gender == 0)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            string ImagePath = _InternationalLicense.ApplicationInfo.PersonInfo.ImagePath;

            if (ImagePath != "")
            {
                if (File.Exists(ImagePath))
                    pbPersonImage.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image: " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _FillInternationalLicenseInfo()
        {
            lblName.Text = _InternationalLicense.ApplicationInfo.PersonInfo.FullName;
            lblInternationalLicenseID.Text = _InternationalLicense.InternationalLicenseID.ToString();
            lblNationalNo.Text = _InternationalLicense.ApplicationInfo.PersonInfo.NationalNo;
            lblGender.Text = (_InternationalLicense.ApplicationInfo.PersonInfo.Gender == 0) ? "Male" : "Female";
            lblIssueDate.Text = _InternationalLicense.IssueDate.ToShortDateString();
            lblApplicationID.Text = _InternationalLicense.ApplicationID.ToString();
            lblActivation.Text = (_InternationalLicense.IsActive) ? "Yes" : "No";
            lblDateOfBirth.Text = _InternationalLicense.ApplicationInfo.PersonInfo.DateOfBirth.ToShortDateString();
            lblExpirationDate.Text = _InternationalLicense.ExpirationDate.ToShortDateString();

            _LoadPersonImage();
        }

        public void LoadInternationalLicenseInfo(int InternationalLicenseID)
        {
            _InternationalLicense = clsInternationalLicensesBusiness.Find(InternationalLicenseID);

            if(_InternationalLicense == null)
            {
                MessageBox.Show("Could not find International License ID = " + InternationalLicenseID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillInternationalLicenseInfo();
        }
    }
}