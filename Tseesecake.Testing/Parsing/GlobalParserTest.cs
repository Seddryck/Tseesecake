using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling;
using Tseesecake.Parsing;
using Tseesecake.Querying;

namespace Tseesecake.Testing.Parsing
{
    internal class GlobalParserTest
    {
        [Test]
        public virtual void Parse_MetaStatement_Valid()
        {
            var text = "SHOW TIMESERIES";
            var statement = GlobalParser.Global.Parse(text);
            Assert.That(statement, Is.Not.Null);
            Assert.That(statement, Is.AssignableTo<IShowStatement>());
        }

        [Test]
        public virtual void Parse_QueryStatement_Valid()
        {
            var text = "SELECT MAX(Forecasted) AS MaxValue FROM WindEnergy";
            var statement = GlobalParser.Global.Parse(text);
            Assert.That(statement, Is.Not.Null);
            Assert.That(statement, Is.AssignableTo<SelectStatement>());
        }
    }
}
