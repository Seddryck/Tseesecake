WindowExpression(expression)::=$\\$
$windowFunctions.(expression.WindowFunction.Name)$$aggregations.(expression.WindowFunction.Name)$($\\$
$if(expression.WindowFunction.Expression)$$\\$
$expression.WindowFunction.Expression:(expression.WindowFunction.Expression.Template)()$$\\$
$endif$
$if(expression.WindowFunction.Offset)$$\\$
, $expression.WindowFunction.Offset:(expression.WindowFunction.Offset.Template)()$$\\$
$endif$
$if(expression.WindowFunction.Default)$$\\$
, $expression.WindowFunction.Default:(expression.WindowFunction.Default.Template)()$$\\$
$endif$) OVER$\\$
$if(expression.Window.Name)$ $expression.Window.Name$$else$$\\$
($expression.Window:WindowDefinition()$)$\\$
$endif$