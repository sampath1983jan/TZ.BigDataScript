using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TZ.CompExtention;

namespace TZ.ImportDesk
{
    public partial class ViewList : Form
    {
        public ViewList()
        {
            InitializeComponent();
        }
        string connection = "Server=183.82.34.174;Initial Catalog=talentozdev;Uid=admin;Pwd=admin312";
        List<CompExtention.ComponentView> Views;
         CompExtention.ComponentView View;
        private void ViewList_Load(object sender, EventArgs e)
        {
            bindViewList();
        }
        private void bindViewList()
        {
            CompExtention.ComponentViewManager cvm = new CompExtention.ComponentViewManager(new CompExtention.DataAccess.ComponentViewHandler(connection,2));
            Views = cvm.GetViews();

            lstView.Items.Clear();
            foreach (CompExtention.ComponentView s in Views)
            {
                var li = new ListViewItem(s.Name);
                lstView.Items.Add(li);
            }

        }

        private void SetViewRelation(int index, CompExtention.ComponentRelation co)
        {
         
           // var co = new CompExtention.ViewComponent() { ComponentID = "", ChildComponentID = "" };
            //view.Components.Add(co);
            int defaultHeight = 25;
            var h = 0;
            foreach (Control c in grContainer.Controls)
            {
                h = c.Height + h;
            }
            if (index != 0)
            {
                var r = new ViewRelation(co);
                r.Name = "r_" + index;
                r.Location = new Point(10, 20 + h);
                r.Height = defaultHeight;
                //r.OnHeightChanged += heighchanged;
                //r.OnRelationItemRemove += relationitemremoved;
                grContainer.Controls.Add(r);
            }
            else
            {
                var r = new ViewRelation(co);
                r.Name = "r_" + index;
                r.Location = new Point(10, 20);
                r.Height = defaultHeight;
                //r.OnRelationItemRemove += relationitemremoved;
                //r.OnHeightChanged += heighchanged;
                grContainer.Controls.Add(r);
            }
        }

        private void lstView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstView.SelectedItems.Count > 0) {
               var v= Views.Where(x => x.Name == lstView.SelectedItems[0].Text).FirstOrDefault();
                CompExtention.ComponentViewManager cvm = new CompExtention.ComponentViewManager(v.ID, 2, new CompExtention.DataAccess.ComponentViewHandler(connection,2));
                View = (CompExtention.ComponentView) cvm.GetView();
                int index = 0;
                txtView.Text = View.Name;

                grContainer.Controls.Clear();
                foreach (ComponentRelation vc in View.ComponentRelations) {
                    SetViewRelation(index,vc);
                        index = index + 1;
                }
            }         
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete", "Confirm delete", MessageBoxButtons.YesNoCancel) == DialogResult.Yes) {
                if (lstView.SelectedItems.Count > 0)
                {
                    var v = Views.Where(x => x.Name == lstView.SelectedItems[0].Text).FirstOrDefault();
                    CompExtention.ComponentViewManager cvm = new CompExtention.ComponentViewManager(v.ID,2, new CompExtention.DataAccess.ComponentViewHandler(connection,2));
                    cvm.Remove();
                    bindViewList();
                    grContainer.Controls.Clear();
                }
            }
            
            
        }
    }
}
