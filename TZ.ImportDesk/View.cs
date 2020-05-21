using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TZ.CompExtention.DataAccess;

namespace TZ.ImportDesk
{
    public partial class View : Form
    {
        public View()
        {
            InitializeComponent();
        }
        private int Clientid;
        public View(int clientID,string conn)
        {
            Clientid = clientID;
            connection = conn;
            InitializeComponent();
        }

        public  CompExtention.ComponentView view;
        int ViewitemIndex = 0;
        List<CompExtention.Component> CompList;
        string connection = "";
             
        private void PopulateListViewTreeView()
        {       
            lstComponentList.Items.Clear();
            foreach (CompExtention.Component s in CompList)
            {
                var li = new ListViewItem(s.Name);
                lstComponentList.Items.Add(li);
            }          
        }
        public void UpdateChanges(string conn, int clientid) {
            if (conn != "") {
                grContainer.Controls.Clear();
                ViewitemIndex = 0;
                CompList = new List<CompExtention.Component>();
                CompList = ComponentDataHandler.GetComponents(connection);
               // lstComponentList.ItemDrag += new ItemDragEventHandler(listView1_ItemDrag);
                view = new CompExtention.ComponentView();             
                PopulateListViewTreeView();
                AddViewRelation(0);
            }           
        }
        private void View_Load(object sender, EventArgs e)
        {
            lstComponentList.ItemDrag += new ItemDragEventHandler(listView1_ItemDrag);
            if (connection != "") {
                UpdateChanges(connection,Clientid );
            }    
        }
        private void AddViewRelation(int index) {
            var co = new CompExtention.ComponentRelation() { ComponentID = "", ChildComponentID = "" };
            view.ComponentRelations.Add(co);
            int defaultHeight = 25;
            var h = 0;
            foreach (Control c in grContainer.Controls) {
                h = c.Height + h;
            }
            if (index != 0)
            {
                var r = new ViewRelation(co, connection);
                r.Name = "r_" + index;
                r.Location = new Point(10, 20 + h);
                r.Height = defaultHeight;
                r.OnHeightChanged += heighchanged;
                r.OnRelationItemRemove += relationitemremoved;
                grContainer.Controls.Add(r);
            }
            else {
                var r = new ViewRelation(co, connection);
                r.Name = "r_" + index;
                r.Location = new Point(10, 20 );
                r.Height = defaultHeight;
                r.OnRelationItemRemove += relationitemremoved;
                r.OnHeightChanged += heighchanged;
                grContainer.Controls.Add(r);
            }          
        }
        private void Changeheight() {
            var h = 0;
            foreach (Control c in grContainer.Controls)
            {
                c.Location = new Point(10, 20 + h);
                h = c.Height + h;
            }
        }
        private void relationitemremoved(int index) {
            Changeheight();
        }
        private void heighchanged() {
            Changeheight();
        }
        private void listView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            lstComponentList.DoDragDrop(lstComponentList.SelectedItems, DragDropEffects.Move);
        }

        private void viewRelation2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListView.SelectedListViewItemCollection)))
                e.Effect = DragDropEffects.Move;
        }
        private void txtCoreComponent_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListView.SelectedListViewItemCollection).ToString(), false))
            {
                ListView.SelectedListViewItemCollection lstViewColl = (ListView.SelectedListViewItemCollection)e.Data.GetData(typeof(ListView.SelectedListViewItemCollection));
                txtCoreComponent.Text = lstViewColl[0].Text;
                //var cm = new CompExtention.ComponentManager(2, lstViewColl[0].Text, new CompExtention.DataAccess.ComponentDataHandler(Connection));
                //ChildComponent = (CompExtention.Component)cm.GetComponent();
                //cm.LoadAttributes();
                //SetComponentChildFields();
                //Component.ChildComponentID = ChildComponent.ID;
            }
        }
        private void btnSaveComponent_Click(object sender, EventArgs e)
        {
           
            ViewitemIndex = ViewitemIndex + 1;
            AddViewRelation(ViewitemIndex);
        }
 
        private void btnSaveView_Click(object sender, EventArgs e)
        {
            CompExtention.ComponentViewManager cvm = new CompExtention.ComponentViewManager(new CompExtention.DataAccess.ComponentViewHandler(connection,2));
           var view= cvm.NewView(txtView.Text);
            int Index = 0;

            var cm = new CompExtention.ComponentManager(2, txtCoreComponent.Text, new CompExtention.DataAccess.ComponentDataHandler(connection));
            view.CoreComponent = ((CompExtention.Component)cm.GetComponent()).ID;

            foreach (Control c in grContainer.Controls) {
                if (c.GetType().Name == "Button") {
                    continue;
                }
                var vr = ((ViewRelation)c);
                if (vr.Component.ComponentID != "" && ((ViewRelation)c).Component.ChildComponentID != "") {                   
                    var comp =vr.GetComponentRelation();
                    //if (Index == 0) {
                    //    view.CoreComponent = comp.ComponentID;
                    //}
                    Index = Index + 1;
                   view.ComponentRelations.Add(comp);                    
                }              
            }
            if (MessageBox.Show("Ensure all the component and its relationship assigned to this view. Are you sure want to proceed?", "Confirm Save", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
            {
                cvm.Save(view);
            }            
            MessageBox.Show("View Created Successfully");
        }

        private void txtCoreComponent_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtCoreComponent_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListView.SelectedListViewItemCollection)))
                e.Effect = DragDropEffects.Move;
        }
    }
}
