using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using TZ.CompExtention.ImportTemplate;
using TZ.ImportDesk.Modal;

namespace TZ.ImportDesk
{
    public partial class ImportTemplateBackup : Form
    {

        public string Connection { get; set; }
        public int ClientID { get; set; }

        public List<Client> Clients { get; set; }

        public ImportTemplateBackup()
        {
            InitializeComponent();
        }
        public ImportTemplateBackup(string conn,int cid)
        {
            ClientID = cid;
            Connection = conn;
            InitializeComponent();
            GetClientList();
        }
        private void bindClientInList() {
            foreach (Client c in Clients) {
                ckListClient.Items.Add(c.ClientName,true );
            }      
        }
        private void BindClient()
        {
            bindClientInList();
            //cmbClient.ComboBox.Items.Clear();
            cmbClient.ValueMember = "ClientKey";
            cmbClient.DisplayMember = "ClientName";
            cmbClient.DataSource = Clients;
            if (Clients.Count > 0)
            {
                cmbClient.SelectedItem = Clients[0];              
            }
        }
        private void GetClientList()
        {
            if (txtConnection.Text != "")
            {
                Clients = new List<Client>();
                var dtClient = CompExtention.Shared.GetClientList(txtConnection.Text);
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

        public void UpdateChange(string conn, int cid)
        {
            ClientID = cid;
            Connection = conn;
        }
        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            opnFile.Filter = "Import template (*.itbk)|*.itbk|All files (*.*)|*.*";
            DialogResult dr=  opnFile.ShowDialog();
            if (dr == DialogResult.OK) {
                txtPath.Text = opnFile.FileName;
            }
        }
        private void btnRestore_Click(object sender, EventArgs e)
        {
            if (txtConnection.Text != "") {
                if (txtPath.Text != "")
                {
                    string s = File.ReadAllText(txtPath.Text);
                    //int cid = Convert.ToInt32(((Client)cmbClient.SelectedItem).ClientKey);
                    CheckedListBox.CheckedItemCollection ckitem =   ckListClient.CheckedItems;
                    for (int i = 0; i < ckitem.Count; i++) {
                       var cname = ckitem[i].ToString();
                       var c= Clients.Where(x => x.ClientName == cname).FirstOrDefault();
                        if (c != null) {
                            int cid = Convert.ToInt32( c.ClientKey);
                            TemplateBackup tb = Newtonsoft.Json.JsonConvert.DeserializeObject<TemplateBackup>(s);
                            try
                            {
                                TemplateRestore tr = new TemplateRestore(cid, txtConnection.Text);
                                tr.Restore(tb, cid);
                                
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error:" + ex.Message);
                            }
                        }
                    }
                    MessageBox.Show("Restored");

                }
                else {
                    MessageBox.Show("Please choose file to restore");
                }
            }else
            {
                MessageBox.Show("Connection required field");
            }         
        }

        private void txtConnection_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtConnection_Leave(object sender, EventArgs e)
        {
            GetClientList();
        }
    }
}
