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
    public partial class EditApplicationTypesForm : Form
    {
        private int _AppTypeID = -1;
        private clsApplicationTypesBusiness _AppType;
        public EditApplicationTypesForm(int ApplicationTypeID)
        {
            InitializeComponent();

            _AppTypeID = ApplicationTypeID;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid! Put the mouse over the red icon(s) to see the errors", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _AppType.Title = txtTitle.Text;
            _AppType.Fees = Convert.ToSingle(txtFees.Text);

            if (_AppType.Save())
                MessageBox.Show("Application Type saved successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                MessageBox.Show("Application Type wasn't saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void _LoadApplicationTypeData()
        {
            _AppType = clsApplicationTypesBusiness.Find(_AppTypeID);

            if(_AppType == null)
            {
                MessageBox.Show("No application type with ID :- " + _AppTypeID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblID.Text = _AppType.ID.ToString();
            txtTitle.Text = _AppType.Title;
            txtFees.Text = _AppType.Fees.ToString();
        }

        private void EditApplicationTypesForm_Load(object sender, EventArgs e)
        {
            _LoadApplicationTypeData();
        }

        private void txtFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }
    }
}