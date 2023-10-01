---
title: WHERE Clause
subtitle: Filter rows
tags: [sql-select, quick-start]
---

## WHERE Clause

The `WHERE` clause specifies any filters to apply to the data. This allows you to select only a subset of the data in which you are interested. The `WHERE` clause can contained multiple criterions but all of them will be combined with an `AND` operator.

All criterions compare a facet or a measure or the timestamp, left part of the expression, to a reference value which is always the right part of the expression.

### Dicers

The following dicers can be applied to any facet:

- Equal, represented by the keyword `IS`. It validates if the value of the facet is equal to the reference value.
- Different, represented by the keyword `IS NOT`. It validates if the value of the facet is **not** equal to the reference value.
- Array, represented by the keyword `IN` followed by a list of reference values contained between parenthesis and separated by commas. It validates if the value of the facet is equal to any of the reference values.

Expl:

```sql
WHERE 
    WindPark IS 'Wind parks in the Sky'
    AND Producers IN ('Green Power Inc.', 'Future Energy')
```

### Sifters

The following sifters can be applied to any measurement:

For all sifters the value of the measurement is compared to the reference value, with the help of an operator.

- `Gatherer` is keeping the row, if the result of the comparison is positive.
- `Culler`, is the negation of a gatherer.

The operators are `=`, `<`, `<=`, `>` and `>=`.

Expl:

```sql
WHERE Produced < 25
```

### Temporizers

The following temporizers can be applied to the timestamp:

For all temporizers the value of the measurement is compared to the reference value.

- `AFTER` is holding the row, if the timestamp of the row is after the reference value, which is a *timestamp*.
- `BEFORE` is holding the row, if the timestamp of the row is before the reference value,  which is a *timestamp*.
- Range is holding the row, if the timestamp of the row is between the two reference values. The syntax is *timestamp_column* `BETWEEN` *soonest_value* `AND` *latest_value* where the two values are *timestamps*.
- `SINCE` is holding the row, if the timestamp of the row is contained in an *interval ending now and of a duration equal to the reference value which is an *interval*.

Expl:

```sql
WHERE 
    Instant BETWEEN TIMESTAMP '2023-09-01' AND TIMESTAMP '2023-10-01'
    AND Instant SINCE INTERVAL '2 HOURS'
```
