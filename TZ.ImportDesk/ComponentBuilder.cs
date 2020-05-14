using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using TZ.CompExtention.DataAccess;

namespace TZ.ImportDesk
{
    public partial class ComponentBuilder : Form
    {
        public ComponentBuilder()
        {
            InitializeComponent();
        }
        public string SelectedTable;

        int ClientID;
        //List<string> tbls;
        //List<CompExtention.Attribute> attributes = new List<CompExtention.Attribute>();
        IDictionary<int, string> d = new Dictionary<int, string>();
        DataTable dtLookup = new DataTable();
        List<CompExtention.Attribute> compDisplayField = new List<CompExtention.Attribute>();
        string[] AttributeTypeList =       { "_number",
        "_decimal",
        "_string",
        "_longstring",
        "_currency",
        "_lookup",
       "_componentlookup",
        "_file",
        "_picture",
        "_date",
        "_time",
        "_datetime",
        "_bit"};
        string[] componentType = {"core",
          "attribute",
         "meta",
       "link",
        "transaction",
        "configuration",
        "system",
       "none"
        };

        string connection = "Server=183.82.34.174;Initial Catalog=talentozdev;Uid=admin;Pwd=admin312";
        List<CompExtention.Component> CompWithTableList = new List<CompExtention.Component>();
        
                    List<CompExtention.Component> CompList = new List<CompExtention.Component>();
        CompExtention.Attribute SelectedAttribute;
        CompExtention.Attribute InitialAttribute;
            
