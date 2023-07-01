WindowDefinition(window)::=$\\$
$if(window.PartitionBys)$
	PARTITION BY $window.PartitionBys: {slicer | $slicer:(slicer.Template)()$}; separator=", "$
$endif$
$if(window.OrderBys)$
	ORDER BY $window.OrderBys: {order | $order:(order.Template)()$}; separator=", "$
$endif$
$if(window.Frame)$
	$window.Frame:(window.Frame.Template)()$
$endif$