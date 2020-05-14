using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TZ.ImportDesk
{
    public partial class ViewRelationItem : UserControl
    {
        public int Index { get; set; }
        public delegate void Removed(int index);
        public event Removed AfterRemoved;
        private List<CompExtention.Attribute> _parentFields;
        private List<CompExtention.Attribute> _childFields;
        
        public CompExtention.ViewRelation Relation { get; set; }
        public List<CompExtention.Attribute> ParentFields { get=> _parentFields; 
            set { 
                _parentFields = value;
                ParentChanged();
            } }             

        public List<CompExtention.Attribute> ChildFields { get=> _childFields; set { _childFields = value; ChildChanged(); } }

        public ViewRelationItem()
        {
            InitializeComponent();
        }
        public ViewRelationItem(int index, CompExtention.ViewRelation _relation)
        {                        
            InitializeComponent();
            this.Index = index;
            cmbParentField.Name = cmbParentField.Name + "_" + index;
            Relation = _relation;
            ChildCombo();     
       
        }

        private void ChildCombo() {
            //cmbChildField1 = new System.Windows.Forms.ComboBox();
            //cmbChildField1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //cmbChildField1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            //cmbChildField1.FormattingEnabled = true;
            //cmbChildField1.Location = new System.Drawing.Point(251, 0);
            //cmbChildField1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            //cmbChildField1.Name = "cmbChildField_" + Index;
            //cmbChildField1.Size = new System.Drawing.Size(239, 28);
            //cmbChildField1.TabIndex = 9;
            //this.Controls.Add(cmbChildField1);
        }
        private void lnkRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AfterRemoved(Index);
        }

        private new void ParentChanged()
        {
            var p = _parentFields.Clone();
            cmbParentField.DisplayMember = "Name";
            cmbParentField.ValueMember = "ID";           
            cmbParentField.DataSource = p;
            

        }
        private void ChildChanged() {
            var c = _childFields.Clone();
            cmbChildField.DisplayMember = "Name";
            cmbChildField.ValueMember = "ID";
          
            cmbChildField.DataSource = c;           

        }
        public void SetParentField(string left) {
            if (left != "") {
                cmbParentField.SelectedItem = ((List<CompExtention.Attribute>)cmbParentField.DataSource).Where(x => x.ID == left).FirstOrDefault();
            }

        }
        public void SetChildField(string right) {
            if (right != "") {
                cmbChildField.SelectedItem = ((List<CompExtention.Attribute>)cmbChildField.DataSource).Where(x => x.ID == right).FirstOrDefault();
            }           
        }
        private void cmbParentField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbParentField.SelectedItem != null) {
                Relation.LeftField = ((CompExtention.Attribute)cmbParentField.SelectedItem).ID;
            }
            
        }

        private void cmbChildField_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbChildField.SelectedItem != null)
            {
                Relation.RightField = ((CompExtention.Attribute)cmbChildField.SelectedItem).ID;
            }
        }
    }
}
