---
title: SELECT & FROM clauses
subtitle: Retrieve rows from the database
tags: [sql-select, quick-start]
---

## SELECT clause

The `SELECT` clause specifies the list of columns that will be returned by the query. The `SELECT` clause can contain arbitrary expressions, as well as aggregates and window functions.

The `SELECT` expressions can be:

- a reference to a facet, timestamp or measurement
- a constant value
- a literal expression.
- an [aggregation](../sql-aggregations) expression
- a [window](../sql-windows) expression
- a reference to a [virtual measurement](../sql-virtual-measurement)

Except the references, all the expressions must received an alias with the keyword `AS`. Each alias must be unique.

## FROM clause

The `FROM` clause specifies the time series on which the remainder of the query should operate. The `FROM` clause can contain a single time series.
