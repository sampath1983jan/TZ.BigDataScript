using System;
using System.Collections.Generic;
using System.Text;
using TZ.DataAnalyzer.State;
namespace TZ.DataAnalyzer
{
    public enum DataStatement { 
    SELECT,
    GROUPBY,
    CUBE,
    ROLLUP,
    INTERSECT,
    EXCEPT,
    JOIN,
    DROP,
    SEARCH,
    LIMIT,
    UNION,
    LOAD,
    DISTRINCT,
    SORT,
    VARIABLE,
    PARAM,
    DC,
    NS,
    IMP,
    DR,
    WITH,
    NADROP,
    NAREPLACE,
    STRINGREPLACE,
            

    }
   public class DataCube
    {
        public DataStatement StatementType { get; set; }
        public Select SelectStatement { get; set; }
        public GroupBy GroupByStatement { get; set; }
        public Cube CubeStatement { get; set; }
        public Except ExceptStatement { get; set; }
        public Intersect IntersectStatement { get; set; }
        public Drop DropStatement { get; set; }
        public List<Join> JoinStatement { get; set; }
        public Rollup RollupStatement { get; set; }
        public Search SearchStatement { get; set; }
        public SortBy SortStatement { get; set; }
        public Limit LimitStatement { get; set; }
        public Union UnionStatement { get; set; }
        public Load LoadStatement { get; set; }
        public string VariableName { get; set; }
        public string AssignTo { get; set; }
        public string SchemaName { get; set; }
        public List<IField> Fields { get; set; }
        public Boolean Distrinct { get; set; }
        public Boolean Drop { get; set; }
        public Declaration Declaration { get; set; }
        public With DataWith { get; set; }
        public string DataRoot { get; set; }
        public string Namespace { get; set; }
        public string ImportData { get; set; }
        public NaDrop NaDrop { get; set; }
        public NullReplace ReplaceNull { get; set; }
        public StringReplace ReplaceString { get; set; }

        public DataCube(string name, DataStatement statement,string assignTo) {
            NaDrop = new NaDrop();
            ReplaceNull = new NullReplace();
            ReplaceString = new StringReplace();
            this.StatementType = statement;
            SchemaName = "";
            VariableName = name;
            AssignTo = assignTo;
            Distrinct = false;
            SelectStatement = new Select();
            GroupByStatement = new GroupBy();
            CubeStatement = new Cube();
            ExceptStatement = new Except();
            IntersectStatement = new Intersect();
            DropStatement = new Drop();
            JoinStatement = new List<Join>();
            RollupStatement = new Rollup();
            SearchStatement = new Search();
            SortStatement = new SortBy();
            LimitStatement = new Limit();
            LoadStatement = new Load();
            Fields = new List<IField>();
            Drop = false;
            UnionStatement = new Union();
            Declaration = new Declaration();
            DataWith = new With();
        }

    }
}
