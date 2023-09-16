CreateOrReplace(ts)::=$\\$
DROP TABLE IF EXISTS $ts.Name; format="identity"$;
CREATE TABLE $ts.Name; format="identity"$(
	$[ts.Timestamp, ts.Facets, ts.Measurements]: {column | $ColumnDefinition(column.Name, column.DbType)$}; separator="
, "$
);