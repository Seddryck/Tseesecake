---
title: Timeseries structure
subtitle: Internal structure of a timeseries
tags: [design-architecture, quick-start]
---

A timeseries always contains a time dimension which is materialized by a **timestamp**.

To qualify and describe a timeseries, you can use optional **facets**. Each facet is a descriptive information (textual format), which is or can be used for slicing and dicing.

One or multiple **measurements** are associated to the timeseries. Measurements are the numerical values that can be aggregated (sum, average ...) up to provide meaning to your timeseries. A timeseries can contain more than a single measurement, if all the measurements are taken at the exact same moment and always share the same facets. 

From a more technical point of view, a timestamp is represented by a *date/time* type (depending of the underlying database it can be `TIMESTAMP` or `DATETIME`), facets by a *variable length list of characters* (`VARCHAR`) and the measurements by a numeric value with finite precision (depending of the underlying database it can be `DECIMAL` or `NUMERIC`).
