CopyFrom(ts)::=$\\$
SET DateStyle TO euro;

COPY
	$ts.Name; format="identity"$
FROM
	$ts.File.Path; format="value"$
WITH (
	FORMAT $ts.File.Extension$
	$ts.File.Options: {option |, $option$ $ts.File.Options.(option); format="value"$}; separator="
"$
);