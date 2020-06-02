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
namespace TZ.ImportDesk
{
    public partial class Export : Form
    {
        public Export()
        {
            InitializeComponent();
        }

        public Export(string conn,int cid)
        {
            ClientID = cid;
            Connection = conn;
            InitializeComponent();
            savedig.Filter = "Import template (*.itbk)|*.itbk|All files (*.*)|*.*";
        }
        public int ClientID { get; set; }
        public string Connection { get; set; }

        public void UpdateChange(string conn, int cid) {
            ClientID = cid;
            Connection = conn;
        }
        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            DialogResult dia =  savedig.ShowDialog();
            if (dia == DialogResult.OK) {
                txtPath.Text = savedig.FileName;
                File.WriteAllText(savedig.FileName, "");
            }
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            var b = new TZ.CompExtention.ImportTemplate.TemplateBackup(ClientID, Connection, 1);
            File.WriteAllText(txtPath.Text, Newtonsoft.Json.JsonConvert.SerializeObject(b));
            MessageBox.Show("Exported");
        }

        private void bntFullExport_Click(object sender, EventArgs e)
        {
            var b = new TZ.CompExtention.ImportTemplate.TemplateBackup(ClientID, Connection, 2);
            File.WriteAllText(txtPath.Text, Newtonsoft.Json.JsonConvert.SerializeObject(b));
            MessageBox.Show("Exported");
        }
    }
}
