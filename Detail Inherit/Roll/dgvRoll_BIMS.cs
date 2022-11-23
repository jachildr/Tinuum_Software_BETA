using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tinuum_Software_BETA.Icon_Masters;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualBasic;
using Tinuum_Software_BETA.Popups.Roll;
using Syncfusion.Windows.Forms.Tools;

namespace Tinuum_Software_BETA.Detail_Inherit.Roll
{
    //***CODE NOT USED***
    class dgvRoll_BIMS
    {
        protected SQLControl SQL_Verse = new SQLControl();
        protected SQLControl SQL_Name = new SQLControl();
        protected int terminate;
        protected int exit;
        protected int chckCol = 4;
        
        // FROM MASTER
        protected string Rslt_Cncl = null;
        protected SQLControl SQL = new SQLControl(); // CREATE NEW INSTANCE OF SQLCONTROL CLASS
        protected List<string> Headers_Submit = new List<string>();
        protected List<string> Header_Name = new List<string>();
        protected List<string> Header_Rename = new List<string>();
        protected int Col_Count;
        protected string tbl_Name = "dtbRollTest";
        protected int Mos_Const = 12;
        protected int sldrCol = 1;
        protected int sldrMax = 4;
        protected int loading;

        protected static int index;
        public static int _index
        {
            get
            {
                return index;
            }
        }

        public dgvRoll_BIMS()
        {
            
        }
        public virtual void Add_Source(DataGridView dataGridView1)
        {
            string btnString = "(b)";
            string cmbString = "(c)";
            string dteString = "(d)";
            int i;

            // UNBIND DATA SOURCE AT BEGINNING FOR UPDATE COMMAND
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ColumnCount = 0;
            dataGridView1.RowCount = 0;
            dataGridView1.Refresh();


            // LINK DATA SOURCE TO GET COL NAMES
            SQL.ExecQuery("SELECT * FROM " + tbl_Name + ";");
            if (SQL.HasException(true))
                return;
            dataGridView1.DataSource = SQL.DBDT;

            // FILL LIST FROM COLUMN HEADERS
            Col_Count = dataGridView1.ColumnCount;

            for (i = 0; i <= Col_Count - 1; i++)
            {
                Headers_Submit.Add("[" + dataGridView1.Columns[i].HeaderText + "]");
            }

            for (i = 0; i <= Col_Count - 1; i++)
            {
                Header_Name.Add(dataGridView1.Columns[i].HeaderText);
            }


            for (i = 0; i <= Col_Count - 1; i++)
            {
                var switchExpr = Header_Name[i].Substring(Header_Name[i].Length - 3, 3);
                switch (switchExpr)
                {
                    case var @case when @case == btnString:
                        {
                            Header_Rename.Add("");
                            break;
                        }

                    case var case1 when case1 == cmbString:
                    case var case2 when case2 == dteString:
                        {
                            Header_Rename.Add(Header_Name[i].Substring(0, Header_Name[i].Length - 3));
                            break;
                        }

                    default:
                        {
                            Header_Rename.Add(Header_Name[i]);
                            break;
                        }
                }
            }
            // CLEAR DATA SOURCE 
            dataGridView1.DataSource = null;
        }

