using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Syncfusion.Windows.Forms.Tools;
namespace Tinuum_Software_BETA.Detail_Masters
{
    [CLSCompliant(true)]
    public partial class FormDetail_Collection : Tinuum_Software_BETA.FormDetail_Percent
    {
        protected Form frm = Application.OpenForms[1] as Form; //CHANGE
        protected DataGridView dgv = Application.OpenForms[1].Controls["dataGridView1"] as DataGridView; //CHANGE 
        protected DataRowView drv;
        protected SQLControl SQL_Configure = new SQLControl();
        protected string tbl_Configure = "dtbMarketConfigurePDPM";
        protected int Mos_Const = 12;
        protected string tbl_Detail = "dtbMarketDetail";
        protected int record;
        protected int terminate;
        protected int frmRow;
        protected int frmCol;

        public FormDetail_Collection()
        {
            InitializeComponent();
        }
        public override void frmDetail_Percent_Load(object sender, EventArgs e)
        {
            Form_Loader();
            column_Fill();
        }

        public override void Form_Loader()
        {
            if (DesignMode) return;

            myMethods.SQL_Grab();
            int i;
            int j;
            int r;
            int n;
            int c;
            string strNum;
            double intNum;
            int index =  0;
            int input;

            frmRow = dgv.CurrentCell.RowIndex;
            frmCol = dgv.CurrentCell.ColumnIndex;

            // SET DGV SPECS    
            dataGridView1.ColumnCount = myMethods.Period + 1;
            dataGridView1.RowCount = Mos_Const + 1;
            dataGridView1.Columns[0].HeaderText = "For The Year Ending:";
            dataGridView1.Columns[0].Frozen = true;
            dataGridView1.Columns[0].Width = 140;

            // FILL COLUMN HEADER TEXT
            for (i = 1; i <= myMethods.Period; i++)
            {
                var headerNme = myMethods.dteStart.AddMonths((i - 1) * Mos_Const - 1);
                dataGridView1.Columns[i].HeaderText = headerNme.ToString("MMM yyyy");
                dataGridView1.Columns[i].Width = 80;
            }

            // FILL MONTH OF YEAR VALUES
            for (i = 0; i <= Mos_Const - 1; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = myMethods.dteStart.AddMonths(i).ToString("MMMM");
            }

            // AVERAGE ANNUAL RATE VALUE & SET ROW READ ONLY
            dataGridView1.Rows[Mos_Const].Cells[0].Value = "Effective Annual Value";
            for (i = 1; i <= dataGridView1.ColumnCount - 1; i++)
            {
                dataGridView1.Rows[Mos_Const].Cells[i].ReadOnly = true;
            }

            // GET RECORDS FROM CONFIG DB
            SQL_Configure.ExecQuery("SELECT * FROM " + tbl_Configure + ";");
            record = SQL_Configure.RecordCount;

            // MAKE 1ST COLUMN READ ONLY
            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                dataGridView1.Rows[i].Cells[0].ReadOnly = true;
            }

            // FIRST COLUMN SPECS    
            dataGridView1.Columns[0].DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView1.Columns[0].DefaultCellStyle.SelectionForeColor = Color.Black;


