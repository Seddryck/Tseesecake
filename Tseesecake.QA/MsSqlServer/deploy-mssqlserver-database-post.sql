USE Energy;  
GO

SET DATEFORMAT dmy; 
INSERT INTO 
	WindEnergy 
SELECT 
	TRY_CONVERT(DateTime, Instant)
	, WindPark
	, Producer
	, TRY_CONVERT(Decimal(9,6), Forecasted)
	, TRY_CONVERT(Decimal(9,6), Produced) 
FROM
	WindEnergyStg;

DROP TABLE
	WindEnergyStg;
GO