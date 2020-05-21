using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TZ.ImportDesk.Modal;

namespace TZ.ImportDesk
{
    public partial class Master : Form
    {
        public int childFormNumber = 0;
        private int _cType = 1;
        private string _connection="";
        private int _client;

        public List<Client> Clients { get; set; }
        public int SelectedClient { get {
                return _client;
            }
            set {
                _client = value;
                ClientChanged();

            } }
        public string Connection
        {
            get
            {
                return _connection;
            }
            set
            {
                _connection = value;
            }
        }
        public int ConnectionType {
            get {
                return _cType;
            }
            set {
                _cType = value;
                SetConnectionType();
            }
        }
        private void SetConnectionType() {
            if (_cType == 1)
            {
                txtConnection.Enabled = true;
            }
            else {
                txtConnection.Enabled = false;
            }
        }

        public List<Form> FormList = new List<Form>();
        private void ClientChanged() {
            foreach (Form f in FormList) {
                if (f.AccessibleName == "ComponentBuilder")
                {
                    ((ComponentBuilder)f).UpdateChanges(Connection, SelectedClient);
                }
                else if (f.AccessibleName == "TalentozComponent")
                {
                    ((TalentozComponent)f).UpdateChanges(Connection, SelectedClient);
                }
                else if (f.AccessibleName == "ViewBuilder")
                {
                    ((View)f).UpdateChanges(Connection, SelectedClient);
                }
                else if (f.AccessibleName == "ViewList")
                {

                }
                else if (f.AccessibleName == "ComponentList")
                {

                }
                else if (f.AccessibleName == "TemplateList")
                {
                    ((Template)f).UpdateChanges(Connection, SelectedClient);
                }
                else if (f.AccessibleName == "ClientAttributeList") {
                    ((ClientComponentBuilder)f).UpdateChanges(Connection, SelectedClient);
                }
            }
        }
        public Master()
        {
            Clients = new List<Client>();
            Connection = "";
            InitializeComponent();       
            cmbConnectionType.SelectedItem = "Local";
        }
        public bool CheckConnection() {
            if (_cType == 1) {
                if (txtConnection.Text == "")
                {
                    MessageBox.Show("Connection cannot empty");
                    txtConnection.Focus();
                    return false;
                }
                else {
                    if (CompExtention.Shared.IsValidConnection(Connection))
                    {
                        return true;
                    }
                    else
                    {
                        txtConnection.Text = "";
                        Connection = "";
                        MessageBox.Show("Invalid Connection");
                        return false;
                    }
                }
                
            }
            return true;
        }
        private void BindClient() {
            //cmbClient.ComboBox.Items.Clear();
            cmbClient.ComboBox.ValueMember  = "ClientKey";
            cmbClient.ComboBox.DisplayMember = "ClientName";
            cmbClient.ComboBox.DataSource = Clients;
            if (Clients.Count > 0) {
                cmbClient.ComboBox.SelectedItem = Clients[0];
                SelectedClient = Convert.ToInt32( Clients[0].ClientKey);
            }
        }     
        private void GetClientList()
        {
            if (CheckConnection()) {
                Clients = new List<Client>();
                var dtClient = CompExtention.Shared.GetClientList(Connection);
                foreach (DataRow dr in dtClient.Rows)
                {
                    var c = new Client();
                    c.ClientKey = dr["ClientID"].ToString();
                    c.ClientName = dr["CustomerName"].ToString();
                    Clients.Add(c);
                }
                BindClient();
            }           
        }
        public void CloseForm(string name) {
            var f = FormList.Where(x => x.AccessibleName == name).FirstOrDefault();
            if (f != null) {
                FormList.Remove(f);
            }          
        }
        private void ShowNewForm(object sender, EventArgs e)
        {
            if (CheckConnection() == true  && SelectedClient >0) {
                Form childForm = null;
                if (childForm == null)
                {
                    childForm = new ComponentBuilder(SelectedClient ,Connection);
                }

                childForm.MdiParent = this;
                childForm.AccessibleName = "ComponentBuilder";
                childForm.Text = "Component Builder";
                childForm.Show();
                FormList.Add(childForm);
            }               
          
            //
        }

        private void OpenFile(object sender, EventArgs e)
        {
            if (CheckConnection() == true && SelectedClient >0)
            {
                Form childForm = null;
                if (childForm == null)
                {
                    childForm = new TalentozComponent();
                }
                childForm.MdiParent = this;
                childForm.AccessibleName = "TalentozComponent";
                childForm.Text = "Talentoz Component";
                childForm.Show();
                FormList.Add(childForm);
            }         
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CheckConnection() == true && SelectedClient >0)
            {
                Form childForm = null;
                if (childForm == null)
                {
                    childForm = new View(SelectedClient,Connection );
                }
                childForm.MdiParent = this;
                childForm.AccessibleName = "ViewBuilder";
                childForm.Text = "View Builder";
                childForm.Show();
                FormList.Add(childForm);
            }
        }

        private void clientAttribe_Click(object sender, EventArgs e)
        {
            if (CheckConnection() == true && SelectedClient > 0)
            {
                Form childForm = null;
                if (childForm == null)
                {
                    childForm = new ClientComponentBuilder (SelectedClient, Connection);
                }
                childForm.MdiParent = this;
                childForm.AccessibleName = "ClientAttributeList";
                childForm.Text = "Client Attribute List";
                childForm.Show();
                FormList.Add(childForm);
            }

        }
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CheckConnection() == true && SelectedClient >0)
            {
                Form childForm = null;
                if (childForm == null)
                {
                    childForm = new ViewList();
                }

                childForm.MdiParent = this;
                childForm.AccessibleName = "ViewList";
                childForm.Text = "View List";
                childForm.Show();
                FormList.Add(childForm);
            }
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CheckConnection() == true && SelectedClient >0)
            {
                Form childForm = null;
                if (childForm == null)
                {
                    childForm = new ComponentList();
                }

                childForm.MdiParent = this;
                childForm.AccessibleName = "ComponentList";
                childForm.Text = "Component List";
                childForm.Show();
               FormList.Add(childForm);
            }
        }
        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CheckConnection() == true && SelectedClient >0)
            {
                Form childForm = null;
                if (childForm == null)
                {
                    childForm = new Template(SelectedClient ,Connection);
                }

                childForm.MdiParent = this;
                childForm.Text = "Template List";
                childForm.AccessibleName = "TemplateList";
                childForm.Show();
                FormList.Add(childForm);
            }
        }
        private void txtConnection_Leave_1(object sender, EventArgs e)
        {
            if (txtConnection.Text != "") {
                Connection = txtConnection.Text;
                GetClientList();
            }
          
        }
        //private void txtConnection_Click(object sender, EventArgs e)
        //{
        //    if (txtConnection.Text != "") {
        //        Connection = txtConnection.Text;
        //        GetClientList();
        //    }
        
        //}
        private void txtConnection_Enter(object sender, EventArgs e)
        {
          
        }

        private void cmbConnectionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbConnectionType.SelectedItem.ToString().ToLower() == "local")
            {
                ConnectionType = 1;
            }
            else
            {
                ConnectionType = 2;
            }

        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void cmbClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedClient = Convert.ToInt32(   ((Client)cmbClient.SelectedItem).ClientKey );

        }

        private void printSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void txtConnection_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.V)) {
               var txt= (sender as System.Windows.Forms.ToolStripTextBox);
                txt.Paste();
            }
               
        }
    }
}
