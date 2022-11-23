using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tinuum_Software_BETA.Detail_Inherit.Expense;

namespace Tinuum_Software_BETA.Icon_Masters
{
    public partial class FormExpense : Form
    {
        public FormExpense()
        {
            InitializeComponent();
        }

        private void FormExpense_Load(object sender, EventArgs e)
        {
            int i;

            for (i = 0; i <= tabCtrl.TabCount - 1; i++)
            {
                switch (i)
                {
                    case 0:
                        {
                            dgvExpense_OPEX dgv = new dgvExpense_OPEX();
                            dgv.Add_Source(this.dataGridView1);
                            dgv.ClinicLoad(this.dataGridView1);

                            dataGridView1.CellEndEdit += new DataGridViewCellEventHandler(dgv.CellEdit);
                            dataGridView1.CellMouseClick += new DataGridViewCellMouseEventHandler(dgv.CellMouseClick);
                            dataGridView1.CellValueChanged += new DataGridViewCellEventHandler(dgv.CellValueChanged);
                            dataGridView1.DataError += new DataGridViewDataErrorEventHandler(dgv.DataError);
                        }
                        break;
                    case 1:
                        {
                            dgvExpense_CAPEX dgv = new dgvExpense_CAPEX();
                            dgv.Add_Source(this.dataGridView2);
                            dgv.ClinicLoad(this.dataGridView2);

                            dataGridView2.CellEndEdit += new DataGridViewCellEventHandler(dgv.CellEdit);
                            dataGridView2.CellMouseClick += new DataGridViewCellMouseEventHandler(dgv.CellMouseClick);
                            dataGridView2.CellValueChanged += new DataGridViewCellEventHandler(dgv.CellValueChanged);
                            dataGridView2.DataError += new DataGridViewDataErrorEventHandler(dgv.DataError);
                        }
                        break;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            switch (this.tabCtrl.SelectedIndex)
            {
                case 0:
                    {
                        dgvExpense_OPEX dgv = new dgvExpense_OPEX();
                        dgv.InsertUser(this.dataGridView1);
                    }
                    break;
                case 1:
                    {
                        dgvExpense_CAPEX dgv = new dgvExpense_CAPEX();
                        dgv.InsertUser(this.dataGridView2);
                    }
                    break;
            }

            
        }

        private void btnSub_Click(object sender, EventArgs e)
        {
            switch (this.tabCtrl.SelectedIndex)
            {
                case 0:
                    {
                        dgvExpense_OPEX dgv = new dgvExpense_OPEX();
                        dgv.Insert_Sub(this.dataGridView1);
                    }
                    break;
                case 1:
                    {
                        dgvExpense_CAPEX dgv = new dgvExpense_CAPEX();
                        dgv.Insert_Sub(this.dataGridView2);
                    }
                    break;
            }
        }

        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            switch (this.tabCtrl.SelectedIndex)
            {
                case 0:
                    {
                        dgvExpense_OPEX dgv = new dgvExpense_OPEX();
                        dgv.Move_CTRLs(this.dataGridView1);
                    }
                    break;
                case 1:
                    {
                        
                    }
                    break;
            }
            
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            int i;

            dgvExpense_OPEX.escapeEXP = 0;

            for (i = 0; i <= tabCtrl.TabCount - 1; i++)
            {
                if (dgvExpense_OPEX.escapeEXP > 0) return;
                switch (i)
                {
                    case 0:
                        {
                            tabCtrl.SelectedIndex = 0;
                            dgvExpense_OPEX dgv = new dgvExpense_OPEX();
                            dgv.UpdateSQL(this.dataGridView1);
                        }
                        break;
                    case 1:
                        {
                            tabCtrl.SelectedIndex = 1;
                            dgvExpense_CAPEX dgv = new dgvExpense_CAPEX();
                            dgv.UpdateSQL(this.dataGridView2);
                        }
                        break;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            switch (this.tabCtrl.SelectedIndex)
            {
                case 0:
                    {
                        dgvExpense_OPEX dgv = new dgvExpense_OPEX();
                        dgv.Delete_Command(this.dataGridView1);
                    }
                    break;
                case 1:
                    {
                        dgvExpense_CAPEX dgv = new dgvExpense_CAPEX();
                        dgv.Delete_Command(this.dataGridView2);
                    }
                    break;
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            dgvExpense_OPEX dgv = new dgvExpense_OPEX();
            dgv.Cancel(this.dataGridView1);
            
        }

        private void FormExpense_FormClosing(object sender, FormClosingEventArgs e)
        {
            int i; 
            var switchExpr = dgvExpense_OPEX.Rslt_Cncl;
            
            switch (switchExpr)
            {
                case null:
                    {
                        for (i = 0; i <= tabCtrl.TabCount - 1; i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    {
                                        dgvExpense_OPEX dgv = new dgvExpense_OPEX();
                                        dgv.Cancel(this.dataGridView1);
                                    }
                                    break;
                                case 1:
                                    {
                                        dgvExpense_CAPEX dgv = new dgvExpense_CAPEX();
                                        dgv.Cancel(this.dataGridView2);
                                    }
                                    break;
                            }
                        }
                        e.Cancel = true;
                        break;
                    }

                case "No":
                    {
                        e.Cancel = true;
                        break;
                    }

                case "Yes":
                    {
                        e.Cancel = false;
                        Dispose();
                        break;
                    }
            }

            dgvExpense_OPEX.Rslt_Cncl = null;
        }

        private void dataGridView2_Scroll(object sender, ScrollEventArgs e)
        {
            dgvExpense_CAPEX dgv = new dgvExpense_CAPEX();
            dgv.Move_CTRLs(this.dataGridView2);
        }
    }
}
