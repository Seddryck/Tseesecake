using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements;
using Tseesecake.Parsing;

namespace Tseesecake.Testing.Parsing
{
    internal class GlobalParserTest
    {
        [Test]
        public virtual void Parse_MetaStatement_Valid()
        {
            var text = "SHOW TIMESERIES";
            var statement = new GlobalParser().Global.Parse(text);
            Assert.That(statement, Is.Not.Null);
            Assert.That(statement, Is.AssignableTo<IShowStatement>());
        }

        [Test]
        public virtual void Parse_QueryStatement_Valid()
        {
            var text = "SELECT MAX(Forecasted) AS MaxValue FROM WindEnergy";
            var statement = new GlobalParser().Global.Parse(text);
            Assert.That(statement, Is.Not.Null);
            Assert.That(statement, Is.AssignableTo<SelectStatement>());
        }

        private interface IFooBarStatement : IStatement { }
        private class FooBarStatement : IFooBarStatement { }
        private class FooBarParser
        {
            public readonly static Parser<IFooBarStatement> FooBar = 
                Parse.IgnoreCase("FooBar").Text().Token().Return(new FooBarStatement());
        }

        [Test]
        public virtual void Add_NewParser_CanParse()
        {
            var text = "FOOBAR";
            var parser = new GlobalParser();
            Assert.Throws<ParseException>(() => parser.Global.Parse(text));
            
            parser.Add(FooBarParser.FooBar);
            var statement = parser.Global.Parse(text);
            Assert.That(statement, Is.Not.Null);
            Assert.That(statement, Is.TypeOf<FooBarStatement>());
        }
    }
}
