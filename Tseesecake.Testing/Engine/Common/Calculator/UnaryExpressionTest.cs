using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Security.AccessControl;

namespace Tseesecake.Testing.Engine.Common.Calculator
{
    public class UnaryExpressionTest
    {
        [Test]
        public void Render_NegateParameter_CorrectRendering()
        {
            var engine = new TestableStringTemplateEngine("$value:(value.NodeType)()$");
            var parameters = new Dictionary<string, object?>()
            {
                { "value", Expression.Negate(Expression.Parameter(typeof(double), "Forecasted"))}
            };
            Assert.That(engine.Render(parameters), Is.EqualTo("-Forecasted"));
        }

        [Test]
        public void Render_NegateBinaryExpression_CorrectRendering()
        {
            var engine = new TestableStringTemplateEngine("$value:(value.NodeType)()$");
            var parameters = new Dictionary<string, object?>()
            {
                { "value", Expression.Negate(
                                Expression.Subtract(
                                    Expression.Parameter(typeof(double), "Forecasted"),
                                    Expression.Parameter(typeof(double), "Produced")
                                )
                            )}
            };
            Assert.That(engine.Render(parameters), Is.EqualTo("-(Forecasted - Produced)"));
        }
    }
}
