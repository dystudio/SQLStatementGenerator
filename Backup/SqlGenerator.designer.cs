using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;

namespace SqlStatementGenerator
{
    partial class SqlGenerator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtSelectSQL = new System.Windows.Forms.TextBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.dgTableInfo = new System.Windows.Forms.DataGrid();
            this.btnShowQueryResults = new System.Windows.Forms.Button();
            this.lblIncludeFields = new System.Windows.Forms.Label();
            this.chklstIncludeFields = new System.Windows.Forms.CheckedListBox();
            this.lblTargetTable = new System.Windows.Forms.Label();
            this.txtTargetTable = new System.Windows.Forms.TextBox();
            this.cmbTables = new System.Windows.Forms.ComboBox();
            this.cmbDatabases = new System.Windows.Forms.ComboBox();
            this.cmbSqlType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgTableInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSelectSQL
            // 
            this.txtSelectSQL.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSelectSQL.Location = new System.Drawing.Point(219, 32);
            this.txtSelectSQL.Multiline = true;
            this.txtSelectSQL.Name = "txtSelectSQL";
            this.txtSelectSQL.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSelectSQL.Size = new System.Drawing.Size(445, 52);
            this.txtSelectSQL.TabIndex = 5;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerate.Enabled = false;
            this.btnGenerate.Location = new System.Drawing.Point(682, 51);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(163, 33);
            this.btnGenerate.TabIndex = 11;
            this.btnGenerate.Text = "Generate SQL Statements";
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // dgTableInfo
            // 
            this.dgTableInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgTableInfo.CaptionText = "Results from SELECT Query:";
            this.dgTableInfo.DataMember = "";
            this.dgTableInfo.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dgTableInfo.Location = new System.Drawing.Point(9, 90);
            this.dgTableInfo.Name = "dgTableInfo";
            this.dgTableInfo.Size = new System.Drawing.Size(655, 477);
            this.dgTableInfo.TabIndex = 63;
            // 
            // btnShowQueryResults
            // 
            this.btnShowQueryResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowQueryResults.Enabled = false;
            this.btnShowQueryResults.Location = new System.Drawing.Point(682, 12);
            this.btnShowQueryResults.Name = "btnShowQueryResults";
            this.btnShowQueryResults.Size = new System.Drawing.Size(163, 33);
            this.btnShowQueryResults.TabIndex = 6;
            this.btnShowQueryResults.Text = "Show SQL / Table Results";
            this.btnShowQueryResults.Click += new System.EventHandler(this.btnShowQueryResults_Click);
            // 
            // lblIncludeFields
            // 
            this.lblIncludeFields.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIncludeFields.Location = new System.Drawing.Point(675, 243);
            this.lblIncludeFields.Name = "lblIncludeFields";
            this.lblIncludeFields.Size = new System.Drawing.Size(155, 14);
            this.lblIncludeFields.TabIndex = 9;
            this.lblIncludeFields.Text = "Check the fields to include:";
            // 
            // chklstIncludeFields
            // 
            this.chklstIncludeFields.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chklstIncludeFields.Location = new System.Drawing.Point(677, 260);
            this.chklstIncludeFields.Name = "chklstIncludeFields";
            this.chklstIncludeFields.Size = new System.Drawing.Size(167, 304);
            this.chklstIncludeFields.TabIndex = 10;
            // 
            // lblTargetTable
            // 
            this.lblTargetTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTargetTable.Location = new System.Drawing.Point(675, 193);
            this.lblTargetTable.Name = "lblTargetTable";
            this.lblTargetTable.Size = new System.Drawing.Size(166, 14);
            this.lblTargetTable.TabIndex = 7;
            this.lblTargetTable.Text = "Table name for generated SQL:";
            // 
            // txtTargetTable
            // 
            this.txtTargetTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTargetTable.Location = new System.Drawing.Point(677, 209);
            this.txtTargetTable.Name = "txtTargetTable";
            this.txtTargetTable.Size = new System.Drawing.Size(167, 20);
            this.txtTargetTable.TabIndex = 8;
            // 
            // cmbTables
            // 
            this.cmbTables.DropDownHeight = 500;
            this.cmbTables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTables.FormattingEnabled = true;
            this.cmbTables.IntegralHeight = false;
            this.cmbTables.Location = new System.Drawing.Point(12, 59);
            this.cmbTables.Name = "cmbTables";
            this.cmbTables.Size = new System.Drawing.Size(186, 21);
            this.cmbTables.TabIndex = 67;
            this.cmbTables.SelectedIndexChanged += new System.EventHandler(this.cmbTables_SelectedIndexChanged);
            // 
            // cmbDatabases
            // 
            this.cmbDatabases.DropDownHeight = 500;
            this.cmbDatabases.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDatabases.IntegralHeight = false;
            this.cmbDatabases.Location = new System.Drawing.Point(12, 32);
            this.cmbDatabases.Name = "cmbDatabases";
            this.cmbDatabases.Size = new System.Drawing.Size(186, 21);
            this.cmbDatabases.TabIndex = 66;
            this.cmbDatabases.SelectedIndexChanged += new System.EventHandler(this.cmbDatabases_SelectedIndexChanged);
            // 
            // cmbSqlType
            // 
            this.cmbSqlType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSqlType.DropDownHeight = 500;
            this.cmbSqlType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSqlType.IntegralHeight = false;
            this.cmbSqlType.Items.AddRange(new object[] {
            "INSERT Statements",
            "UPDATE Statements",
            "DELETE Statements"});
            this.cmbSqlType.Location = new System.Drawing.Point(677, 162);
            this.cmbSqlType.Name = "cmbSqlType";
            this.cmbSqlType.Size = new System.Drawing.Size(167, 21);
            this.cmbSqlType.TabIndex = 69;
            this.cmbSqlType.SelectedIndexChanged += new System.EventHandler(this.cmbSqlType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(674, 145);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 14);
            this.label1.TabIndex = 70;
            this.label1.Text = "SQL Statement Type:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(216, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 14);
            this.label2.TabIndex = 72;
            this.label2.Text = "SELECT SQL Query:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(9, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 14);
            this.label3.TabIndex = 73;
            this.label3.Text = "Selected Database / Table:";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(682, 90);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(163, 33);
            this.btnClose.TabIndex = 74;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // SqlGenerator
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(857, 579);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbSqlType);
            this.Controls.Add(this.cmbTables);
            this.Controls.Add(this.cmbDatabases);
            this.Controls.Add(this.txtTargetTable);
            this.Controls.Add(this.txtSelectSQL);
            this.Controls.Add(this.lblTargetTable);
            this.Controls.Add(this.chklstIncludeFields);
            this.Controls.Add(this.lblIncludeFields);
            this.Controls.Add(this.btnShowQueryResults);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.dgTableInfo);
            this.Name = "SqlGenerator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SQL Statement Generator";
            this.Load += new System.EventHandler(this.SqlGenerator_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgTableInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.DataGrid dgTableInfo;
        private System.Windows.Forms.TextBox txtSelectSQL;
        private System.Windows.Forms.Button btnShowQueryResults;
        private System.Windows.Forms.Label lblIncludeFields;
        private System.Windows.Forms.CheckedListBox chklstIncludeFields;
        private System.Windows.Forms.Label lblTargetTable;
        private System.Windows.Forms.TextBox txtTargetTable;

        private ComboBox cmbTables;
        private ComboBox cmbDatabases;
        private ComboBox cmbSqlType;
        private Label label1;
        private Label label2;
        private Label label3;
        private Button btnClose;
            

    }
}
