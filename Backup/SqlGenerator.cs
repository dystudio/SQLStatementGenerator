using System;
using System.Configuration;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace SqlStatementGenerator
{
	/// <summary>
	/// Summary description
	/// </summary>
	public partial class SqlGenerator : System.Windows.Forms.Form
	{       
        private string m_sSqlConnectionString = string.Empty;
        private DataTable m_TableInfo = null;
        private string m_sSqlStatementText = string.Empty;
        private enum STATEMENT_TYPES { INSERT, UPDATE, DELETE }

        public SqlGenerator()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
        }

        void SqlGenerator_Load(object sender, EventArgs e)
        {
            cmbSqlType.SelectedIndex = (int)STATEMENT_TYPES.INSERT;

            m_sSqlConnectionString = Convert.ToString(ConfigurationManager.AppSettings["SqlConnectionString"]);
            InitializeDatabaseControls();   
        }      
		        
        private void btnShowQueryResults_Click(object sender, System.EventArgs e)
        {            
            LoadQueryRecords(txtSelectSQL.Text.Trim());
        }

        private void btnGenerate_Click(object sender, System.EventArgs e)
        {						
            GenerateSqlStatements();

            // if user cancelled or problem occured, just exit
            if (m_sSqlStatementText.Trim() == string.Empty)
                return;

            string sFilePath = FileUtilities.GetUniqueTempFileName(".txt");
            FileUtilities.WriteFileContents(sFilePath, m_sSqlStatementText, true);
            Process.Start(sFilePath);         
        }
                
        private void cmbDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDatabases.SelectedIndex < 0)
                return;

            // user selected a different database, so load the table combobox
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                cmbTables.DataSource = null;
                cmbTables.Items.Clear();
                cmbTables.DisplayMember = "TABLE_NAME";
                cmbTables.ValueMember = "TABLE_NAME";

                DataTable dt = DatabaseUtilities.GetDatabaseTables(m_sSqlConnectionString, cmbDatabases.Text);
                cmbTables.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("cmbDatabases_SelectedIndexChanged error: " + ex.Message);
            }
            finally
            { 
                Cursor.Current = Cursors.Default;
                UpdateControls();
            }            
        }
        
        private void cmbTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTables.SelectedIndex < 0)
                return;
            
            // automatically setup a query based upon the table selected
            string sTableName = cmbTables.Text;
            txtSelectSQL.Text = "SELECT * FROM " + sTableName;
            txtTargetTable.Text = sTableName;
            
            dgTableInfo.DataSource = null;
            UpdateControls();
        }

        private void cmbSqlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTables.SelectedIndex < 0)
                return;
            
            // user changed the type of statement to generate, 
            //  so flip the checkboxes for user convenience
            for (int i = 0; i < chklstIncludeFields.Items.Count; i++)
            {
                if(cmbSqlType.SelectedIndex == (int)STATEMENT_TYPES.DELETE)
                {
                    // assume that the primary key is in column 0, 
                    //  and that deletes trigger off that
                    chklstIncludeFields.SetItemChecked(i, (i == 0));
                }
                else
                {
                    // assume that the primary key is in column 0, 
                    //  and that it should NOT be included in inserts and updates
                    chklstIncludeFields.SetItemChecked(i, (i != 0));
                }                
            }             
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Initializes the database combobox control
        /// </summary>
        private void InitializeDatabaseControls()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                cmbDatabases.DataSource = null;
                cmbDatabases.Items.Clear();
                cmbTables.DataSource = null;
                cmbTables.Items.Clear();     // clear the tables also since they'll be invalid       

                cmbDatabases.DisplayMember = "DATABASE_NAME";
                cmbDatabases.ValueMember = "DATABASE_NAME";
                DataTable dt = DatabaseUtilities.GetDatabases(m_sSqlConnectionString);
                cmbDatabases.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading database-tables into list: " + ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                UpdateControls();
            }
        }

        /// <summary>
        /// Updates the enable states of various form controls
        /// </summary>
        private void UpdateControls()
        {
            btnShowQueryResults.Enabled = (cmbDatabases.SelectedIndex >= 0);
            btnGenerate.Enabled = (dgTableInfo.DataSource != null);
        }

        /// <summary>
        /// Loads the grid and column list controls according to the specified query
        /// </summary>
        /// <param name="sSQL"></param>
        private void LoadQueryRecords(string sSQL)
        {
            if (sSQL.Trim() == string.Empty)
            {
                MessageBox.Show("The SQL query was empty!  Please enter a valid SQL query!");
                return;
            }

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                m_TableInfo = DatabaseUtilities.LoadDataTable(m_sSqlConnectionString, cmbDatabases.Text, sSQL);
                dgTableInfo.DataSource = m_TableInfo;

                // load the list of columns to include in the generator
                chklstIncludeFields.Items.Clear();
                foreach (DataColumn col in m_TableInfo.Columns)
                {
                    // exclude the primary/auto-increment key by default, but select/check all the others
                    chklstIncludeFields.Items.Add(col.ColumnName, (chklstIncludeFields.Items.Count > 0));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("LoadQueryRecords error: " + ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                UpdateControls();
            }
        }

        /// <summary>
        /// Generates sql statements based upon the generator type selected and the tableinfo provided
        /// </summary>
        private void GenerateSqlStatements()
        {
            // clear the string member
            m_sSqlStatementText = string.Empty;

            // create an array of all the columns that are to be included
            ArrayList aryColumns = new ArrayList();
            for (int i = 0; i < chklstIncludeFields.CheckedItems.Count; i++)
            {
                aryColumns.Add(chklstIncludeFields.CheckedItems[i].ToString());
            }

            // if no columns included, return with a msg
            if(aryColumns.Count <= 0)
            {
                MessageBox.Show("No columns selected!  Please check/select some columns to include!");
                return;
            }

            string sTargetTableName = txtTargetTable.Text.Trim();
            if(sTargetTableName == string.Empty)
            {
                MessageBox.Show("No valid target table name!  Please enter a table name to be used in the SQL statements!");
                return;
            }            

            // generate the sql by type
            if (cmbSqlType.SelectedIndex == (int)STATEMENT_TYPES.INSERT)
            {
                m_sSqlStatementText = SqlScriptGenerator.GenerateSqlInserts(aryColumns, m_TableInfo, sTargetTableName);
            }
            else if (cmbSqlType.SelectedIndex == (int)STATEMENT_TYPES.UPDATE)
            {
                // get an array of all the active table columns         
                ArrayList aryWhereColumns = new ArrayList();
                for (int i = 0; i < chklstIncludeFields.Items.Count; i++)
                {
                    aryWhereColumns.Add(chklstIncludeFields.GetItemText(chklstIncludeFields.Items[i]));
                }

                // create dlg and pass in array of columns
                SelectMultipleItems dlg = new SelectMultipleItems();
                dlg.Text = "Select WHERE Columns";
                dlg.Description = "Select WHERE-Clause Columns for UPDATE SQLs:";
                dlg.Initialize(aryWhereColumns, string.Empty, false);

                // user cancelled, so exit
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                aryWhereColumns.Clear();
                aryWhereColumns = dlg.UserSelectedItems;

                m_sSqlStatementText = SqlScriptGenerator.GenerateSqlUpdates(aryColumns, aryWhereColumns, m_TableInfo, sTargetTableName);
            }
            else if (cmbSqlType.SelectedIndex == (int)STATEMENT_TYPES.DELETE)
            {
                m_sSqlStatementText = SqlScriptGenerator.GenerateSqlDeletes(aryColumns, m_TableInfo, sTargetTableName);
            }            
        }

	}
}