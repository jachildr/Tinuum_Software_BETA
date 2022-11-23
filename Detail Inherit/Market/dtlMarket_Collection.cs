using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Tinuum_Software_BETA.Detail_Classes.Market
{
    public partial class dtlMarket_Collection : Tinuum_Software_BETA.Detail_Masters.FormDetail_Collection
    {
        public dtlMarket_Collection()
        {
            InitializeComponent();
        }
        public override void Form_Loader()
        {
            int dgvRow = dgv.CurrentCell.RowIndex;
            switch (dgvRow)
            {
                case 5:
                    {
                        tbl_Configure = "dtbMarketConfigurePayors";
                    }
                    break;
                case 6:
                    {
                        tbl_Configure = "dtbMarketConfigurePDPM";
                    }
                    break;
                case 7:
                    {
                        tbl_Configure = "dtbMarketConfigureIncome";
                    }
                    break;
                case 8:
                    {
                        tbl_Configure = "dtbMarketConfigureAsset";
                    }
                    break;
                case 9:
                    {
                        tbl_Configure = "dtbMarketConfigureAge";
                    }
                    break;
                default:
                    break;
            }
            base.Form_Loader();
        }
    }
}
