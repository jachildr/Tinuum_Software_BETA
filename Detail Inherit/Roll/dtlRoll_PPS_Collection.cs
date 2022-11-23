using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Tinuum_Software_BETA.Popups.Roll;


namespace Tinuum_Software_BETA.Detail_Inherit.Roll
{
    public partial class dtlRoll_PPS_Collection : Tinuum_Software_BETA.Detail_Masters.FormDetail_Collection
    {
        public dtlRoll_PPS_Collection()
        {
            InitializeComponent();
            frm = Application.OpenForms[3] as Form;
            dgv = Application.OpenForms[3].Controls["dataGridView1"] as DataGridView;
            tbl_Configure = "dtbRollConfigureMDS";
            tbl_Detail = "dtbRollDetail_Assess" + FormRoll_PPS._primeKey;
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
