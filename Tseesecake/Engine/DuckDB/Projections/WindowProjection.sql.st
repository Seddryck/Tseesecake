$windowFunctions.(projection.WindowFunction.Name)$$\\$
($projection.WindowFunction.Expression : {expression | $(expression.Template)()$}$$\\$
$if(projection.WindowFunction.Offset)$$\\$
, $projection.WindowFunction.Offset : {expression | $(expression.Template)()$}$$\\$
$endif$
$if(projection.WindowFunction.Default)$$\\$
, $projection.WindowFunction.Default : {expression | $(expression.Template)()$}$$\\$
$endif$) OVER($\\$
$if(projection.PartitionBys)$
	PARTITION BY $projection.PartitionBys: {slicer | $(slicer.Template)()$}; separator=", "$
$endif$
$if(projection.OrderBys)$
	ORDER BY $projection.OrderBys: {order | $(order.Template)()$}; separator=", "$
$endif$
) AS $projection.Alias$