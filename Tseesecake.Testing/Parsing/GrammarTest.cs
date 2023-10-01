using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Parsing;

namespace Tseesecake.Testing.Parsing
{
    public class GrammarTest
    {
        [Test]
        [TestCase("f")]
        [TestCase("F")]
        [TestCase("foo")]
        [TestCase("FooBar")]
        [TestCase("FooBar123")]
        [TestCase("Foo_Bar")]
        [TestCase("_bar")]
        [TestCase("\"foo\"")]
        [TestCase("\"_foo123\"")]
        public void Identifier_ValidIdentifier_Successful(string id)
            => Assert.That(Grammar.Identifier.Parse(id), Is.EqualTo(id.Replace("\"", "")));

        [Test]
        [TestCase("1")]
        [TestCase("1F")]
        public void Identifier_NotValidIdentifier_Throws(string id)
            => Assert.Throws<ParseException>(() => Grammar.Identifier.Parse(id));

        [Test]
        [TestCase("foo.bar")]
        [TestCase("foo-bar")]
        [TestCase("foo,bar")]
        public void Identifier_NotValidIdentifier_NotSuccessful(string id)
            => Assert.That(Grammar.Identifier.Parse(id), Is.Not.EqualTo(id));
    }
}
