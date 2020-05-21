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
    public partial class PivotTemplate : UserControl
    {
        public PivotTemplate()
        {
            InitializeComponent();                    
        }
        public List<string> Rows { get; set; }
        public string Column { get; set; }
        public string PivotColumn { get; set; }

        //public delegate void PivotEventHandler(object sender,AttributeArgument ar);
        public event AttributeList.MyEventHandler  AfterSelect;
        public PivotTemplate(List<CompExtention.Attribute> cat) {
            ComponentAttribute = cat;
            Rows = new List<string>();
            ComponentAttribute=ComponentAttribute.OrderBy(x => x.DisplayName).ToList();
            InitializeComponent();         
        }      
        public List<CompExtention.Attribute> ComponentAttribute { get; set; }
        private void BindRow() {
            lblRows.Text = "";
            foreach (string s in this.Rows) {
                lblRows.Text = lblRows.Text +"," +  ComponentAttribute.Where(x => x.ID == s).Select(x => x.DisplayName).FirstOrDefault();
            }
            if (lblRows.Text.StartsWith(",")) {
                lblRows.Text = lblRows.Text.Substring(1);
            }
        }
        private void BindColumn() {
            lblCol.Text =  ComponentAttribute.Where(x => x.ID == this.Column).Select(x => x.DisplayName).FirstOrDefault();
        }
        private void BindPivotColumn()
        {
            lblpCol.Text = ComponentAttribute.Where(x => x.ID == this.PivotColumn).Select(x => x.DisplayName).FirstOrDefault();
        }
        public void onAfterSelect( object sender, AttributeArgument ar) {
            if (ar.PivotType == 1)
            {
                Rows = ar.AttributeIDs;
                BindRow();
            }else if (ar.PivotType == 2) {
                Column = ar.AttributeID;
                BindColumn();
            }
            else
            {
               PivotColumn  = ar.AttributeID;
                BindPivotColumn();
            }                      
                     
        }
        private void lkRow_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AttributeList childForm = new AttributeList(ComponentAttribute,1);
            childForm.SelectedValues = Rows;
        //    childForm.MdiParent = this.ParentForm.ParentForm;
            childForm.Text = "Attribute list";
            childForm.AfterAttributeSelected += new AttributeList.MyEventHandler(onAfterSelect);
            //this.fo.Hide();
            childForm.Show();
        }
        
        private void lkColumn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AttributeList childForm = new AttributeList(ComponentAttribute, 2);
            //    childForm.MdiParent = this.ParentForm.ParentForm;
            childForm.SelectedValue =Column;
            childForm.Text = "Attribute list";
            childForm.AfterAttributeSelected += new AttributeList.MyEventHandler(onAfterSelect);  
            //this.fo.Hide();
            childForm.Show();
        }

        private void lkpColumn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AttributeList childForm = new AttributeList(ComponentAttribute, 3);
            //    childForm.MdiParent = this.ParentForm.ParentForm;
            childForm.SelectedValue = PivotColumn;
            childForm.Text = "Attribute list";
            childForm.AfterAttributeSelected += new AttributeList.MyEventHandler(onAfterSelect);
            //this.fo.Hide();
            childForm.Show();
        }

        private void PivotTemplate_Load(object sender, EventArgs e)
        {
      
            BindRow();
            BindColumn();
            BindPivotColumn();
        }
    }
}
