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

namespace DVLD_Presentation
{
    public partial class EditLocalDrivingLicenseApplicationForm : Form
    {
        private enum enMode { AddNew, Update };

        private enMode _Mode;
        private int _LDLAppID = -1;
        private clsLocalDrivingLicenseApplicationsBusiness _LDLApp;

        public EditLocalDrivingLicenseApplicationForm()
        {
            InitializeComponent();

            _Mode = enMode.AddNew;
        }
        public EditLocalDrivingLicenseApplicationForm(int LDLAppID)
        {
            InitializeComponent();

            _Mode = enMode.Update;
            _LDLAppID = LDLAppID;
        }

        private void _FillLicenseClassesInComboBox()
        {

            DataTable dtLicenseClasses = clsLicenseClassesBusiness.ListLicenseClasses();

            foreach (DataRow Row in dtLicenseClasses.Rows)
            {
                cbLicenseClass.Items.Add(Row["ClassName"]);
            }

        }

        private void _ResetDefaultValues()
        {
            if(_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Local Driving License Application";
                _LDLApp = new clsLocalDrivingLicenseApplicationsBusiness();
                tpApplicationInfo.Enabled = false;
            }
            else
            {
                lblTitle.Text = "Update Local Driving License Application";
                tpApplicationInfo.Enabled = true;
                btnSave.Enabled = true;
            }

            _FillLicenseClassesInComboBox();
            cbLicenseClass.SelectedIndex = 2;
            lblDate.Text = DateTime.Now.ToShortDateString();
            lblFees.Text = clsApplicationTypesBusiness.Find((int)clsApplicationsBusiness.enAppliactionType.NewLocalDrivingLicense).Fees.ToString();
            lblCreatedBy.Text = clsGlobal.CurrentUser.Username;
        }

        private void _LoadLDLApplicationData()
        {
            ctrlPersonDetailsWithFilter1.FilterEnabled = false;
            _LDLApp = clsLocalDrivingLicenseApplicationsBusiness.Find(_LDLAppID);

            if(_LDLApp == null)
            {
                MessageBox.Show("No Local Driving License Application with ID = " + _LDLAppID, "Application Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            ctrlPersonDetailsWithFilter1.LoadPersonInfo(_LDLApp.PersonID);
            lblID.Text = _LDLApp.LocalDrivingLicenseApplicationID.ToString();
            lblDate.Text = _LDLApp.Date.ToShortDateString();
            cbLicenseClass.SelectedIndex = cbLicenseClass.FindString(_LDLApp.LicenseClassesInfo.Name);
            lblFees.Text = _LDLApp.PaidFees.ToString();
            lblCreatedBy.Text = _LDLApp.UserInfo.Username;
        }

        private void EditLocalDrivingLicenseApplicationForm_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if (_Mode == enMode.Update)
                _LoadLDLApplicationData();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if(_Mode == enMode.Update)
            {
                tcLocalDrivingLicenseApplicationInfo.SelectedIndex = 1;
                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
                return;
            }

            if(ctrlPersonDetailsWithFilter1.PersonID != -1)
            {
                tcLocalDrivingLicenseApplicationInfo.SelectedIndex = 1;
                btnSave.Enabled = true;
                tpApplicationInfo.Enabled = true;
            }
            else
            {
                MessageBox.Show("Please, Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("An error occured!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int SelectedLicenseClassID = clsLicenseClassesBusiness.FindByName(cbLicenseClass.Text).ID;

            DateTime DateOfBirth = clsPeopleBusiness.Find(ctrlPersonDetailsWithFilter1.PersonID).DateOfBirth;

            byte DifferenceInYears = Convert.ToByte(DateTime.Now.Year - DateOfBirth.Year);

            if(DateTime.Now < DateOfBirth.AddYears(DifferenceInYears))
            {
                --DifferenceInYears;
            }

            if (clsLicenseClassesBusiness.Find(SelectedLicenseClassID).MinimumAllowedAge > DifferenceInYears)
            {
                MessageBox.Show("This Person hasn't reached the minimum allowed age for this license class", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int ActiveApplicationID = clsApplicationsBusiness.GetActiveApplicationID(ctrlPersonDetailsWithFilter1.PersonID,
                    (int)clsApplicationsBusiness.enAppliactionType.NewLocalDrivingLicense, SelectedLicenseClassID);

            if (_Mode == enMode.AddNew)
            {
                if (ActiveApplicationID != -1)
                {
                    MessageBox.Show("Choose another License Class, the selected Person Already has an active application for the selected class", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                if (ActiveApplicationID != _LDLApp.ApplicationID && ActiveApplicationID != -1)
                {
                    MessageBox.Show("Choose another License Class, the selected Person Already has an active application for the selected class", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if(clsLicensesBusiness.IsActiveLicenseExist(ctrlPersonDetailsWithFilter1.PersonID, SelectedLicenseClassID))
            {
                MessageBox.Show("Person already has a license with the same applied driving class, Choose diffrent driving class", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_Mode == enMode.AddNew)
            {
                _LDLApp.PersonID = ctrlPersonDetailsWithFilter1.PersonID;
                _LDLApp.Date = DateTime.Now;
                _LDLApp.TypeID = (int)clsApplicationsBusiness.enAppliactionType.NewLocalDrivingLicense;
                _LDLApp.Status = clsApplicationsBusiness.enStatus.New;
                _LDLApp.PaidFees = Convert.ToSingle(lblFees.Text);
                _LDLApp.UserID = clsGlobal.CurrentUser.ID;
            }

            _LDLApp.LastStatusDate = DateTime.Now;
            _LDLApp.LicenseClassID = SelectedLicenseClassID;

            if (_LDLApp.Save())
            {
                lblTitle.Text = "Update Local Driving License Application";
                lblID.Text = _LDLApp.LocalDrivingLicenseApplicationID.ToString();
                _Mode = enMode.Update;

                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error :- Data isn't Saved Successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}