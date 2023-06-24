SELECT
	$statement.Projections: {projection | $projection:(projection.Template)()$}; separator="
, "$
FROM
	$statement.Timeseries.Name; format="identity"$
$if(statement.Filters)$
WHERE
	$statement.Filters: {filter | $filter:(filter.Template)()$}; separator="
AND "$
$endif$
$if(statement.Slicers)$
GROUP BY
	$statement.Slicers: {slicer | $slicer:(slicer.Template)()$}; separator="
, "$
$endif$
$if(statement.Orders)$
ORDER BY
	$statement.Orders: {order | $order:(order.Template)()$}; separator="
, "$
$endif$
$if(statement.Restriction)$
$statement.Restriction:(statement.Restriction.Template)()$
$endif$