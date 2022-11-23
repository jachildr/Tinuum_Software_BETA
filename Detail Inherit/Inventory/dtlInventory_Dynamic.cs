using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Tinuum_Software_BETA.Detail_Inherit.Inventory
{
    public partial class dtlInventory_Dynamic : Tinuum_Software_BETA.FormDetail_Dynamic
    {
        public dtlInventory_Dynamic()
        {
            InitializeComponent();
            tbl_Name = "dtbInventoryDetail_SF"; //VALUES VIEW
            tbl_MajorDyna = "dtbInventoryDynamic_SF"; //RATES MAJOR
            tbl_Dynamic = "dtbInventoryDetailDynamic_SF"; //RATES MINOR
            frm = Application.OpenForms[1] as Form;
            dgv = Application.OpenForms[1].Controls["dataGridView1"] as DataGridView;
        }
        public override void fill_DataTable()
        {
            int r;
            int n;
            int c;

            // FILL DATAGRIDVIEW WITH DT VALUES
            if (Information.IsNumeric(dgv.CurrentCell.Value))
            {
                for (r = 0; r <= Mos_Const - 1; r++)
                {
                    for (n = 1; n <= myMethods.Period; n++)
                    {
                        dataGridView1.Rows[r].Cells[n].Value = dgv.CurrentCell.Value;
                    }
                }
            }
            else
            {
                try
                {
                    for (r = 0; r <= Mos_Const - 1; r++)
                    {
                        for (n = 1; n <= myMethods.Period; n++)
                        {
                            c = r + (n - 1) * Mos_Const + 1 + 1; // PLUS 2 EFFECTIVELY BECAUSE CELL FILL DATA STARTS ON COL 2 IN DATABASE
                            dataGridView1.Rows[r].Cells[n].Value = SQL_DETAIL.DBDT.Rows[frmRow][c];
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        public override void Write_Detail()
        {
            // NO NEED TO SET CURRENT CELL - SET ON CLICK EVENT IN PARENT FRM
            dgv.CurrentCell.Value = "Detail";
            dgv.CurrentCell.Selected = true;
            frm.Enabled = true;
        }

        public override void current_Cell()
        {
            // NOTHING
        }
        public override void btnCancel_Click(object sender, EventArgs e)
        {
            base.Form_Cancel();
            dgv.CurrentCell.Value = DBNull.Value;
            dgv.CurrentCell.Selected = true;
            frm.Enabled = true;
            this.Dispose();
        }
    }
}
