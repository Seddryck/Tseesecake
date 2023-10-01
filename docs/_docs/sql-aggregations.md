---
title: SQL Aggregations
subtitle: Retrieve rows from the database
tags: [sql-select]
---

A SQL aggregation is the transformation of a set of values to return a single value. Aggragate only applies to measurements. It is done with the help of aggregate functions and optionally a filter.

Aggregations are expressions, stipulated in the [`SELECT` clause](../sql-select-from), an alias is mandatory for each of them.

## Aggregate functions

The following functions are implemented

- `MAX`: returns the largest value of a measurement
- `MIN`: returns the smallest value of a measurement
- `AVG`: returns the average value of a measurement
- `MEDIAN`: returns the value in the middle of a set of values, meaning that 50% of data points have a value smaller or equal to the median and 50% of data points have a value larger or equal to the median
- `SUM`: returns the sum of all values of a measurement
- `PRODUCT`: returns the product of all values of a measurement
- `COUNT`: returns the number of non-null values for a measurement, a facet or a timestamp.

Each function accept an argument which must be a reference to a measurement, except for `COUNT` which also accepts reference to a facet or a timestamp or even a reference to a full row (`*`).

`NULL` values are never taken into account into the aggregation functions. It means that athe average of values `3`, `5` and `NULL` is `4` (which calculated as (3+5)/2).

## Filter clause

The `FILTER` clause may optionally follow an aggregate function in a `SELECT` statement. This will filter the rows of data that are fed into the aggregate function in the same way that a `WHERE` clause filters rows, but localized to the specific aggregate function.

Expl: the following query aggregates all rows (expect the ones from the WindPark named *Wind in the Sky* defined in the `WHERE` clause) and returns two values. The first one is the average of all rows where the production is less than 5 and the second one where the production is more than 25.

```sql
SELECT
    AVG(Produced) FILTER(WHERE Produced < 5) AS AvgSmallProduction,
    AVG(Produced) FILTER(WHERE Produced > 25) AS AvgLargeProduction,
FROM
    WindEnergy
WHERE
    WindPark IS NOT 'Wind in the Sky'
```