        public void Loader(DataGridView dataGridView1)
        {
            int i;
            int r;
            int j;
            
            // DGV CTRLS
            terminate = 1;

            SQL_Verse.ExecQuery("SELECT * FROM " + tbl_Name + ";");

            // REFRESH ROWS & COLUMNS
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            // CREATE GRIDVIEW COLUMNS
            Add_Check(dataGridView1);

            // SET HEADERS AND NON SORT
            for (i = 0; i <= dataGridView1.ColumnCount - 1; i++)
            {
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns[i].HeaderText = Header_Rename[i];
            }

            // SET DGV NUMBER OF ROWS BY REFRESHING TBL ROWCOUNT 
            SQL_Verse.ExecQuery("SELECT * FROM " + tbl_Name + ";");
            dataGridView1.RowCount = SQL_Verse.RecordCount;

            // FILL DATAGRID FROM DATA TABLE
            for (r = 0; r <= SQL_Verse.RecordCount - 1; r++)
            {
                for (i = 0; i <= Col_Count - 1; i++)
                {
                    if (i != chckCol)
                    {
                        dataGridView1.Rows[r].Cells[i].Value = SQL_Verse.DBDT.Rows[r][i];
                    }
                    else
                    {
                        switch (Convert.ToString(SQL_Verse.DBDT.Rows[r][i]))
                        {
                            case "1":
                                {
                                    dataGridView1.Rows[r].Cells[i].Value = true;
                                }
                                break;
                            case "0":
                                {
                                    dataGridView1.Rows[r].Cells[i].Value = false;
                                }
                                break;
                            case "":
                                {
                                    dataGridView1.Rows[r].Cells[i].Value = false;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            // MAKE ROWS IN COLUMN 1 READ ONLY
            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                dataGridView1.Rows[i].Cells[1].ReadOnly = true;
            }

            // FREEZE COLUMNS & VISIBILITY
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[chckCol - 1].Visible = false;
            dataGridView1.Columns[chckCol - 2].Visible = false;
            
            // PLACEHOLDER
            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                if (new int[] { 7, 12, 17, 22, 27, 32 }.Contains(i))
                {
                        
                }   
            }

            // MAKE 1ST COLUMN READ ONLY
            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                dataGridView1.Rows[i].Cells[1].ReadOnly = true;
            }

            // MAKE 1ST COLUMN STATIC WHITE
            dataGridView1.Columns[1].DefaultCellStyle.SelectionBackColor = Color.White;
            dataGridView1.Columns[1].DefaultCellStyle.SelectionForeColor = Color.Black;

            // COLUMN ALIGNMENT & WIDTH
            dataGridView1.Columns[1].Width = 300;
            dataGridView1.Columns[chckCol].Width = 40;

            dataGridView1.Columns[chckCol].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns[chckCol].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            Dynamic_CTRLs(dataGridView1);

            terminate = 0;
        }

        public void Add_Check(DataGridView dataGridView1)
        {
            int i;
            var chck1 = new DataGridViewCheckBoxColumn();

            // COLUMN CONTROLS
            {
                // ADD SPECS FOR BUTTON14
                chck1.FlatStyle = FlatStyle.System;
                chck1.DefaultCellStyle.Alignment = (DataGridViewContentAlignment)ContentAlignment.BottomRight;
            }

            // CREATE GRIDVIEW COLUMNS
            for (i = 0; i <= Col_Count - 1; i++)
            {
                switch (i)
                {
                    case 4:
                        {
                            dataGridView1.Columns.Add(chck1);
                        }
                        break;
                    default:
                        {
                            dataGridView1.Columns.Add("txt", "New Text");
                        }
                        break;
                }
            } 
        }

        public void Dynamic_CTRLs(DataGridView dataGridView1)
        {
            int Counter = 0; //STARTING FROM ZERO
            int x;
            int y;
            int i;
            int j;
            int Width;
            int Height;

            Rectangle rect; // STORES A SET OF FOUR INTEGERS
            RangeSlider sldrCtrl = new RangeSlider();

            sldrCtrl.Name = "sldr" + Counter;
            sldrCtrl.Tag = dataGridView1;
            sldrCtrl.VisualStyle = RangeSlider.RangeSliderStyle.Metro;
            sldrCtrl.RangeColor = SystemColors.Highlight;
            sldrCtrl.HighlightedThumbColor = SystemColors.Highlight;
            sldrCtrl.ThumbColor = SystemColors.Highlight;
            sldrCtrl.PushedThumbColor = SystemColors.Highlight;
            sldrCtrl.ChannelColor = SystemColors.ControlDark;
            sldrCtrl.BackColor = Color.White;
            
            sldrCtrl.Minimum = 0;
            sldrCtrl.Maximum = sldrMax;
            sldrCtrl.SliderMin = 0;
            sldrCtrl.SliderMax = 0;
            //sldrCtrl.Enabled = false;

            dataGridView1.Controls.Add(sldrCtrl);

            // SET POSITION
            rect = dataGridView1.GetCellDisplayRectangle(sldrCol, 0, false);
            x = rect.X;
            y = rect.Y;
            Width = rect.Width;
            Height = rect.Height;

            sldrCtrl.SetBounds(x, y, Width, Height);
            sldrCtrl.Visible = true;

            // ADD EVENT HANDLER
            sldrCtrl.Click += new EventHandler(sldrCrtl_Click);
            sldrCtrl.ValueChanged += new EventHandler(sldrCrtl_ValueChanged);
            sldrCtrl.MouseDoubleClick += new MouseEventHandler(sldrCtrl_DoubleClick);
            sldrCtrl.LostFocus += new EventHandler(sldrCtrl_Leave);
        }

        public virtual void sldrCrtl_Click(object sender, EventArgs e)
        {
            RangeSlider sldrCtrl = (RangeSlider)sender;
            DataGridView dataGridView1 = (DataGridView)sldrCtrl.Tag;
            int Diff;
            var rowNum = default(int);
            string name = "sldr";

            sldrCtrl.Enabled = true;

            try
            {
                Diff = sldrCtrl.Name.Length - name.Trim().Length;
                rowNum = Convert.ToInt32(sldrCtrl.Name.Substring(sldrCtrl.Name.Length - Diff, Diff));
            }
            catch (Exception ex)
            {
            }

            dataGridView1.CurrentCell = dataGridView1.Rows[rowNum].Cells[sldrCol];
        }

        public virtual void sldrCrtl_ValueChanged(object sender, EventArgs e)
        {
            RangeSlider sldrCtrl = (RangeSlider)sender;
            DataGridView dataGridView1 = (DataGridView)sldrCtrl.Tag;
            int Diff;
            int rowNum = 0;
            string name = "sldr";

            if (loading > 0) return;

            try
            {
                Diff = sldrCtrl.Name.Length - name.Trim().Length;
                rowNum = Convert.ToInt32(sldrCtrl.Name.Substring(sldrCtrl.Name.Length - Diff, Diff));
            }
            catch (Exception ex)
            {
            }

            if (dataGridView1.CurrentCell.RowIndex != rowNum)
            {
                return;
            }

            // SET MIN & MAX TO CELL
            dataGridView1.Rows[rowNum].Cells[sldrCol + 1].Value = string.Format("{0:N0}", sldrCtrl.SliderMin);
            dataGridView1.Rows[rowNum].Cells[sldrCol + 2].Value = string.Format("{0:N0}", sldrCtrl.SliderMax);

            if (sldrCtrl.SliderMin == 0 && sldrCtrl.SliderMax == 0)
            {
                dataGridView1.Rows[rowNum].Cells[sldrCol + 3].Value = null;
            }
            else
            {
                dataGridView1.Rows[rowNum].Cells[sldrCol + 3].Value = string.Format("{0:N0}", sldrCtrl.SliderMin) + " - " + string.Format("{0:N0}", sldrCtrl.SliderMax);
            }

            //AVERAGE
            dataGridView1.Rows[rowNum].Cells[sldrCol].Value = (sldrCtrl.SliderMin + sldrCtrl.SliderMax) / 2;

            // REMOVE CHECKBOX CHECK 
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[chckCol];
                if (chk.Selected == true)
                {
                    chk.Selected = false;
                }
            }
        }

        public void sldrCtrl_DoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            RangeSlider sldrCtrl = (RangeSlider)sender;
            if (e.Button == MouseButtons.Left)
            {
                // RESET
                sldrCtrl.SliderMin = 0;
                sldrCtrl.SliderMax = 0;
            }
        }

