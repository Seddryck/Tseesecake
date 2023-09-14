$if(statement.Qualifiers)$
SELECT * FROM (
	$BaseQuery(statement)$
) AS T1
WHERE
	$statement.Qualifiers: {qualify | $qualify:(qualify.Template)()$}; separator="
AND "$
$else$
$BaseQuery(statement)$
$endif$
$if(statement.Orders)$
ORDER BY
	$statement.Orders: {order | $order:(order.Template)()$}; separator="
, "$
$endif$
$if(statement.Restriction)$
$statement.Restriction:(statement.Restriction.Template)()$
$endif$