        List<string> ChangedAttribute = new List<string>();
        Dictionary<int, string> CompType = new Dictionary<int, string>();
        CompExtention.Component component;
        TZ.CompExtention.ComponentManager cm;
        private void MyForm_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void ComponentBuilder_Load(object sender, EventArgs e)
        {
            int index = 0;
            foreach (string i in componentType)
            {
                CompType.Add(index, i);
                index = index + 1;
            }
            //component = new CompExtention.Component();

            //  TZ.CompExtention.Shared.ExecuteSetup(" Server=183.82.34.174;Initial Catalog=talentozdev;Uid=admin;Pwd=admin312");
            TZ.CompExtention.ComponentBuilder cb = new CompExtention.ComponentBuilder(connection);
            CompWithTableList= cb.GetTableAsComponent();
            bindList(CompWithTableList);
            bindClients();
            bindAttributeType();
            //bindLookup();
           bindComponentList();
            cmbLookUp.Enabled = false;
            bindComponentCombo();
            bindComponentType();
            gbField.Enabled = true;
            gbAttributes.Enabled = true;
            btnSaveComponent.Visible = true;           
            btnCancel.Visible = true;
        }
        private void bindClients()
        {
            DataTable dtClient = new DataTable();
            dtClient = CompExtention.Shared.GetClientList(connection);
            cmbClients.ValueMember = "ClientID";
            cmbClients.DisplayMember = "CustomerName";
            cmbClients.DataSource = dtClient;
        }
        private void bindComponentList()
        {

      CompList = ComponentDataHandler.GetComponents(connection);
        }
        private void bindComponentLookupDisplayName(string lookupcomp)
        {
            GetComponentLookUp(lookupcomp);
            cmbCompDisplayName.DisplayMember = "DisplayName";
            cmbCompDisplayName.ValueMember = "ID";
            cmbCompDisplayName.DataSource = compDisplayField;
        }
        private void bindComponentType()
        {
            cmbType.DisplayMember = "Value";
            cmbType.ValueMember = "Key";
            cmbType.DataSource = CompType.ToList();
        }
        private void GetComponentLookUp(string ccid)
        {
            compDisplayField = ComponentDataHandler.GetAttributes(connection, ccid, ClientID);
        }
        private void bindAttributeType()
        {
            int i = 0;
            d.Add(new KeyValuePair<int, string>(-1, "Select"));
            foreach (string s in AttributeTypeList)
            {
                KeyValuePair<int, string> attributeType = new KeyValuePair<int, string>(i, s.Replace("_", "").ToUpper());
                i = i + 1;
                d.Add(attributeType);
            }
            cmbAttributeType.ValueMember = "Key";
            cmbAttributeType.DisplayMember = "Value";
            cmbAttributeType.DataSource = d.ToList();
        }
        private void bindLookup()
        {
            if (dtLookup.Rows.Count == 0)
            {
                dtLookup = CompExtention.Shared.GetLookup(ClientID, " Server=183.82.34.174;Initial Catalog=talentozdev;Uid=admin;Pwd=admin312");
                dtLookup.DefaultView.Sort = "Name";
                dtLookup = dtLookup.DefaultView.ToTable();
            }

            cmbLookUp.ValueMember = "FieldInstanceID";
            cmbLookUp.DisplayMember = "Name";
            cmbLookUp.DataSource = dtLookup;
        }
        private void bindComponentCombo()
        {
            cmbCompLookup.DisplayMember = "Name";
            cmbCompLookup.ValueMember = "ID";
            cmbCompLookup.DataSource = CompList;
        }
        private void cmbCompLookup_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void bindList(List<CompExtention.Component> _tbls)
        {
            lstTableList.Items.Clear();
            foreach (CompExtention.Component s in _tbls)
            {
                var row = new string[] {s.TableName, s.Name, s.Type.ToString() };
                var li = new ListViewItem(row);                
                lstTableList.Items.Add(li);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstTableList_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (lstTableList.SelectedItems.Count > 0)
            {

                SelectedTable = lstTableList.SelectedItems[0].Text; // table Name 

                SetComponentAndAttribute();
            }

        }
            private void SetComponentAndAttribute() {
            var ecomp = CompWithTableList.Where(x => x.TableName == SelectedTable).FirstOrDefault();
            TZ.CompExtention.ComponentBuilder cb = new CompExtention.ComponentBuilder(" Server=183.82.34.174;Initial Catalog=talentozdev;Uid=admin;Pwd=admin312");
                if (ecomp.Type != CompExtention.ComponentType.none)
                {

                    txtComponentName.Text = ecomp.Name;
                txtEntityKey.Text = ecomp.EntityKey;
                    lblMsg.Text = "This table already created as component";
                    // Get Existing Component For that table
                    SetExistComponent();
                }
                else
                {
                    cm = new CompExtention.ComponentManager();
                    cm.Set(new TZ.CompExtention.DataAccess.ComponentDataHandler(connection));
                    cm.NewComponent(ClientID, lstTableList.SelectedItems[0].Text, (CompExtention.ComponentType)((KeyValuePair<int, string>)cmbType.SelectedItem).Key);
                    component = (CompExtention.Component)cm.Component;
                    component.TableName = lstTableList.SelectedItems[0].Text;
                    txtComponentName.Text = lstTableList.SelectedItems[0].Text;
                    txtEntityKey.Text = "";
                    lblMsg.Text = "";
                    component.Attributes = cb.GetTableFields(SelectedTable, ClientID);
                    bindAttributeList();
                }
             
        }
        private void SetExistComponent() {
            cm = new CompExtention.ComponentManager(ClientID, SelectedTable, new TZ.CompExtention.DataAccess.ComponentDataHandler(connection));
            cm.LoadAttributes();
            cmbType.SelectedItem = CompType.Where(x => x.Key == (int)cm.Component.Type).FirstOrDefault();
            component = (CompExtention.Component)cm.Component;
            bindAttributeList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="att"></param>
        private void SetValue()
        {
            var att = SelectedAttribute;
            var item = d.Where(x => x.Value.ToLower() == att.Type.ToString().Replace("_", "").ToLower()).FirstOrDefault();
            //   if (item != null) {
            cmbAttributeType.SelectedItem = item;
            txtLength.Text = att.Length.ToString();
            ckKey.Checked = att.IsKey;
            ckRequied.Checked = att.IsRequired;
            ckNull.Checked = att.IsNullable;
            txtDefaultValue.Text = att.DefaultValue;
            txtDisplay.Text = att.DisplayName;
            bindComboLookup();
            //}
            //  cmbAttributeType.SelectedItem = cmbAttributeType.Items.IndexOf(att.Type.ToString().Replace("_", ""));//comboBox1.Items.IndexOf("test1")att.Type;
        }
        /// <summary>
        /// 
        /// </summary>
        private void bindComboLookup()
        {
            var att = SelectedAttribute;
            if (att.Type == CompExtention.AttributeType._lookup)
            {
                cmbLookUp.Enabled = true;
                bindLookup();
                cmbLookUp.SelectedItem = dtLookup.AsEnumerable().Where(x => x["FieldInstanceID"].ToString() == att.LookupInstanceID.ToString()).FirstOrDefault();
            }
            else
            {
                cmbLookUp.Enabled = false;
                cmbLookUp.SelectedItem = "";
            }
            if (att.Type == CompExtention.AttributeType._componentlookup)
            {
                bindComponentCombo();
                bindComponentLookupDisplayName(att.ComponentLookup);
                cmbCompLookup.Enabled = true;
                cmbCompDisplayName.Enabled = true;
                cmbCompLookup.SelectedItem = CompList.Where(x => x.ID == att.ComponentLookup).FirstOrDefault();
                cmbCompDisplayName.SelectedItem = compDisplayField.Where(x => x.ID == att.ComponentLookupDisplayField).FirstOrDefault();
            }
            else
            {
                cmbCompLookup.Enabled = false;
                cmbCompDisplayName.Enabled = false;
                cmbCompDisplayName.SelectedItem = "";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void bindAttributeList()
        {
            lstDetail.Items.Clear();
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            foreach (CompExtention.Attribute s in component.Attributes)
            {
                var row = new string[] { s.Name,s.DisplayName, textInfo.ToTitleCase((s.Type.ToString().Replace("_", ""))), s.Length.ToString(), s.IsKey.ToString() };
                var li = new ListViewItem(row);
                if (ChangedAttribute.Where(x => x == s.Name).FirstOrDefault() != null)
                {
                    li.BackColor = System.Drawing.Color.BlueViolet;
                    li.ForeColor = System.Drawing.Color.White;
                }
                lstDetail.Items.Add(li);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            bindList(filterComponent());

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckReadOnly_CheckedChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckKey_CheckedChanged(object sender, EventArgs e)
        {
            if (ckKey.Checked == true)
            {
                ckUnique.Enabled = false;
                ckNull.Enabled = false;
                ckUnique.Checked = false;
                ckNull.Checked = false;
                ckRequied.Checked = true;
            }
            else
            {
                ckUnique.Enabled = true;
                ckNull.Enabled = true;
                ckUnique.Checked = false;
                ckNull.Checked = true;
                ckRequied.Checked = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (component.Attributes.Count > 0)
            {
                if (lstDetail.SelectedItems.Count > 0)
                {
                    var s = component.Attributes.Where(x => x.Name == lstDetail.SelectedItems[0].Text).FirstOrDefault();
                    SelectedAttribute = s;
                     InitialAttribute = s.Clone();
                    {
                        if (SelectedAttribute != null)
                        SetValue();
                    }
                    gbField.Text = "Field Attribute - " + SelectedAttribute.DisplayName;
                }
            }
        }

        private void cmbAttributeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var s = (KeyValuePair<int, string>)cmbAttributeType.SelectedItem;

            if (s.Key == 5 || s.Key == 6)
            {
                if (s.Key == 5)
                {
                    SelectedAttribute.Type = CompExtention.AttributeType._lookup;
                }
                else
                {
                    SelectedAttribute.Type = CompExtention.AttributeType._componentlookup;
                }
                bindComboLookup();
            }

            if (s.Key == 0 || s.Key == 2)
            {
                ckKey.Enabled = true;
            }
            else
            {
                ckKey.Enabled = false;
            }
            if (s.Key == (int)CompExtention.AttributeType._number)
            {
                ckAuto.Enabled = true;
            }
            else
            {
                ckAuto.Enabled = false;
            }
            if (s.Key == (int)CompExtention.AttributeType._number || s.Key == (int)CompExtention.AttributeType._string)
            {
                ckUnique.Enabled = true;
                txtLength.Enabled = true;
            }
            else
            {
                ckUnique.Enabled = false;
                txtLength.Enabled = false;
            }

            if ((s.Key == (int)CompExtention.AttributeType._file || (s.Key == (int)CompExtention.AttributeType._picture)))
            {
                txtExtension.Enabled = true;
            }
            else
            {
                txtExtension.Enabled = false;
            }


        }

        private void cmbClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView dr = (DataRowView)cmbClients.SelectedItem;
            ClientID = Convert.ToInt32(dr.Row["ClientID"]);
        }

        private void cmbCompLookup_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbCompLookup.SelectedItem != null)
            {
                bindComponentLookupDisplayName(((CompExtention.Component)cmbCompLookup.SelectedItem).ID);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        //private void btnNewComponent_Click(object sender, EventArgs e)
        //{
        //    gbField.Enabled = true;
        //    gbAttributes.Enabled = true;
        //    btnSaveComponent.Visible = true;
             
        //    gbTableList.Enabled = false;
        //    btnCancel.Visible = true;
        //    // component = new CompExtention.Component("", CompExtention.ComponentType.none);
        //}
        private void back() {
            gbField.Enabled = true;
            gbAttributes.Enabled = true;
            btnSaveComponent.Visible = false;             
            gbTableList.Enabled = true;
            btnCancel.Visible = false;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            //back();
            SetComponentAndAttribute();
            ChangedAttribute = new List<string>();
        }
        private void btnSaveComponent_Click(object sender, EventArgs e)
        {

            component.Keys.Clear();
            foreach (CompExtention.Attribute att in component.Attributes)
            {
                if (att.IsKey == true)
                {
                    component.Keys.Add(att);
                }
                
            }
            //TZ.CompExtention.ComponentManager cm = new CompExtention.ComponentManager(SelectedTable, new TZ.CompExtention.DataAccess.ComponentDataHandler(connection));
            if (cm.Save(component)) {
                MessageBox.Show("Component " + cm.Component.Name + " Create Successfully");
            }
            back();
        }
        private void btnSaveAttribute_Click(object sender, EventArgs e)
        {
            if (SelectedAttribute == null) {
                MessageBox.Show("Choose Attribute from the list");
                return ;
            }
            SelectedAttribute.Type = (CompExtention.AttributeType)((KeyValuePair<int, string>)cmbAttributeType.SelectedItem).Key;

            SelectedAttribute.DisplayName = txtDisplay.Text;
            SelectedAttribute.IsKey = ckKey.Checked;
            SelectedAttribute.IsRequired = ckRequied.Checked;
            SelectedAttribute.IsCore = ckCore.Checked;
            SelectedAttribute.IsUnique = ckUnique.Checked;
            SelectedAttribute.IsSecured = ckSecuried.Checked;
            SelectedAttribute.IsNullable = ckNull.Checked;
            SelectedAttribute.IsAuto = ckAuto.Checked;
            if (cmbLookUp.SelectedItem != null)
            {
                SelectedAttribute.LookupInstanceID = Convert.ToString(((System.Data.DataRowView)cmbLookUp.SelectedItem)["FieldInstanceID"]);
            }
            if (cmbCompLookup.SelectedItem != null)
            {
                SelectedAttribute.ComponentLookup = (string)((CompExtention.Component)cmbCompLookup.SelectedItem).ID;
            }
            if (cmbCompDisplayName.SelectedItem != null)
            {
                SelectedAttribute.ComponentLookupDisplayField  = (string)((CompExtention.Attribute)cmbCompDisplayName.SelectedItem).ID;
            }
            if (txtLength.Text != "")
            {
                // if (Char.IsDigit(txtLength.Text)) {
                SelectedAttribute.Length = Convert.ToInt32(txtLength.Text);
                // }

            }
            SelectedAttribute.FileExtension = txtExtension.Text;
            SelectedAttribute.DefaultValue = txtDefaultValue.Text;
            if (ChangedAttribute.Where(x => x == SelectedAttribute.Name).FirstOrDefault() == null)
            {
                ChangedAttribute.Add(SelectedAttribute.Name);
            }
            bindAttributeList();
        }
        private void txtEntityKey_TextChanged(object sender, EventArgs e)
        {
            if (component != null)
            {
                component.EntityKey = txtEntityKey.Text;
            }
        }
        private void txtComponentName_TextChanged(object sender, EventArgs e)
        {
            if (component != null)
            {
                component.Name = txtComponentName.Text;
            }
        }
        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            component = new CompExtention.Component(txtComponentName.Text, (CompExtention.ComponentType)((KeyValuePair<int, string>)cmbType.SelectedItem).Key);
        }
        private List<CompExtention.Component> filterComponent() {
            if (txtSearch.Text != "")
            {
                var filterList = CompWithTableList.Where(x => x.TableName.Contains(txtSearch.Text.ToLower()) || x.Name.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
                return (filterList);
            }
            else
            {
                return (CompWithTableList);
            }
        }
        private int Type = -1;
        private void showCoreTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showCoreTableToolStripMenuItem.Checked = true;
            showLinkTableToolStripMenuItem.Checked = false;
            showMetaTableToolStripMenuItem.Checked = false;
            showAttributeTableToolStripMenuItem.Checked = false;
            showNoComponentTablesToolStripMenuItem.Checked = false;

            var filterList = filterComponent().Where(x => x.Type == CompExtention.ComponentType.core).ToList();
                bindList(filterList);            
        }

        private void showLinkTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showCoreTableToolStripMenuItem.Checked = false;
            showLinkTableToolStripMenuItem.Checked = true;
            showMetaTableToolStripMenuItem.Checked = false;
            showAttributeTableToolStripMenuItem.Checked = false;
            showNoComponentTablesToolStripMenuItem.Checked = false;


            var filterList = filterComponent().Where(x => x.Type == CompExtention.ComponentType.link).ToList();
            bindList(filterList);
        }

        private void showMetaTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showCoreTableToolStripMenuItem.Checked = false;
            showLinkTableToolStripMenuItem.Checked = false;
            showMetaTableToolStripMenuItem.Checked = true;
            showAttributeTableToolStripMenuItem.Checked = false;
            showNoComponentTablesToolStripMenuItem.Checked = false;
            var filterList = filterComponent().Where(x => x.Type == CompExtention.ComponentType.meta).ToList();
            bindList(filterList);
        }

        private void showAttributeTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showCoreTableToolStripMenuItem.Checked = false;
            showLinkTableToolStripMenuItem.Checked = false;
            showMetaTableToolStripMenuItem.Checked = false;
            showAttributeTableToolStripMenuItem.Checked = true;
            showNoComponentTablesToolStripMenuItem.Checked = false;
            var filterList = filterComponent().Where(x => x.Type == CompExtention.ComponentType.attribute).ToList();
            bindList(filterList);
        }

        private void showNoComponentTablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showCoreTableToolStripMenuItem.Checked = false;
            showLinkTableToolStripMenuItem.Checked = false;
            showMetaTableToolStripMenuItem.Checked = false;
            showAttributeTableToolStripMenuItem.Checked = false;
            showNoComponentTablesToolStripMenuItem.Checked = true;
            var filterList = filterComponent().Where(x => x.Type == CompExtention.ComponentType.none).ToList();
            bindList(filterList);
        }

        private void lstTableList_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            

           // cntxCompType .Show(lstTableList, new Point(50, 50));
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (lstDetail.SelectedItems != null)
            {
                var s = component.Attributes.Where(x => x.Name == InitialAttribute.Name).FirstOrDefault();
                if (s != null)
                {
                    for (int i = 0; i < component.Attributes.Count; i++)
                    {
                        if (component.Attributes[i].Name == InitialAttribute.Name)
                        {
                            component.Attributes[i] = InitialAttribute.Clone();
                            SelectedAttribute = InitialAttribute.Clone();
                        }


                        SetValue();
                        bindAttributeList();
                    }
                }
            }
        }

        private void ComponentBuilder_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.N)
            {
                // Write your code for what you want for this shortcut (Ctrl+N) here
            }
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

    
    }
}
