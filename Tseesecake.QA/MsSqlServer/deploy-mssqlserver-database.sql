USE master;  
GO

IF EXISTS (SELECT * FROM sys.databases WHERE name = 'EnergyMount')
BEGIN
    DROP DATABASE EnergyMount;  
END
GO

CREATE DATABASE EnergyMount;  
GO

IF EXISTS (SELECT * FROM sys.databases WHERE name = 'Energy')
BEGIN
    DROP DATABASE Energy;  
END
GO

CREATE DATABASE Energy;  
GO

USE Energy;
GO

CREATE TABLE [WindEnergy] (
    [Instant] DATETIME NOT NULL,
    [WindPark] VARCHAR(50),
    [Producer] VARCHAR(50),
	[Forecasted] DECIMAL(9,6),
	[Produced] DECIMAL(9,6)
);
GO

CREATE TABLE [WindEnergyStg] (
    [Instant] VARCHAR(50) NOT NULL,
    [WindPark] VARCHAR(50),
    [Producer] VARCHAR(50),
	[Forecasted] VARCHAR(50),
	[Produced] VARCHAR(50)
);
GO