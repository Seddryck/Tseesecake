$windowFunctions.(projection.WindowFunction.Name)$$\\$
($projection.WindowFunction.Column.Name$$\\$
$if(projection.WindowFunction.Offset)$$\\$
, $projection.WindowFunction.Offset$$\\$
$endif$
$if(projection.WindowFunction.Default)$$\\$
, $projection.WindowFunction.Default$$\\$
$endif$) OVER($\\$
$if(projection.PartitionBys)$
	PARTITION BY $projection.PartitionBys: {slicer | $(slicer.Template)()$}; separator=", "$
$endif$
$if(projection.OrderBys)$
	ORDER BY $projection.OrderBys: {order | $(order.Template)()$}; separator=", "$
$endif$
) AS $projection.Alias$