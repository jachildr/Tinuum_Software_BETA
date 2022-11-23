using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.VisualBasic;

namespace Tinuum_Software_BETA
{
    [CLSCompliant(true)]
    static class myMethods
    {
        public static SQLControl SQL = new SQLControl();
        public static SQLControl SQL_ADD = new SQLControl();
        public static DateTime dteStart;
        public static string dtlVacant;
        public static string subjectNme;
        public static int Period;
        public static string geo_area;

        public static void TblCreate_Test()
        {
            SQL.ExecQuery("CREATE TABLE products(" + 
                "product_id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, " + 
                "product_name VARCHAR(50) NOT NULL, " + 
                "category VARCHAR(25)" + 
                ");");

            SQL.ExecQuery("CREATE TABLE inventory(" + 
                "inventory_id INT, " + 
                "product_id INT, " + 
                "quantity INT, " + 
                "min_level INT, " + 
                "max_level INT, " + 
                "Constraint fk_inv_product_id " + 
                "FOREIGN KEY(product_id) " + 
                "REFERENCES products(product_id)" + 
                "ON DELETE CASCADE" + 
                ");");
            if (SQL.HasException(true))
                return;
        }

        public static void SQL_Grab()
        {
            try
            {
                SQL_ADD.ExecQuery("SELECT * FROM dtbHome;");
                subjectNme = Convert.ToString(SQL_ADD.DBDT.Rows[0][1]);
                Period = Convert.ToInt32(SQL_ADD.DBDT.Rows[0][11]);
                dteStart = Convert.ToDateTime(SQL_ADD.DBDT.Rows[0][10]);
                geo_area = Convert.ToString(SQL_ADD.DBDT.Rows[0][6]);
            }
            catch (Exception ex)
            {
            }
        }

        public static double ToDecimal(string Percentage)
        {
            Percentage = Percentage.Substring(0, Percentage.Length - 1);
            return Convert.ToDouble(Percentage) / 100; 
        }

        public static string ToPercent(string value)
        {   
            if (Information.IsNumeric(value) == true)
            {
                string formatted = String.Format("{0:p}", Convert.ToDouble(value));

                return formatted;
            }
            else
            {
                MessageBox.Show("You must enter a numeric value.","TINUUM SOFTWARE",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

    }
    
}
