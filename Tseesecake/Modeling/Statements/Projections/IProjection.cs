using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Statements.Expressions;

namespace Tseesecake.Modeling.Statements.Projections
{
    public interface IProjection
    {
        string Alias { get; }
        IExpression Expression { get; set; }
    }
}
