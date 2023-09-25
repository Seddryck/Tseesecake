ColumnOrder(order)::=$\\$
$if(order.IsNullSortOpposing)$
CASE WHEN $order.Expression:(order.Expression.Template)()$ IS NULL THEN 1 ELSE 0 END $sorts.(order.NullSort)$ $\\$
$else$
$order.Expression:(order.Expression.Template)()$ $sorts.(order.Sort)$$\\$
$endif$