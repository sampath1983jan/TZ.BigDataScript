using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AScriptParser;

namespace TZ.DataAnalyzer
{
    public class AScriptVisitor : AScriptBaseVisitor<object>
    {
        public List<DataCube> DataCubes;
        
        public AScriptVisitor()
        {
            DataCubes = new List<DataCube>();
        }
        public override object VisitLine(AScriptParser.LineContext context)
        {
            VarContext var = context.var();
            if (context.declare() != null)
            {
                if (context.declare().GetText() == "v")
                {
                    var dq = new DataCube(var.GetText(), DataStatement.VARIABLE, var.GetText())
                    {
                        Declaration = new State.Declaration() { Name = var.GetText(), Type = context.declare().GetText(), Value = context.TEXT().GetText() }
                    };
                    DataCubes.Add(dq);
                }
                else if (context.declare().GetText() == "p")
                {
                    var dq = new DataCube(var.GetText(), DataStatement.PARAM, var.GetText())
                    {
                        Declaration = new State.Declaration() { Name = var.GetText(), Type = context.declare().GetText(), Value = context.param().GetText() }
                    };
                    DataCubes.Add(dq);
                }
                else if (context.declare().GetText() == "dc")
                {
                    var dq = new DataCube(var.GetText(), DataStatement.DC, var.GetText())
                    {
                        Declaration = new State.Declaration() { Name = var.GetText(), Type = context.declare().GetText(), Value = context.todata().dataitem().GetText() }
                    };
                    DataCubes.Add(dq);
                }
                return DataCubes;
            }
            else if (context.dr() != null) {
                var dq = new DataCube("dataroot", DataStatement.DR, "dataroot")
                {
                    DataRoot = context.path().GetText()
            };                
                DataCubes.Add(dq);
                return DataCubes;
            }
            else if (context.ns() != null)
            {
                var dq = new DataCube("namespace", DataStatement.NS, "namespace")
                {
                    Namespace = context.TEXT().GetText()
                };
                DataCubes.Add(dq);
                return DataCubes;
            }
            else if (context.nsp() != null)
            {
                var dq = new DataCube("namespace", DataStatement.NS, "namespace")
                {
                    Namespace = context.param().GetText()
                };
                DataCubes.Add(dq);
                return DataCubes;
            }
            else if (context.imp() != null)
            {
                var st = context.imptext().GetText().Split(',');
                foreach (string s in st) {
                    var dq = new DataCube(s, DataStatement.IMP, s)
                    {
                        ImportData = s
                    };
                    DataCubes.Add(dq);
                }
                
                return DataCubes;
            }
            if (context.data() != null)
            {
                SetSelect(context.select(), context.data().GetText(), var.GetText());
                
                SetJoin(context.join(), context.data().GetText(), var.GetText());
                SetGroupby(context.groupby(), context.data().GetText(), var.GetText());
                SetCube(context.cube(), context.data().GetText(), var.GetText());
                SetRollup(context.rollup(), context.data().GetText(), var.GetText());
                Setdistinct(context.dist(), context.data().GetText(), var.GetText());
                SetLimit(context.limit(), context.data().GetText(), var.GetText());
                SetIntersect(context.intersect(), context.data().GetText(), var.GetText());
                SetUnion(context.union(), context.data().GetText(), var.GetText());
                SetExcept(context.except(), context.data().GetText(), var.GetText());
                SetExcept(context.except(), context.data().GetText(), var.GetText());
                SetSort(context.sortstatement(), context.data().GetText(), var.GetText());
                SetSearch(context.filtercondition(), context.data().GetText(), var.GetText());
                SetDrop(context.drop(), context.data().GetText(), var.GetText());
                SetWith(context.withcolumn(), context.data().GetText(), var.GetText());
                SetNaDrop(context.nadrop(), context.data().GetText(), var.GetText());
                SetNullReplace(context.nullreplace(), context.data().GetText(), var.GetText());
                SetStringReplace(context.stringreplace(), context.data().GetText(), var.GetText());
            }
            else {
                SetLoad(context.load(), var.GetText());
            }         
            return DataCubes;
        }
        private void SetNaDrop(NadropContext nadrop, string name, string varname) {
            if (nadrop != null) {
                var dq = new DataCube(name, DataStatement.NADROP, varname);
                dq.NaDrop.ColumnName = nadrop.TEXT().GetText();
                DataCubes.Add(dq);
            }           
        }

