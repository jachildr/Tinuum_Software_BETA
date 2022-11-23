using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Tinuum_Software_BETA.Detail_Inherit.Inventory
{
    public partial class dtlInventory_Collection : Tinuum_Software_BETA.Detail_Masters.FormDetail_Collection
    {
        public dtlInventory_Collection()
        {
            InitializeComponent();
            frm = Application.OpenForms[1] as Form;
            dgv = Application.OpenForms[1].Controls["dataGridView1"] as DataGridView;
            tbl_Configure = "dtbInventoryConfigureStar";
            tbl_Detail = "dtbInventoryDetail_Star";
        }
        public override void Write_Detail()
        {
            dgv.CurrentCell = dgv.Rows[frmRow].Cells[frmCol];
            dgv.CurrentCell.Value = "Detail";
            frm.Enabled = true;
        }

        public override void Form_Cancel()
        {
            dgv.CurrentCell = dgv.Rows[frmRow].Cells[frmCol];
            dgv.CurrentCell.Value = DBNull.Value;
            frm.Enabled = true;
        }
    }
}
