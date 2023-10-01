---
title: Why did we create Tseesecake?
subtitle: The quest for an effective abstraction layer on top of timeseries specific solutions
tags: [quick-start]
---
We started the development of Tseesecake to compensate the lack of solution to abstract queries execution on timeseries from the effective underlying query engine but also the storage engine. The typical story that we wanted to address was a shop with timeseries on parquet files but also in PostgreSQL and Microsoft SQL Server.

The dialect to query to these timeseries was dependent of the system hosting them, with tiny differences for each of them. Many queries were complex to achieve basic requirements (aggregation by month, filtering on aggregations, transform a time series to a box and whisker chart) and most of the time leading to implementation where all these queries were de facto largely implemented on the client side after fetching huge datasets from the most performant query and storage engines.

Tseesecake's primary design goals are to:

* query time series stored in different systems (with different query engines and different dialects) through a single SQL dialect dedicated to timeseries
* ease the creation of common SQL queries on timeseries by providing shortcuts to standard SQL
* exposition of time series available on existing databases independantly of the underlying vendor
* we pre-suppose that the timeseries are correctly modeled in the underlying database. As such most of the SQL functions, especially the ones manipulating list of characters are not needed.
