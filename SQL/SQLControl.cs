using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

[assembly: CLSCompliant(true)]

namespace Tinuum_Software_BETA
{
    [CLSCompliant(true)]
    public class SQLControl
    {
        private SqlConnection DBConnect = new SqlConnection("Server=DESKTOP-PMAC527;Database=Tinuum_Software;Trusted_Connection=True");
        private SqlCommand DBCmd; // BUILD NEW COMMAND EACH TIME RUN A QUERY -- WHICH IS WHY NOT NEW

        // DB DATA
        public SqlDataAdapter DBDA; // BUILD NEW COMMAND EACH TIME RUN A QUERY -- WHICH IS WHY NOT NEW
        public DataTable DBDT;
        public SqlCommandBuilder DBCB;

        // QUERY PARAMETERS
        public List<SqlParameter> Params = new List<SqlParameter>();

        // QUERY STATISTICS
        public int RecordCount;
        public string Exception;

        public SQLControl()
        {
        }

        // ALLOW CONNECTION STRING OVERRIDE
        public SQLControl(string ConnectionString)
        {
            DBConnect = new SqlConnection(ConnectionString);
        }

        // EXECUTE QUERY SUB
        public void ExecQuery(string Query)
        {
            // RESET QUERY STATS
            RecordCount = 0;
            Exception = "";
            try
            {
                DBConnect.Open();
                // CREATE DATABASE COMMAND
                DBCmd = new SqlCommand(Query, DBConnect);

                // LOAD PARAMS INTO DB COMMAND

                Params.ForEach(p => DBCmd.Parameters.Add(p)); // LAMBDA EXPRESSION

                // CLEAR PARAMS LIST
                Params.Clear();

                // EXECUTE COMMAND & FILL DATASET
                DBDT = new DataTable();
                DBDA = new SqlDataAdapter(DBCmd);
                DBCB = new SqlCommandBuilder(DBDA);
                RecordCount = DBDA.Fill(DBDT);
            }
            catch (Exception ex)
            {
                // CAPTURE ERROR
                Exception = "ExecQuery Error: " + Environment.NewLine + ex.Message;
            }
            finally
            {
                // CLOSE CONNECTION
                if (DBConnect.State == ConnectionState.Open)
                    DBConnect.Close();
            }
        }

        // ADD PARAMS

        public void AddParam(string Name, object Value)
        {
            var NewParam = new SqlParameter(Name, Value);
            Params.Add(NewParam);
        }

        // ERROR CHECKING
        public bool HasException(bool Report = false)
        {
            if (string.IsNullOrEmpty(Exception))
                return false;
            if (Report == true)
                MessageBox.Show(Exception, "Exception:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return true;
        }
    }
}
