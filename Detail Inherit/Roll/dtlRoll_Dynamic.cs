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
    public partial class dtlRoll_Dynamic : Tinuum_Software_BETA.Detail_Inherit.Inventory.dtlInventory_Dynamic
    {
        public dtlRoll_Dynamic()
        {
            InitializeComponent();
            switch (dgvRoll_Clinical._index)
            {
                case 5:
                    {
                        tbl_Name = "dtbRoll_PPSRate" + FormRoll_PPS._primeKey; //VALUES VIEW
                        tbl_MajorDyna = "dtbRollDynamic_PPSRate" + FormRoll_PPS._primeKey; //RATES MAJOR
                        tbl_Dynamic = "dtbRollDetailDynamic_PPSRate" + FormRoll_PPS._primeKey; //RATES MINOR
                        frm = Application.OpenForms[3] as Form;
                        dgv = Application.OpenForms[3].Controls["dataGridView1"] as DataGridView;
                    }
                    break;
                case 10:
                    {
                        tbl_Name = "dtbRoll_MedicaidRate" + FormRollMedicaid._primeKey; //VALUES VIEW
                        tbl_MajorDyna = "dtbRollDynamic_MedicaidRate" + FormRollMedicaid._primeKey; //RATES MAJOR
                        tbl_Dynamic = "dtbRollDetailDynamic_MedicaidRate" + FormRollMedicaid._primeKey; //RATES MINOR
                        frm = Application.OpenForms[3] as Form;
                        dgv = Application.OpenForms[3].Controls["dataGridView1"] as DataGridView;
                    }
                    break;
                case 15:
                    {
                        tbl_Name = "dtbRoll_PrivatePayRate" + FormRollPrivatePay._primeKey; //VALUES VIEW
                        tbl_MajorDyna = "dtbRollDynamic_PrivatePayRate" + FormRollPrivatePay._primeKey; //RATES MAJOR
                        tbl_Dynamic = "dtbRollDetailDynamic_PrivatePayRate" + FormRollPrivatePay._primeKey; //RATES MINOR
                        frm = Application.OpenForms[3] as Form;
                        dgv = Application.OpenForms[3].Controls["dataGridView1"] as DataGridView;
                    }
                    break;

                case 20:
                    {
                        tbl_Name = "dtbRoll_MCOcareRate" + FormRollMCOcare._primeKey; //VALUES VIEW
                        tbl_MajorDyna = "dtbRollDynamic_MCOcareRate" + FormRollMCOcare._primeKey; //RATES MAJOR
                        tbl_Dynamic = "dtbRollDetailDynamic_MCOcareRate" + FormRollMCOcare._primeKey; //RATES MINOR
                        frm = Application.OpenForms[3] as Form;
                        dgv = Application.OpenForms[3].Controls["dataGridView1"] as DataGridView;
                    }
                    break;
                case 25:
                    {
                        tbl_Name = "dtbRoll_MCOcaidRate" + FormRollMCOcaid._primeKey; //VALUES VIEW
                        tbl_MajorDyna = "dtbRollDynamic_MCOcaidRate" + FormRollMCOcaid._primeKey; //RATES MAJOR
                        tbl_Dynamic = "dtbRollDetailDynamic_MCOcaidRate" + FormRollMCOcaid._primeKey; //RATES MINOR
                        frm = Application.OpenForms[3] as Form;
                        dgv = Application.OpenForms[3].Controls["dataGridView1"] as DataGridView;
                    }
                    break;
                case 30:
                    {
                        tbl_Name = "dtbRoll_VetsRate" + FormRollVets._primeKey; //VALUES VIEW
                        tbl_MajorDyna = "dtbRollDynamic_VetsRate" + FormRollVets._primeKey; //RATES MAJOR
                        tbl_Dynamic = "dtbRollDetailDynamic_VetsRate" + FormRollVets._primeKey; //RATES MINOR
                        frm = Application.OpenForms[3] as Form;
                        dgv = Application.OpenForms[3].Controls["dataGridView1"] as DataGridView;
                    }
                    break;
                case 35:
                    {
                        tbl_Name = "dtbRoll_OtherRate" + FormRollOther._primeKey; //VALUES VIEW
                        tbl_MajorDyna = "dtbRollDynamic_OtherRate" + FormRollOther._primeKey; //RATES MAJOR
                        tbl_Dynamic = "dtbRollDetailDynamic_OtherRate" + FormRollOther._primeKey; //RATES MINOR
                        frm = Application.OpenForms[3] as Form;
                        dgv = Application.OpenForms[3].Controls["dataGridView1"] as DataGridView;
                    }
                    break;
            }
            
        }
        public override void btnCancel_Click(object sender, EventArgs e)
        {
            if (dgvRoll_Clinical._index == 5)
            {
                base.Form_Cancel();
                dgv.CurrentCell.Value = null;
                dgv.CurrentCell.Selected = true;
                frm.Enabled = true;
                this.Dispose();
            }
            else
            {
                base.btnCancel_Click(sender, e);
            }
            
        }
    }
}
