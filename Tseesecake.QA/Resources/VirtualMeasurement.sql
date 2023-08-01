WITH MEASUREMENT Accuracy AS (
	Forecasted - Produced
)

SELECT
	WindPark,
	MIN(Accuracy) AS MinAccuracy
FROM
	WindEnergy
ORDER BY
	MinAccuracy DESC