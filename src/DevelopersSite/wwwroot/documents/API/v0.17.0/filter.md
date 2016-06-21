## Filter
Powerful **search engine**. Build **smart** search functions or filters. Easily access to your datasets with **simple queries**, **logical expressions** and **wild cards**. Manage your language dependencies using **optinal tokenizer**.

With Filter you can:
* Create simple search queries
* Filter by tags
* Search in multiple fields
* Access to all the available document fields and parameters
* Use logical expressions
* Use wild cards
* Use optional tokenizers
* Use built in pagination

*Example REQUEST*
> [POST /api/Documents/Filter](#operation--api-Documents-Filter-post)
>
Header   |Value
---------|---
X-DataSet|example
```JSON
{
    "Filter" : {
        "TagIds" : ["1"],
        "Query" : "title:michelin"
    },
    "Pagination" : {
        "Offset" : 0,
        "Limit": 100,
        "OrderDirection" : "Asc",
        "OrderByField" : "title"
    }
}
```

*Example RESPONSE*
> HTTP/1.1 200 OK
```JSON
{
    "Items": [
    {
        "id": "1455197455453",
        "title": "example title 1",
        "desc": "example description",
        "tags": [
        "1"
        ]
    },
    {
        "id": "1455197455203",
        "title": "example title 2",
        "desc": "example description",
        "tags": [
        "1"
        ]
    },
    ...
    ],
    "Pagination": {
    "Offset": 0,
    "Limit": 100,
    "OrderDirection": "Asc",
    "OrderByField": "title"
    },
    "Count": 100,
    "Total": 1543
}
```

##### For the parameters explanation check the DocumentFilterSettings schema definition [here](#/definitions/DocumentFilterSettings)
##### For the pagination explanation check the pagination section [here](#pagination)

> **Tip:** Easily create a powerful search engine by using tokenizer and detailed search queries.