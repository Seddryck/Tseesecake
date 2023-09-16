using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Security.AccessControl;

namespace Tseesecake.Testing.Engine.Common.Calculator
{
    public class BinaryExpressionTest
    {
        [Test]
        public void Render_AddConstants_CorrectRendering()
        {
            var engine = new TestableStringTemplateEngine("$value:(value.NodeType)()$");
            var parameters = new Dictionary<string, object?>()
            {
                { "value", Expression.Add(Expression.Constant(5),Expression.Constant(10)) }
            };
            Assert.That(engine.Render(parameters), Is.EqualTo("(5 + 10)"));
        }

        [Test]
        public void Render_AddCheckedConstants_CorrectRendering()
        {
            var engine = new TestableStringTemplateEngine("$value:(value.NodeType)()$");
            var parameters = new Dictionary<string, object?>()
            {
                { "value", Expression.AddChecked(Expression.Constant(5),Expression.Constant(10)) }
            };
            Assert.That(engine.Render(parameters), Is.EqualTo("(5 + 10)"));
        }

        [Test]
        public void Render_ModuloConstants_CorrectRendering()
        {
            var engine = new TestableStringTemplateEngine("$value:(value.NodeType)()$");
            var parameters = new Dictionary<string, object?>()
            {
                { "value", Expression.Modulo(Expression.Constant(5),Expression.Constant(10)) }
            };
            Assert.That(engine.Render(parameters), Is.EqualTo("(5 % 10)"));
        }

        [Test]
        public void Render_ComplexConstant_CorrectRendering()
        {
            var engine = new TestableStringTemplateEngine("$value:(value.NodeType)()$");
            var parameters = new Dictionary<string, object?>()
            {
                { "value", Expression.Add(Expression.Constant(5),Expression.Multiply(Expression.Constant(2),Expression.Constant(3))) }
            };
            Assert.That(engine.Render(parameters), Is.EqualTo("(5 + (2 * 3))"));
        }

        [Test]
        public void Render_SimpleParameter_CorrectRendering()
        {
            var engine = new TestableStringTemplateEngine("$value:(value.NodeType)()$");
            var parameters = new Dictionary<string, object?>()
            {
                { "value", Expression.Subtract(Expression.Parameter(typeof(double), "Forecasted"), Expression.Parameter(typeof(double), "Produced")) }
            };
            Assert.That(engine.Render(parameters), Is.EqualTo("(Forecasted - Produced)"));
        }

        [Test]
        public void Render_MixParameterConstant_CorrectRendering()
        {
            var engine = new TestableStringTemplateEngine("$value:(value.NodeType)()$");
            var parameters = new Dictionary<string, object?>()
            {
                { "value", Expression.Subtract(
                    Expression.Divide(Expression.Parameter(typeof(double), "Forecasted"), Expression.Constant(2.0))
                    , Expression.Multiply(Expression.Parameter(typeof(double), "Produced"), Expression.Constant(1000.0))) }
            };
            Assert.That(engine.Render(parameters), Is.EqualTo("((Forecasted / 2) - (Produced * 1000))"));
        }

        [Test]
        public void Render_BinaryAndCall_CorrectRendering()
        {
            var engine = new TestableStringTemplateEngine("$value:(value.NodeType)()$");
            var parameters = new Dictionary<string, object?>()
            {
                { "value", Expression.Divide(
                    Expression.Power(Expression.Parameter(typeof(double), "Forecasted"), Expression.Constant(2.0))
                    , Expression.Subtract(
                        Expression.Divide(Expression.Parameter(typeof(double), "Forecasted"), Expression.Constant(2.0))
                        , Expression.Multiply(Expression.Parameter(typeof(double), "Produced"), Expression.Constant(1000.0))))}
            };
            Assert.That(engine.Render(parameters), Is.EqualTo("(POW(Forecasted, 2) / ((Forecasted / 2) - (Produced * 1000)))"));
        }
    }
}
