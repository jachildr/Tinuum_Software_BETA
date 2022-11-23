using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tinuum_Software_BETA.Detail_Inherit.Roster;

namespace Tinuum_Software_BETA.Icon_Masters
{
    public partial class FormRoster : Form
    {
        protected int count;
        protected string tbl_Inventory = "dtbInventoryVerse";
        protected SQLControl SQL_Facility = new SQLControl();
        protected List<string> Names = new List<string>();
        public FormRoster()
        {
            InitializeComponent();
            int i;

            SQL_Facility.ExecQuery("SELECT * FROM " + tbl_Inventory + ";");
            count = SQL_Facility.RecordCount;

            for (i = 0; i <= count - 1; i++)
            {
                Names.Add(SQL_Facility.DBDT.Rows[i][2].ToString());
            }
        }

        private void FormRoster_Load(object sender, EventArgs e)
        {
            int i;

            for (i = 1; i <= count; i++)
            {
                switch (i)
                {
                    case 1:
                        {
                            tabCtrl.TabPages[i - 1].Text = Names[i - 1];
                            
                            dgvRoster_Facility1 dgv = new dgvRoster_Facility1();
                            dgv.Add_Source(this.dataGridView1);
                            dgv.ClinicLoad(this.dataGridView1);

                            dataGridView1.CellEndEdit += new DataGridViewCellEventHandler(dgv.CellEdit);
                            dataGridView1.CellMouseClick += new DataGridViewCellMouseEventHandler(dgv.CellMouseClick);
                            dataGridView1.CellValueChanged += new DataGridViewCellEventHandler(dgv.CellValueChanged);
                            dataGridView1.DataError += new DataGridViewDataErrorEventHandler(dgv.DataError);
                        }
                        break;
                    case 2:
                        {
                            tabCtrl.TabPages[i - 1].Text = Names[i - 1];

                            dgvRoster_Facility2 dgv = new dgvRoster_Facility2();
                            dgv.Add_Source(this.dataGridView2);
                            dgv.ClinicLoad(this.dataGridView2);

                            dataGridView2.CellEndEdit += new DataGridViewCellEventHandler(dgv.CellEdit);
                            dataGridView2.CellMouseClick += new DataGridViewCellMouseEventHandler(dgv.CellMouseClick);
                            dataGridView2.CellValueChanged += new DataGridViewCellEventHandler(dgv.CellValueChanged);
                            dataGridView2.DataError += new DataGridViewDataErrorEventHandler(dgv.DataError);
                        }
                        break;
                    case 3:
                        {
                            tabCtrl.TabPages[i - 1].Text = Names[i - 1];

                            dgvRoster_Facility3 dgv = new dgvRoster_Facility3();
                            dgv.Add_Source(this.dataGridView3);
                            dgv.ClinicLoad(this.dataGridView3);

                            dataGridView3.CellEndEdit += new DataGridViewCellEventHandler(dgv.CellEdit);
                            dataGridView3.CellMouseClick += new DataGridViewCellMouseEventHandler(dgv.CellMouseClick);
                            dataGridView3.CellValueChanged += new DataGridViewCellEventHandler(dgv.CellValueChanged);
                            dataGridView3.DataError += new DataGridViewDataErrorEventHandler(dgv.DataError);
                        }
                        break;
                    case 4:
                        {
                            tabCtrl.TabPages[i - 1].Text = Names[i - 1];

                            dgvRoster_Facility4 dgv = new dgvRoster_Facility4();
                            dgv.Add_Source(this.dataGridView4);
                            dgv.ClinicLoad(this.dataGridView4);

                            dataGridView4.CellEndEdit += new DataGridViewCellEventHandler(dgv.CellEdit);
                            dataGridView4.CellMouseClick += new DataGridViewCellMouseEventHandler(dgv.CellMouseClick);
                            dataGridView4.CellValueChanged += new DataGridViewCellEventHandler(dgv.CellValueChanged);
                            dataGridView4.DataError += new DataGridViewDataErrorEventHandler(dgv.DataError);
                        }
                        break;
                    case 5:
                        {
                            tabCtrl.TabPages[i - 1].Text = Names[i - 1];

                            dgvRoster_Facility5 dgv = new dgvRoster_Facility5();
                            dgv.Add_Source(this.dataGridView5);
                            dgv.ClinicLoad(this.dataGridView5);

                            dataGridView5.CellEndEdit += new DataGridViewCellEventHandler(dgv.CellEdit);
                            dataGridView5.CellMouseClick += new DataGridViewCellMouseEventHandler(dgv.CellMouseClick);
                            dataGridView5.CellValueChanged += new DataGridViewCellEventHandler(dgv.CellValueChanged);
                            dataGridView5.DataError += new DataGridViewDataErrorEventHandler(dgv.DataError);
                        }
                        break;
                }
            }
        }

        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            switch (this.tabCtrl.SelectedIndex)
            {
                case 0:
                    {
                        dgvRoster_Facility1 dgv = new dgvRoster_Facility1();
                        dgv.Move_CTRLs(this.dataGridView1);
                    }
                    break;
                case 1:
                    {
                        dgvRoster_Facility2 dgv = new dgvRoster_Facility2();
                        dgv.Move_CTRLs(this.dataGridView2);
                    }
                    break;
                case 2:
                    {
                        dgvRoster_Facility3 dgv = new dgvRoster_Facility3();
                        dgv.Move_CTRLs(this.dataGridView3);
                    }
                    break;
                case 3:
                    {
                        dgvRoster_Facility4 dgv = new dgvRoster_Facility4();
                        dgv.Move_CTRLs(this.dataGridView4);
                    }
                    break;
                case 4:
                    {
                        dgvRoster_Facility5 dgv = new dgvRoster_Facility5();
                        dgv.Move_CTRLs(this.dataGridView5);
                    }
                    break;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            switch (this.tabCtrl.SelectedIndex)
            {
                case 0:
                    {
                        dgvRoster_Facility1 dgv = new dgvRoster_Facility1();
                        dgv.InsertUser(this.dataGridView1);
                    }
                    break;
                case 1:
                    {
                        dgvRoster_Facility2 dgv = new dgvRoster_Facility2();
                        dgv.InsertUser(this.dataGridView2);
                    }
                    break;
                case 2:
                    {
                        dgvRoster_Facility3 dgv = new dgvRoster_Facility3();
                        dgv.InsertUser(this.dataGridView3);
                    }
                    break;
                case 3:
                    {
                        dgvRoster_Facility4 dgv = new dgvRoster_Facility4();
                        dgv.InsertUser(this.dataGridView4);
                    }
                    break;
                case 4:
                    {
                        dgvRoster_Facility5 dgv = new dgvRoster_Facility5();
                        dgv.InsertUser(this.dataGridView5);
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
                        dgvRoster_Facility1 dgv = new dgvRoster_Facility1();
                        dgv.Insert_Sub(this.dataGridView1);
                    }
                    break;
                case 1:
                    {
                        dgvRoster_Facility2 dgv = new dgvRoster_Facility2();
                        dgv.Insert_Sub(this.dataGridView2);
                    }
                    break;
                case 2:
                    {
                        dgvRoster_Facility3 dgv = new dgvRoster_Facility3();
                        dgv.Insert_Sub(this.dataGridView3);
                    }
                    break;
                case 3:
                    {
                        dgvRoster_Facility4 dgv = new dgvRoster_Facility4();
                        dgv.Insert_Sub(this.dataGridView4);
                    }
                    break;
                case 4:
                    {
                        dgvRoster_Facility5 dgv = new dgvRoster_Facility5();
                        dgv.Insert_Sub(this.dataGridView5);
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
                        dgvRoster_Facility1 dgv = new dgvRoster_Facility1();
                        dgv.Delete_Command(this.dataGridView1);
                    }
                    break;
                case 1:
                    {
                        dgvRoster_Facility2 dgv = new dgvRoster_Facility2();
                        dgv.Delete_Command(this.dataGridView2);
                    }
                    break;
                case 2:
                    {
                        dgvRoster_Facility3 dgv = new dgvRoster_Facility3();
                        dgv.Delete_Command(this.dataGridView3);
                    }
                    break;
                case 3:
                    {
                        dgvRoster_Facility4 dgv = new dgvRoster_Facility4();
                        dgv.Delete_Command(this.dataGridView4);
                    }
                    break;
                case 4:
                    {
                        dgvRoster_Facility5 dgv = new dgvRoster_Facility5();
                        dgv.Delete_Command(this.dataGridView5);
                    }
                    break;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            int i;

            for (i = 1; i <= count; i++)
            {
                switch (i)
                {
                    case 1:
                        {
                            dgvRoster_Facility1 dgv = new dgvRoster_Facility1();
                            dgv.Cancel(this.dataGridView1);   
                        }
                        break;
                    case 2:
                        {
                            dgvRoster_Facility2 dgv = new dgvRoster_Facility2();
                            dgv.Cancel(this.dataGridView2);   
                        }
                        break;
                    case 3:
                        {
                            dgvRoster_Facility3 dgv = new dgvRoster_Facility3();
                            dgv.Cancel(this.dataGridView3);   
                        }
                        break;
                    case 4:
                        {
                            dgvRoster_Facility4 dgv = new dgvRoster_Facility4();
                            dgv.Cancel(this.dataGridView4);   
                        }
                        break;
                    case 5:
                        {
                            dgvRoster_Facility5 dgv = new dgvRoster_Facility5();
                            dgv.Cancel(this.dataGridView5);   
                        }
                        break;
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            int i;

            dgvRoster_Facility1.escape = 0;

            for (i = 1; i <= count; i++)
            {
                if (dgvRoster_Facility1.escape > 0) return;
                switch (i)
                {
                    case 1:
                        {
                            dgvRoster_Facility1 dgv = new dgvRoster_Facility1();
                            dgv.UpdateSQL(this.dataGridView1);
                        }
                        break;
                    case 2:
                        {
                            dgvRoster_Facility2 dgv = new dgvRoster_Facility2();
                            dgv.UpdateSQL(this.dataGridView2);
                        }
                        break;
                    case 3:
                        {
                            dgvRoster_Facility3 dgv = new dgvRoster_Facility3();
                            dgv.UpdateSQL(this.dataGridView3);
                        }
                        break;
                    case 4:
                        {
                            dgvRoster_Facility4 dgv = new dgvRoster_Facility4();
                            dgv.UpdateSQL(this.dataGridView4);
                        }
                        break;
                    case 5:
                        {
                            dgvRoster_Facility5 dgv = new dgvRoster_Facility5();
                            dgv.UpdateSQL(this.dataGridView5);
                        }
                        break;
                }
            }
        }

        private void FormRoster_FormClosing(object sender, FormClosingEventArgs e)
        {
            int i;

            var switchExpr = dgvRoster_Facility1.Rslt_Cncl;
            switch (switchExpr)
            {
                case null:
                    {
                        for (i = 1; i <= count; i++)
                        {
                            switch (i)
                            {
                                case 1:
                                    {
                                        dgvRoster_Facility1 dgv = new dgvRoster_Facility1();
                                        dgv.Cancel(this.dataGridView1);
                                    }
                                    break;
                                case 2:
                                    {
                                        dgvRoster_Facility2 dgv = new dgvRoster_Facility2();
                                        dgv.Cancel(this.dataGridView2);
                                    }
                                    break;
                                case 3:
                                    {
                                        dgvRoster_Facility3 dgv = new dgvRoster_Facility3();
                                        dgv.Cancel(this.dataGridView3);
                                    }
                                    break;
                                case 4:
                                    {
                                        dgvRoster_Facility4 dgv = new dgvRoster_Facility4();
                                        dgv.Cancel(this.dataGridView4);
                                    }
                                    break;
                                case 5:
                                    {
                                        dgvRoster_Facility5 dgv = new dgvRoster_Facility5();
                                        dgv.Cancel(this.dataGridView5);
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
            dgvRoster_Facility1.Rslt_Cncl = null;
        }

        private void tabCtrl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPageIndex < count) return;
            e.Cancel = true;
        }
    }
}
