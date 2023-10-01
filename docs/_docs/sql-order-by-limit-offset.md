---
title: ORDER BY & LIMIT/OFFSET clauses
subtitle: Sort rows and retrieve a supart of them
tags: [sql-select]
---

`ORDER BY` and `LIMIT/OFFSET` are output modifiers. The `LIMIT` clause restricts the amount of rows fetched, and the `ORDER BY` clause sorts the rows on the sorting criteria in either ascending or descending order.

## ORDER BY clause

The `ORDER BY` clause accepts reference to the timestamp, the facets and the measurements as defined in the timeseries or as defined in the [`SELECT` clause](../sql-select-from).

```sql
SELECT
    WindPark,
    WindFarm,
    SUM(Produced) AS TotalProduced
FROM
    WindEnergy
ORDER BY
    WindPark, TotalProduced
```

You can also define the sorting of the value of the column as ascending (`ASC`) or descending (`DESC`). Numeric (meanseurements) and date/time (timestamp) values sorted ascendingly are ordered from the minimum to maximum value and textual values (facets) are ordered alphabetically. The opposite when sorted descendingly.

You can also specify if `NULL` values should be sorted first or last.

```sql
SELECT
    WindPark,
    WindFarm,
    SUM(Produced) AS TotalProduced
FROM
    WindEnergy
ORDER BY
    WindPark ASC NULLS LAST, TotalProduced DESC
```

## LIMIT/OFFSET clause

The `LIMIT` clause is used to specify the number of rows to return.

The `OFFSET` clause is optional. If you omit it, the query will return the amount of rows specified in the `LIMIT` from the first row returned by the `SELECT` clause. When specified, it stipulates how many roch should be skipped before returning them.

*Expl:* The following query returns the 4th and 5th rows ordered by TotalProduced

```sql
SELECT
    WindPark,
    WindFarm,
    SUM(Produced) AS TotalProduced
FROM
    WindEnergy
ORDER BY
    TotalProduced DESC
LIMIT 2
OFFSET 3 
```
