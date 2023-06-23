using DubUrl.Querying.Parametrizing;
using DubUrl.Querying.Templating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Querying;

namespace Tseesecake.Engine
{
    public class BasicQuery : EmbeddedSqlTemplateCommand
    {
        public BasicQuery(SelectStatement statement)
            : base(
                   //, $"{typeof(BasicQuery).GetType().Assembly.GetName().Name}.{nameof(BasicQuery)}"
                   "Tseesecake.Engine.BasicQuery"
                  , "Tseesecake.Engine"
                  , "Tseesecake.Engine"
                  , new Dictionary<string, object?>() { { "statement", statement } }
            )
        { }
    }
}
