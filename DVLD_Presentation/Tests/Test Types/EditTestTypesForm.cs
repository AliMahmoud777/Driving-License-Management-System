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
    public partial class EditTestTypesForm : Form
    {
        private clsTestTypesBusiness.enTestType _TestTypeID = clsTestTypesBusiness.enTestType.Vision;
        private clsTestTypesBusiness _TestType = new clsTestTypesBusiness();
        public EditTestTypesForm(clsTestTypesBusiness.enTestType TestTypeID)
        {
            InitializeComponent();

            _TestTypeID = TestTypeID;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _LoadTestTypeData()
        {
            _TestType = clsTestTypesBusiness.Find(_TestTypeID);

            if (_TestType == null)
            {
                MessageBox.Show("No test type with ID :- " + _TestTypeID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblID.Text = ((int)_TestType.ID).ToString();
            txtTitle.Text = _TestType.Title;
            txtDescription.Text = _TestType.Description;
            txtFees.Text = _TestType.Fees.ToString();
        }


        private void EditTestTypesForm_Load(object sender, EventArgs e)
        {
            _LoadTestTypeData();
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                e.Cancel = true;
                txtTitle.Focus();
                errorProvider1.SetError(txtTitle, "Title field cannot be empty");
            }
            else
            {
                e.Cancel = false;
            }
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFees.Text))
            {
                e.Cancel = true;
                txtFees.Focus();
                errorProvider1.SetError(txtFees, "Fees field cannot be empty");
            }
            else
            {
                e.Cancel = false;
            }
        }

        private void txtDescription_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                e.Cancel = true;
                txtDescription.Focus();
                errorProvider1.SetError(txtDescription, "Description field cannot be empty");
            }
            else
            {
                e.Cancel = false;
            }
        }

        private void txtFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if((e.KeyChar == '.') && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid! Put the mouse over the red icon(s) to see the errors", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _TestType.Title = txtTitle.Text;
            _TestType.Description = txtDescription.Text;
            _TestType.Fees = Convert.ToSingle(txtFees.Text);

            if (_TestType.Save())
                MessageBox.Show("Test Type saved successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                MessageBox.Show("Test Type wasn't saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}