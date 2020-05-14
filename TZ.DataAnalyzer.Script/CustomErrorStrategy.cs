using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.DataAnalyzer.Script
{
   public class CustomErrorStrategy : IAntlrErrorStrategy
    {
        public bool InErrorRecoveryMode(Parser recognizer)
        {
            throw new NotImplementedException();
        }

        public void Recover(Parser recognizer, RecognitionException e)
        {
           // throw new NotImplementedException();
        }

        [return: NotNull]
        public IToken RecoverInline(Parser recognizer)
        {
            throw new NotImplementedException();
        }

        public void ReportError(Parser recognizer, RecognitionException e)
        {
         //   throw new NotImplementedException();
        }

        public void ReportMatch(Parser recognizer)
        {
            throw new NotImplementedException();
        }

        public void Reset(Parser recognizer)
        {
            throw new NotImplementedException();
        }

        public void Sync(Parser recognizer)
        {
           // base.Sync(recognizer);
        }
    }
}
