## Sampling

Statistical method to support sampling activity. Using sampling you can  create **random samples** for experiments.

With sampling you can:

- Create statistical samples
- Set the source categories
- Set sample size by fix number or percentage
- Filter fields

For sampling, you have to specify which dataset you want to use in the `X-DataSet` header by the name of the dataset.

*Example REQUEST*

> [POST /api/Documents/Sample](#operation--api-Documents-Sample-post)

Header   |Value
---------|---
X-DataSet|example

```JSON
{
    "Id" : "6902a2d3-0708-41f7-b21d-c5bd4b302bdc",
    "Percent" : "10",
    "Size" : "0",
    "TagIds" : [],
    "Fields": ["id", "name", "desc"]
}
```

`Available settings:`

Name    |   Description
--- |   ---
Id  |   To create a sample you need to provide a specific sample id. This id specifies your sample process and you can use it in the future to refer your sample. Sample id can be any generated `guid` or other string content.
Percent |   You can determine the required sample size using percentage or fix number. When you use percentage `eg.: 10%` fill this field by the required value and leave the Size field 0.
Size    |   You can determine the required sample size using a fix number instead of a given percentage. When you need a sample with 42 items, you can specify your sample size providing Size field with value 42. In this case, leave the Percent field as 0.
TagIds  |   If you need a sample just from a given category or categories, you can specify the source categories by providing an id list. It is not a required field, you can create sample without using tag selection as well.
Fields  |   Field selector to determine the response item fields. If you do not need full document objects, just specify the required fields here.

*Example RESPONSE*

> HTTP/1.1 200 OK

```JSON
{
    "Items": [
        {
            "id": "1455197295447",
            "title": "example title",
            "desc": "example description",
            "tags": [ "2", "3"]
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
    "Count": 15,
    "Total": 150
}
```

`Response items:`

Name    |   Description
--- |   ---
Items   |   Object, it contains all the result documents.
Count   |   Sample size. It should be the same value as the sample size when we use Size instead of Percent.
Total   |   Dataset total document count value.


> `Important:` there is no built-in scroll or pagination. Please be careful when you determine the sample size.

##### For the parameters explanation check the DocumentSampleSettings schema definition [here](#/definitions/DocumentSampleSettings)

##### For the pagination explanation check the pagination section [here](#pagination)