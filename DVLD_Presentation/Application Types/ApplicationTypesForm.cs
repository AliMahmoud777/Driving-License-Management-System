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
    public partial class ApplicationTypesForm : Form
    {
        private DataTable _dtAllApplicationTypes = clsApplicationTypesBusiness.GetAllApplicationTypes();
        public ApplicationTypesForm()
        {
            InitializeComponent();
        }

        private void _RefreshApplicationTypesList()
        {
            _dtAllApplicationTypes = clsApplicationTypesBusiness.GetAllApplicationTypes();
            dgvApplicationTypes.DataSource = _dtAllApplicationTypes;
            lblCount.Text = dgvApplicationTypes.Rows.Count.ToString();
        }

        private void ApplicationTypesForm_Load(object sender, EventArgs e)
        {
            _RefreshApplicationTypesList();

            if (dgvApplicationTypes.Rows.Count > 0)
            {
                dgvApplicationTypes.Columns[0].HeaderText = "ID";

                dgvApplicationTypes.Columns[1].HeaderText = "Title";

                dgvApplicationTypes.Columns[2].HeaderText = "Fees";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EditApplicationTypesForm form = new EditApplicationTypesForm((int)dgvApplicationTypes.CurrentRow.Cells[0].Value);
            form.ShowDialog();
            _RefreshApplicationTypesList();
        }
    }
}