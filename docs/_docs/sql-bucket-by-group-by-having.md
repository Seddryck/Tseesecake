---
title: BUCKET BY & GROUP BY clauses
subtitle: Group rows
tags: [sql-select]
---

## BUCKET BY clause

The `BUCKET BY` clause specifies how to group the rows based on the time horizon. Two sets of options are available or the truncations removing a part of the timestamp but also cyclics taking a specific part of the timestamp.

### Truncation

Trnucate the timestamp to a specified precision:

- `SECOND`
- `MINUTE`
- `HOUR`
- `DAY`
- `MONTH`
- `QUARTER`
- `YEAR`

```sql
SELECT
    Instant,
    SUM(Produced) AS TotalProduced
FROM
    WindEnergy
BUCKET BY
    MONTH
```

### Cyclic

Get a subfield of the timestamp:

- `DAY OF WEEK`
- `DAY OF MONTH`
- `DAY OF YEAR`
- `WEEK OF YEAR`
- `MONTH OF YEAR`
- `QUARTER OF YEAR`

```sql
SELECT
    Instant,
    SUM(Produced) AS TotalProduced
FROM
    WindEnergy
BUCKET BY
    MONTH OF YEAR
```

## GROUP BY clause

The `GROUP BY` clause specifies which grouping facets should be used to perform any aggregations in the SELECT clause.

In this dialect the `GROUP BY` clause is facultative. Indeed all the facets defined in the `SELECT` clause are automatically added to the `GROUP BY` clause. The following two querues are equivalent.

```sql
SELECT
    WindPark,
    SUM(Produced) AS TotalProduced
FROM
    WindEnergy
GROUP BY
    WindPark
```

and

```sql
SELECT
    WindPark,
    SUM(Produced) AS TotalProduced
FROM
    WindEnergy
```
