using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TZ.DataAnalyzer.Script;
using static AScriptParser;

namespace TZ.DataAnalyzer.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                const string dataFmt = "{0,-30}{1}";
                const string timeFmt = "{0,-30}{1:MM-dd-yyyy HH:mm}";
                TimeZone curTimeZone = TimeZone.CurrentTimeZone;
                // What is TimeZone name?  
                System.Console.WriteLine(dataFmt, "TimeZone Name:", curTimeZone.StandardName);
                // Is TimeZone DayLight Saving?|  
                System.Console.WriteLine(dataFmt, "Daylight saving time?", curTimeZone.IsDaylightSavingTime(DateTime.Now));
                // What is GMT (also called Coordinated Universal Time (UTC)  
                DateTime curUTC = curTimeZone.ToUniversalTime(DateTime.Now);
                System.Console.WriteLine(timeFmt, "Coordinated Universal Time:", curUTC);
                // What is GMT/UTC offset ?  
                TimeSpan currentOffset = curTimeZone.GetUtcOffset(DateTime.Now);
                System.Console.WriteLine(dataFmt, "UTC offset:", currentOffset);
                // Get DaylightTime object  
                System.Globalization.DaylightTime dl = curTimeZone.GetDaylightChanges(DateTime.Now.Year);
                // DateTime when the daylight-saving period begins.  
                System.Console.WriteLine("Start: {0:MM-dd-yyyy HH:mm} ", dl.Start);
                // DateTime when the daylight-saving period ends.  
                System.Console.WriteLine("End: {0:MM-dd-yyyy HH:mm} ", dl.End);
                // Difference between standard time and the daylight-saving time.  
                System.Console.WriteLine("delta: {0}", dl.Delta);
                System. Console.Read();

                //string input = "";
                //bool state = true;
                //var value = Environment.GetEnvironmentVariable("DOTNETBACKEND_PORT");
                //// If necessary, create it.
                //if (value == null)
                //{
                //    Environment.SetEnvironmentVariable("DOTNETBACKEND_PORT", "5567");


                //    // Now retrieve it.
                //    value = Environment.GetEnvironmentVariable("DOTNETBACKEND_PORT");
                //}
                //Environment.SetEnvironmentVariable("DOTNET_ASSEMBLY_SEARCH_PATHS", @"C:\WorkingFolder-Custom\TZ.BigDataScript\TZ.DataAnalyzer.Console\bin\Debug");
                //while (state)
                //{
                //    System.System.Console.WriteLine("Enter Query.");
                //    input = System.Console.ReadLine();
                //    AScript script = new AScript();
                //    script.Compile(input,"");

                //    //AntlrInputStream inputStream = new AntlrInputStream(input);
                //    //AScriptLexer exprL = new AScriptLexer(inputStream);
                //    //CommonTokenStream commonTokenStream = new CommonTokenStream(exprL);
                //    //AScriptParser expPar = new AScriptParser(commonTokenStream);

                //    //// exprParser.GroupdataContext getContext = expPar.groupdata();
                //    //AScriptParser.StatementContext getContext = expPar.statement();
                //    //AScriptVisitor visitor = new AScriptVisitor();
                //    //visitor.Visit(getContext);

                //    //foreach (var line in visitor.Lines)
                //    //{
                //    //    System.System.Console.WriteLine("{0   } has said {1}  ", line.Person, line.Text);
                //    //}
                //    //// var item= System.Console.ReadLine();
                //    //if (input == "exist")
                //    //{
                //    //    state = false;
                //    //}
                //    //else
                //    //{
                //    //    state = true;
                //    //}
                //}
            }
            catch (Exception ex)
            {

            }
            finally
            {
                System.Console.ReadLine();
            }



        }
    }
}


 