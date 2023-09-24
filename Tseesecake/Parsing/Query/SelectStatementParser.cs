using Sprache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tseesecake.Modeling.Catalog;
using Tseesecake.Modeling.Statements;
using Tseesecake.Modeling.Statements.Filters;
using Tseesecake.Modeling.Statements.Ordering;
using Tseesecake.Modeling.Statements.Slicers;

namespace Tseesecake.Parsing.Query
{
    internal class SelectStatementParser
    {
        protected internal readonly static Parser<TimeseriesReference> TimeseriesReference =
            from identifier in Grammar.Identifier
            select new TimeseriesReference(identifier);

        public readonly static Parser<IFilter[]> Filters =
            from @where in Keyword.Where
            from filters in FilterParser.Filter.DelimitedBy(Parse.IgnoreCase("AND"))
            select filters.ToArray();

        protected internal readonly static Parser<ISlicer[]> FacetSlicers =
            from keyword in Keyword.GroupBy
            from slicers in SlicerParser.Facet.DelimitedBy(Parse.Char(','))
            select slicers.ToArray();

        protected internal readonly static Parser<ISlicer[]> TemporalSlicers =
            from slicer in TemporalParser.Slicer.Optional()
            select slicer.IsDefined ? new ISlicer[] { slicer.Get() } : Array.Empty<ISlicer>();

        public readonly static Parser<ISlicer[]> Slicers =
            from temporals in TemporalSlicers.Optional()
            from facets in FacetSlicers.Optional()
            select facets.GetOrElse(Array.Empty<ISlicer>()).Concat(temporals.GetOrElse(Array.Empty<ISlicer>())).ToArray();

        public readonly static Parser<IOrderBy[]> OrderBys =
            from keyword in Keyword.OrderBy
            from orderBys in OrderByParser.OrderBy
            select orderBys.ToArray();

        protected internal readonly static Parser<VirtualMeasurement[]> MeasurementExpressions =
            from expressions in VirtualMeasurementParser.VirtualMeasurement.DelimitedBy(Parse.Char(','))
            select expressions.ToArray();

        public readonly static Parser<SelectStatement> Query =
            from measurementExpressions in MeasurementExpressions.Optional()
            from @select in Keyword.Select
            from projections in ProjectionParser.Projection.DelimitedBy(Parse.Char(','))
            from @from in Keyword.From
            from ts in TimeseriesReference
            from filters in Filters.Optional()
            from slicers in Slicers.Optional()
            from orderBys in OrderBys.Optional()
            from restriction in RestrictionParser.Restriction.Optional()
            select new SelectStatement(ts, projections.ToArray(), filters.GetOrElse(null), slicers.GetOrElse(null), null, null, null, orderBys.GetOrElse(null), restriction.GetOrElse(null), measurementExpressions.GetOrElse(null));
    }
}
