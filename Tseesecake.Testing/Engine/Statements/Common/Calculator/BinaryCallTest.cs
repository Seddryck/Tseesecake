using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Security.AccessControl;

namespace Tseesecake.Testing.Engine.Statements.Common.Calculator
{
    public class BinaryCallTest
    {
        [Test]
        public void Render_Power_CorrectRendering()
        {
            var engine = new TestableStringTemplateEngine("$value:(value.NodeType)()$");
            var parameters = new Dictionary<string, object?>()
            {
                { "value", Expression.Power(Expression.Parameter(typeof(double), "Forecasted"), Expression.Constant(2.0))}
            };
            Assert.That(engine.Render(parameters), Is.EqualTo("POW(Forecasted, 2)"));
        }
    }
}
