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
    public partial class ViewRelation : UserControl
    {
        public ViewRelation()
        {
            InitializeComponent();
        }

        public ViewRelation(CompExtention.ComponentRelation vc,string connection)
        {
            InitializeComponent();
            Connection = connection;
            Component = vc;
        
        }
        public CompExtention.ComponentRelation Component { get; set; }
                
        private CompExtention.Component ParentComponent { get; set; }
        private CompExtention.Component ChildComponent { get; set; }

        public delegate void HeightChanged();
        public event HeightChanged OnHeightChanged;

        public delegate void RelationItemRemove(int index);
        public event RelationItemRemove OnRelationItemRemove;
        string Connection = "";
        int itemIndex = 0;
        int defaultHeight = 25;

        private void ViewRelation_Load(object sender, EventArgs e)
        {
            if (Connection != "") {
                if (Component.Relationship.Count == 0)
                {
                    var vr = new CompExtention.ViewRelation();
                    AddViewRelation(itemIndex, vr);
                    Component.Relationship.Add(vr);
                }
                else
                {
                    foreach (CompExtention.ViewRelation vr in Component.Relationship)
                    {
                        AddViewRelation(itemIndex, vr);
                        itemIndex = itemIndex + 1;
                    }
                }
            }              
        }
        private void AddViewRelation(int index, CompExtention.ViewRelation vr)
        {         
           
            if (index != 0)
            {
                var r = new ViewRelationItem(index, vr);
                r.Name = "ritem_" + index;
                r.Location = new Point(10, 20 + (defaultHeight * index));
                r.Height = defaultHeight;
                r.AfterRemoved += AfterRemove;
                gbRelation.Controls.Add(r);
            }
            else
            {
                var r = new ViewRelationItem(index, vr);
                r.Name = "ritem_" + index;
                r.Location = new Point(10, 20);
                r.Height = defaultHeight;
                r.AfterRemoved += AfterRemove;
                gbRelation.Controls.Add(r);
            }            
            SetComponentChildFields();
            SetComponentParentFields();
            ChangePosition();
            if (OnHeightChanged != null) {
                OnHeightChanged();
            }
         
        }
        public CompExtention.ComponentRelation GetComponentRelation() {
            var cc = Component.Clone();
            List<CompExtention.ViewRelation> vr = new List<CompExtention.ViewRelation>();
            foreach (Control c in gbRelation.Controls) {
                var vritem = ((ViewRelationItem)c);
                vritem.Relation.Left = ParentComponent.ID;
                vritem.Relation.Right = ChildComponent.ID;
                vr.Add(vritem.Relation);
            }
            cc.Relationship = vr;
            return cc;
        }
        private void SetComponentParentFields()
        {
            foreach (Control c in gbRelation.Controls)
            {
                if (ParentComponent != null)
                {
                    string left = ((ViewRelationItem)c).Relation.LeftField;
                    ((ViewRelationItem)c).ParentFields = ParentComponent.Attributes.Clone();
                    ((ViewRelationItem)c).SetParentField(left);
                }
            }
        }
        private void SetComponentChildFields()
        {
            foreach (Control c in gbRelation.Controls)
            {
                if (ChildComponent != null)
                {
                    string right = ((ViewRelationItem)c).Relation.RightField;
                    ((ViewRelationItem)c).ChildFields = ChildComponent.Attributes.Clone();
                    ((ViewRelationItem)c).SetChildField(right);
                }
            }
        }
        private void ChangePosition()
        {
            int i = 0;
            foreach (Control c in gbRelation.Controls)
            {
                if (i == 0)
                {
                    c.Location = new Point(10, 20 + (defaultHeight * (i)));
                }
                else
                {
                    c.Location = new Point(10, 20 + (defaultHeight * (i)) + (5 * i));
                }

                //  c.Height = defaultHeight ;
                i = i + 1;
            }
            itemIndex = i - 1;
        }
        private void AfterRemove(int index)
        {
            Control cntrl = null;
            foreach (Control c in gbRelation.Controls)
            {
                if (c.Name == "ritem_" + index)
                {
                    cntrl = c;
                }
            }
            if (cntrl != null)
            {
                gbRelation.Controls.Remove(cntrl);
            }
            ChangePosition();

            OnRelationItemRemove(index);
        }
        private void lnkAddRelation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            itemIndex = itemIndex + 1;
            var vr = new CompExtention.ViewRelation();
            AddViewRelation(itemIndex, vr);
            Component.Relationship.Add(vr);
        }

        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListView.SelectedListViewItemCollection).ToString(), false)) {
                ListView.SelectedListViewItemCollection lstViewColl = (ListView.SelectedListViewItemCollection)e.Data.GetData(typeof(ListView.SelectedListViewItemCollection));
                textBox1.Text = lstViewColl[0].Text;
                var cm = new CompExtention.ComponentManager(2, lstViewColl[0].Text, new CompExtention.DataAccess.ComponentDataHandler(Connection));
                ParentComponent = (CompExtention.Component)cm.GetComponent();
                 cm.LoadAttributes();
                SetComponentParentFields();
                Component.ComponentID = ParentComponent.ID;
            }
        }

        private void textBox2_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListView.SelectedListViewItemCollection).ToString(), false))
            {
                ListView.SelectedListViewItemCollection lstViewColl = (ListView.SelectedListViewItemCollection)e.Data.GetData(typeof(ListView.SelectedListViewItemCollection));
                textBox2.Text = lstViewColl[0].Text;
                var cm = new CompExtention.ComponentManager(2, lstViewColl[0].Text, new CompExtention.DataAccess.ComponentDataHandler(Connection));
                ChildComponent = (CompExtention.Component)cm.GetComponent();
                cm.LoadAttributes();
                SetComponentChildFields();
                Component.ChildComponentID = ChildComponent.ID;
            }
        }

        private void textBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListView.SelectedListViewItemCollection)))
                e.Effect = DragDropEffects.Move;

        }

        private void textBox2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListView.SelectedListViewItemCollection)))
                e.Effect = DragDropEffects.Move;
        }              

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        
    
       
        
    }
}
