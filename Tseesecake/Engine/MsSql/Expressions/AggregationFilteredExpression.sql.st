AggregationFilteredExpression(aggregation, filters)::=$\\$
$aggregations.(aggregation.Name)$(
	CASE 
		WHEN $projection.Filters: {filter | $filter:(filter.Template)()$}; separator=" AND "$
		THEN $aggregation.Expression:(aggregation.Expression.Template)()$
		ELSE NULL
	END
)