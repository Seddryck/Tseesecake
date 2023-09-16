AggregationProjection(projection)::=$\\$
$projection.Aggregation:AggregationExpression()$ $\\$
$if(projection.Filters)$$\\$
FILTER (WHERE $projection.Filters: {filter | $filter:(filter.Template)()$}; separator=" AND "$) $\\$
$endif$
AS $projection.Alias; format="identity"$