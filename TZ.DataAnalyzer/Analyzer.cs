using Microsoft.Spark.Sql;
using Microsoft.Spark.Sql.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using static Microsoft.Spark.Sql.Functions;

namespace TZ.DataAnalyzer
{
    public class Analyzer
    {
        /// <summary>
        /// 
        /// </summary>
        public enum OutputFormat
        {
            JSON,
            CSV,
            JSONL,
            TABLE
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Schema> DataSchema { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public OutputFormat Output { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private  SparkSession Spark;
        /// <summary>
        /// 
        /// </summary>
        private   Schema SchemaData { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        private string DataRoot { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private string Namespace { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private List<string> Import { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IDictionary<string, string> Param { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="o"></param>
        public Analyzer(string name, OutputFormat o)
        {
            this.Name = name;
            this.Output = o;
            this.DataSchema = new List<Schema>();
            Import = new List<string>();
            Namespace = "";
            Param = new Dictionary<string, string>();
            DataRoot = "";
        }
        public void Add(string name, string value)
        {
            this.Param.Add(name, value);
        }
        /// <summary>
        /// 
        /// </summary>
        public Analyzer()
        {
            Param = new Dictionary<string, string>();
        }
        private string SetParamToVariable(string val) {
           // List<Schema> declaration = DataSchema.Where(x => x.Type == SchemaType.PARAM || x.Type == SchemaType.VARIABLE).ToList();
            //foreach (Schema sc in declaration)
            //{
                var list = this.Param.ToList();
                foreach (KeyValuePair<string, string> kv in list)
                {
                val = val.ToLower().Replace("#" + kv.Key.ToLower() + "#", kv.Value.ToLower());
                }
            return val;
                //s = s.Replace(sc.Name, sc.Value);
                //s = s.Replace("#" + sc.Name + "#", sc.Value);
                //  }
        }
        private string ParamReplace(string value) {
          var list=  this.Param.ToList();
            foreach (KeyValuePair<string, string> kv in list) {
                value= value.ToLower().Replace("#" +kv.Key.ToLower() +"#", kv.Value.ToLower());
            }
            return value;

        //var item=    list.Where(x => x.Key == name).FirstOrDefault();
        }
        /// <summary>
        /// 
        /// </summary>
        public void Init(string conn) {

            Spark = SparkSession
                  .Builder()
                  .AppName(this.Name)
                  .GetOrCreate();                      

            this.DataRoot=Formating(this.DataRoot);
            this.Namespace = ParamReplace(Formating(this.Namespace));

            var rootpath = this.DataRoot + @"\" + this.Namespace;
            for (int i=0;i<this.Import.Count;i++) {
                var s = this.Import[i];              
                if (!System.IO.Directory.Exists(rootpath + @"\"))
                {
                    System.IO.Directory.CreateDirectory(rootpath);
                }
               var path =  ( this.DataRoot) + @"\" + Formating(s) + ".json";              
               var text = System.IO.File.ReadAllText(path);

               TZ.Data.DataManager dm = new Data.DataManager(text);

                if (!System.IO.Directory.Exists(rootpath + @"\" + "data" +@"\"))
                {
                    System.IO.Directory.CreateDirectory(rootpath + @"\" + "data");
                }
                var op = rootpath + @"\" + "data" + @"\" + Formating(s) + ".json";
                if (System.IO.File.Exists(op))
                {                    
             //       this.Import[i] = op;
                   LoadFromImport(op, Formating(s));
                }
                else {
                    dm.GetData(conn, op, this.Param);
                  //  this.Import[i] = op;
                     LoadFromImport(op, Formating(s));
                }          
            }
            
        }
        
        private void LoadFromImport(string path,string name) {         
          
            DataFrame df = Spark.Read().Json(Formating(path));
            AddSchema(new Schema() { Data = df, Name = name });
            //df.CreateOrReplaceTempView(Formating(cube.SchemaName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cube"></param>
        public void AddPreStatement(DataCube cube)
        {
            if (cube.StatementType == DataStatement.IMP)
            {
                string[] s = cube.ImportData.Split(',');
                foreach (string id in s) {
                    this.Import.Add(id);
                }           
            }
            else if (cube.StatementType == DataStatement.DR)
            {
                this.DataRoot = cube.DataRoot;
            }
            else if (cube.StatementType == DataStatement.NS)
            {
                this.Namespace = cube.Namespace;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cube"></param>
        public void AddStatement(DataCube cube)
        {
            try
            {
                if (cube.StatementType == DataStatement.LOAD)
                {
                    GetLoad(cube);
                }
                else if (cube.StatementType == DataStatement.SELECT)
                {
                    GetSelect(cube);
                }
                else if (cube.StatementType == DataStatement.GROUPBY)
                {
                    GetGroupby(cube);
                }
                else if (cube.StatementType == DataStatement.CUBE)
                {
                    GetCube(cube);
                }
                else if (cube.StatementType == DataStatement.ROLLUP)
                {
                    GetRollup(cube);
                }
                else if (cube.StatementType == DataStatement.SEARCH)
                {
                    GetSearch(cube);
                }
                else if (cube.StatementType == DataStatement.SORT)
                {
                    GetSort(cube);
                }
                else if (cube.StatementType == DataStatement.UNION)
                {
                    GetUnion(cube);
                }
                else if (cube.StatementType == DataStatement.INTERSECT)
                {
                    GetIntersect(cube);
                }
                else if (cube.StatementType == DataStatement.EXCEPT)
                {
                    GetExcept(cube);
                }
                else if (cube.StatementType == DataStatement.JOIN)
                {
                    GetJoin(cube);
                }
                else if (cube.StatementType == DataStatement.DISTRINCT)
                {
                    GetDistinct(cube);
                }
                else if (cube.StatementType == DataStatement.LIMIT)
                {
                    GetLimit(cube);
                }
                else if (cube.StatementType == DataStatement.DROP)
                {
                    GetDrop(cube);
                }
                else if (cube.StatementType == DataStatement.WITH)
                {
                    GetWith(cube);
                }
                else if (cube.StatementType == DataStatement.VARIABLE || cube.StatementType == DataStatement.PARAM)
                {

                    GetVariable(cube);
                }
                else if (cube.StatementType == DataStatement.DC)
                {
                    GetDC(cube);
                }
                else if (cube.StatementType == DataStatement.NADROP) {
                    GetNullDrop(cube);
                }
                else if (cube.StatementType == DataStatement.NAREPLACE)
                {
                    GetNullReplace(cube);

                }
                else if (cube.StatementType == DataStatement.STRINGREPLACE)
                {
                    GetStringReplace(cube);
                }
            }
            catch (Exception ex)
            {
                Stop();
                throw ex;
            }
            finally
            {
                // Spark.Stop();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cube"></param>
        private void GetLoad(DataCube cube)
        {
            try
            {
                List<StructField> _sfs = new List<StructField>();
                foreach (State.IField f in cube.Fields)
                {
                    _sfs.Add(GetStructField(f));
                }
                var inputSchema = new StructType(_sfs);
                DataFrame df = Spark.Read().Schema(inputSchema).Json(Formating(cube.LoadStatement.Path));
                AddSchema(new Schema() { Data = df, Name = cube.VariableName });
                df.CreateOrReplaceTempView(Formating(cube.SchemaName));
                
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void GetWith(DataCube cube)
        {
            SchemaData = DataSchema.Where(x => x.Name == cube.VariableName).FirstOrDefault();
            Schema assignData = DataSchema.Where(x => x.Name == cube.AssignTo).FirstOrDefault();
            if (assignData == null)
            {
                DataSchema.Add(new Schema() { Data = null, Name = cube.AssignTo });
            }
            if (SchemaData != null)
            {
                if (cube.DataWith.WhenItems.Count > 0)
                {
                    System.Text.StringBuilder statement = new System.Text.StringBuilder();
                    statement.Append("CASE ");
                    assignData = DataSchema.Where(x => x.Name == cube.AssignTo).FirstOrDefault();
                    foreach (State.MultipleWhen mw in cube.DataWith.WhenItems)
                    {
                        statement.Append(" WHEN ");
                        foreach (State.When w in mw.WhenItem)
                        {
                            if (w.MethodName == "")
                            {
                                if (w.Logical == "")
                                {
                                    statement.AppendFormat(" {0} == '{1}' ", w.FieldName, Formating(w.Value));
                                }
                                else
                                {
                                    statement.AppendFormat(" {0} == '{1}' {2} ", w.FieldName, Formating(w.Value), Getlogic(w.Logical));
                                }
                            }
                            else
                            {
                                statement.Append(" " + w.FieldName + " IS NULL ");
                            }
                        }
                        statement.Append(" THEN ");
                        if (mw.AliasValue.IndexOf("concat") >= 0)
                        {                            
                            statement.AppendFormat(" '{0}' ", GetConcat(mw.AliasValue));
                        }
                        else {
                            statement.AppendFormat(" '{0}' ", GetDefaultValue(Formating(mw.AliasValue)));
                        }
                        
                    }
                    statement.Append(" ELSE "+ cube.DataWith.Name + " END");
                    var st = statement.ToString();
                    assignData.Data = SchemaData.Data.WithColumn(cube.DataWith.Name, Expr(st));
                }
                else if (cube.DataWith.Concat.Count > 0) {
                    List<Column> cols =new List<Column>();
                    foreach (string s in cube.DataWith.Concat) {
                        cols.Add(Lit(s));
                    }             
                    assignData.Data = SchemaData.Data.WithColumn(cube.DataWith.Name, Concat(cols.ToArray()) );
                }
               
            }
        }

        private string GetConcat(string val) {
            if (val.IndexOf("concat") >= 0)
            {
                var aliasval = Formating(val);
                var item = aliasval.ToLower().Replace("concat(", "").Replace(")", "").Split(',');
                var frmString = string.Join("", item);
                return frmString;
            }
            else {
                return "";
            }
        }

        private string GetDefaultValue(string s) {
            if (s == "@cdate")
            {
                return new DateTime().ToString("yyyy-MM-dd HH:mm:ss");
            }
            else if (s == "@fdate")
            {
                var d = DateTime.Today;
                return new DateTime(d.Year, d.Month, 1, 0, 0, 0).ToString("yyyy-MM-dd HH:mm:ss");
            }
            else if (s == "@ldate") {
                var d = DateTime.Today;
                var lastDayOfMonth = DateTime.DaysInMonth(d.Year, d.Month);
                return new DateTime(d.Year, d.Month, lastDayOfMonth, 0, 0, 0).ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                return s;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cube"></param>
        private void GetVariable(DataCube cube) {
            SchemaData = DataSchema.Where(x => x.Name == cube.VariableName).FirstOrDefault();
            Schema assignData = DataSchema.Where(x => x.Name == cube.AssignTo).FirstOrDefault();
            if (assignData == null)
            {
                DataSchema.Add(new Schema() { Data = null, Name = cube.AssignTo });
            }
            if (SchemaData != null)
            {
                assignData = DataSchema.Where(x => x.Name == cube.AssignTo).FirstOrDefault();
                SchemaData.Name = cube.Declaration.Name;
                if (cube.Declaration.Type == "v")
                {
                    SchemaData.Type = SchemaType.VARIABLE;
                }
                else
                    SchemaData.Type = SchemaType.PARAM;
                cube.Declaration.Value = SetParamToVariable(cube.Declaration.Value);
                SchemaData.Value = cube.Declaration.Value;

            }
            else {
                assignData = DataSchema.Where(x => x.Name == cube.AssignTo).FirstOrDefault();
                assignData.Name = cube.Declaration.Name;
                if (cube.Declaration.Type == "v")
                {
                    assignData.Type = SchemaType.VARIABLE;
                }
                else
                    assignData.Type = SchemaType.PARAM;
                cube.Declaration.Value = SetParamToVariable(cube.Declaration.Value);
                assignData.Value = cube.Declaration.Value;
            }
        }
        private void GetDC(DataCube cube)
        {
            SchemaData = DataSchema.Where(x => x.Name == cube.VariableName).FirstOrDefault();
            Schema assignData = DataSchema.Where(x => x.Name == cube.AssignTo).FirstOrDefault();
            if (assignData == null)
            {
                DataSchema.Add(new Schema() { Data = null, Name = cube.AssignTo });
            }
            //if (assignData != null)
            //{
                assignData = DataSchema.Where(x => x.Name == cube.AssignTo).FirstOrDefault();
                assignData.Name = cube.Declaration.Name;                 
                List<string> items = Formating(cube.Declaration.Value).Split(',').ToList();
                assignData.Data = Spark.CreateDataFrame(items).ToDF(cube.Declaration.Name);
            //}
             
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cube"></param>
        private void GetCube(DataCube cube)
        {
             SchemaData = DataSchema.Where(x => x.Name == cube.VariableName).FirstOrDefault();
            Schema assignData = DataSchema.Where(x => x.Name == cube.AssignTo).FirstOrDefault();
            if (assignData == null)
            {
                DataSchema.Add(new Schema() { Data = null, Name = cube.AssignTo });
            }
            if (SchemaData != null)
            {
                List<Microsoft.Spark.Sql.Column> cols = new List<Column>();
                foreach (State.IField f in cube.CubeStatement.CubeFields)
                {
                    cols.Add(SchemaData.Data[f.FieldName]);
                }
                DataFrame df;
                if (cube.CubeStatement.Aggregate == "count")
                {
                    df = SchemaData.Data.Cube(cols.ToArray()).Count();
                }
                else if (cube.CubeStatement.Aggregate == "avg")
                {
                    df = SchemaData.Data.Cube(cols.ToArray()).Avg();
                }
                else if (cube.CubeStatement.Aggregate == "sum")
                {
                    df = SchemaData.Data.Cube(cols.ToArray()).Sum();
                }
                else if (cube.CubeStatement.Aggregate == "min")
                {
                    df = SchemaData.Data.Cube(cols.ToArray()).Min();
                }
                else if (cube.CubeStatement.Aggregate == "max")
                {
                    df = SchemaData.Data.Cube(cols.ToArray()).Max();
                }
                else if (cube.CubeStatement.Aggregate == "sd")
                {
                    df = SchemaData.Data.Cube(cols.ToArray()).Mean();
                }
                else
                {
                    df = SchemaData.Data.Cube(cols.ToArray()).Count();
                }
                assignData = DataSchema.Where(x => x.Name == cube.AssignTo).FirstOrDefault();
                if (SetOrderBy(cube.CubeStatement.Orders).Count > 0 && GetSearch(cube.CubeStatement.Conditions) != "")
                {
                    assignData.Data = df.Where(GetSearch(cube.CubeStatement.Conditions)).OrderBy(SetOrderBy(cube.CubeStatement.Orders).ToArray());
                }
                else if (SetOrderBy(cube.CubeStatement.Orders).Count > 0)
                {
                    assignData.Data = df.OrderBy(SetOrderBy(cube.CubeStatement.Orders).ToArray());
                }
                else if (GetSearch(cube.CubeStatement.Conditions) != "")
                {
                    assignData.Data = df.
                        Where(GetSearch(cube.CubeStatement.Conditions));
                }
                else
                {
                    assignData.Data = df;
                }

                
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cube"></param>
        private void GetRollup(DataCube cube)
        {
              SchemaData = DataSchema.Where(x => x.Name == cube.VariableName).FirstOrDefault();
            Schema assignData = DataSchema.Where(x => x.Name == cube.AssignTo).FirstOrDefault();
            if (assignData == null)
            {
                DataSchema.Add(new Schema() { Data = null, Name = cube.AssignTo });
            }
            if (SchemaData != null)
            {
                List<Microsoft.Spark.Sql.Column> cols = new List<Column>();
                foreach (State.IField f in cube.RollupStatement.Fields)
                {
                    cols.Add(SchemaData.Data[f.FieldName]);
                }
                DataFrame df;
                if (cube.RollupStatement.Aggregate == "count")
                {
                    df = SchemaData.Data.Rollup(cols.ToArray()).Count();
                }
                else if (cube.RollupStatement.Aggregate == "avg")
                {
                    df = SchemaData.Data.Rollup(cols.ToArray()).Avg();
                }
                else if (cube.RollupStatement.Aggregate == "sum")
                {
                    df = SchemaData.Data.Rollup(cols.ToArray()).Sum();
                }
                else if (cube.RollupStatement.Aggregate == "min")
                {
                    df = SchemaData.Data.Rollup(cols.ToArray()).Min();
                }
                else if (cube.RollupStatement.Aggregate == "max")
                {
                    df = SchemaData.Data.Rollup(cols.ToArray()).Max();
                }
                else if (cube.RollupStatement.Aggregate == "sd")
                {
                    df = SchemaData.Data.Rollup(cols.ToArray()).Mean();
                }
                else
                {
                    df = SchemaData.Data.Rollup(cols.ToArray()).Count();
                }
                assignData = DataSchema.Where(x => x.Name == cube.AssignTo).FirstOrDefault();
                if (SetOrderBy(cube.RollupStatement.Orders).Count > 0 && GetSearch(cube.RollupStatement.Conditions) != "")
                {
                    assignData.Data = df.Where(GetSearch(cube.RollupStatement.Conditions)).OrderBy(SetOrderBy(cube.RollupStatement.Orders).ToArray());
                }
                else if (SetOrderBy(cube.RollupStatement.Orders).Count > 0)
                {
                    assignData.Data = df.OrderBy(SetOrderBy(cube.RollupStatement.Orders).ToArray());
                }
                else if (GetSearch(cube.RollupStatement.Conditions) != "")
                {
                    assignData.Data = df.
                        Where(GetSearch(cube.RollupStatement.Conditions));
                }
                else
                {
                    assignData.Data = df;
                }

                
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cube"></param>
        private void GetGroupby(DataCube cube)
        {
            SchemaData = DataSchema.Where(x => x.Name == cube.VariableName).FirstOrDefault();
            Schema assignData = DataSchema.Where(x => x.Name == cube.AssignTo).FirstOrDefault();
            if (assignData == null)
            {
                DataSchema.Add(new Schema() { Data = null, Name = cube.AssignTo });
            }
            if (SchemaData != null)
            {
                List<Microsoft.Spark.Sql.Column> cols = new List<Column>();
                foreach (State.IField f in cube.GroupByStatement.Fields)
                {
                    cols.Add(SchemaData.Data[f.FieldName]);
                }
                List<Microsoft.Spark.Sql.Column> aggcols = GetAggColumns(cube.GroupByStatement.AggregateFields, SchemaData.Data);
                assignData = DataSchema.Where(x => x.Name == cube.AssignTo).FirstOrDefault();
                if (SetOrderBy(cube.GroupByStatement.Orders).Count > 0 && GetSearch(cube.GroupByStatement.Conditions) != "")
                {
                    assignData.Data = SchemaData.Data.GroupBy(cols.ToArray()).
                        Agg(GetAggColumn(cube.GroupByStatement.AggregateFields[0], SchemaData.Data), aggcols.ToArray()).Where(GetSearch(cube.GroupByStatement.Conditions)).OrderBy(SetOrderBy(cube.GroupByStatement.Orders).ToArray());
                }
                else if (SetOrderBy(cube.GroupByStatement.Orders).Count > 0)
                {
                    assignData.Data = SchemaData.Data.GroupBy(cols.ToArray()).
                       Agg(GetAggColumn(cube.GroupByStatement.AggregateFields[0], SchemaData.Data), aggcols.ToArray()).OrderBy(SetOrderBy(cube.GroupByStatement.Orders).ToArray());
                }
                else if (GetSearch(cube.GroupByStatement.Conditions) != "")
                {
                    assignData.Data = SchemaData.Data.GroupBy(cols.ToArray()).
                        Agg(GetAggColumn(cube.GroupByStatement.AggregateFields[0], SchemaData.Data), aggcols.ToArray()).
                        Where(GetSearch(cube.GroupByStatement.Conditions));
                }
                else
                {
                    if (aggcols.Count > 0)
                    {
                        assignData.Data = SchemaData.Data.GroupBy(cols.ToArray()).
                             Agg(GetAggColumn(cube.GroupByStatement.AggregateFields[0], SchemaData.Data), aggcols.ToArray());
                    }
                    else
                    {
                        assignData.Data = SchemaData.Data.GroupBy(cols.ToArray()).
                             Agg(GetAggColumn(cube.GroupByStatement.AggregateFields[0], SchemaData.Data));
                    }

                }
              
            }
        }
        private Microsoft.Spark.Sql.Column FunctionColumn(Microsoft.Spark.Sql.Column c, State.IField f) {
            if (f.FieldType == "string")
            {
                var k = ((State.Field.StringField)f);
                if (k.Function == State.Field.StringField.FunctionType.FORMAT)
                    return FormatString(Formating(k.Format), c);
                else if (k.Function == State.Field.StringField.FunctionType.LENGTH)
                    return Length(c);
                else if (k.Function == State.Field.StringField.FunctionType.LOWER)
                    return Lower(c);
                else if (k.Function == State.Field.StringField.FunctionType.UPPER)
                    return Upper(c);
                else if (k.Function == State.Field.StringField.FunctionType.TRIM)
                    return Trim(c);
                else if (k.Function == State.Field.StringField.FunctionType.ISNULL)
                    return IsNull(c);
                else if (k.Function == State.Field.StringField.FunctionType.ISNOTNULL)
                    return (c).IsNotNull();
                else if (k.Function == State.Field.StringField.FunctionType.ISNAN)
                    return IsNaN(c);
                else if (k.Function == State.Field.StringField.FunctionType.ISEMPTY)
                    return c.EqualTo("");
                else
                    return c;
            } if (f.FieldType == "number")
            {
                var k = ((State.Field.NumberField)f);
                if (k.Function == State.Field.NumberField.FunctionType.FLOOR)
                    return Floor(c);
                else if (k.Function == State.Field.NumberField.FunctionType.CEIL)
                    return Ceil(c);
                else if (k.Function == State.Field.NumberField.FunctionType.ROUND)
                    return Round(c, Convert.ToInt32(k.Format));
                else if (k.Function == State.Field.NumberField.FunctionType.NUMBERFORMAT)
                    return FormatNumber(c, Convert.ToInt32(k.Format));
                else
                    return c;
            }
            else if (f.FieldType == "date") {
                var k = ((State.Field.DateField)f);
                if (k.Function == State.Field.DateField.FunctionType.ADDMONTH)
                {
                    
                    return AddMonths(c, k.NumberOfMonths);
                }
                else if (k.Function == State.Field.DateField.FunctionType.TODATE)
                    return ToDate(c, Formating( k.Format));
                else if (k.Function == State.Field.DateField.FunctionType.TOTIME)
                    return ToTimestamp(c, Formating(k.Format));
                else if (k.Function == State.Field.DateField.FunctionType.YEAR)
                    return Year(c);
                else if (k.Function == State.Field.DateField.FunctionType.MONTH)
                    return Month(c);
                else if (k.Function == State.Field.DateField.FunctionType.DAY)
                    return DayOfMonth(c);
                else if (k.Function == State.Field.DateField.FunctionType.HOUR)
                    return Hour(c);
                else if (k.Function == State.Field.DateField.FunctionType.MIN)
                    return Minute(c);
                else if (k.Function == State.Field.DateField.FunctionType.SEC)
                    return Second(c);
                else if (k.Function == State.Field.DateField.FunctionType.WEEK)
                    return WeekOfYear(c);
                else if (k.Function == State.Field.DateField.FunctionType.QUARTER)
                    return Quarter(c);
                else if (k.Function == State.Field.DateField.FunctionType.DATEADD)
                    return DateAdd(c, k.NumberOfDays);
                else if (k.Function == State.Field.DateField.FunctionType.DATEDIFF)
                {
                    if (k.CompareColumn == "cdate")
                    {
                        return DateDiff(CurrentDate(), c);
                    }
                    else
                    {
                        var compare = SchemaData.Data[k.CompareColumn];
                        return DateDiff(c, compare);
                    }
                }
                else if (k.Function == State.Field.DateField.FunctionType.BETWEENMONTH)
                {
                    if (k.CompareColumn == "cdate")
                    {
                        return MonthsBetween(CurrentDate(), c);
                    }
                    else
                    {
                        var compare = SchemaData.Data[k.CompareColumn];
                        return MonthsBetween(c, compare);
                    }
                }
                else if (k.Function == State.Field.DateField.FunctionType.LAST)
                    return LastDay(c);
                else if (k.Function == State.Field.DateField.FunctionType.DATEFORMAT)
                    return DateFormat(c, k.Format);
                else if (k.Function == State.Field.DateField.FunctionType.NEXT)
                    return NextDay(c, k.Format); //"Mon", "Tue", "Wed", "Thu", "Fri", "Sat"
                else if (k.Function == State.Field.DateField.FunctionType.TODATE)
                    return ToDate(c);
                else
                    return c;
            }
            else
                return c;
         
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cube"></param>
        private void GetSelect(DataCube cube)
        {            
            SchemaData = DataSchema.Where(x => x.Name == cube.VariableName).FirstOrDefault();
            Schema assignData = DataSchema.Where(x => x.Name == cube.AssignTo).FirstOrDefault();
            if (assignData == null)
            {
                DataSchema.Add(new Schema() { Data = null, Name = cube.AssignTo });
            }
            if (SchemaData != null)
            {
                assignData = DataSchema.Where(x => x.Name == cube.AssignTo).FirstOrDefault();
                List<Microsoft.Spark.Sql.Column> cols = new List<Column>();
                foreach (State.IField f in cube.SelectStatement.Fields)     {
                    if (f.FieldName == "*")
                    {
                     List<string> cs=   SchemaData.Data.Columns().ToList();
                        foreach (string s in cs) {
                            cols.Add(SchemaData.Data[s].As(s));
                        }
                    }
                    else {
                        cols.Add(FunctionColumn(SchemaData.Data[f.FieldName], f).As(f.AliasName));
                    }
                    
                }
                if (SetOrderBy(cube.SelectStatement.Orders).Count > 0 && GetSearch(cube.SelectStatement.Conditions) != "")
                {
                    assignData.Data = SchemaData.Data.Select(cols.ToArray()).Where(GetSearch(cube.SelectStatement.Conditions)).OrderBy(SetOrderBy(cube.SelectStatement.Orders).ToArray());
                }
                else if (SetOrderBy(cube.SelectStatement.Orders).Count > 0)
                {
                    assignData.Data = SchemaData.Data.Select(cols.ToArray()).OrderBy(SetOrderBy(cube.SelectStatement.Orders).ToArray());
                }
                else if (GetSearch(cube.SelectStatement.Conditions) != "")
                {
                    assignData.Data = SchemaData.Data.Select(cols.ToArray()).Where(GetSearch(cube.SelectStatement.Conditions));
                }
                else
                {
                    assignData.Data = SchemaData.Data.Select(cols.ToArray());
                }
              
            }
            else
            {
                throw new Exception("");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cube"></param>
        private void GetSearch(DataCube cube)
        {
            SchemaData = DataSchema.Where(x => x.Name == cube.VariableName).FirstOrDefault();
            Schema assignData = DataSchema.Where(x => x.Name == cube.AssignTo).FirstOrDefault();
            if (assignData == null)
            {
                DataSchema.Add(new Schema() { Data = null, Name = cube.AssignTo });

            }
            if (SchemaData != null)
            {
                assignData = DataSchema.Where(x => x.Name == cube.AssignTo).FirstOrDefault();
                var searchstr = GetSearch(cube.SearchStatement);
                if (searchstr != "")
                {
                    SchemaData.Data.CreateOrReplaceTempView(SchemaData.Name);
                  //  assignData.Data = SchemaData.Data.Where(GetSearch(cube.SearchStatement));
                    assignData.Data = Spark.Sql("Select * from " + SchemaData.Name + " Where " + searchstr);
                }
               // assignData.Data.WithColumn("", When());


            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cube"></param>
        private void GetNullDrop(DataCube cube) {
            SchemaData = DataSchema.Where(x => x.Name == cube.VariableName).FirstOrDefault();
            Schema assignData = DataSchema.Where(x => x.Name == cube.AssignTo).FirstOrDefault();
            if (assignData == null)
            {
                DataSchema.Add(new Schema() { Data = null, Name = cube.AssignTo });
            }
            if (SchemaData != null)
            {
                assignData = DataSchema.Where(x => x.Name == cube.AssignTo).FirstOrDefault();              

                if (cube.NaDrop != null)
                {
                     
                    List<string> col = new List<string>();
                    col.Add(Formating( cube.NaDrop.ColumnName));
                    assignData.Data = SchemaData.Data.Na().Drop(col);
                }
            }
        }
        private void GetNullReplace(DataCube cube)
        {
            SchemaData = DataSchema.Where(x => x.Name == cube.VariableName).FirstOrDefault();
            Schema assignData = DataSchema.Where(x => x.Name == cube.AssignTo).FirstOrDefault();
            if (assignData == null)
            {
                DataSchema.Add(new Schema() { Data = null, Name = cube.AssignTo });
            }
            if (SchemaData != null)
            {
                assignData = DataSchema.Where(x => x.Name == cube.AssignTo).FirstOrDefault();

                if (cube.ReplaceNull != null)
                {                     
                    List<string> col = new List<string>();
                    col.Add(cube.ReplaceNull.ColumnName);
                    StructType structType = SchemaData.Data.Schema();
                    var dd = structType.Fields.Where(x => x.Name == cube.ReplaceNull.ColumnName).FirstOrDefault();

                    if (dd.DataType.TypeName == "long")
                    {
                        assignData.Data = SchemaData.Data.Na().Fill(GetDefaultValue(Formating(cube.ReplaceNull.Value)), col);
                    }
                    else if (dd.DataType.TypeName == "string")
                    {
                        assignData.Data = SchemaData.Data.Na().Fill(GetDefaultValue(Formating(cube.ReplaceNull.Value)), col);
                    }
                    else if (dd.DataType.TypeName == "date")
                    {
                        assignData.Data = SchemaData.Data.Na().Fill(GetDefaultValue(Formating(cube.ReplaceNull.Value)), col);
                    }
                    else if (dd.DataType.TypeName == "bool")
                    {
                        assignData.Data = SchemaData.Data.Na().Fill(GetDefaultValue(Formating(cube.ReplaceNull.Value)), col);
                    }
                    
                }
            }
        }
        private void GetStringReplace(DataCube cube)
        {
            SchemaData = DataSchema.Where(x => x.Name == cube.VariableName).FirstOrDefault();
            Schema assignData = DataSchema.Where(x => x.Name == cube.AssignTo).FirstOrDefault();
            if (assignData == null)
            {
                DataSchema.Add(new Schema() { Data = null, Name = cube.AssignTo });
            }
            if (SchemaData != null)
            {
                assignData = DataSchema.Where(x => x.Name == cube.AssignTo).FirstOrDefault();

                if (cube.ReplaceNull != null)
                {
               StructType structType =    SchemaData.Data.Schema();
                    var dd = structType.Fields.Where(x => x.Name == cube.ReplaceString.ColumnName).FirstOrDefault();
                    if (dd.DataType.TypeName == "long")
                    {
                        IDictionary<double, double> key = new Dictionary<double, double>();
                        key.Add(Convert.ToInt32(Formating(cube.ReplaceString.Value)), Convert.ToInt32(Formating(cube.ReplaceString.ReplaceValue)));
                        assignData.Data = SchemaData.Data.Na().Replace(cube.ReplaceString.ColumnName, key);
                    }
                    else if (dd.DataType.TypeName == "string") {
                        IDictionary<string, string> key = new Dictionary<string, string>();
                        key.Add(GetDefaultValue(Formating(cube.ReplaceString.Value)), GetDefaultValue(Formating(cube.ReplaceString.ReplaceValue)));
                        assignData.Data = SchemaData.Data.Na().Replace(cube.ReplaceString.ColumnName, key);
                    }
                    //else if (dd.DataType.TypeName == "date")
                    //{
                    //    IDictionary<DateTime, DateTime> key = new Dictionary<DateTime, DateTime>();
                    //    key.Add(GetDefaultValue(Formating(cube.ReplaceString.Value)), GetDefaultValue(Formating(cube.ReplaceString.ReplaceValue)));
                    //    assignData.Data = SchemaData.Data.Na().Replace(cube.ReplaceString.ColumnName, key);
                    //}
                    else if (dd.DataType.TypeName == "bool")
                    {
                        IDictionary<bool, bool> key = new Dictionary<bool, bool>();
                        key.Add( Convert.ToBoolean (Formating(cube.ReplaceString.Value)), Convert.ToBoolean(Formating(cube.ReplaceString.ReplaceValue)));
                        assignData.Data = SchemaData.Data.Na().Replace(cube.ReplaceString.ColumnName, key);
                    }
                    else if (dd.DataType.TypeName == "string")
                    {
                        IDictionary<string, string> key = new Dictionary<string, string>();
                        key.Add(GetDefaultValue(Formating(cube.ReplaceString.Value)), GetDefaultValue(Formating(cube.ReplaceString.ReplaceValue)));
                        assignData.Data = SchemaData.Data.Na().Replace(cube.ReplaceString.ColumnName, key);

                    }
                  
               
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cube"></param>
        private void GetSort(DataCube cube)
        {
            SchemaData = DataSchema.Where(x => x.Name == cube.VariableName).FirstOrDefault();
            Schema assignData = DataSchema.Where(x => x.Name == cube.AssignTo).FirstOrDefault();
            if (assignData == null)
            {
                DataSchema.Add(new Schema() { Data = null, Name = cube.AssignTo });
            }
            if (SchemaData != null)
            {
                assignData = DataSchema.Where(x => x.Name == cube.AssignTo).FirstOrDefault();
                //List<Microsoft.Spark.Sql.Column> cols = new List<Column>();
                //foreach (State.Field f in cube.Fields)
                //{
                //    cols.Add(schemaData.Data[f.FieldName]);
                //}

                if (cube.SortStatement != null)
                {
                    assignData.Data = SchemaData.Data.OrderBy(SetOrderBy(cube.SortStatement));
                }
                 
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cube"></param>
        private void GetUnion(DataCube cube)
        {
            SchemaData = DataSchema.Where(x => x.Name == cube.VariableName).FirstOrDefault();
            Schema assignData = DataSchema.Where(x => x.Name == cube.AssignTo).FirstOrDefault();
            if (assignData == null)
            {
                DataSchema.Add(new Schema() { Data = null, Name = cube.AssignTo });
            }
            if (SchemaData != null)
            {
                assignData = DataSchema.Where(x => x.Name == cube.AssignTo).FirstOrDefault();

                if (cube.UnionStatement != null)
                {
                    var unionData = DataSchema.Where(x => x.Name == cube.UnionStatement.UnionData).FirstOrDefault();
                    if (unionData != null)
                    {
                        assignData.Data = SchemaData.Data.Union(unionData.Data);
                    }                    //assignData.Data = schemaData.Data.OrderBy(setOrderBy(cube.SortStatement));
                }
                
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cube"></param>
        private void GetIntersect(DataCube cube)
        {
            SchemaData = DataSchema.Where(x => x.Name == cube.VariableName).FirstOrDefault();
            Schema assignData = DataSchema.Where(x => x.Name == cube.AssignTo).FirstOrDefault();
            if (assignData == null)
            {
                DataSchema.Add(new Schema() { Data = null, Name = cube.AssignTo });
            }
            if (SchemaData != null)
            {
                assignData = DataSchema.Where(x => x.Name == cube.AssignTo).FirstOrDefault();

                if (cube.IntersectStatement != null)
                {
                    var unionData = DataSchema.Where(x => x.Name == cube.IntersectStatement.IntersectData).FirstOrDefault();
                    if (unionData != null)
                    {
                        assignData.Data = SchemaData.Data.Intersect(unionData.Data);
                    }                    //assignData.Data = schemaData.Data.OrderBy(setOrderBy(cube.SortStatement));
                }
             
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cube"></param>
        private void GetExcept(DataCube cube)
        {
            SchemaData = DataSchema.Where(x => x.Name == cube.VariableName).FirstOrDefault();
            Schema assignData = DataSchema.Where(x => x.Name == cube.AssignTo).FirstOrDefault();
            if (assignData == null)
            {
                DataSchema.Add(new Schema() { Data = null, Name = cube.AssignTo });
            }
            if (SchemaData != null)
            {
                assignData = DataSchema.Where(x => x.Name == cube.AssignTo).FirstOrDefault();

                if (cube.ExceptStatement != null)
                {
                    var unionData = DataSchema.Where(x => x.Name == cube.ExceptStatement.ExceptData).FirstOrDefault();
                    if (unionData != null)
                    {
                        assignData.Data = SchemaData.Data.Except(unionData.Data);
                    }                    //assignData.Data = schemaData.Data.OrderBy(setOrderBy(cube.SortStatement));
                }
              
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cube"></param>
        private void GetDistinct(DataCube cube)
        {
            SchemaData = DataSchema.Where(x => x.Name == cube.VariableName).FirstOrDefault();
            Schema assignData = DataSchema.Where(x => x.Name == cube.AssignTo).FirstOrDefault();
            if (assignData == null)
            {
                DataSchema.Add(new Schema() { Data = null, Name = cube.AssignTo });
            }
            if (SchemaData != null)
            {
                assignData = DataSchema.Where(x => x.Name == cube.AssignTo).FirstOrDefault();
                if (cube.Distrinct == true)
                {
                    assignData.Data = SchemaData.Data.Distinct();
                }
            }
        
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cube"></param>
        private void GetLimit(DataCube cube)
        {
            SchemaData = DataSchema.Where(x => x.Name == cube.VariableName).FirstOrDefault();
            Schema assignData = DataSchema.Where(x => x.Name == cube.AssignTo).FirstOrDefault();
            if (assignData == null)
            {
                DataSchema.Add(new Schema() { Data = null, Name = cube.AssignTo });
            }
            if (SchemaData != null)
            {
                assignData = DataSchema.Where(x => x.Name == cube.AssignTo).FirstOrDefault();
                if (cube.LimitStatement.Max > 0)
                {
                    assignData.Data = SchemaData.Data.Limit(cube.LimitStatement.Max);
                }
            }
          
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cube"></param>
        private void GetDrop(DataCube cube)
        {
            SchemaData = DataSchema.Where(x => x.Name == cube.VariableName).FirstOrDefault();
            Schema assignData = DataSchema.Where(x => x.Name == cube.AssignTo).FirstOrDefault();
            if (assignData == null)
            {
                DataSchema.Add(new Schema() { Data = null, Name = cube.AssignTo });
            }
            if (SchemaData != null)
            {
                assignData = DataSchema.Where(x => x.Name == cube.AssignTo).FirstOrDefault();
                if (cube.Drop == true)

                {
                    List<string> dropfields = new List<string>();
                    foreach (State.IField f in cube.Fields)
                    {
                        dropfields.Add(f.FieldName);
                    }
                    assignData.Data = SchemaData.Data.Drop(dropfields.ToArray());
                }
            }
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cube"></param>
        private void GetJoin(DataCube cube)
        {
            try
            {
                SchemaData = DataSchema.Where(x => x.Name == cube.VariableName).FirstOrDefault();
                Schema assignData = DataSchema.Where(x => x.Name == cube.AssignTo).FirstOrDefault();
                if (assignData == null)
                {
                    DataSchema.Add(new Schema() { Data = null, Name = cube.AssignTo });
                }
                if (SchemaData != null)
                {
                    assignData = DataSchema.Where(x => x.Name == cube.AssignTo).FirstOrDefault();
                    if (cube.JoinStatement != null)
                    {
                        var joinstatement = cube.JoinStatement[0];
                        var joinData = DataSchema.Where(x => x.Name == joinstatement.JoinData).FirstOrDefault();
                       // var joinedData = DataSchema.Where(x => x.Name == cube.JoinStatement.JoinedData).FirstOrDefault();

                        joinData.Data.CreateOrReplaceTempView(joinData.Name);
                        SchemaData.Data.CreateOrReplaceTempView(SchemaData.Name);
                        //List<Column> cs = new List<Column>();
                        //foreach (string c in joinData.Data.Columns()) {
                        //    if (SchemaData.Data.Columns().Contains(c) ==false) {
                        //        cs.Add(joinData.Data[c]);
                        //    }
                        //}
                    //    joinData.Data = joinData.Data.Select(cs.ToArray());
                        List<string> acol = new List<string>();
                        List<string> bcol = new List<string>();
                        foreach (string c in SchemaData.Data.Columns())
                        {
                            var a =  c.Split('_');
                            var cname = a[a.Length - 1];
                            if (acol.Where(x => x.ToLower().IndexOf((SchemaData.Name + "_" + cname).ToLower() ) > 0).Count() == 0) {
                                acol.Add("a." + c + " as " + SchemaData.Name + "_" + cname);
                            }                            
                        }
                        var sccolumn = SchemaData.Data.Columns();
                        foreach (string c in joinData.Data.Columns())
                        {
                            //var existinSame = SchemaData.Data.Columns().Where(x => x.ToLower() == c.ToLower()).FirstOrDefault();
                            //if (existinSame == null)
                            //{
                            var a = c.Split('_');
                            var cname = a[a.Length - 1];
                      
                            if (sccolumn.Where(x => (x + "_" + cname).ToLower() == (joinData.Name + "_" + cname).ToLower()).Count() == 0)
                            {
                                bcol.Add("b." + c + " as " + joinData.Name + "_" + cname);
                            }
                            //}
                        }
                        var state = "select " + string.Join(",", acol);
                        if (bcol.Count > 0)
                        {
                            state = state
                        + "," + string.Join(",", bcol);
                        }
                        state = state
                        + " from " + SchemaData.Name + " a  "+ GetJoinType(joinstatement.JoinType) + " JOIN " + joinData.Name + " b "+ GetCondition(joinstatement.JoinType) + "";
                        int index = 0;
                        foreach (State.Join j in cube.JoinStatement) {
                            if (index == cube.JoinStatement.Count - 1) {
                                state = state + " a." + j.JoinedField + " == b." + j.JoinField;
                            } else {
                                state = state + " a." + j.JoinedField + " == b." + j.JoinField + " AND ";
                            }
                            index = index + 1;
                        }
                        //+" a." + cube.JoinStatement.JoinedField + " == b." + cube.JoinStatement.JoinField +

                        //  state = "select a.BreakHours,b.WeekDayType from lmsschedule a  INNER JOIN lmsscheduleYear b  ON  a.scheduleid == b.scheduleid";
                        assignData.Data = Spark.Sql(state);
                      
                       // assignData.Data.Show();
                        //assignData.Data.Select((assignData.Data[""]).As("sdf"));
                        //assignData.Data.WithColumn("isNull", assignData.Data[""].IsNaN());
                        // assignData.Data = SchemaData.Data.Join(joinData.Data, SchemaData.Data[cube.JoinStatement.JoinedField] == joinData.Data[cube.JoinStatement.JoinField], cube.JoinStatement.JoinType);

                    }
                }            
            }
            catch (Exception e) {
                throw e;
            }
           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private string GetCondition(string s) {
            if (s == "inner" || s == "left" || s == "right" || s == "cross" || s == "inner")
            {
                return " ON ";
            }
            else {
                return " Where ";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jtype"></param>
        /// <returns></returns>
        private string GetJoinType(string jtype) {
            //cube.JoinStatement.JoinType
            if (jtype == "inner")
                return "INNER";
            else if (jtype == "left")
                return "LEFT OUTER";
            else if (jtype == "right")
                return "RIGHT OUTER";
            else if (jtype == "cross")
                return "CROSS";
            else if (jtype == "full")
                return "FULL";
            else
                return "INNER";

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private string Getlogic(string s) {
            if (s == "||")
            {
                return "or";
            }
            else if (s == "&&") {
                return "and";
            } else
                return "";

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sortBy"></param>
        /// <returns></returns>
        private Column SetOrderBy(State.SortBy sortBy)
        {
            Column cls;

            if (sortBy.SortType == "asc")
            {
                cls = (Asc(sortBy.FieldName));
            }
            else
            {
                cls = (Desc(sortBy.FieldName));
            }

            return cls;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sortBy"></param>
        /// <returns></returns>
        private List<Column> SetOrderBy(List<State.SortBy> sortBy)
        {
            List<Column> cls = new List<Column>();
            foreach (State.SortBy s in sortBy)
            {
                if (s.SortType == "asc")
                {
                    cls.Add(Asc(s.FieldName));
                }
                else
                {
                    cls.Add(Desc(s.FieldName));
                }
            }
            return cls;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        private string GetSearch(State.Search search)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (search != null)
            {
                //   foreach (State.Search s in search)
                // {
                if (search.Operation == "#")
                {
                 var val=   ReplaceWithParam(Formating(search.FieldValue)).Split(',');
                    var outer = "(";
                    if (val.Length > 0) {
                        var inval = "";
                        for (int i = 0; i < val.Length; i++) {
                            inval = inval + ",'" + val[i] + "'";
                        }
                        if (inval.StartsWith(",")) {
                            inval = inval.Substring(1);
                        }
                        outer = outer + inval + ")";

                    }
                    sb.AppendFormat("{0} {1} {2}", search.FieldName, GetOperator(search.Operation), outer);
                }
                else {
                    var val = search.FieldValue;
                    val = ReplaceWithParam(Formating(val));
                    if (search.FieldValue.IndexOf("concat") > 0) {
                        val= GetConcat(search.FieldValue);
                    }
                    sb.AppendFormat("{0} {1} {2}", search.FieldName, GetOperator(search.Operation), val);
                }               
                //  }
            }
            return sb.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        private string GetSearch(List<State.Search> search)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (search != null)
            {
                foreach (State.Search s in search)
                {
                    sb.AppendFormat(GetSearch(s));
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private string GetOperator(string s)
        {
            if (s == "==")
            {
                return "=";
            }
            else if (s == "#") {
                return "in";
            }
            else
            {
                return s;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="af"></param>
        /// <param name="df"></param>
        /// <returns></returns>
        private Column GetAggColumn(State.AggregateField af, DataFrame df)
        {
            //    Column c = new Column();

            //'avg' | 'sum' | 'min' | 'max' | 'sd' | 'davg' | 'count' | 'dcount'
            if (af.AggregateType == "count")
            {
                return (Functions.Count(df[af.FieldName]));
            }
            else if (af.AggregateType == "avg")
            {
                return (Functions.Avg(df[af.FieldName]));
            }
            else if (af.AggregateType == "sum")
            {
                return (Functions.Sum(df[af.FieldName]));
            }
            else if (af.AggregateType == "min")
            {
                return (Functions.Min(df[af.FieldName]));
            }
            else if (af.AggregateType == "max")
            {
                return (Functions.Max(df[af.FieldName]));
            }
            else if (af.AggregateType == "sd")
            {
                return (Functions.SumDistinct(df[af.FieldName]));
            }
            else if (af.AggregateType == "davg")
            {
                return (Functions.Stddev(df[af.FieldName]));
            }
            else if (af.AggregateType == "dcount")
            {
                return (Functions.CountDistinct(df[af.FieldName]));
            }
            else
                return null;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aggs"></param>
        /// <param name="df"></param>
        /// <returns></returns>
        private List<Column> GetAggColumns(List<State.AggregateField> aggs, DataFrame df)
        {
            List<Column> cls = new List<Column>();
            int index = 0;
            foreach (State.AggregateField af in aggs)
            {
                if (index == 0)
                {
                    index = 1;
                    continue;
                }
                GetAggColumn(af, df);
            }
            return cls;
        }
        /// <summary>
        /// 
        /// </summary>
        public void Stop()
        {
            Spark.Stop();
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public Analyzer AddSchema(Schema s)
        {                       

            this.DataSchema.Add(s);
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public string ReplaceWithParam(string s) {
            List<Schema> declaration = DataSchema.Where(x => x.Type == SchemaType.PARAM || x.Type == SchemaType.VARIABLE).ToList();
            foreach (Schema sc in declaration) {
              s=  s.Replace(sc.Name.ToLower(), sc.Value.ToLower());
                s = s.Replace("#" +sc.Name.ToLower() + "#", sc.Value.ToLower());
            }
            return s;          
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private string Formating(string s)
        {
            return s.Replace("\"", "");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        private StructField GetStructField(State.IField f)
        {
            if (f.FieldType == "number")
            {
                return new StructField(Formating(f.FieldName), new IntegerType());
            }
            else if (f.FieldType == "double") {
                return new StructField(Formating(f.FieldName), new DoubleType());
            }
            else if (f.FieldType == "decimal")
            {
                return new StructField(Formating(f.FieldName), new DecimalType());
            }
            else if (f.FieldType == "string")
            {
                return new StructField(Formating(f.FieldName), new StringType());
            }
            else if (f.FieldType == "date")
            {
                return new StructField(Formating(f.FieldName), new DateType());
            }
            else if (f.FieldType == "bool")
            {
                return new StructField(Formating(f.FieldName), new BooleanType());
            }
            else
            {
                return new StructField(Formating(f.FieldName), new StringType());
            }
        }
    }


}
