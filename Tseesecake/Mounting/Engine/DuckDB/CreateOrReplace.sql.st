CreateOrReplace(ts)::=CREATE OR REPLACE TABLE $ts.Name; format="identity"$(
	$[ts.Timestamp, ts.Measurements, ts.Facets]: {column | $ColumnDefinition(column.Name, column.DbType)$}; separator="
, "$
);