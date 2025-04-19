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
    public partial class ShowPersonLicensesHistoryForm : Form
    {
        private int _PersonID = -1;
        public ShowPersonLicensesHistoryForm(int PersonID)
        {
            InitializeComponent();

            _PersonID = PersonID;
        }

        private void ShowPersonLicensesHistoryForm_Load(object sender, EventArgs e)
        {
            ctrlPersonDetails1.LoadPersonInfo(_PersonID);
            ctrlPersonLicensesHistory1.LoadLicensesInfoByPersonID(_PersonID);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}