CreateOrReplace(ts)::=$\\$
CREATE OR REPLACE TABLE $ts.Name; format="identity"$(
	$[ts.Timestamp, ts.Facets, ts.Measurements]: {column | $ColumnDefinition(column.Name, column.DbType)$}; separator="
, "$
);