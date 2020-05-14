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
using TZ.CompExtention.DataAccess;
namespace TZ.ImportDesk
{
    public partial class ComponentList : Form
    {
        public ComponentList()
        {
            InitializeComponent();
        }
        int ClientID;
        string SelectedComponentName ="";
        string connection = "Server=183.82.34.174;Initial Catalog=talentozdev;Uid=admin;Pwd=admin312";
        List<CompExtention.Component> CompList = new List<CompExtention.Component>();
        List<CompExtention.Attribute> atts = new List<CompExtention.Attribute>();
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

        private void ComponentList_Load(object sender, EventArgs e)
        {
            bindClients();
            bindComponentList();
            bindAttributeType();
            bindComponentCombo();
            //bindComponentLookupDisplayName();
        }

        private void bindComponentList() {
            
            CompList= ComponentDataHandler.GetComponents(connection);
            lstComponentList.Items.Clear();
            foreach (CompExtention.Component s in CompList)
            {
                var li = new ListViewItem(s.Name);
                lstComponentList.Items.Add(li);
            }
        }
        private void bindComponentCombo() {
            cmbCompLookup.DisplayMember = "Name";
            cmbCompLookup.ValueMember = "ID";
            cmbCompLookup.DataSource = CompList;
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

        private void bindAttributeType()
        {
            int i = 0;
            d.Add(new KeyValuePair<int, string>(-1, "Select"));
            foreach (string s in AttributeTypeList)
            {
                KeyValuePair<int, string> attributeType = new KeyValuePair<int, string>(i, s.Replace("_", ""));
                i = i + 1;
                d.Add(attributeType);
            }
            cmbAttributeType.ValueMember = "Key";
            cmbAttributeType.DisplayMember = "Value";
            cmbAttributeType.DataSource = d.ToList();
        }
        private void bindComponentLookupDisplayName(string lookupcomp) {
            GetComponentLookUp(lookupcomp);
            cmbCompDisplayName.DisplayMember = "DisplayName";
            cmbCompDisplayName.ValueMember = "ID";
            cmbCompDisplayName.DataSource = compDisplayField;
        }
       
        private void bindAttributeList() {
         
           var cc= CompList.Where(x => x.Name == SelectedComponentName).FirstOrDefault();
            if (cc != null) {
                atts = ComponentDataHandler.GetAttributes(connection, cc.ID, ClientID);
                lstDetail.Items.Clear();
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                foreach (CompExtention.Attribute s in atts)
                {
                    var row = new string[] { s.Name,s.DisplayName,
                        textInfo.ToTitleCase((s.Type.ToString().Replace("_", ""))),                        
                        s.IsKey.ToString() ,
                    s.DefaultValue,
                    s.IsCore.ToString(),
                    s.IsUnique.ToString(),
                    s.Length.ToString(),
                    s.IsRequired.ToString(),
                    s.IsNullable.ToString(),
                    s.LookupInstanceID ,
                    s.ComponentLookup,
                    s.ComponentLookupDisplayField ,
                    s.FileExtension};
                    var li = new ListViewItem(row);
                    lstDetail.Items.Add(li);
                }
            }
          

        }     
        private void bindClients()
        {
            DataTable dtClient = new DataTable();
            dtClient = CompExtention.Shared.GetClientList(connection);
            cmbClients.ValueMember = "ClientID";
            cmbClients.DisplayMember = "CustomerName";
            cmbClients.DataSource = dtClient;
        }
        
        private void cmbClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView dr = (DataRowView)cmbClients.SelectedItem;
            ClientID = Convert.ToInt32(dr.Row["ClientID"]);
        }

        private void lstComponentList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstComponentList.SelectedItems.Count > 0) {
                SelectedComponentName = lstComponentList.SelectedItems[0].Text;
                bindAttributeList();
            }
        }
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

        private void cmbAttributeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var s = (KeyValuePair<int, string>)cmbAttributeType.SelectedItem;
            if (s.Key == 5)
            {
                bindLookup();
                cmbLookUp.Enabled = true;
            }
            else
            {
                cmbLookUp.Enabled = false;
            }
        }
        private void SetValue(CompExtention.Attribute att)
        {
            var item = d.Where(x => x.Value == att.Type.ToString().Replace("_", "")).FirstOrDefault();
            //   if (item != null) {
            cmbAttributeType.SelectedItem = item;
            txtLength.Text = att.Length.ToString();
            ckKey.Checked = att.IsKey;
            ckRequied.Checked = att.IsRequired;
            ckNull.Checked = att.IsNullable;
            txtDefaultValue.Text = att.DefaultValue;
            txtDisplay.Text = att.DisplayName;
            txtExtension.Text = att.FileExtension;
                if (att.Type == CompExtention.AttributeType._lookup)
                {
                    cmbLookUp.Enabled = true;
                    bindLookup();
                    cmbLookUp.SelectedItem = dtLookup.AsEnumerable().Where(x => x["FieldInstanceID"].ToString() == att.LookupInstanceID.ToString()).FirstOrDefault();
                }
                else {
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
            else {
                cmbCompLookup.Enabled = false;
                cmbCompDisplayName.Enabled = false;
                cmbCompDisplayName.SelectedItem = "";
            }          
           
            //}
            //  cmbAttributeType.SelectedItem = cmbAttributeType.Items.IndexOf(att.Type.ToString().Replace("_", ""));//comboBox1.Items.IndexOf("test1")att.Type;
        }
        private void GetComponentLookUp(string ccid) {
            compDisplayField= ComponentDataHandler.GetAttributes(connection, ccid, ClientID);
        }
        private void lstDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (atts.Count > 0)
            {
                if (lstDetail.SelectedItems.Count > 0)
                {
                    CompExtention.Attribute att = atts.Where(x => x.Name == lstDetail.SelectedItems[0].Text).FirstOrDefault();
                    if (att != null)
                    {
                        SetValue(att);
                    }
                }
            }
        }

        private void cmbCompLookup_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindComponentLookupDisplayName( ((CompExtention.Component)cmbCompLookup.SelectedItem).ID);
        }
    }
}
