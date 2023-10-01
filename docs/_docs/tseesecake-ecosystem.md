---
title: Tseesecake ecosystem
subtitle: You're not alone on earth
tags: [design-architecture]
---

[Tseesecake](https://seddryck.github.io/Tseesecake) is built on top of the library [DubUrl](https://seddryck.github.io/DubUrl) which provides the abstraction of the underlying connection to the database. With its feature of "query template", DubUrl also provides the mechanism to translate from the time series-oriented dialect to the SLQ dialect of the effective query engine.

Tseesecake can be exposed as a endpoint (server) through the project [Tseetah](https://seddryck.github.io/Tseetah) (early stage). This server communicates with the rest of the world using the "Postgreqsql wire protocol", a binary protocol to communicate between a client and server. This implementation of the protocol is provided by the library [Pgnoli](https://seddryck.github.io/Pgnoli). Most tools able to connect to a Postgresql server will be able to connect to a Tseetah server and query it with SQL statements.

Tseetah will expose a REST endpoint where people will be able to POST queries and retrieve a time series serialized in different formats. The serialization is implemented with Tserry (Will come later) a library dedicated to handle time series serialization.
