$windowFunctions.(projection.WindowFunction.Name)$($\\$
$if(projection.WindowFunction.Expression)$$\\$
$projection.WindowFunction.Expression:(projection.WindowFunction.Expression.Template)()$$\\$
$endif$
$if(projection.WindowFunction.Offset)$$\\$
, $projection.WindowFunction.Offset:(projection.WindowFunction.Offset.Template)()$$\\$
$endif$
$if(projection.WindowFunction.Default)$$\\$
, $projection.WindowFunction.Default:(projection.WindowFunction.Default.Template)()$$\\$
$endif$) OVER($\\$
$if(projection.PartitionBys)$
	PARTITION BY $projection.PartitionBys: {slicer | $slicer:(slicer.Template)()$}; separator=", "$
$endif$
$if(projection.OrderBys)$
	ORDER BY $projection.OrderBys: {order | $(order.Template)()$}; separator=", "$
$endif$
$if(projection.Frame)$
	$projection.Frame:(projection.Frame.Template)()$
$endif$
) AS $projection.Alias$