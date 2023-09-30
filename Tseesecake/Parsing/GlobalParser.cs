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
    public class GlobalParser
    {
        private IList<Parser<IStatement>> Parsers { get; } = new List<Parser<IStatement>>();

        public GlobalParser()
        {
            Parsers = new List<Parser<IStatement>>()
            {
                TimeseriesMetaParser.Show,
                SelectStatementParser.Query
            };
        }

        public void Add(Parser<IStatement> parser)
            => Parsers.Add(parser);

        public Parser<IStatement> Global
        {
            get
            {
                Parser<IStatement>? global = null;
                foreach (var parser in Parsers) 
                    global = global is null ? parser : parser.Or(global);
                
                return global is not null ? global: throw new InvalidOperationException() ;
            }
        }
    }
}
