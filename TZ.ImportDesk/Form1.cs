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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<ComponentView> Views = new List<ComponentView>();
        int selectedView = 0;
        private void opDiaglog_FileOk(object sender, CancelEventArgs e)
        {

        }
        string path = @"C:\WorkingFolder-Custom\TZ.BigDataScript\TZ.ImportDesk\ComponentLibrary";
        public void SetBUView() {           
            ComponentViewManager cvm = new ComponentViewManager(new ComponentViewDataAccess(path));            
            var view = cvm.NewView("BUView");
            ComponentManager cmg = new ComponentManager(2,"2ee2f913d59145299a172c17a77cc3b5539962295", new ComponentJsonDataAccess(path) { });
           // view.Components.Add((TZ.CompExtention.Component)cmg.GetComponent());
            cvm.Save(view);
        }
        public void SetBU() {
            ComponentManager cmg = new ComponentManager();
            cmg.Set(new ComponentJsonDataAccess(path) { });
            TZ.CompExtention.Component c = new CompExtention.Component();
            c.ID = "";
            c.Name = "Business Unit";
            c.TableName = "businessunit";
            c.Keys = new List<TZ.CompExtention.Attribute>();
            c.Attributes.Add(new TZ.CompExtention.Attribute()
            {
                ComponentID = c.ID,
                ID = "1",
                Name = "F_300005",
                DisplayName = "Name",
                Type = AttributeType._string,
                IsKey = false,
                IsUnique = false,
            });
            c.Attributes.Add(new TZ.CompExtention.Attribute()
            {
                ComponentID = c.ID,
                ID = "2",
                Name = "F_300060",
                DisplayName = "Code",
                Type = AttributeType._string,
                IsKey = false,
                IsUnique = true,
            });
            c.Attributes.Add(new TZ.CompExtention.Attribute()
            {
                ComponentID = c.ID,
                ID = "3",
                Name = "F_300020",
                DisplayName = "Description",
                Type = AttributeType._string,
                IsKey = false,
                IsUnique = true,
            });
            c.Attributes.Add(new TZ.CompExtention.Attribute()
            {
                ComponentID = c.ID,
                ID = "4",
                Name = "F_300025",
                DisplayName = "Currency Type",
                Type = AttributeType._string,
                IsKey = false,
                IsUnique = true,
            
            });
            c.Attributes.Add(new TZ.CompExtention.Attribute()
            {
                ComponentID = c.ID,
                ID = "5",
                Name = "F_300030",
                DisplayName = "Country",
                Type = AttributeType._lookup,
                IsKey = false,
                IsUnique = true,
                LookupInstanceID = "106",
                LookupDisplayField = "Description",
            });

            c.Keys.Add(new TZ.CompExtention.Attribute()
            {
                ComponentID = c.ID,
                ID = "6",
                Name = "BusinessUnitID",
                DisplayName = "Business UnitID",
                Type = AttributeType._number,
                IsKey = true,
                IsUnique = true,
            });
            cmg.Save(c);

        }
        public void SetPosition() { 
        
        }
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

        private void button1_Click(object sender, EventArgs e)
        {
            lblTime.Text = "Start at:" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt");
            if (opDiaglog.ShowDialog() == DialogResult.OK)
            {
                lbFile.Text = opDiaglog.FileName;


            var ext=    System.IO.Path.GetExtension(opDiaglog.FileName);
                if (ext == ".xlsx")
                {
                    im.Context.FileType = Import.ImportFileType.excelx;
                }
                else if (ext == ".xls")
                {
                    im.Context.FileType = Import.ImportFileType.excel;
                }
                else if (ext == ".csv") {
                    im.Context.FileType = Import.ImportFileType.csv;
                }
                im.Context.DataFilePath = opDiaglog.FileName;
                NextStep("File validation progress");

                im.Context.ImportFields  = new List<ImportFieldMap>();
                ImportFieldMap mf = new ImportFieldMap()
                {
                    ComponentID = "ed04c35e434145bba3529d7365ff1ff5186455310",
                    DataField = "2",
                    FileFields = "Employee Number",
                    IsKey = true,
                };
                im.Context.ImportFields.Add(mf);
                mf = new ImportFieldMap()
                {
                    ComponentID = "ed04c35e434145bba3529d7365ff1ff5186455310",
                    DataField = "7",
                    FileFields = "Marital Status",
                    IsKey = false,
                };
                im.Context.ImportFields.Add(mf);
                //mf = new ImportFieldMap()
                //{
                //    ComponentID = "ed04c35e434145bba3529d7365ff1ff5186455310",
                //    DataField = "2",
                //    FileFields = "Employee Number",
                //    IsKey = true,
                //};
                //im.Context.ImportFields.Add(mf);
                mf = new ImportFieldMap()
                {
                    ComponentID = "ed04c35e434145bba3529d7365ff1ff5186455310",
                    DataField = "3",
                    FileFields = "Employee Name",
                    IsKey = false,
                };
                im.Context.ImportFields.Add(mf);
                mf = new ImportFieldMap()
                {
                    ComponentID = "ed04c35e434145bba3529d7365ff1ff5186455310",
                    DataField = "4",
                    FileFields = "EmailAddress",
                    IsKey = false,
                };
                im.Context.ImportFields.Add(mf);
                mf = new ImportFieldMap()
                {
                    ComponentID = "ed04c35e434145bba3529d7365ff1ff5186455310",
                    DataField = "5",
                    FileFields = "Gender",
                    IsKey = false,
                };
                im.Context.ImportFields.Add(mf);
                mf = new ImportFieldMap()
                {
                    ComponentID = "ed04c35e434145bba3529d7365ff1ff5186455310",
                    DataField = "6",
                    FileFields = "Nationality",
                    IsKey = false,
                };
                im.Context.ImportFields.Add(mf);
                NextStep("Field mapping and field validation aginst component progress");
                NextStep("Data Validation started");
             //   im.HandleNextStep();
                
               var data= im.Context.ComponentData[0].SourceData ;
                if (data.Columns.Contains("State"))
                {
              DataRow[]dataRows = data.Select("State = 'error'", "");
                    if (dataRows.Count() > 0)
                    {
                        data = dataRows.CopyToDataTable();
                      // gView.DataSource = data;
                        lblStatus.Text = "Status:Data Import validation done and error list shown in the grid";
                    }
                    else {
                        data = new DataTable();                      
                        NextStep("Imported Progress");
                    //    gView.DataSource = im.Context.Errors;
                        if (im.Context.Errors.Count() > 0)
                        {
                            lblStatus.Text = "Status: Data Import done successfully with Error which listed";
                        }
                        else {

                            lblStatus.Text = "Status: Data Import done successfully";
                        }
                       
                    }
                    lblTime.Text = lblTime.Text + " End at:" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt");
                    MessageBox.Show("Done");
                   
                }
                else {
                    
                }
             
              //  MessageBox.Show("Data Import Validation Done");
              
            }
        }
        Import.ImportManager im;
        ComponentView view;
        private void cmbData_SelectedIndexChanged(object sender, EventArgs e)
        {
         //   selectedView = cmbData.SelectedIndex;
            if (selectedView > 0) {
                view = Views[selectedView - 1];
                im = new Import.ImportManager();             
              
                im.LogPath = path + @"\log";
                im.New("", path, view.ID);
                im.Context.ClientID =3;
                im.Context.SetConnection("Server=192.168.0.80;Database=talentozdev;Uid=admin;Pwd=admin312");
                NextStep("Component View Selected and validation completed");
            }   

        }
        private void NextStep(string text) {
            lblStatus.Text = "Status: " + text;
          ImportError ie=  im.HandleNextStep();
            if (ie != null) {
                if (ie.Type == ErrorType.NOERROR)
                {
                    lblStatus.Text = "Status:" + ie.Message;
                }
            }
        
        }
        private void lbFile_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //SetBU();
            SetBUView();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            lblTime.Text = "Start at:" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt");
            if (opDiaglog.ShowDialog() == DialogResult.OK)
            {
                lbFile.Text = opDiaglog.FileName;


                var ext = System.IO.Path.GetExtension(opDiaglog.FileName);
                if (ext == ".xlsx")
                {
                    im.Context.FileType = Import.ImportFileType.excelx;
                }
                else if (ext == ".xls")
                {
                    im.Context.FileType = Import.ImportFileType.excel;
                }
                else if (ext == ".csv")
                {
                    im.Context.FileType = Import.ImportFileType.csv;
                }
                im.Context.DataFilePath = opDiaglog.FileName;
                NextStep("File validation progress");

                im.Context.ImportFields = new List<ImportFieldMap>();
                ImportFieldMap mf = new ImportFieldMap()
                {
                    ComponentID = "ed04c35e434145bba3529d7365ff1ff5186455310",
                    DataField = "2",
                    FileFields = "Employee Number",
                    IsKey = true,
                };
                im.Context.ImportFields.Add(mf);
                mf = new ImportFieldMap()
                {
                    ComponentID = "ed04c35e434145bba3529d7365ff1ff5186455310",
                    DataField = "7",
                    FileFields = "Marital Status",
                    IsKey = false,
                };
                im.Context.ImportFields.Add(mf);
                //mf = new ImportFieldMap()
                //{
                //    ComponentID = "ed04c35e434145bba3529d7365ff1ff5186455310",
                //    DataField = "2",
                //    FileFields = "Employee Number",
                //    IsKey = true,
                //};
                //im.Context.ImportFields.Add(mf);
                mf = new ImportFieldMap()
                {
                    ComponentID = "ed04c35e434145bba3529d7365ff1ff5186455310",
                    DataField = "3",
                    FileFields = "Employee Name",
                    IsKey = false,
                };
                im.Context.ImportFields.Add(mf);
                mf = new ImportFieldMap()
                {
                    ComponentID = "ed04c35e434145bba3529d7365ff1ff5186455310",
                    DataField = "4",
                    FileFields = "EmailAddress",
                    IsKey = false,
                };
                im.Context.ImportFields.Add(mf);
                mf = new ImportFieldMap()
                {
                    ComponentID = "ed04c35e434145bba3529d7365ff1ff5186455310",
                    DataField = "5",
                    FileFields = "Gender",
                    IsKey = false,
                };
                im.Context.ImportFields.Add(mf);
                mf = new ImportFieldMap()
                {
                    ComponentID = "ed04c35e434145bba3529d7365ff1ff5186455310",
                    DataField = "6",
                    FileFields = "Nationality",
                    IsKey = false,
                };
                im.Context.ImportFields.Add(mf);
                NextStep("Field mapping and field validation aginst component progress");
                NextStep("Data Validation started");
                //   im.HandleNextStep();

                var data = im.Context.ComponentData[0].SourceData;
                if (data.Columns.Contains("State"))
                {
                    DataRow[] dataRows = data.Select("State = 'error'", "");
                    if (dataRows.Count() > 0)
                    {
                        data = dataRows.CopyToDataTable();
                      //  gView.DataSource = data;
                        lblStatus.Text = "Status:Data Import validation done and error list shown in the grid";
                    }
                    else
                    {
                        data = new DataTable();
                        NextStep("Imported Progress");
                    //    gView.DataSource = im.Context.Errors;
                        if (im.Context.Errors.Count() > 0)
                        {
                            lblStatus.Text = "Status: Data Import done successfully with Error which listed";
                        }
                        else
                        {

                            lblStatus.Text = "Status: Data Import done successfully";
                        }

                    }
                    lblTime.Text = lblTime.Text + " End at:" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt");
                    MessageBox.Show("Done");

                }
                else
                {

                }

                //  MessageBox.Show("Data Import Validation Done");

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
            TZ.CompExtention.Shared.ExecuteSetup(" Server=183.82.34.174;Initial Catalog=talentozdev;Uid=admin;Pwd=admin312");
            MessageBox.Show("done");

        }
    }
}
