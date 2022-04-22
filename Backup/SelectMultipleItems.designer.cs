namespace SqlStatementGenerator
{
    partial class SelectMultipleItems
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
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chklstListBox = new System.Windows.Forms.CheckedListBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lnkUncheckAll = new System.Windows.Forms.LinkLabel();
            this.lnkCheckAll = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelect.Location = new System.Drawing.Point(255, 35);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 0;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(255, 64);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chklstListBox
            // 
            this.chklstListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chklstListBox.CheckOnClick = true;
            this.chklstListBox.FormattingEnabled = true;
            this.chklstListBox.Location = new System.Drawing.Point(12, 35);
            this.chklstListBox.Name = "chklstTableList";
            this.chklstListBox.Size = new System.Drawing.Size(234, 544);
            this.chklstListBox.TabIndex = 3;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(9, 19);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(137, 13);
            this.lblDescription.TabIndex = 4;
            this.lblDescription.Text = "Database Tables Available:";
            // 
            // lnkUncheckAll
            // 
            this.lnkUncheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkUncheckAll.AutoSize = true;
            this.lnkUncheckAll.Location = new System.Drawing.Point(181, 584);
            this.lnkUncheckAll.Name = "lnkUncheckAll";
            this.lnkUncheckAll.Size = new System.Drawing.Size(65, 13);
            this.lnkUncheckAll.TabIndex = 5;
            this.lnkUncheckAll.TabStop = true;
            this.lnkUncheckAll.Text = "Uncheck All";
            this.lnkUncheckAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUncheckAll_LinkClicked);
            // 
            // lnkCheckAll
            // 
            this.lnkCheckAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lnkCheckAll.AutoSize = true;
            this.lnkCheckAll.Location = new System.Drawing.Point(12, 584);
            this.lnkCheckAll.Name = "lnkCheckAll";
            this.lnkCheckAll.Size = new System.Drawing.Size(52, 13);
            this.lnkCheckAll.TabIndex = 6;
            this.lnkCheckAll.TabStop = true;
            this.lnkCheckAll.Text = "Check All";
            this.lnkCheckAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkCheckAll_LinkClicked);
            // 
            // SelectMultipleItems
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 612);
            this.Controls.Add(this.lnkCheckAll);
            this.Controls.Add(this.lnkUncheckAll);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.chklstListBox);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSelect);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectMultipleItems";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Tables";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckedListBox chklstListBox;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.LinkLabel lnkUncheckAll;
        private System.Windows.Forms.LinkLabel lnkCheckAll;
    }
}