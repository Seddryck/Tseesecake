---
title: SQL select statement
subtitle: Retrieve rows from the timeseries
tags: [sql-select]
---

The `SELECT` statement retrieves rows from the timeseries.

## Syntax

The canonical order of a `SELECT` statement is as follows:

```sql
SELECT select_list
FROM timeseries
WHERE condition
BUCKET BY time_groups
GROUP BY facet_groups
HAVING facet_groups_filter
WINDOW window_expr
ORDER BY order_expr
LIMIT n
OFFSET m;
```

Optionally, the `SELECT` statement can be prefixed with a `WITH` clause.
