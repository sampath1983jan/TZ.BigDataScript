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
using TZ.Import;

namespace TZ.ImportDesk
{
    public partial class Stepup : Form
    {
        public Stepup()
        {
            InitializeComponent();
        }
        public Stepup(string conn,int cid)
        {
            Connection = conn;
            ClientID = cid;
            InitializeComponent();
        }
        public int ClientID { get; set; }
        public string Connection { get; set; }
        List<ComponentView> Views = new List<ComponentView>();
        int selectedView = 0;
         
        string path = @"C:\WorkingFolder-Custom\TZ.BigDataScript\TZ.ImportDesk\ComponentLibrary";
    
        private void Form1_Load(object sender, EventArgs e)
        {
         //   var path=      @"C:\WorkingFolder-Custom\TZ.BigDataScript\TZ.ImportDesk\ComponentLibrary";
            ComponentView vm = new ComponentView();
            ComponentViewManager cvm = new ComponentViewManager(new ComponentViewDataAccess(path));
         //   Views =     cvm.GetViews();
            //var view= cvm.NewView("EmployeeView");

            // ComponentManager cmg = new ComponentManager("ed04c35e434145bba3529d7365ff1ff5186455310", new ComponentDataAccess(path) { });

            // view.Components.Add( cmg.GetComponent());
            // cvm.Save(view);
            //cmbData.Items.Add("Choose from the list");
            //cmbData.SelectedIndex = 0;
            //foreach (ComponentView cv in Views) {
            //    cmbData.Items.Add(cv.Name);
            //}
     
             

            //TZ.CompExtention.  Component c = new CompExtention.Component();
            //  c.ID = "";
            //  c.Name = "Employee";
            //  c.TableName = "sys_user";
            //  c.Keys = new List<IAttribute>();
            //  c.Keys.Add(new TZ.CompExtention.Attribute()
            //  {
            //      ComponentID = c.ID,
            //      ID = "1",
            //      Name = "UserID",
            //      DisplayName = "User Key",
            //      Type = AttributeType._number,
            //      IsKey = true,
            //      IsUnique = true,
            //  });
            //  c.Attributes = new List<IAttribute>();
            //  c.Attributes.Add(new TZ.CompExtention.Attribute()
            //  {
            //      ComponentID = c.ID,
            //      ID = "1",
            //      Name = "UserID",
            //      DisplayName = "User Key",
            //      Type = AttributeType._number,
            //      IsKey = true,
            //      IsUnique = true,
            //  });
            //  c.Attributes.Add(new TZ.CompExtention.Attribute()
            //  {
            //      ComponentID = c.ID,
            //      ID = "2",
            //      Name = "F_200005",
            //      DisplayName = "Employee No",
            //      Type = AttributeType._string,
            //      IsKey = false,
            //      IsUnique = true,
            //  });
            //  c.Attributes.Add(new TZ.CompExtention.Attribute()
            //  {
            //      ComponentID = c.ID,
            //      ID = "3",
            //      Name = "F_200015",
            //      DisplayName = "Employee Name",
            //      Type = AttributeType._string,
            //      IsKey = false,
            //      IsUnique = false,
            //  });
            //  c.Attributes.Add(new TZ.CompExtention.Attribute()
            //  {
            //      ComponentID = c.ID,
            //      ID = "4",
            //      Name = "F_200115",
            //      DisplayName = "Email",
            //      Type = AttributeType._string,
            //      IsKey = false,
            //      IsUnique = false,
            //  });
            //  c.Attributes.Add(new TZ.CompExtention.Attribute()
            //  {
            //      ComponentID = c.ID,
            //      ID = "5",
            //      Name = "F_200040",
            //      DisplayName = "Gender",
            //      Type = AttributeType._lookup,
            //      IsKey = false,
            //      IsUnique = false,
            //      LookupInstanceID ="102",
            //      LookupDisplayField ="Description",

            //  });
            //  c.Attributes.Add(new TZ.CompExtention.Attribute()
            //  {
            //      ComponentID = c.ID,
            //      ID = "6",
            //      Name = "F_200050",
            //      DisplayName = "Nationality",
            //      Type = AttributeType._lookup,
            //      IsKey = false,
            //      IsUnique = false,
            //      LookupInstanceID = "107",
            //      LookupDisplayField = "Description",
            //  });
            //  c.Attributes.Add(new TZ.CompExtention.Attribute()
            //  {
            //      ComponentID = c.ID,
            //      ID = "7",
            //      Name = "F_31075",
            //      DisplayName = "Marital Status",
            //      Type = AttributeType._lookup,
            //      IsKey = false,
            //      IsUnique = false,
            //      LookupInstanceID = "254",
            //      LookupDisplayField = "Description",
            //  });
            // cmg.Save(c);
        }

        public void UpdateChange(string conn,int clientID) {
            ClientID = clientID;
            Connection = conn;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            lblTime.Text = "Start at:" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt");             
        }
     
         
       
        private void lbFile_Click(object sender, EventArgs e)
        {

        }
      
        private void button4_Click(object sender, EventArgs e)
        {
            //Server=183.82.34.174;Initial Catalog=talentozdev;Uid=admin;Pwd=admin312
            if (Connection != "") {
                try
                {
                    TZ.CompExtention.Shared.ExecuteSetup(Connection);
                    MessageBox.Show("done");
                }
                catch (Exception ex) {
                    MessageBox.Show("Error:" + ex.Message );
                }               
            } 
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (Connection != "")
            {
                TZ.CompExtention.Shared.ClearSchema(Connection);
                MessageBox.Show("cleared");
            }
        }
        private void btnConvert_Click(object sender, EventArgs e)
        {

            if (Connection != "")
            {

                try
                {
                    TZ.CompExtention.Shared.ConvertoImportComponent(Connection, ClientID);
                    MessageBox.Show("done");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:" + ex.Message);

                }
               
                
            }
          
        }
    }
}
