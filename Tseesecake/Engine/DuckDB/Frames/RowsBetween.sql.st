RowsBetween(frame)::=$\\$
ROWS BETWEEN $if(frame.Lower.Value)$$frame.Lower.Value:(frame.Lower.Value.Template)()$ $endif$$framePosition.(frame.Lower.Position)$
	AND $if(frame.Upper.Value)$$frame.Upper.Value:(frame.Upper.Value.Template)()$ $endif$$framePosition.(frame.Upper.Position)$