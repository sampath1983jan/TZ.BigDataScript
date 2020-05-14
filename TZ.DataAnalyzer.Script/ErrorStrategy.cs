using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.DataAnalyzer.Script
{
    public class MyErrorStrategy : Antlr4.Runtime.DefaultErrorStrategy
    {
        public MyErrorStrategy() : base()
        { }
        public override void ReportError(Parser recognizer, RecognitionException e)
        {
          
            base.ReportError(recognizer, e);
        }
      
        public override void Sync(Antlr4.Runtime.Parser recognizer)
        {
         //   if (recognizer.Context is Dict.TextAnalyzer.DictionaryParser.TextContext)
                base.Sync(recognizer);
        }
    }
}
