using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TZ.ImportDesk
{
    public partial class AttributeList : Form
    {

        public delegate void MyEventHandler(object sender,AttributeArgument ar);
  

        public event MyEventHandler AfterAttributeSelected;
        public List<string> SelectedValues { get; set; }
        public string SelectedValue { get; set; }
        private int Type { get; set; } //1 row 2 column 3 pcolumn
        public AttributeList()
        {
            InitializeComponent();
        }     
        public AttributeList(List<CompExtention.Attribute> cat,int type)
        {
            Type = type;
            ComponentAttribute = cat;
            InitializeComponent();
        }      
        public List<CompExtention.Attribute> ComponentAttribute { get; set; }
        private void SetValue()
        {

            foreach (DataGridViewRow drv in dgAttribute.Rows)
            {
                string id = drv.Cells[0].Value.ToString();
                if (Type == 1)
                {
                    if (this.SelectedValues.Where(x => x == id).ToList().Count > 0)
                    {
                        DataGridViewCheckBoxCell chk1 = (DataGridViewCheckBoxCell)drv.Cells["Select"];
                        chk1.Value = true;
                    }
                }
                else if (Type == 2 || Type == 3) {
                    if (this.SelectedValue == id) {
                        DataGridViewCheckBoxCell chk1 = (DataGridViewCheckBoxCell)drv.Cells["Select"];
                        chk1.Value = true;
                    }           
                }          

            }
        }
        private void BindAttribute() {         

                dgAttribute.Columns.Clear();
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("ID", typeof(string)));
                dt.Columns.Add(new DataColumn("ComponentID", typeof(string)));
                dt.Columns.Add(new DataColumn("Name", typeof(string)));
                dt.Columns.Add(new DataColumn("DisplayName", typeof(string)));
                foreach (CompExtention.Attribute s in ComponentAttribute)
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
             
           
            var col3 = new DataGridViewCheckBoxColumn();
            col3.HeaderText = "Select";
            col3.Name  = "Select";
            dgAttribute.Columns.AddRange(new DataGridViewColumn[] { col3 });

            dgAttribute.Columns[0].ReadOnly = true;
                dgAttribute.Columns[1].ReadOnly = true;
                dgAttribute.Columns[2].ReadOnly = true;
                dgAttribute.Columns[3].ReadOnly = true;
            dgAttribute.Columns[1].Visible = false ;
            dgAttribute.Columns[0].Visible = false;

            dgAttribute.Columns[2].Width = 180;
            dgAttribute.Columns[3].Width = 380;
        }
        private void AttributeList_Load(object sender, EventArgs e)
        {
            BindAttribute();
            SetValue();
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            List<string> val = new List<string>();
            foreach (DataGridViewRow drv in dgAttribute.Rows)
            {
                var id = drv.Cells[0].Value.ToString();
                DataGridViewCheckBoxCell chk1 = (DataGridViewCheckBoxCell)drv.Cells["Select"];
                if (chk1.Value != null) {
                    if ((bool)chk1.Value == true)
                    {
                        if (Type == 2 || Type == 3)
                        {
                            SelectedValue = id;
                            AfterAttributeSelected(this,new AttributeArgument() { AttributeID = id , PivotType=Type});
                            this.Close();
                            break;
                        }
                        else
                        {
                            val.Add(id);
                        }
                    }
                }                
            }
            if (Type == 1)
            {
                if (val.Count == 0)
                {
                    MessageBox.Show("Select aleast one field");
                }
                else
                {
                    AfterAttributeSelected(this, new AttributeArgument() { AttributeIDs = val, PivotType = Type });
                    this.Close();
                }            
            }
            else {
                if (SelectedValue == "") {
                    MessageBox.Show("Select aleast one field");
                }
            }            
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ClearChecked() {

            foreach (DataGridViewRow drv in dgAttribute.Rows)
            {
                var se = (DataGridViewCheckBoxCell)drv.Cells["Select"];
                se.Value = false;
            }

            }
        private void dgAttribute_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Type == 2 || Type == 3) {
                if (e.RowIndex >= 0 && e.ColumnIndex == 4)
                {
                    DataGridViewRow row = dgAttribute.Rows[e.RowIndex];
                    var se = (DataGridViewCheckBoxCell)row.Cells["Select"];
                    ClearChecked();
                    se.Value = true;
                }
            }
          
        }
    }
    public class AttributeArgument: EventArgs
    { 
    public string AttributeID { get; set; }
        public List<string> AttributeIDs { get; set; }
        public int PivotType { get; set; }
        public AttributeArgument() {
            AttributeIDs = new List<string>();
            AttributeID = "";
            PivotType = 0;
        }
    }
}