        public void sldrCtrl_Leave(object sender, EventArgs e)
        {
            RangeSlider sldrCtrl = (RangeSlider)sender;
            sldrCtrl.Enabled = false;
        }

        public void SubmitSQL(DataGridView dataGridView1)
        {
            int i;
            int count = default;
            string title = "TINUUM SOFTWARE";
            string cmdUpdate;

            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                DataGridViewCheckBoxCell cell = dataGridView1.Rows[i].Cells[chckCol] as DataGridViewCheckBoxCell;

                if (cell.Value != DBNull.Value)
                {
                    if (Convert.ToBoolean(cell.Value) == true)
                    {
                        count += 1;
                    }
                }
            }

            if (count == 0)
            {
                MessageBox.Show("You must check at least one item before continuing.", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                // ADD PARAMS
                SQL_Verse.AddParam("@PrimKey", dataGridView1.Rows[i].Cells[0].Value);
                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[chckCol].Value) == true)
                {
                    SQL_Verse.AddParam("@vals", 1);
                }
                else
                {
                    SQL_Verse.AddParam("@vals", 0);
                }
                
                // UPDATE STATEMENT FOR MAINVERSE
                cmdUpdate = "UPDATE " + tbl_Name + " SET [Select]=@vals WHERE Prime=@PrimKey;";
                SQL_Verse.ExecQuery(cmdUpdate);
            }
        }

        public void CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dataGridView1 = (DataGridView)sender;
            int slct = dataGridView1.CurrentCell.RowIndex;

            try
            {
                if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
                {
                    foreach (RangeSlider sldr in dataGridView1.Controls)
                    {
                        if (slct > 0)
                        {
                            sldr.SliderMin = 0;
                            sldr.SliderMax = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

    }
}
