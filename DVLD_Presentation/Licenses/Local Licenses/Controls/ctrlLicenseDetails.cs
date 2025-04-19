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
using System.IO;
using DVLD_Presentation.Properties;

namespace DVLD_Presentation.Licenses
{
    public partial class ctrlLicenseDetails : UserControl
    {
        private clsLicensesBusiness _License;
        private int _LicenseID = -1;

        public int LicenseID
        {
            get
            {
                return _LicenseID;
            }
        }

        public clsLicensesBusiness License
        {
            get
            {
                return _License;
            }
        }
        
        public ctrlLicenseDetails()
        {
            InitializeComponent();
        }

        private void _LoadPersonImage()
        {
            if (_License.ApplicationInfo.PersonInfo.Gender == 0)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            string ImagePath = _License.ApplicationInfo.PersonInfo.ImagePath;

            if(ImagePath != "")
            {
                if (File.Exists(ImagePath))
                    pbPersonImage.ImageLocation = ImagePath;
                else
                    MessageBox.Show("Could not find this image: " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _FillLicenseInfo()
        {
            _LicenseID = _License.ID;
            lblClass.Text = _License.LicenseClassInfo.Name;
            lblName.Text = _License.ApplicationInfo.PersonInfo.FullName;
            lblLicenseID.Text = _License.ID.ToString();
            lblNationalNo.Text = _License.ApplicationInfo.PersonInfo.NationalNo;
            lblGender.Text = (_License.ApplicationInfo.PersonInfo.Gender == 0) ? "Male" : "Female";
            lblIssueDate.Text = _License.IssueDate.ToShortDateString();
            lblIssueReason.Text = _License.IssueReasonText;
            lblNotes.Text = (_License.Notes != "") ? _License.Notes : "No Notes";
            lblActivation.Text = (_License.IsActive) ? "Yes" : "No";
            lblDateOfBirth.Text = _License.ApplicationInfo.PersonInfo.DateOfBirth.ToShortDateString();
            lblDriverID.Text = _License.DriverID.ToString();
            lblExpirationDate.Text = _License.ExpirationDate.ToShortDateString();
            lblDetentionStatus.Text = (_License.IsDetained) ? "Yes" : "No";

            _LoadPersonImage();
        }

        public void LoadLicenseInfo(int LicenseID)
        {
            _License = clsLicensesBusiness.Find(LicenseID);

            if(_License == null)
            {
                MessageBox.Show("No License with ID :- " + LicenseID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillLicenseInfo();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
