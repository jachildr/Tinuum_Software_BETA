using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Tinuum_Software_BETA.Detail_Inherit.Roll
{
    public partial class dtlRoll_Dynamic_Discharges : Tinuum_Software_BETA.Detail_Inherit.Inventory.dtlInventory_Dynamic
    {
        protected TabControl tab;
        public dtlRoll_Dynamic_Discharges()
        {
            InitializeComponent();
            switch (dgvRoll_Discharge._index)
            {
                case 6:
                    {
                        tbl_Name = "dtbRollDetail_Downtime"; //VALUES VIEW
                        tbl_MajorDyna = "dtbRollDynamic_Downtime"; //RATES MAJOR
                        tbl_Dynamic = "dtbRollDetailDynamic_Downtime"; //RATES MINOR
                        frm = Application.OpenForms[1] as Form;
                        tab = frm.Controls["tabCtrl"] as TabControl;
                        dgv = tab.TabPages[1].Controls["dataGridView2"] as DataGridView;
                    }
                    break;
                case 11:
                    {
                        tbl_Name = "dtbRollDetail_Maintenance"; //VALUES VIEW
                        tbl_MajorDyna = "dtbRollDynamic_Maintenance"; //RATES MAJOR
                        tbl_Dynamic = "dtbRollDetailDynamic_Maintenance"; //RATES MINOR
                        frm = Application.OpenForms[1] as Form;
                        tab = frm.Controls["tabCtrl"] as TabControl;
                        dgv = tab.TabPages[1].Controls["dataGridView2"] as DataGridView;
                    }
                    break;
                case 16:
                    {
                        tbl_Name = "dtbRollDetail_Placement"; //VALUES VIEW
                        tbl_MajorDyna = "dtbRollDynamic_Placement"; //RATES MAJOR
                        tbl_Dynamic = "dtbRollDetailDynamic_Placement"; //RATES MINOR
                        frm = Application.OpenForms[1] as Form;
                        tab = frm.Controls["tabCtrl"] as TabControl;
                        dgv = tab.TabPages[1].Controls["dataGridView2"] as DataGridView;
                    }
                    break;
            }
        }
        public override void btnCancel_Click(object sender, EventArgs e)
        {
            base.Form_Cancel();
            dgv.CurrentCell.Value = null;
            dgv.CurrentCell.Selected = true;
            frm.Enabled = true;
            this.Dispose();
        }
    }
}
