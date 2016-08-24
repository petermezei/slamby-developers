## Filter

Built-in **search engine**. Easily access to your documents with **simple search queries**, **logical expressions** and **wild cards**. 

Manage your language dependencies using **optional tokenizer**.

With Filter you can:
* Create search queries
* Filter by tags
* Search in multiple fields
* Access to all the available document fields and parameters
* Use logical expressions
* Use wild cards
* Use optional tokenizers
* Use built-in pagination
* Filter fields

*Using filter please check `scrolling`.*

*Example REQUEST*

> [POST /api/Documents/Filter](#operation--api-Documents-Filter-post)

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

`Available Filter Object Parameters`

Parameter Group |   Parameter Name    |   Description
--- |   --- |   ---
Filter  |   TagIds  |   Source tag id list in which the filter request will be processed.
Filter  |   Query   |   Search query. Simple search query, logical expressions, field:value form, wildcard.
Order   |   OrderDirection  |   Asc, Desc
Order   |   OrderByField    |   Any field from your dataset.
Pagination  |   Limit   |   Item count in the response object. Maximum value is 1000. When limit value is -1 it will return all the result items, but maximum 1000.
Fields  |   -   |   Requested fields. Empty means all the available fields. Use "*" to get all the fields.

> `Important`: Maximum pagination limit: 1000.

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

The `ScrollId` is used to get the next `Limit` items if available. Provide the `ScrollId` from every response to the Filter endpoint to get the next page until the `Count` equals with the `Limit` or a `ScrollId` is present. 

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

### How to use the ScrollId?

When your filter result object contains more items than your count value, you need to use the scroll to scroll over the results. In the first response, you can find a `ScrollId` field that you can use to identify your filter process and request the next items.

As you can see when you have a ScrollId you can use it in your request URL: `POST /API/Documents/Filter/ScrollId`

To check when to stop scrolling, check the count value. When the count value is 0 there are no more items to scroll.

##### For the parameters explanation check the DocumentFilterSettings schema definition [here](#/definitions/DocumentFilterSettings)

##### For the pagination explanation check the pagination section [here](#pagination)