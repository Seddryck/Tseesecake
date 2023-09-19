AggregationProjection(projection)::=$\\$
$if(projection.Filters)$
$projection.Aggregation:AggregationFilteredExpression(projection.Filters)$ $\\$
$else$
$projection.Aggregation:AggregationExpression()$ $\\$
$endif$
AS $projection.Alias; format="identity"$