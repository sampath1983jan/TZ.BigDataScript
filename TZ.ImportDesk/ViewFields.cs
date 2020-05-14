using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TZ.CompExtention;

namespace TZ.ImportDesk
{
    public partial class ViewFields : Form
    {
        private string ViewID;
        private int ClientID;
        private string Connection;
        public ViewFields(string viewid,int clientID,string connection)
        {
            InitializeComponent();
            ViewID = viewid;
            Connection = connection;
            ClientID = clientID;
            LoadAtt();
        }
        private CompExtention.ComponentView View;
        List<CompExtention.Attribute> ViewAttributes;

        private void LoadAtt() {
            CompExtention.ComponentViewManager cvm = new CompExtention.ComponentViewManager(ViewID,
                       ClientID, new CompExtention.DataAccess.ComponentViewHandler(Connection, ClientID));           
            //cvm.LoadViewComponents();
            var v = (CompExtention.ComponentView)cvm.GetView();

            List<string> comp = new List<string>();
            comp.Add(v.CoreComponent);
            foreach (ComponentRelation vc in v.ComponentRelations)
            {
                if (v.CoreComponent != vc.ComponentID)
                {
                    comp.Add(vc.ComponentID);
                }
                if (v.CoreComponent != vc.ChildComponentID)
                {
                    comp.Add(vc.ChildComponentID);
                }
            }
            //  CompExtention.ComponentManager cm = new CompExtention.ComponentManager(((CompExtention.ComponentView)cmbViewList.SelectedItem).ID, new CompExtention.DataAccess.ComponentViewHandler(Connection));
            ViewAttributes = CompExtention.ComponentManager.GetComponentAttributes(string.Join(",", comp.ToArray()), ClientID, 
                new CompExtention.DataAccess.ComponentDataHandler(Connection));
            BindAttributes();

        }
        private void BindAttributes()
        {

            dgAttribute.Columns.Clear();

            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ID", typeof(string)));
            dt.Columns.Add(new DataColumn("ComponentID", typeof(string)));
            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            dt.Columns.Add(new DataColumn("DisplayName", typeof(string)));
       
            foreach (CompExtention.Attribute s in ViewAttributes)
            {
                DataRow dr;
                dr = dt.NewRow();
                dr["ID"] = s.ID;
                dr["ComponentID"] = s.ComponentID;
                dr["Name"] = s.Name;
                dr["DisplayName"] = s.DisplayName;
                dt.Rows.Add(dr);
            }

            dgAttribute.DataSource = dt;

            var col6 = new DataGridViewCheckBoxColumn();
            col6.HeaderText = "Is Show";
            col6.Name = "show";

            var col3 = new DataGridViewCheckBoxColumn();
            col3.HeaderText = "Is Key Field";
            col3.Name = "key";

            var col4 = new DataGridViewCheckBoxColumn();
            col4.HeaderText = "Is Default Field";
            col4.Name = "default";

            var col5 = new DataGridViewCheckBoxColumn();
            col5.HeaderText = "Is Pivot";
            col5.Name = "pivot";

            dgAttribute.Columns.AddRange(new DataGridViewColumn[] { col6, col3, col4, col5 });
            dgAttribute.Columns[0].ReadOnly = true;
            dgAttribute.Columns[1].ReadOnly = true;
            dgAttribute.Columns[2].ReadOnly = true;
            dgAttribute.Columns[3].ReadOnly = true;

            dgAttribute.Columns[0].Width = 0;
            dgAttribute.Columns[0].Visible = false;
            dgAttribute.Columns[1].Width = 0;
            dgAttribute.Columns[1].Visible = false;
            dgAttribute.Columns[3].Width = 200;
            dgAttribute.Columns[4].Width = 70;
            dgAttribute.Columns[5].Width = 70;
            dgAttribute.Columns[6].Width = 70;
            dgAttribute.Columns[7].Width = 60;
             
        }
    }
}
