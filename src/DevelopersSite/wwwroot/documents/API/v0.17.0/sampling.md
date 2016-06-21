## Sampling
Statistical method to support sampling activity. Using sampling you can easily create **random samples** for experiments.

With sampling you can:
- Create sample easily
- Set the source categories
- Use normal or stratified sampling method
- Set sample size by fix number or percentage
- Use built in pagination.

For sampling you have to specify which dataset you want to use in the `X-DataSet` header by the name of the dataset.

*Example REQUEST*
> [POST /api/Documents/Sample](#operation--api-Documents-Sample-post)
>
Header   |Value
---------|---
X-DataSet|example
```JSON
{
    "Id" : "6902a2d3-0708-41f7-b21d-c5bd4b302bdc",
    "IsStratified" : "false",
    "Percent" : "0",
    "Size" : "15000",
    "TagIds" : [],
    "Pagination" : {
        "Offset" : 0,
        "Limit": 100,
        "OrderDirection" : "Asc",
        "OrderByField" : "id"
    }
}
```

*Example RESPONSE*
> HTTP/1.1 200 OK
```JSON
{
    "Items": [
    {
        "id": "1455197295447",
        "title": "example title",
        "desc": "example description",
        "tags": [
        "2",
        "3"
        ]
    },
    {
        "id": "1455197591439",
        "title": "example title",
        "desc": "example description",
        "tags": [
        "3"
        ]
    },
    ...
    ],
    "Pagination": {
    "Offset": 0,
    "Limit": 100,
    "OrderDirection": "Asc",
    "OrderByField": "desc"
    },
    "Count": 100,
    "Total": 15000
}
```

##### For the parameters explanation check the DocumentSampleSettings schema definition [here](#/definitions/DocumentSampleSettings)
##### For the pagination explanation check the pagination section [here](#pagination)