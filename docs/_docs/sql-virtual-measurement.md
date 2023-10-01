---
title: Virtual measurements
subtitle: Create measurements on the fly
tags: [sql-select]
---

In some cases, you need to return a value which is not a measurement provided by the original time series but a combination of them.

*Expl*: You have a timeseries related to *wind energy* providing the forecasting of the production and the energy effectively produced but you want to return the delta between the forecasting and tje effective production (absolute value).

You can define this kind of virtual measurement at the beginning of your SLQ query and use a reference to it wherever you would have been able to use a reference a standard measurement.

```sql
WITH VIRTUAL MEASUREMENT Delta AS (
    ABS(Produced - Forecasted)
)

SELECT
    Instant,
    Delta
WHERE
    Delta > 10
```
