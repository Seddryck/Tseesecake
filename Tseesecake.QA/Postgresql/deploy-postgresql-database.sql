DROP DATABASE IF EXISTS "WindEnergy";
CREATE DATABASE "Energy";

\connect "Energy"

CREATE TABLE "WindEnergy" (
    "Instant" TIMESTAMP NOT NULL,
    "WindPark" VARCHAR(50),
    "Producer" VARCHAR(50),
	"Forecasted" DECIMAL(9,6),
	"Produced" DECIMAL(9,6)
);