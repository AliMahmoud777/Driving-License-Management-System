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
    public partial class PersonDetailsForm : Form
    {
        public PersonDetailsForm(int PersonID)
        {
            InitializeComponent();

            ctrlPersonDetails1.LoadPersonInfo(PersonID);
        }

        public PersonDetailsForm(string NationalNo)
        {
            InitializeComponent();

            ctrlPersonDetails1.LoadPersonInfo(NationalNo);
        }

        private void PersonDetailsForm__Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