            // MAKE DGV COLUMNS NOT SORTABLE
            foreach (DataGridViewColumn Col in dataGridView1.Columns)
            {
                Col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // COLUMN TEXT ALIGNMENT
            {
                dataGridView1.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView1.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                for (i = 1; i <= dataGridView1.ColumnCount - 1; i++)
                {
                    dataGridView1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

            }

            // MAKE LAST ROW READ ONLY
            dataGridView1.Rows[Mos_Const].DefaultCellStyle.SelectionBackColor = SystemColors.Control;
            dataGridView1.Rows[Mos_Const].DefaultCellStyle.SelectionForeColor = SystemColors.ControlDark;
            dataGridView1.Rows[Mos_Const].DefaultCellStyle.BackColor = SystemColors.Control;
            dataGridView1.Rows[Mos_Const].DefaultCellStyle.ForeColor = SystemColors.ControlDark;
            // dataGridView1.Rows[Mos_Const].DefaultCellStyle.Font = new Font("Sans Serif", 8.25F, FontStyle.Italic);

            // CHANGE TXT GRIDVIEW CELLS TO COMBO CELLS
            SQL_DETAIL.ExecQuery("SELECT * FROM " + tbl_Configure + ";");
            for (i = 1; i <= dataGridView1.ColumnCount - 1; i++)
            {
                for (j = 0; j <= Mos_Const - 1; j++)
                {       
                    var newCell = new DataGridViewComboBoxCell();
                    // ADD SPECS FOR COMBOCELL
                    newCell.DataSource = SQL_Configure.DBDT;
                    newCell.DisplayMember = "collection_groups";
                    newCell.ValueMember = "Prime";
                    newCell.FlatStyle = FlatStyle.Popup;
                    newCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                    newCell.DisplayStyleForCurrentCellOnly = false;
                    dataGridView1.Rows[j].Cells[i] = newCell;    
                }
            }

            // FILL DATAGRIDVIEW WITH DT VALUES 
            SQL_DETAIL.ExecQuery("SELECT * FROM " + tbl_Detail + ";");
            try
            {
                for (r = 0; r <= Mos_Const - 1; r++)
                {
                    for (n = 1; n <= myMethods.Period; n++)
                    {
                        c = r + (n - 1) * Mos_Const + 1 + 1; // PLUS 2 EFFECTIVELY BECAUSE CELL FILL DATA STARTS ON COL 2 IN DATABASE
                        for (i = 0; i <= record - 1; i++)
                        {
                            // CHECK IF DETAIL DB ENTRY EQUAL TO CONFIGURE PRIME KEY
                            if (SQL_DETAIL.DBDT.Rows[frmRow][c] == DBNull.Value || SQL_Configure.DBDT.Rows[i][0] == DBNull.Value) break;
                            if (Convert.ToInt32(SQL_DETAIL.DBDT.Rows[frmRow][c]) == Convert.ToInt32(SQL_Configure.DBDT.Rows[i][0]))
                            {
                                index += 1;
                                break;
                            }
                        }
                        // IF NOT IDENTIFIED, CHANGE TO FIRST ENTRY
                        if (index > 0)
                        {
                            input = i;
                        }
                        else
                        {
                            continue;
                        }
                        // CHANGE DISPLAY ELEMENT FROM PRIME KEY TO COLLECTION NAME
                        dataGridView1.Rows[r].Cells[n].Value = SQL_Configure.DBDT.Rows[input][0];
                        index = 0;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public override void DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == Mos_Const) return;
            column_Fill();
        }

        public override void btnSubmit_Click(object sender, EventArgs e)
        {
            int num = Convert.ToInt32(dgv.Rows[frmRow].Cells[0].Value);
            int r;
            int n;
            int mos_Num;
            string sel_cell;
            string tbl_Col;
            double dec_Val;
            string tbl_Name;
            string cmdUpdate;

            for (r = 0; r <= Mos_Const - 1; r++)
            {
                for (n = 1; n <= dataGridView1.ColumnCount - 1; n++)
                {
                    if (dataGridView1.Rows[r].Cells[n].Value == null)
                    {
                        MessageBox.Show("You must enter valid data before continuing.", "TINUUM SOFTWARE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            for (r = 0; r <= Mos_Const - 1; r++)
            {
                for (n = 1; n <= myMethods.Period; n++)
                {
                    sel_cell = Convert.ToString(dataGridView1.Rows[r].Cells[n].Value);
                    mos_Num = r + (n - 1) * Mos_Const + 1;
                    tbl_Col = "month" + mos_Num;
                    SQL_DETAIL.AddParam("@PrimKey", num);
                    SQL_DETAIL.AddParam("@months_data", sel_cell);
                    cmdUpdate = "UPDATE " + tbl_Detail + " SET " + tbl_Col + "=@months_data WHERE ID_Num=@PrimKey;";
                    SQL_DETAIL.ExecQuery(cmdUpdate);
                }
            }

            if (SQL_DETAIL.HasException(true))
                return;
            Write_Detail();
            frm.ActiveControl = null;
            Dispose();
        }

        public void column_Fill()
        {
            int i;
            int j;

            try
            {
                for (i = 1; i <= dataGridView1.ColumnCount - 1; i++)
                {
                    for (j = 0; j <= Mos_Const - 1; j++)
                    {
                        if (dataGridView1.Rows[j].Cells[i].Value == null)
                        {
                            break;
                        }
                    }
                    if (j < Mos_Const)
                    {
                        return;
                    }
                    else
                    {
                        dataGridView1.Rows[Mos_Const].Cells[i].Value = "(Collection)";
                    }
                    
                }

            }
            catch (Exception ex)
            {
            }
        }

        public virtual void Write_Detail()
        {
            int colStart = 5;
            int colEnd;
            int i;

            colEnd = colStart + myMethods.Period - 1;

            for (i = colStart; i <= colEnd; i++)
            {
                dgv.Rows[frmRow].Cells[i].Value = "Detail";
                dgv.Rows[frmRow].Cells[i].Selected = true;
            }
        }

        public override void btnRow_Click(object sender, EventArgs e)
        {
            int r;
            int n;
            int x;
            // Dim y As Integer

            r = dataGridView1.CurrentCell.RowIndex; // STATIC
            n = dataGridView1.CurrentCell.ColumnIndex; // STATIC
            try
            {
                for (x = n + 1; x <= dataGridView1.ColumnCount - 1; x++)
                {
                    dataGridView1.Rows[r].Cells[x].Value = dataGridView1.CurrentCell.Value;
                    dataGridView1.Rows[r].Cells[x].Selected = true;
                }
            }
            catch (Exception ex)
            {
            }

            column_Fill();
        }

        public override void btnCol_Click(object sender, EventArgs e)
        {
            int r;
            int n;
            int y;

            r = dataGridView1.CurrentCell.RowIndex; // STATIC
            n = dataGridView1.CurrentCell.ColumnIndex; // STATIC

            try
            {
                for (y = r + 1; y <= Mos_Const - 1; y++)
                {
                    dataGridView1.Rows[y].Cells[n].Value = dataGridView1.CurrentCell.Value;
                    dataGridView1.Rows[y].Cells[n].Selected = true;
                }
            }
            catch (Exception ex)
            {
            }

            column_Fill();
        }

        public override void btnAll_Click(object sender, EventArgs e)
        {
            int r;
            int n;
            int x;
            int y;

            r = dataGridView1.CurrentCell.RowIndex; // STATIC
            n = dataGridView1.CurrentCell.ColumnIndex; // STATIC

            try
            {
                for (y = r + 1; y <= Mos_Const - 1; y++)
                {
                    dataGridView1.Rows[y].Cells[n].Value = dataGridView1.CurrentCell.Value;
                    dataGridView1.Rows[y].Cells[n].Selected = true;
                }

                for (x = n + 1; x <= dataGridView1.ColumnCount - 1; x++)
                {
                    for (y = 0; y <= Mos_Const - 1; y++)
                    {
                        dataGridView1.Rows[y].Cells[x].Value = dataGridView1.CurrentCell.Value;
                        dataGridView1.Rows[y].Cells[x].Selected = true;
                    }
                }
            }
            catch (Exception ex)
            {
            }

            column_Fill();
        }

        public virtual void Form_Cancel()
        {
            dgv.CurrentCell = dgv.Rows[frmRow].Cells[3];
            dgv.Rows[frmRow].Cells[3].Value = "";
            frm.ActiveControl = null;
        }

        public override void btnCancel_Click(object sender, EventArgs e)
        {
            Form_Cancel();
            base.btnCancel_Click(sender, e);
        }

        private void FormDetail_Collection_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form_Cancel();
            Dispose();
        }
    }
}
