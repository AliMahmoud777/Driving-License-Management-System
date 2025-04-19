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
    public partial class TestTypesForm : Form
    {
        private DataTable _dtAllTestTypes = clsTestTypesBusiness.GetAllTestTypes();
        public TestTypesForm()
        {
            InitializeComponent();
        }

        private void _RefreshTestTypesList()
        {
            _dtAllTestTypes = clsTestTypesBusiness.GetAllTestTypes();
            dgvTestTypes.DataSource = _dtAllTestTypes;
            lblCount.Text = dgvTestTypes.Rows.Count.ToString();
        }

        private void TestTypesForm_Load(object sender, EventArgs e)
        {
            _RefreshTestTypesList();

            if (dgvTestTypes.Rows.Count > 0)
            {
                dgvTestTypes.Columns[0].HeaderText = "ID";

                dgvTestTypes.Columns[1].HeaderText = "Title";

                dgvTestTypes.Columns[2].HeaderText = "Description";

                dgvTestTypes.Columns[3].HeaderText = "Fees";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EditTestTypesForm form = new EditTestTypesForm((clsTestTypesBusiness.enTestType)dgvTestTypes.CurrentRow.Cells[0].Value);
            form.ShowDialog();
            _RefreshTestTypesList();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}