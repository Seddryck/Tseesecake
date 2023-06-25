SELECT
	$statement.Projections: {projection | $(projection.Template)()$}; separator="
, "$
FROM
	$statement.Timeseries.Name; format="identity"$
$if(statement.Filters)$
WHERE
	$statement.Filters: {filter | $(filter.Template)()$}; separator="
AND "$
$endif$
$if(statement.Slicers)$
GROUP BY
	$statement.Slicers: {slicer | $(slicer.Template)()$}; separator="
, "$
$endif$
$if(statement.GroupFilters)$
HAVING
	$statement.GroupFilters: {filter | $(filter.Template)()$}; separator="
, "$
$endif$
$if(statement.Orders)$
ORDER BY
	$statement.Orders: {order | $(order.Template)()$}; separator="
, "$
$endif$
$if(statement.Restriction)$
$(statement.Restriction.Template)()$
$endif$