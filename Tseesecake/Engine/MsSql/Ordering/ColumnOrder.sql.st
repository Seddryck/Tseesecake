ColumnOrder(order)::=$if(order.IsNullSortOpposing)$
CASE WHEN $order.Reference.Name; format="identity"$ IS NULL THEN 1 ELSE 0 END $sorts.(order.NullSort)$ 
, $\\$
$endif$
$order.Reference.Name; format="identity"$ $sorts.(order.Sort)$