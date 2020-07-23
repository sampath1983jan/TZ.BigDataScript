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
using TZ.CompExtention.ImportTemplate;

namespace TZ.ImportDesk
{
    public partial class Template : Form
    {
        public Template()
        {
            InitializeComponent();
        }

        public Template(int clientID, string connection)
        {
            Connection = connection;
            ClientID = clientID;
            InitializeComponent();
        }

        string Connection = "";
        List<CompExtention.ImportTemplate.Template> Templates;
        CompExtention.ImportTemplate.Template SelectedTemplate;
        List<CompExtention.ComponentView> Views;
        CompExtention.ComponentView SelectedView;
        List<CompExtention.Attribute> ViewAttributes;
        int ClientID;
        Dictionary<int, string> type = new Dictionary<int, string>();
        private PivotTemplate Pivot { get; set; }
        private void Template_Load(object sender, EventArgs e)
        {
            if (Connection != "")
            {
                UpdateChanges(Connection, ClientID);
            }
        }
        public void UpdateChanges(string connection, int clientid)
        {
            ClientID = clientid;
            Connection = connection;
            ViewAttributes = new List<CompExtention.Attribute>();
            SelectedView = new ComponentView();
            Views = new List<ComponentView>();
            SelectedTemplate = new CompExtention.ImportTemplate.Template();
            Templates = new List<CompExtention.ImportTemplate.Template>();
            Pivot = new PivotTemplate();
            type = new Dictionary<int, string>();

            type.Add(0, "Direct");
            type.Add(1, "Pivot");
            BindCompType();
            bindTemplate();
            bindView();
        }

        private void BindAttributes(List<CompExtention.Attribute> va)
        {

            dgAttribute.Columns.Clear();

            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ID", typeof(string)));
            dt.Columns.Add(new DataColumn("ComponentID", typeof(string)));
            dt.Columns.Add(new DataColumn("Name", typeof(string)));
            dt.Columns.Add(new DataColumn("DisplayName", typeof(string)));

            foreach (CompExtention.Attribute s in va)
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

            var col7 = new DataGridViewCheckBoxColumn();
            col7.HeaderText = "Is Required";
            col7.Name = "required";

            var col4 = new DataGridViewCheckBoxColumn();
            col4.HeaderText = "Is Default Field";
            col4.Name = "default";

            var col5 = new DataGridViewCheckBoxColumn();
            col5.HeaderText = "Is Pivot";
            col5.Name = "pivot";

            dgAttribute.Columns.AddRange(new DataGridViewColumn[] { col6, col3, col7, col4, col5 });
            dgAttribute.Columns[0].ReadOnly = true;
            dgAttribute.Columns[1].ReadOnly = true;
            dgAttribute.Columns[2].ReadOnly = true;
            dgAttribute.Columns[3].ReadOnly = true;

            dgAttribute.Columns[0].Width = 0;
            dgAttribute.Columns[0].Visible = false;
            dgAttribute.Columns[1].Width = 0;
            dgAttribute.Columns[1].Visible = false;
            dgAttribute.Columns[3].Width = 230;
            dgAttribute.Columns[4].Width = 70;
            dgAttribute.Columns[5].Width = 70;
            dgAttribute.Columns[6].Width = 70;
            dgAttribute.Columns[7].Width = 70;
            dgAttribute.Columns[8].Width = 60;
            SetValues();
        }

