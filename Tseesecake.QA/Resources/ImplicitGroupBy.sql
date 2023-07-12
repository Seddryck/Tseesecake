SELECT
	Producer,
	MAX(Forecasted) AS Maximum
FROM
	WindEnergy
BUCKET BY
	WEEK OF YEAR