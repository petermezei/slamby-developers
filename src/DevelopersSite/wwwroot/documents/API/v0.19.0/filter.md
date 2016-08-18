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
* Filter fields

*Example REQUEST*
> [POST /api/Documents/Filter](#operation--api-Documents-Filter-post)
>
Header   |Value
---------|---
X-DataSet|example

If Fields is not set then by default returns with IdField, TagField and InterpretedFields (`attachment` fields are excluded because their size) of the current DataSet. You can specify a field list or "*" which means all fields will return.

```JSON
{
    "Filter" : {
        "TagIds" : ["1"],
        "Query" : "title:michelin"
    },
    "Order": {
        "OrderDirection" : "Asc",
        "OrderByField" : "id"
    },
    "Pagination" : {
        "Limit": 100
    },
    "Fields": ["id", "name", "desc"]
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
            "desc": "example description"
        },
        {
            "id": "1455197455203",
            "title": "example title 2",
            "desc": "example description"
        },
        ...
        ],
    "ScrollId": "cXVlcnlBbmRGZXRjaDsxOzE2OTA6b3NoRjRMZlVUNUNIWlNxa1RDdzdEZzswOw==",
    "Count": 100,
    "Total": 154
}
```

The `ScrollId` is used to get the next `Limit` items if available. Provide the `ScrollId` from every response to the Filter endpoint to get the next page until the `Count` is equals with the `Limit` or a `ScrollId` is present. 

> TIP: `ScrollId` can be same or different from call to call. Use the value from the last response.


*Example REQUEST*
> [POST /api/Documents/Filter/cXVlcnlBbmRGZXRjaDsxOzE2OTA6b3NoRjRMZlVUNUNIWlNxa1RDdzdEZzswOw==](#operation--api-Documents-Filter-post)

*Example RESPONSE*
> HTTP/1.1 200 OK
```JSON
{
    "Items": [
        {
            "id": "1455197459967",
            "title": "example title 101",
            "desc": "example description"
        },
        {
            "id": "1455197451428",
            "title": "example title 102",
            "desc": "example description"
        },
        ...
        ],
    "ScrollId": "cXVlcnlBbmRGZXRjaDsxOzE2OTA6b3NoRjRMZlVUNUNIWlNxa1RDdzdEZzswOw==",
    "Count": 54,
    "Total": 1543
}
```

##### For the parameters explanation check the DocumentFilterSettings schema definition [here](#/definitions/DocumentFilterSettings)
##### For the pagination explanation check the pagination section [here](#pagination)

> **Tip:** Easily create a powerful search engine by using tokenizer and detailed search queries.