        private void SetTemplatefieldtoGrid(bool UpdateSource = false)
        {
            if (SelectedTemplate != null)
            {
                foreach (DataGridViewRow drv in dgAttribute.Rows)
                {
                    if (drv.Cells[3].Value != null)
                    {
                        var id = drv.Cells[0].Value.ToString();
                        var tf = SelectedTemplate.TemplateFields.Where(x => x.ID == id).FirstOrDefault();
                        if (tf != null)
                        {
                            if (tf.IsKey == true)
                            {
                                DataGridViewCheckBoxCell chk1 = (DataGridViewCheckBoxCell)drv.Cells["key"];
                                chk1.Value = true;

                            }
                            if (tf.IsDefault == true)
                            {
                                DataGridViewCheckBoxCell chk2 = (DataGridViewCheckBoxCell)drv.Cells["default"];
                                chk2.Value = true;
                            }
                            if (tf.IsPivot == true)
                            {
                                DataGridViewCheckBoxCell chk3 = (DataGridViewCheckBoxCell)drv.Cells["pivot"];
                                chk3.Value = true;

                            }
                            if (tf.IsRequired == true)
                            {
                                DataGridViewCheckBoxCell chk3 = (DataGridViewCheckBoxCell)drv.Cells["required"];
                                chk3.Value = true;

                            }
                            DataGridViewCheckBoxCell chk4 = (DataGridViewCheckBoxCell)drv.Cells["show"];
                            chk4.Value = true;
                        }
                        else
                        {
                            if (UpdateSource)
                            {
                                if (Convert.ToBoolean((DataGridViewCheckBoxCell)drv.Cells["default"].Value) == true ||
                                       Convert.ToBoolean((DataGridViewCheckBoxCell)drv.Cells["pivot"].Value) == true ||
                                        Convert.ToBoolean((DataGridViewCheckBoxCell)drv.Cells["required"].Value) == true ||
                                        Convert.ToBoolean((DataGridViewCheckBoxCell)drv.Cells["show"].Value) == true ||
                                         Convert.ToBoolean((DataGridViewCheckBoxCell)drv.Cells["key"].Value) == true)
                                {
                                    if (Convert.ToBoolean((DataGridViewCheckBoxCell)drv.Cells["show"].Value) == true)
                                    {
                                        tf.ID = id;
                                        tf.IsKey = Convert.ToBoolean((DataGridViewCheckBoxCell)drv.Cells["key"].Value);
                                        tf.IsKey = Convert.ToBoolean((DataGridViewCheckBoxCell)drv.Cells["show"].Value);

                                        tf.IsRequired = Convert.ToBoolean((DataGridViewCheckBoxCell)drv.Cells["required"].Value);
                                        tf.IsPivot = Convert.ToBoolean((DataGridViewCheckBoxCell)drv.Cells["pivot"].Value);
                                        tf.IsDefault = Convert.ToBoolean((DataGridViewCheckBoxCell)drv.Cells["default"].Value);

                                        SelectedTemplate.TemplateFields.Add(tf);
                                    }
                                }
                            }

                            //if (UpdateSource) {
                            //    var att = ViewAttributes.Where(x => x.ID == id).FirstOrDefault();
                            //    if (att.IsKey == false) {
                            //        tf = new TemplateField() { ID = id, IsDefault = att.IsRequired, IsKey = false, IsPivot = false, IsRequired = att.IsRequired };
                            //    }                           
                            //    SelectedTemplate.TemplateFields.Add(tf);
                            //}                      
                        }
                    }

                }
            }

        }
        private void SetValues()
        {
            if (SelectedTemplate != null)
            {

                txtTemplateName.Text = SelectedTemplate.Name;
                txtCategory.Text = SelectedTemplate.Category;
                txtCode.Text = SelectedTemplate.Code;
                cmbType.SelectedIndex = (int)SelectedTemplate.Type;
                SetTemplatefieldtoGrid(true);
            }
        }
        private void BindCompType()
        {
            cmbType.ValueMember = "Key";
            cmbType.DisplayMember = "Value";
            cmbType.DataSource = type.ToArray();
        }
        private void bindTemplate()
        {
            // CompExtention.ImportTemplate.Template imp = new CompExtention.ImportTemplate.Template(Connection);
            Templates = CompExtention.ImportTemplate.Template.GetTemplates(ClientID, Connection);
            lstTemplate.Items.Clear();
            foreach (CompExtention.ImportTemplate.Template s in Templates)
            {
                var row = new string[] { s.Name, s.Code, s.Category };
                var li = new ListViewItem(row);
                lstTemplate.Items.Add(li);
            }
        }

        private void bindView()
        {
            CompExtention.ComponentViewManager cvm = new CompExtention.ComponentViewManager(new CompExtention.DataAccess.ComponentViewHandler(Connection, ClientID));
            Views = cvm.GetViews();
            cmbViewList.DisplayMember = "Name";
            cmbViewList.ValueMember = "ID";
            cmbViewList.DataSource = Views;
        }

