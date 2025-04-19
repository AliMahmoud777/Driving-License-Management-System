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
    public partial class EditTestAppointmentForm : Form
    {
        private int _LDLAppID = -1;
        private int _TestAppointmentID = -1;

        public EditTestAppointmentForm(int LocalDrivingLicenseApplicationID, clsTestTypesBusiness.enTestType TestType, 
            int TestAppointmentID = -1)
        {
            InitializeComponent();

            ctrlScheduleTest1.TestType = TestType;
            _LDLAppID = LocalDrivingLicenseApplicationID;
            _TestAppointmentID = TestAppointmentID;
        }

        private void EditTestAppointmentForm_Load(object sender, EventArgs e)
        {
            ctrlScheduleTest1.ClosingParentForm += CloseForm;
            ctrlScheduleTest1.LoadTestAppointmentInfo(_LDLAppID, _TestAppointmentID);    
        }

        private void CloseForm()
        {
            this.Close();
        }
    }
}