using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TZ.DataAnalyzer;
using TZ.DataAnalyzer.Script;
using static AScriptParser;

namespace TZ.DataAnalyzer
{
    public class AScript
    {
        readonly AScriptVisitor visitor = new AScriptVisitor();
        public List<string> Errors { get; private set; }
        public bool EnableCache { get; set; }
        public IDictionary<string, string> Param { get; set; }
        public AScript() {
            Param = new Dictionary<string, string>();
        }
        public void Add(string name, string value) {
            this.Param.Add(name, value);
        }
        public void Compile(string name,
            string script,
            string conn, string outputPath,
            string scriptAssemblyPath
            )
        {
            Analyzer a = new Analyzer(name, Analyzer.OutputFormat.JSON);
            try
            {
                //@"C:\WorkingFolder-Custom\TZ.BigDataScript\TZ.DataAnalyzer.Console\bin\Debug"
                Environment.SetEnvironmentVariable("DOTNETBACKEND_PORT", "5567");
            //Environment.SetEnvironmentVariable("DOTNET_ASSEMBLY_SEARCH_PATHS", scriptAssemblyPath);
            //  ExecuteCommand("cmd", @"%SPARK_HOME%\bin\spark-submit --class org.apache.spark.deploy.dotnet.DotnetRunner --master local  "+ scriptAssemblyPath + @"\microsoft-spark-2.4.x-0.8.0.jar\ dotnet  " + scriptAssemblyPath +@"\ TZ.DataAnalyzer.dll");
            ExecuteCommand("cmd", @"%SPARK_HOME%\bin\spark-submit --class org.apache.spark.deploy.dotnet.DotnetRunner --master local  " + scriptAssemblyPath + @"\microsoft-spark-2.4.x-0.10.0.jar\ debug");

            AntlrInputStream inputStream = new AntlrInputStream(script);
            AScriptLexer lexer = new AScriptLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);
            AScriptParser expPar = new AScriptParser(commonTokenStream);
            expPar.ErrorHandler = new MyErrorStrategy();
            AScriptParser.StatementContext getContext = expPar.statement();
                if (expPar.Errors != null)
                {
                    Errors = expPar.Errors;
                    return;
                }
                else {
                    Errors = new List<string>();
                }
            visitor.Visit(getContext);
          
            var p = this.Param.ToList();
            foreach (KeyValuePair<string, string> s in p)
            {
                a.Param.Add(s.Key, s.Value);
            }
            var varStates = visitor.DataCubes.Where(x => x.StatementType == DataStatement.VARIABLE
           || x.StatementType == DataStatement.PARAM);
            foreach (DataCube dc in varStates)
            {
                a.AddStatement(dc);
            }

            var preDC = visitor.DataCubes.Where(x => x.StatementType == DataStatement.IMP
            || x.StatementType == DataStatement.DR
            || x.StatementType == DataStatement.NS);
            foreach (DataCube dc in preDC)
            {
                a.AddPreStatement(dc);
            }
            a.Init(conn);
            var mainstate = visitor.DataCubes.Where(x => x.StatementType != DataStatement.VARIABLE
           && x.StatementType != DataStatement.PARAM);
            foreach (DataCube dc in visitor.DataCubes)
            {
                a.AddStatement(dc);
            }
                a.DataSchema[a.DataSchema.Count - 1].Data.Coalesce(1).Write().Mode(Microsoft.Spark.Sql.SaveMode.Overwrite).Json(outputPath);
                a.DataSchema.Clear();
                a.Stop();
                System.IO.File.WriteAllText(outputPath + @"/log.txt", "execution completed");
                //DirectorySecurity sec = Directory.GetAccessControl(outputPath);
                //// Using this instead of the "Everyone" string means we work on non-English systems.
                //SecurityIdentifier everyone = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
                //sec.AddAccessRule(new FileSystemAccessRule(everyone, FileSystemRights.Modify | FileSystemRights.Synchronize, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
                //Directory.SetAccessControl(outputPath, sec);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                a.Stop();
            }

        }

        public bool portOpened()
        {
            using (TcpClient tcpClient = new TcpClient())
            {
                try
                {
                    tcpClient.Connect("localhost", 5567);
                    // Console.WriteLine("Port open" + tcpClient.Connected);
                    return true;
                }
                catch (Exception ex)
                {
                    portOpened();
                }

            }
            return false;
        }
            

        public void ExecuteCommand(string exeDir, string args)
        {        
            using (TcpClient tcpClient = new TcpClient())
            {
                try
                {
                    tcpClient.Connect("localhost", 5567);
                    Console.WriteLine("Port open" + tcpClient.Connected);
                }
                catch (Exception eax)
                {
                    try
                    {
                        System.Diagnostics.ProcessStartInfo procStartInfo =
                        new System.Diagnostics.ProcessStartInfo("cmd", "/c " + args);
                        procStartInfo.RedirectStandardOutput = true;
                        procStartInfo.UseShellExecute = false;
                        procStartInfo.CreateNoWindow = true;
                        System.Diagnostics.Process proc = new System.Diagnostics.Process();
                        proc.StartInfo = procStartInfo;
                        proc.Start();
                        portOpened();

                        //Task t = Task.Run(() => {

                        //});
                        //TimeSpan ts = TimeSpan.FromMilliseconds(1250);
                        //if (!t.Wait(ts))
                        //    Console.WriteLine("The timeout interval elapsed.");

                        //System.Threading.

                        //   ExecuteCommand("", args);
                    }
                    catch (Exception ex)
                    {
                 
                    }
                }
            }



        }
        public void LoadScript(string path)
        {

        }
        public void SaveAsScript(string path)
        {

        }
        public void Execute(string name,
            string script,
            string conn,
            string scriptAssemblyPath,
            string outputPath)
        {
          Compile(name,script, conn, outputPath, scriptAssemblyPath);
        }
    }
   
}
