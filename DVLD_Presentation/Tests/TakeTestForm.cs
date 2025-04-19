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

namespace DVLD_Presentation.Tests
{
    public partial class TakeTestForm : Form
    {
        private int _TestAppointmentID = -1;

        public TakeTestForm(int TestAppointmentID, clsTestTypesBusiness.enTestType TestType)
        {
            InitializeComponent();

            ctrlTakeTest1.TestType = TestType;
            _TestAppointmentID = TestAppointmentID;
        }

        private void _CloseForm()
        {
            this.Close();
        }

        private void TakeTestForm_Load(object sender, EventArgs e)
        {
            ctrlTakeTest1.ClosingParentForm += _CloseForm;
            ctrlTakeTest1.LoadTestData(_TestAppointmentID);
        }
    }
}
