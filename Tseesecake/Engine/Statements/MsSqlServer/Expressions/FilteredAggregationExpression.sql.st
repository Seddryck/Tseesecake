FilteredAggregationExpression(expression)::=$\\$
$aggregations.(expression.Aggregation.Name)$(
	CASE 
		WHEN $expression.Filters: {filter | $filter:(filter.Template)()$}; separator=" AND "$
		THEN $expression.Aggregation.Expression:(expression.Aggregation.Expression.Template)()$
		ELSE NULL
	END
)