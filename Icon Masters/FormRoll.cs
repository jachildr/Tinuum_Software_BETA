using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tinuum_Software_BETA.Detail_Inherit.Roll;

namespace Tinuum_Software_BETA.Icon_Masters
{
    public partial class FormRoll : Form
    {
        public FormRoll()
        {
            InitializeComponent();
        }

        private void FromRoll_Load(object sender, EventArgs e)
        {
            int i;

            for (i = 0; i <= tabCtrl.TabCount - 1; i++)
            {
                switch (i)
                {
                    case 0:
                        {
                            dgvRoll_Clinical dgv = new dgvRoll_Clinical();
                            dgv.Add_Source(this.dataGridView1);
                            dgv.ClinicLoad(this.dataGridView1);

                            dataGridView1.CellEndEdit += new DataGridViewCellEventHandler(dgv.CellEdit);
                            dataGridView1.CellMouseClick += new DataGridViewCellMouseEventHandler(dgv.CellMouseClick);
                            dataGridView1.CellValueChanged += new DataGridViewCellEventHandler(dgv.CellValueChanged);
                        }
                        break;
                    case 1:
                        {
                            dgvRoll_Discharge dgv = new dgvRoll_Discharge();
                            dgv.Add_Source(this.dataGridView2);
                            dgv.ClinicLoad(this.dataGridView2);

                            dataGridView2.CellEndEdit += new DataGridViewCellEventHandler(dgv.CellEdit);
                            dataGridView2.CellMouseClick += new DataGridViewCellMouseEventHandler(dgv.CellMouseClick);
                            dataGridView2.CellValueChanged += new DataGridViewCellEventHandler(dgv.CellValueChanged);
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
                        dgvRoll_Clinical dgv = new dgvRoll_Clinical();
                        dgv.InsertUser(this.dataGridView1);
                    }
                    break;
                case 1:
                    {
                        dgvRoll_Discharge dgv = new dgvRoll_Discharge();
                        dgv.InsertUser(this.dataGridView2);
                    }
                    break;
            }
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            switch (this.tabCtrl.SelectedIndex)
            {
                case 0:
                    {
                        dgvRoll_Clinical dgv = new dgvRoll_Clinical();
                        dgv.Delete_Command(this.dataGridView1);
                    }
                    break;
                case 1:
                    {
                        dgvRoll_Discharge dgv = new dgvRoll_Discharge();
                        dgv.Delete_Command(this.dataGridView2);
                    }
                    break;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            int i;

            for (i = 0; i <= tabCtrl.TabCount - 1; i++)
            {
                switch (i)
                {
                    case 0:
                        {
                            dgvRoll_Clinical dgv = new dgvRoll_Clinical();
                            dgv.Cancel(this.dataGridView1);
                        }
                        break;
                    case 1:
                        {
                            dgvRoll_Discharge dgv = new dgvRoll_Discharge();
                            dgv.Cancel(this.dataGridView2);
                        }
                        break;
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            int i;

            dgvRoll_Clinical.escapeNum = 0;

            for (i = 0; i <= tabCtrl.TabCount - 1; i++)
            {
                if (dgvRoll_Clinical.escapeNum > 0) return;
                switch (i)
                {
                    case 0:
                        {
                            tabCtrl.SelectedIndex = 0;
                            dgvRoll_Clinical dgv = new dgvRoll_Clinical();
                            dgv.UpdateSQL(this.dataGridView1);
                        }
                        break;
                    case 1:
                        {
                            tabCtrl.SelectedIndex = 1;
                            dgvRoll_Discharge dgv = new dgvRoll_Discharge();
                            dgv.UpdateSQL(this.dataGridView2);
                        }
                        break;
                }
            }
        }

        private void FormRoll_FormClosing(object sender, FormClosingEventArgs e)
        {
            int i;

            var switchExpr = dgvRoll_Clinical.Rslt_Cncl;
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
                                        dgvRoll_Clinical dgv = new dgvRoll_Clinical();
                                        dgv.Cancel(this.dataGridView1);
                                    }
                                    break;
                                case 1:
                                    {
                                        dgvRoll_Discharge dgv = new dgvRoll_Discharge();
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

            dgvRoll_Clinical.Rslt_Cncl = null;
        }
    }
}
