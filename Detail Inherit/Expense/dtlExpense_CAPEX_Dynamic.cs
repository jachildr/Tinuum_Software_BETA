using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Tinuum_Software_BETA.Detail_Inherit.Expense
{
    public partial class dtlExpense_CAPEX_Dynamic : Tinuum_Software_BETA.Detail_Inherit.Expense.dtlExpense_Dynamic
    {
        public dtlExpense_CAPEX_Dynamic()
        {
            InitializeComponent();
            switch (dgvExpense_CAPEX._index)
            {
                case 8:
                    {
                        tbl_Name = "dtbExpenseCAPEXDetail_GeneralRate"; //VALUES VIEW
                        tbl_Dynamic = "dtbExpenseCAPEXDetailDynamic_GeneralRate"; //RATES MINOR
                        tbl_MajorDyna = "dtbExpenseCAPEXDynamic_GeneralRate"; //RATES MAJOR
                        frm = Application.OpenForms[1] as Form;
                        tab = frm.Controls["tabCtrl"] as TabControl;
                        dgv = tab.TabPages[1].Controls["dataGridView2"] as DataGridView;
                    }
                    break;
            }
        }
    }
}
