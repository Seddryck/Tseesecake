WindowProjection(projection)::=$windowFunctions.(projection.WindowFunction.Name)$$aggregations.(projection.WindowFunction.Name)$($\\$
$if(projection.WindowFunction.Expression)$$\\$
$projection.WindowFunction.Expression:(projection.WindowFunction.Expression.Template)()$$\\$
$endif$
$if(projection.WindowFunction.Offset)$$\\$
, $projection.WindowFunction.Offset:(projection.WindowFunction.Offset.Template)()$$\\$
$endif$
$if(projection.WindowFunction.Default)$$\\$
, $projection.WindowFunction.Default:(projection.WindowFunction.Default.Template)()$$\\$
$endif$) OVER$\\$
$if(projection.Reference)$ $projection.Reference.Name$ $else$$\\$
($projection:WindowDefinition()$) $\\$
$endif$
AS $projection.Alias; format="identity"$