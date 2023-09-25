using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements;
using Tseesecake.Parsing.Catalog;
using Tseesecake.Parsing.Select;

namespace Tseesecake.Parsing
{
    internal class GlobalParser
    {
        public readonly static Parser<IStatement> Global = 
            TimeseriesMetaParser.Show
            .Or((Parser<IStatement>)SelectStatementParser.Query);
    }
}
