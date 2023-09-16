using DubUrl.Querying;
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
    public class ElementalQuery : EmbeddedSqlTemplateCommand
    {
        internal ElementalQuery(SelectStatement statement)
            : this(
                  statement
                  , NullQueryLogger.Instance
            )
        { }

        public ElementalQuery(SelectStatement statement, IQueryLogger logger)
            : base(
                   $"{typeof(ElementalQuery).Namespace}.{nameof(ElementalQuery)}"
                  , $"{typeof(ElementalQuery).Namespace}"
                  , $"{typeof(ElementalQuery).Namespace}"
                  , new Dictionary<string, object?>() { { "statement", statement } }
                  , logger
            )
        { }
    }
}