        private void SetNullReplace(NullreplaceContext nullreplace, string name, string varname) {
            if (nullreplace != null) {
                var dq = new DataCube(name, DataStatement.NAREPLACE, varname);
                dq.ReplaceNull.ColumnName = nullreplace.strfield().GetText() ;
                dq.ReplaceNull.Value = nullreplace.strvalue().GetText();
                DataCubes.Add(dq);
            }
        }
        private void SetStringReplace(StringreplaceContext stringreplace, string name, string varname)
        {
            if (stringreplace != null)
            {
                var dq = new DataCube(name, DataStatement.STRINGREPLACE, varname);
                dq.ReplaceString.ColumnName = stringreplace.strfield().GetText();
                dq.ReplaceString.Value = stringreplace.strvalue().GetText();
                dq.ReplaceString.ReplaceValue = stringreplace.strreplacevalue().GetText();
                DataCubes.Add(dq);
            }
        }
        private void SetWith(WithcolumnContext withcolumn, string name, string varName) {
            if (withcolumn != null) {
                var dq = new DataCube(name, DataStatement.WITH, varName);
                if (withcolumn.newcolumn() != null)
                {
                    dq.DataWith.Name = withcolumn.newcolumn().WORD().GetText();
                    List<AScriptParser.MulticaseContext> mc = withcolumn.newcolumn().multiwhen().multicase().ToList();
                    foreach (AScriptParser.MulticaseContext mcitem in mc)
                    {
                        var mv = new State.MultipleWhen();

                        if (mcitem.joincase().Length > 0)
                        {
                            foreach (JoincaseContext jc in mcitem.joincase())
                            {
                                var methodname = "";
                                var val = "";
                                if (mcitem.newcase().STRINGUDF() != null)
                                {
                                    methodname = mcitem.newcase().STRINGUDF().GetText();
                                }
                                else
                                {
                                    val = jc.newcase().TEXT().GetText();
                                }
                                var w = new State.When();
                                w.Logical = jc.OPER().GetText();
                                w.FieldName = jc.newcase().WORD().GetText();
                                w.Value = val;
                                w.MethodName = methodname;
                                mv.WhenItem.Add(w);
                            }
                        }
                        if (mcitem.newcase() != null)
                        {
                            var methodname = "";
                            var val = "";
                            if (mcitem.newcase().STRINGUDF() != null)
                            {
                                methodname = mcitem.newcase().STRINGUDF().GetText();
                            }
                            else
                            {
                                val = mcitem.newcase().TEXT().GetText();
                            }
                            var cw = new State.When()
                            {
                                FieldName = mcitem.newcase().WORD().GetText(),
                                Value = val,
                                MethodName = methodname,
                            };
                            mv.WhenItem.Add(cw);

                        }


                        mv.AliasValue = mcitem.TEXT().GetText();
                        dq.DataWith.WhenItems.Add(mv);
                    }
                }
                else if (withcolumn.concatcolumn() != null) {
                    dq.DataWith.Name = withcolumn.concatcolumn().WORD().GetText();
                  var state=  withcolumn.concatcolumn().concatstate().conlit();
                    foreach (ConlitContext c in state) {
                        dq.DataWith.Concat.Add(c.GetText());
                    }
                }             
             
                DataCubes.Add(dq);
            }
        }
        private void SetSearch(FilterconditionContext filter, string name, string varName) {
            if (filter != null) {
                var dq = new DataCube(name, DataStatement.SEARCH, varName);
                ConditionContext condition = filter.condition();
                var search = new State.Search(condition.children[0].GetText(), condition.children[1].GetText(), condition.children[2].GetText());
                dq.SearchStatement=(search);
                DataCubes.Add(dq);
            }
         
        }
        private void SetSort(SortstatementContext sortcontext, string name, string varName) {
            if (sortcontext != null) {
                var dq = new DataCube(name, DataStatement.SORT, varName);                
                    OrderContext order = sortcontext.order();
                    dq.SortStatement =(new State.SortBy(order.ORDERBY()[0].GetText(), order.fields()[0].GetText()));
                DataCubes.Add(dq);
            }
        }
        private void SetLoad(LoadContext load, string name)
        {
            var dq = new DataCube(name, DataStatement.LOAD,name);
            if (load != null)
            {
                dq.LoadStatement = new State.Load(load.path().GetText());
                var fields = load.datafields().GetText().Split(',');
                var schema = load.schemaname().GetText();
                dq.SchemaName = schema;
                dq.Fields = new List<State.IField>();
                for (int i = 0; i < fields.Length; i++)
                {
                    var fin = fields[i].Split(':');
                    
                    dq.Fields.Add(new State.DataField() { FieldName = fin[0], FieldType = fin[1] });
                }
                DataCubes.Add(dq);
            }
        }
        private State.Field.DateField.FunctionType getDateFunction(string fn) {
            if (fn == "year")
            {
                return State.Field.DateField.FunctionType.YEAR;
            }
            else if (fn == "dformat")
                return State.Field.DateField.FunctionType.DATEFORMAT;
            else if (fn == "month")
            {
                return State.Field.DateField.FunctionType.MONTH;
            }
            else if (fn == "day")
            {
                return State.Field.DateField.FunctionType.DAY;
            }
            else if (fn == "minute")
            {
                return State.Field.DateField.FunctionType.MIN;
            }
            else if (fn == "sec")
            {
                return State.Field.DateField.FunctionType.SEC;

            }
            else if (fn == "weekno")
            {
                return State.Field.DateField.FunctionType.WEEK;
            }
            else if (fn == "hours")
            {
                return State.Field.DateField.FunctionType.HOUR;
            }

            else if (fn == "last")
            {
                return State.Field.DateField.FunctionType.LAST;
            }
            else if (fn == "quarter")
            {
                return State.Field.DateField.FunctionType.QUARTER;
            }
            else if (fn == "dateadd")
            {
                return State.Field.DateField.FunctionType.DATEADD;
            }
            else if (fn == "datediff")
            {
                return State.Field.DateField.FunctionType.DATEDIFF;
            }
            else if (fn == "addmonth")
            {
                return State.Field.DateField.FunctionType.ADDMONTH;
            }
            else if (fn == "monthbetween")
            {
                return State.Field.DateField.FunctionType.BETWEENMONTH;
            }
            else if (fn == "next")
            {
                return State.Field.DateField.FunctionType.NEXT;
            }
            else if (fn == "todate") {
                return State.Field.DateField.FunctionType.TODATE;
            }
            else if (fn == "totime")
            {
                return State.Field.DateField.FunctionType.TOTIME;
            }
            else
            {
                return State.Field.DateField.FunctionType.NONE;
            }
        }
        private State.Field.NumberField.FunctionType getNumFunction(string fn) {
            if (fn == "nformat")
            {
                return State.Field.NumberField.FunctionType.NUMBERFORMAT;
            }
            else if (fn == "round")
            {
                return State.Field.NumberField.FunctionType.ROUND;
            }
            else if (fn == "floor")
            {
                return State.Field.NumberField.FunctionType.FLOOR;
            }
            else if (fn == "ceil")
            {
                return State.Field.NumberField.FunctionType.CEIL;
            }
            else {

                return State.Field.NumberField.FunctionType.NONE;
            }
        }
        private State.Field.StringField.FunctionType getStringFunction(string fn)
        {
            if (fn == "lower")
                return State.Field.StringField.FunctionType.LOWER;
            else if (fn == "trim")
                return State.Field.StringField.FunctionType.TRIM;
            else if (fn == "upper")
                return State.Field.StringField.FunctionType.UPPER;
            else if (fn == "length")
                return State.Field.StringField.FunctionType.LENGTH;
            else if (fn == "sformat")
                return State.Field.StringField.FunctionType.FORMAT;
            else if (fn == "isnull")
                return State.Field.StringField.FunctionType.ISNULL;
            else if (fn == "isnotnull")
                return State.Field.StringField.FunctionType.ISNOTNULL;
            else if (fn == "isnan")
                return State.Field.StringField.FunctionType.ISNAN;
            else if (fn == "isempty")
                return State.Field.StringField.FunctionType.ISEMPTY;
            else
                return State.Field.StringField.FunctionType.NONE;            
        }
        private State.Field.DateField GetDateField(DateadvancefunctionContext df) {
            State.Field.DateField.FunctionType ft = getDateFunction(df.DATEUDF().GetText());
            if (ft == State.Field.DateField.FunctionType.DATEDIFF)
            {
                var fv = df.children[2].GetText().Split(',');
                return new State.Field.DateField(fv[0], "")
                {
                    CompareColumn = fv[1],
                    Function = ft,
                };
            }
            else if (ft == State.Field.DateField.FunctionType.DATEADD)
            {
                var fv = df.children[2].GetText().Split(',');
                return new State.Field.DateField(fv[0], "")
                {
                    NumberOfDays = Convert.ToInt32(fv[1]),
                    Function = ft,
                };
            }
            else if (ft == State.Field.DateField.FunctionType.DATEFORMAT)
            {
                var fv = df.children[2].GetText().Split(',');
                return new State.Field.DateField(fv[0], "")
                {
                    Format = fv[1],
                    Function = ft,
                };
            }
            else if (ft == State.Field.DateField.FunctionType.BETWEENMONTH)
            {
                var fv = df.children[2].GetText().Split(',');
                return new State.Field.DateField(fv[0], "")
                {
                    CompareColumn = fv[1],
                    Function = ft,
                };
            }
            else if (ft == State.Field.DateField.FunctionType.ADDMONTH)
            {
                var fv = df.children[2].GetText().Split(',');
                return new State.Field.DateField(fv[0], "")
                {
                    NumberOfMonths = Convert.ToInt32(fv[1]),
                    Function = ft,
                };
            }
            else if (ft == State.Field.DateField.FunctionType.NEXT)
            {
                var fv = df.children[2].GetText().Split(',');
                return new State.Field.DateField(fv[0], "")
                {
                    Format = fv[1],
                    Function = ft,
                };
            }
            else if (ft == State.Field.DateField.FunctionType.TODATE) {
                var fv = df.children[2].GetText().Split(',');
                return new State.Field.DateField(fv[0], "")
                {
                    Format = fv[1],
                    Function = ft,
                };
            }
            else if (ft == State.Field.DateField.FunctionType.TOTIME)
            {
                var fv = df.children[2].GetText().Split(',');
                return new State.Field.DateField(fv[0], "")
                {
                    Format = fv[1],
                    Function = ft,
                };
            }
            else
            {
                throw new Exception("Undefined function in date");
            }
        }
        #region Validation
        private bool IsEmpty(string s) {
            if (s == "")
            {
                return true;
            }
            else {
                return false;
            }
        }
        #endregion 
        private void SetSelect(SelectContext select, string name,string varName)
        {
            if (select != null)
            {
                var dq = new DataCube(name, DataStatement.SELECT, varName);
                FieldsContext fields = select.fields();
                FilterconditionContext Filtercondition = select.filtercondition();
                SortstatementContext sortStatement = select.sortstatement();
                if (fields != null)
                {                   
                  StringltrContext[] fieldStrings =  fields.stringltr();
                    if (fieldStrings != null) {
                        foreach (StringltrContext sc in fieldStrings) {
                            if (sc.strings() != null)
                            {
                                var ss = sc.strings().GetText();
                                if (ss.IndexOf(":") > 0)
                                {
                                    var sf = ss.Split(':');
                                    dq.SelectStatement.AddField(new State.Field.StringField(sf[0], sf[1]));
                                }
                                else
                                {
                                    dq.SelectStatement.AddField(new State.Field.StringField(ss, ss));
                                }
                            }
                            else if (sc.datefunction() != null)
                            {
                                DatefunctionContext df = sc.datefunction();
                                dq.SelectStatement.AddField(new State.Field.DateField(df.WORD().GetText(), sc.WORD().GetText())
                                { Function = getDateFunction(df.DATEFUNCTION().GetText()) });
                            }
                            else if (sc.dateadvancefunction() != null)
                            {
                                DateadvancefunctionContext df = sc.dateadvancefunction();
                                var dfield = GetDateField(df);
                                dfield.AliasName = sc.WORD().GetText();
                                dq.SelectStatement.AddField(dfield);
                            }
                            else if (sc.numberfunction() != null) {
                                NumberfunctionContext nf = sc.numberfunction();
                                State.Field.NumberField.FunctionType nft = getNumFunction(nf.NUMBERUDF().GetText());
                                string fm = "";
                                if (nft == State.Field.NumberField.FunctionType.NUMBERFORMAT || nft == State.Field.NumberField.FunctionType.ROUND) {
                                  //  fm = nf.dataformat().INT().GetText().ToString();
                                }
                                dq.SelectStatement.AddField(new State.Field.NumberField(nf.WORD().GetText(), sc.WORD().GetText())
                                { Function = getNumFunction(nf.NUMBERUDF().GetText()) ,Format = fm });
                            }
                            else if (sc.numberformat() != null)
                            {
                                NumberformatContext nf = sc.numberformat();
                                State.Field.NumberField.FunctionType nft = getNumFunction(nf.NUMBERUDF().GetText());
                                string fm = "";
                                if (nft == State.Field.NumberField.FunctionType.NUMBERFORMAT || nft == State.Field.NumberField.FunctionType.ROUND)
                                {
                                    fm = nf.INT().GetText().ToString();
                                }
                                dq.SelectStatement.AddField(new State.Field.NumberField(nf.WORD().GetText(), sc.WORD().GetText())
                                { Function = getNumFunction(nf.NUMBERUDF().GetText()), Format = fm });
                            }
                            else if (sc.stringfunction() != null)
                            {
                                StringfunctionContext df = sc.stringfunction();
                                State.Field.StringField.FunctionType nft = getStringFunction(df.STRINGUDF().GetText());
                                //string fm = "";
                                //if (nft == State.Field.StringField.FunctionType.FORMAT)
                                //{
                                //    fm = df.dataformat().INT().GetText().ToString();
                                //}
                                dq.SelectStatement.AddField(new State.Field.StringField(df.WORD().GetText(), sc.WORD().GetText())
                                { Function = getStringFunction(df.STRINGUDF().GetText()) ,Format= "" });
                            }
                            else if (sc.stringformat() != null)
                            {
                                StringformatContext df = sc.stringformat();
                                State.Field.StringField.FunctionType nft = State.Field.StringField.FunctionType.FORMAT;
                                string fm = "";
                                if (nft == State.Field.StringField.FunctionType.FORMAT)
                                {
                                    fm = df.TEXT().GetText();
                                }
                                dq.SelectStatement.AddField(new State.Field.StringField(df.WORD().GetText(), sc.WORD().GetText())
                                { Function = nft, Format = fm });
                            }
                        }                             
                    }
                }
                if (Filtercondition != null)
                {
                    ConditionContext condition = Filtercondition.condition();
                    var search = new State.Search(condition.children[0].GetText(), condition.children[1].GetText(), condition.children[2].GetText());
                    dq.SelectStatement.AddCondition(search);
                }
                if (sortStatement != null)
                {
                    OrderContext order = sortStatement.order();
                    dq.SelectStatement.AddOrder(new State.SortBy(order.ORDERBY()[0].GetText(), order.fields()[0].GetText()));
                }
                DataCubes.Add(dq);
            }
        }
        private void SetGroupby(GroupbyContext groupby, string name, string varName)
        {
            if (groupby != null)
            {
                var dq = new DataCube(name, DataStatement.GROUPBY, varName);
                FieldsContext fields = groupby.fields();
                FilterconditionContext Filtercondition = groupby.filtercondition();
                SortstatementContext sortStatement = groupby.sortstatement();
                AggContext agg = groupby.agg();
                if (fields != null)
                {
                    string[] s = fields.GetText().Split(',');
                    for (int i = 0; i < s.Length; i++)
                    {
                        dq.GroupByStatement.AddField(new State.DataField(s[i], s[i]));
                    }
                }
                if (agg != null)
                {
                    string[] s = agg.GetText().Split(',');
                    for (int i = 0; i < s.Length; i++)
                    {
                        string[] f = s[i].Split('.');
                        var a = new State.AggregateField(f[0], f[0])
                        {
                            AggregateType = f[1]
                        };
                        dq.GroupByStatement.AddAgg(a);
                    }
                }
                if (Filtercondition != null)
                {
                    ConditionContext condition = Filtercondition.condition();
                    var search = new State.Search(condition.children[0].GetText(), condition.children[1].GetText(), condition.children[2].GetText());
                    dq.SelectStatement.AddCondition(search);
                }
                if (sortStatement != null)
                {
                    OrderContext order = sortStatement.order();
                    dq.SelectStatement.AddOrder(new State.SortBy(order.ORDERBY()[0].GetText(), order.fields()[0].GetText()));
                }
                DataCubes.Add(dq);
            }
        }
        private void SetJoin(JoinContext join, string name, string varName)
        {
            var dq = new DataCube(name, DataStatement.JOIN, varName);
            if (join != null)
            {
                MultijoinContext mj = join.multijoin();
                if (mj != null)
                {
                   List<RelationContext> rc = mj.relation().ToList();
                    foreach (RelationContext r in rc) {
                        dq.JoinStatement.Add(new State.Join()
                        {
                            JoinType = join.JOINTYPE().GetText(),
                            JoinData = r.joindata().GetText(),
                            JoinField = r.joinfield().GetText(),
                            JoinedData = r.joineddata().GetText(),
                            JoinedField = r.joinedfield().GetText()
                        });
                    }                  
                }
                DataCubes.Add(dq);
            }
        }
        private void SetCube(CubeContext cube, string name, string varName)
        {
            var dq = new DataCube(name, DataStatement.CUBE, varName);
            if (cube != null)
            {
                FilterconditionContext Filtercondition = cube.filtercondition();
                SortstatementContext sortStatement = cube.sortstatement();
                FieldsContext fs = cube.fields();
                AggtypeContext aggtype = cube.aggtype();
                dq.CubeStatement = new State.Cube() { Aggregate = aggtype.GetText() };
                if (fs != null)
                {
                    string[] s = fs.GetText().Split(',');
                    for (int i = 0; i < s.Length; i++)
                    {
                        dq.CubeStatement.AddField(new State.DataField(s[i], s[i]));
                    }
                }
                if (Filtercondition != null)
                {
                    ConditionContext condition = Filtercondition.condition();
                    var search = new State.Search(condition.children[0].GetText(), condition.children[1].GetText(), condition.children[2].GetText());
                    dq.CubeStatement.AddCondition(search);
                }
                if (sortStatement != null)
                {
                    OrderContext order = sortStatement.order();
                    dq.CubeStatement.AddOrder(new State.SortBy(order.ORDERBY()[0].GetText(), order.fields()[0].GetText()));
                }
                DataCubes.Add(dq);
            }
        }
        private void SetRollup(RollupContext rollup, string name, string varName)
        {
            var dq = new DataCube(name, DataStatement.ROLLUP, varName);

            if (rollup != null)
            {
                FilterconditionContext Filtercondition = rollup.filtercondition();
                SortstatementContext sortStatement = rollup.sortstatement();
                FieldsContext fs = rollup.fields();
                AggtypeContext aggtype = rollup.aggtype();
                dq.RollupStatement = new State.Rollup() { Aggregate = aggtype.GetText() };
                if (fs != null)
                {
                    string[] s = fs.GetText().Split(',');
                    for (int i = 0; i < s.Length; i++)
                    {
                        dq.RollupStatement.AddField(new State.DataField(s[i], s[i]));
                    }
                }
                if (Filtercondition != null)
                {
                    ConditionContext condition = Filtercondition.condition();
                    var search = new State.Search(condition.children[0].GetText(), condition.children[1].GetText(), condition.children[2].GetText());
                    dq.RollupStatement.AddCondition(search);
                }
                if (sortStatement != null)
                {
                    OrderContext order = sortStatement.order();
                    dq.RollupStatement.AddOrder(new State.SortBy(order.ORDERBY()[0].GetText(), order.fields()[0].GetText()));
                }
                DataCubes.Add(dq);
            }


        }
        private void SetIntersect(IntersectContext ints, string name, string varName)
        {
            var dq = new DataCube(name, DataStatement.INTERSECT, varName);
            if (ints != null)
            {
                bool isall = false;
                if (ints.isall() != null)
                {
                    isall = true;
                }
                dq.IntersectStatement = new State.Intersect() { IsAll = isall, IntersectData = ints.data().GetText() };
                DataCubes.Add(dq);
                //  Lines.Add(new SpeakLine() { Person = ints.data().GetText(), Text = isall.ToString() });
            }

        }
        private void SetExcept(ExceptContext expt, string name, string varName)
        {
            var dq = new DataCube(name, DataStatement.EXCEPT, varName);
            if (expt != null)
            {
                bool isall = false;
                if (expt.isall() != null)
                {
                    isall = true;
                }
                dq.ExceptStatement = new State.Except() { IsAll = isall, ExceptData = expt.data().GetText() };
                DataCubes.Add(dq);
                //Lines.Add(new SpeakLine() { Person = expt.data().GetText(), Text = isall.ToString() });
            }
        }
        private void SetUnion(UnionContext union, string name, string varName)
        {
            if (union != null)
            {
                var dq = new DataCube(name, DataStatement.UNION, varName);
                bool isall = false;
                if (union.isall() != null)
                {
                    isall = true;
                }
                dq.UnionStatement = new State.Union() { IsAll = isall, UnionData = union.data().GetText() };
                DataCubes.Add(dq);
            }
        }
        private void Setdistinct(DistContext dist, string name, string varName)
        {
            if (dist != null)
            {
                var dq = new DataCube(name, DataStatement.DISTRINCT, varName)
                {
                    Distrinct = true
                };
                DataCubes.Add(dq);
            }
        }
        private void SetDrop(DropContext dist, string name, string varName)
        {
            if (dist != null)
            {
                var dq = new DataCube(name, DataStatement.DROP, varName);
                if (dist.fields().GetText() != "") {
                    string[] s = dist.fields().GetText().Split(',');
                    for (int i = 0; i < s.Length; i++)
                    {
                        dq.Fields.Add(new State.DataField(s[i], s[i]));
                    }
                }
                dq.Drop = true;
                DataCubes.Add(dq);
            }
        }
        private void SetLimit(LimitContext limit, string name, string varName)
        {
            if (limit != null)
            {
                var dq = new DataCube(name, DataStatement.LIMIT, varName)
                {
                    LimitStatement = new State.Limit() { Max = Convert.ToInt32(limit.limitvalue().GetText()) }
                };
                //  Lines.Add(new SpeakLine() { Person = "", Text = limit.limitvalue().GetText() });
                DataCubes.Add(dq);
            }
        }

    }
}