        private void lstTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTemplate.SelectedItems.Count > 0)
            {
                CompExtention.ImportTemplate.Template imp = new CompExtention.ImportTemplate.Template(Connection, ClientID);
                SelectedTemplate = Templates[lstTemplate.SelectedIndices[0]];
                cmbViewList.SelectedItem = null;
                cmbViewList.SelectedItem = Views.Where(x => x.ID == SelectedTemplate.ViewID).FirstOrDefault();
                cmbType.Enabled = false;
                cmbViewList.Enabled = false;

            }
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            cmbType.Enabled = true;
            cmbViewList.Enabled = true;
            SelectedTemplate = null;
            txtCode.Text = "";
            txtTemplateName.Text = "";
            txtCategory.Text = "";
            bindView();
            BindCompType();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (SelectedTemplate != null)
            {
                CompExtention.ImportTemplate.Template tmp = new CompExtention.ImportTemplate.Template(SelectedTemplate.TemplateID, ClientID, Connection);
                if (MessageBox.Show("Are you sure,want to remove this template?", "Confirm Remove", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
                {
                    if (tmp.Remove())
                    {
                        MessageBox.Show("Template Removed");
                        bindTemplate();
                        btnNew_Click(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Unable to remove template");
                    }
                }


            }
            else
            {
                MessageBox.Show("Choose one template from below list and then delete");
            }
        }
        private void cmbViewList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbViewList.SelectedItem != null)
            {
                CompExtention.ComponentViewManager cvm = new CompExtention.ComponentViewManager(((CompExtention.ComponentView)cmbViewList.SelectedItem).ID,
                    ClientID, new CompExtention.DataAccess.ComponentViewHandler(Connection, ClientID));
                SelectedView = (CompExtention.ComponentView)cvm.GetView();
                List<string> comp = new List<string>();
                comp.Add(SelectedView.CoreComponent);
                foreach (ComponentRelation vc in SelectedView.ComponentRelations)
                {
                    if (SelectedView.CoreComponent != vc.ComponentID)
                    {
                        comp.Add(vc.ComponentID);
                    }
                    if (SelectedView.CoreComponent != vc.ChildComponentID)
                    {
                        comp.Add(vc.ChildComponentID);
                    }
                }
                ViewAttributes = CompExtention.ComponentManager.GetComponentAttributes(string.Join(",", comp.ToArray()), ClientID, new CompExtention.DataAccess.ComponentDataHandler(Connection));
                ViewAttributes= ViewAttributes.OrderBy(x=> x.DisplayName ).ToList ();
                BindAttributes(ViewAttributes);
            }

        }
        private void btnSaveTemplate_Click(object sender, EventArgs e)
        {
            if (SelectedTemplate == null)
            {
                SelectedTemplate = new CompExtention.ImportTemplate.Template();
            }
            CompExtention.ImportTemplate.Template tmp;
            if (SelectedTemplate.TemplateID != "")
            {
                tmp = new CompExtention.ImportTemplate.Template(SelectedTemplate.TemplateID, ClientID, Connection);
            }
            else
            {
                tmp = new CompExtention.ImportTemplate.Template(Connection, ClientID);
            }
            tmp.Name = txtTemplateName.Text;
            tmp.Code = txtCode.Text;
            tmp.Category = txtCategory.Text;
            tmp.ViewID = SelectedView.ID;
            if (((KeyValuePair<int, string>)cmbType.SelectedItem).Key == 0)
            {
                tmp.Type = CompExtention.ImportTemplate.Template.TemplateType.DIRECT;
                var Removed = new List<TemplateField>();

                for (int x = 0; x < dgAttribute.Rows.Count; x++)
                {
                    CompExtention.ImportTemplate.TemplateField te = new CompExtention.ImportTemplate.TemplateField();
                    if (dgAttribute.Rows[x].Cells[3].Value == null)
                    {
                        continue;
                    }
                    te.ID = dgAttribute.Rows[x].Cells[0].Value.ToString();

                    if (Convert.ToBoolean(dgAttribute.Rows[x].Cells["show"].Value) == true)
                    {
                        if (Convert.ToBoolean(dgAttribute.Rows[x].Cells["key"].Value))
                        {
                            te.IsKey = true;
                        }
                        else
                        {
                            te.IsKey = false;
                        }
                        if (Convert.ToBoolean(dgAttribute.Rows[x].Cells["default"].Value))
                        {
                            te.IsDefault = true;
                        }
                        else
                        {
                            te.IsDefault = false;
                        }
                        if (Convert.ToBoolean(dgAttribute.Rows[x].Cells["pivot"].Value))
                        {
                            te.IsPivot = true;
                        }
                        else
                        {
                            te.IsPivot = false;
                        }
                        if (Convert.ToBoolean(dgAttribute.Rows[x].Cells["required"].Value))
                        {
                            te.IsRequired = true;
                        }
                        else
                        {
                            te.IsRequired = false;
                        }
                        if (te.ID != "")
                        {
                            var tmap = tmp.TemplateFields.Where(xx => xx.ID == te.ID).FirstOrDefault();


                            if (tmap != null)
                            {
                                for (int i = 0; i < tmp.TemplateFields.Count; i++)
                                {
                                    if (tmp.TemplateFields[i].ID == te.ID)
                                    {                                      
                                            tmp.TemplateFields[i] = te;                                        
                                    }
                                }
                            }
                            else
                            {
                                tmp.TemplateFields.Add(te);
                            }
                        }
                    }
                    else
                    {
                        var tmap = tmp.TemplateFields.Where(xx => xx.ID == te.ID).FirstOrDefault();


                        if (tmap != null)
                        {
                            for (int i = 0; i < tmp.TemplateFields.Count; i++)
                            {
                                if (tmp.TemplateFields[i].ID == te.ID)
                                {
                                    if (te.IsKey == false  && te.IsDefault == false && te.IsRequired == false && te.IsPivot == false)
                                    {
                                        Removed.Add(te);
                                    }
                                    else
                                    {
                                        tmp.TemplateFields[i] = te;
                                    }
                                }
                            }
                        }
                    }
                }

                var temp = new List<TemplateField>();
                foreach (TemplateField f in tmp.TemplateFields)
                {
                    if (Removed.Where(x => x.ID == f.ID).FirstOrDefault() == null)
                    {
                        temp.Add(f);
                    }
                }
                tmp.TemplateFields = temp;
            }
            else
            {
                tmp.Type = CompExtention.ImportTemplate.Template.TemplateType.PIVOT;
                if (Pivot != null)
                {
                    var te = new CompExtention.ImportTemplate.TemplateField();
                    foreach (string s in Pivot.Rows)
                    {
                        te = new CompExtention.ImportTemplate.TemplateField();
                        te.ID = s;
                        te.IsRow = true;
                        te.IsRequired = true;
                        te.IsKey = true;
                        te.IsColumn = false;
                        te.IsPivot = false;
                        te.IsDefault = true;
                        tmp.TemplateFields.Add(te);
                    }
                    te = new CompExtention.ImportTemplate.TemplateField();
                    te.ID = Pivot.Column;
                    te.IsRow = false;
                    te.IsColumn = true;
                    te.IsPivot = false;
                    te.IsRequired = true;
                    te.IsKey = true;
                    te.IsDefault = true;
                    tmp.TemplateFields.Add(te);

                    te = new CompExtention.ImportTemplate.TemplateField();
                    te.ID = Pivot.PivotColumn;
                    te.IsRow = false;
                    te.IsColumn = false;
                    te.IsPivot = true;
                    te.IsRequired = true;
                    te.IsKey = true;
                    te.IsDefault = true;
                    tmp.TemplateFields.Add(te);
                }
            }

            if (tmp.Save())
            {
                MessageBox.Show("Templated Created Successfully");
                bindTemplate();
                btnNew_Click(null, null);
            }
            else
            {
                MessageBox.Show("Error while save template");
            }
        }

