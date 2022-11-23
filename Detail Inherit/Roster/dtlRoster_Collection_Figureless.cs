using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Tinuum_Software_BETA.Detail_Inherit.Roster
{
    public partial class dtlRoster_Collection_Figureless : Tinuum_Software_BETA.Detail_Inherit.Roster.dtlRoster_Collection
    {
        public dtlRoster_Collection_Figureless()
        {
            InitializeComponent();
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
            int index = 0;
            int input;

            frm = Application.OpenForms[1] as Form;
            tab = frm.Controls["tabCtrl"] as TabControl;

            switch (tab.SelectedIndex)
            {
                case 0:
                    {
                        dgv = tab.TabPages[0].Controls["dataGridView1"] as DataGridView;
                        switch (dgv.CurrentCell.ColumnIndex)
                        {
                            case 27:
                                {
                                    tbl_Detail = "dtbRosterDetail_Transition";
                                    tbl_Configure = "dtbRollVerse";
                                }
                                break;
                            case 29:
                                {
                                    tbl_Detail = "dtbRosterDetail_Trans_RE";
                                    tbl_Configure = "dtbRollVerse_Discharge";
                                }
                                break;
                        }
                    }
                    break;
                case 1:
                    {
                        dgv = tab.TabPages[1].Controls["dataGridView2"] as DataGridView;
                        switch (dgv.CurrentCell.ColumnIndex)
                        {
                            case 27:
                                {
                                    tbl_Detail = "dtbRosterDetail_Transition2";
                                    tbl_Configure = "dtbRollVerse";
                                }
                                break;
                            case 29:
                                {
                                    tbl_Detail = "dtbRosterDetail_Trans_RE2";
                                    tbl_Configure = "dtbRollVerse_Discharge";
                                }
                                break;
                        }
                    }
                    break;
                case 2:
                    {
                        dgv = tab.TabPages[2].Controls["dataGridView3"] as DataGridView;
                        switch (dgv.CurrentCell.ColumnIndex)
                        {
                            case 27:
                                {
                                    tbl_Detail = "dtbRosterDetail_Transition3";
                                    tbl_Configure = "dtbRollVerse";
                                }
                                break;
                            case 29:
                                {
                                    tbl_Detail = "dtbRosterDetail_Trans_RE3";
                                    tbl_Configure = "dtbRollVerse_Discharge";
                                }
                                break;
                        }
                    }
                    break;
                case 3:
                    {
                        dgv = tab.TabPages[3].Controls["dataGridView4"] as DataGridView;
                        switch (dgv.CurrentCell.ColumnIndex)
                        {
                            case 27:
                                {
                                    tbl_Detail = "dtbRosterDetail_Transition4";
                                    tbl_Configure = "dtbRollVerse";
                                }
                                break;
                            case 29:
                                {
                                    tbl_Detail = "dtbRosterDetail_Trans_RE4";
                                    tbl_Configure = "dtbRollVerse_Discharge";
                                }
                                break;
                        }
                    }
                    break;
                case 4:
                    {
                        dgv = tab.TabPages[4].Controls["dataGridView5"] as DataGridView;
                        switch (dgv.CurrentCell.ColumnIndex)
                        {
                            case 27:
                                {
                                    tbl_Detail = "dtbRosterDetail_Transition5";
                                    tbl_Configure = "dtbRollVerse";
                                }
                                break;
                            case 29:
                                {
                                    tbl_Detail = "dtbRosterDetail_Trans_RE5";
                                    tbl_Configure = "dtbRollVerse_Discharge";
                                }
                                break;
                        }
                    }
                    break;
            }

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
                    newCell.DisplayMember = "Transition Label";
                    newCell.ValueMember = "ID_Num";
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
                            if (SQL_DETAIL.DBDT.Rows[frmRow][c + 1] == DBNull.Value || SQL_Configure.DBDT.Rows[i][0] == DBNull.Value) break;
                            if (Convert.ToInt32(SQL_DETAIL.DBDT.Rows[frmRow][c + 1]) == Convert.ToInt32(SQL_Configure.DBDT.Rows[i][0]))
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
    }
}
