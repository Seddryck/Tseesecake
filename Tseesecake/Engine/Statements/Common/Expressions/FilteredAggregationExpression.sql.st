FilteredAggregationExpression(expression)::=$\\$
$expression:AggregationExpression()$ $\\$
FILTER (WHERE $\\$
$expression.Filters: {filter | $filter:(filter.Template)()$}; separator=" AND "$$\\$
)