        private void AddPivotDesigner()
        {
            Pivot = new PivotTemplate(ViewAttributes);
            Pivot.Name = "PDesigner";
            Pivot.Location = new Point(6, 32);
            if (this.SelectedTemplate != null)
            {
                foreach (TemplateField tf in this.SelectedTemplate.TemplateFields)
                {
                    if (tf.IsRow)
                    {
                        Pivot.Rows.Add(tf.ID);
                    }
                    if (tf.IsColumn)
                    {
                        Pivot.Column = tf.ID;
                    }
                    if (tf.IsPivot)
                    {
                        Pivot.PivotColumn = tf.ID;
                    }
                }
            }
            groupBox1.Controls.Add(Pivot);
        }
        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((KeyValuePair<int, string>)cmbType.SelectedItem).Key == 0)
            {
                if (groupBox1.Controls.Find("PDesigner", false).Count() > 0)
                {
                    groupBox1.Controls.Remove(groupBox1.Controls.Find("PDesigner", false).FirstOrDefault());
                }
                lblSearch.Visible = true;
                txtSearch.Visible = true;
                dgAttribute.Visible = true;
            }
            else if (((KeyValuePair<int, string>)cmbType.SelectedItem).Key == 1)
            {
                lblSearch.Visible = false;
                txtSearch.Visible = false;
                dgAttribute.Visible = false;
                AddPivotDesigner();
            }
        }

        private void lstAttribute_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgAttribute_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lkfull_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form childForm = new ViewFields(((CompExtention.ComponentView)cmbViewList.SelectedItem).ID, ClientID, Connection);
            childForm.MdiParent = this.ParentForm;
            childForm.Text = "Field List";
            childForm.Show();
        }

        private void dgAttribute_Sorted(object sender, EventArgs e)
        {
            SetValues();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
            {
                SetTemplatefieldtoGrid(true);
                var filterList = ViewAttributes.Where(x => x.Name.Contains(txtSearch.Text.ToLower()) || x.DisplayName.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
                BindAttributes(filterList);
            }
            else
            {
                BindAttributes(ViewAttributes);
            }

        }
    }
}
