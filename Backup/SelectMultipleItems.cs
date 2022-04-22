using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace SqlStatementGenerator
{
    public partial class SelectMultipleItems : Form
    {
        private ArrayList m_aryUserSelectedItems = new ArrayList();
        public ArrayList UserSelectedItems
        {
            get { return m_aryUserSelectedItems; }
        }

        public string Description 
        {
            set { lblDescription.Text = value; }
        }

      
        public SelectMultipleItems()
        {
            InitializeComponent();
        }

        public void Initialize(ArrayList aryItems, string sDisplayMember, bool bSorted)
        {           
            // init the list
            chklstListBox.Items.Clear();
    
            if(sDisplayMember != string.Empty)
                chklstListBox.DisplayMember = sDisplayMember;

            chklstListBox.Items.AddRange(aryItems.ToArray());

            chklstListBox.Sorted = bSorted;
		}

        private void btnSelect_Click(object sender, EventArgs e)
        {
            m_aryUserSelectedItems.Clear();

            foreach (object itemChecked in chklstListBox.CheckedItems)
            {
                m_aryUserSelectedItems.Add(itemChecked);
            }

            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {            
            this.DialogResult = DialogResult.Cancel;
        }

        private void lnkCheckAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            for(int index=0; index<chklstListBox.Items.Count; index++)            
            {
                chklstListBox.SetItemCheckState(index, CheckState.Checked);
            }
        }

        private void lnkUncheckAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            for (int index = 0; index < chklstListBox.Items.Count; index++)
            {
                chklstListBox.SetItemCheckState(index, CheckState.Unchecked);
            }
        }
    }
}