## General


### Pagination
There are situations when your query results lots of data. In these cases the pagination can be handy.
You have to only provide an offset and a limit in the pagination object.
Optionally you can set a field which will be the base of the ordering and also the direction of the ordering (ascendig or descending). If you specify `-1` value for `Limit` then you will get all the elements in one result.

In the result (PaginatedList[Object]) there is an Items property which containing the requested elements (or the part of the requested elements). Also it provides the count of the items (this is equal or lesser than the limit property) and the total count of the requested items. Also it returns the same pagination object which was int the request.

>*Example REQUEST*
```json
{
    ...
    "Pagination" : {
        "Offset" : 0,
        "Limit": 10,
        "OrderDirection" : "Asc",
        "OrderByField" : "title"
    }
}
```

>*Example RESPONSE*
```json
{
    "Items": [
    {
        ...
    },
    {
        ...
    },
    ...
    ],
    "Pagination": {
    "Offset": 0,
    "Limit": 100,
    "OrderDirection": "Asc",
    "OrderByField": "title"
    },
    "Count": 10,
    "Total": 21
}
```

##### Check the Pagination schema definition [here](#/definitions/Pagination)
##### Check the PaginatedList[Object] schema definition [here](#/definitions/PaginatedList[Object])

### Status

*Example REQUEST*

> [GET /api/Status](#operation--api-Status-get))

*Example RESPONSE*
> HTTP/1.1 200 OK
```JSON
{
    "ProcessorCount": 4,
    "AvailableFreeSpace": 47895.53,
    "ApiVersion": "0.14.0",
    "CpuUsage": 0.6,
    "TotalMemory": 996.08,
    "FreeMemory": 36.3
}
```
