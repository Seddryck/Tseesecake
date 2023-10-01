---
title: SQL Windows
subtitle: Retrieve rows from the database
tags: [sql-select]
---

Windowing works by breaking a timeseries up into independent partitions, ordering those partitions, and then computing a new column for each row as a function of the nearby values.

Window functions are expressions, stipulated in the [`SELECT` clause](../sql-select-from), an alias is mandatory for each of them.

## Window functions

The following functions are implemented

- `ROW_NUMBER`: returns the **position** of the current row within the partition, depending on the `ORDER BY` clause, counting from 1
- `RANK`: returns the position of the current row within the partition, counting from 1 but if two rows cannot be distinguished by the order by clause, they will receive the same rank and will **leave gaps** after two or more rows with the same rank.
- `DENSE_RANK`: returns the same value than `RANK` but will always generate a **contiguous sequence** of ranks like (1,2,3,...) and will have no gaps following rows with the same rank.
- `FIRST`: returns the value of the **first** row in the window
- `LAST`: returns the value of the **last** row in the window
- `LAG`: returns an expression evaluated at the row that is offset rows **before** the current row within the partition; if there is no such row, instead it returns the default value. Both offset and default are evaluated with respect to the current row. If omitted, offset defaults to 1 and default to null.
- `LEAD`: returns an expression evaluated at the row that is offset rows **after** the current row within the partition; if there is no such row, instead it returns the default value. Both offset and default are evaluated with respect to the current row. If omitted, offset defaults to 1 and default to null.

Each function accept an argument which must be a reference to a measurement, except for `ROW_NUMBER`, `RANK`, `DENSE_RANK` which accept no argument.

Window functions don't skip `NULL` rows.

`LAG` and `LEAD` accept two additional arguments. The first specifies the *offset* of the row considered and default to 1. The second arguments specifies the value to be returned if the offset row doesn't exist. The default value is `NULL`.

On top of these functions, all [aggregation functions](../sql-aggregations#aggregate-functions) can also used in a window expression.

## WINDOW clause

A window is a set of ordered rows in a timeseries. Each row belongs to a single set of ordered rows. To define a window, you'll use the `PARTITION BY` and `ORDER BY` concepts.

Partitioning breaks the relation up into independent, unrelated pieces. Partitioning is optional, and if none is specified then the entire timeseries is treated as a single partition. Window functions cannot access values outside of the partition containing the row they are being evaluated at.

Ordering is also optional, but without it the results are not well-defined. Each partition is ordered using the same ordering clause.

Partitions and orders are defined after the `OVER` keyword.

Expl:

```sql
SELECT
    RANK(Produced) OVER(
        PARTITION BY WindPark 
        ORDER BY Produced DESC
    ) AS Position,
    Instant,
    WindPark,
    Produced,
FROM
    WindEnergy
```

## Frame

Framing specifies a set of rows relative to each row where the function is evaluated. The distance from the current row is given as an expression either `PRECEDING` or `FOLLOWING` the current row. This distance is specified as an integral number of ROWS.

```sql
SELECT
    AVG(Produced) OVER(
        PARTITION BY WindPark
        ORDER BY Instant 
        ROWS BETWEEN 7 PRECEDING AND 3 FOLLOWING
    ) AS RollingAverage,
    Instant,
    WindPark,
    Produced,
FROM
    WindEnergy
```

`CURRENT ROW` allows you to define one of the boudaries as the row currentl evaluated. `UNBOUNDED PRECEDING` defines that that frame always starts at the first row of the window and `UNBOUNDED FOLLOWING` applies the same concept to specify that the frame always ends at the last row of the window.

## Named window

Multiple different `OVER` clauses can be specified in the same `SELECT`, and each will be computed separately. Often, however, we want to use the same layout for multiple window functions. The `WINDOW` clause can be used to define a named window that can be shared between multiple window functions.

```sql
SELECT
    AVG(Produced) OVER TenValues AS AvgTenValues,
    MIN(Produced) OVER TenValues AS AvgMinValues,
    MAX(Produced) OVER TenValues AS AvgMaxValues,
    Instant,
    WindPark,
    Produced,
FROM
    WindEnergy
WINDOW
    TenValues AS (
        PARTITION BY WindPark
        ORDER BY Instant 
        ROWS BETWEEN 7 PRECEDING AND 3 FOLLOWING
    ) AS RollingAverage
```
