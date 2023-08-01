WITH MEASUREMENT Accuracy AS (
	Forecasted - Produced
)

SELECT
	Accuracy
FROM
	WindEnergy
ORDER BY
	Accuracy DESC