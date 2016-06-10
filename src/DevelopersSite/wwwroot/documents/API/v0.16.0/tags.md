## Tags
Manage tags to organize your data. Using tags create a tag cloud or a hiearchical tag tree.

Every tag is related to a Dataset. You have to specify which dataset you want to use in the `X-DataSet` header by the name of the dataset.

> **Tip:** If you use any of the tag methods without or an unexisting `X-DataSet` header you will get a `Missing X-DataSet header!` error.

With Tags you can:
* Create new tag
* Update a tag
* Get a single tag or a full tag list
* Organize your tags into hierarchy
* Use tags for categorization
* Use tags for tagging.

### Create New Tag
Create a new tag in a dataset.

>**Tip:** To create hierarchy you have to specify the ParentId of the tag. The ParentId is the Id of the parent of the tag. In the dataset there must be an existing tag with the id given in the ParentId. If the tag is a root element, or you don't want to use hierarchy then just skip the property or set to `null`.

*Example REQUEST*
> [POST /api/Tags](#operation--api-Tags-post)
>
Header   |Value
---------|---
X-DataSet|example
```JSON
{
    "Id": "1",
    "Name": "example tag 1",
    "ParentId": null
}
```

*Example RESPONSE*
> HTTP/1.1 201 CREATED


### Get Tag
Get a tag by its Id. Provide 'withDetails=true' query parameter in order to get DocumentCount, WordCount values. Default value is 'false' because it takes time to calculate these properties.

*Example REQUEST*
> [GET /api/Tags/`1`?withDetails=false](#operation--api-Tags-get)
>
Header   |Value
---------|---
X-DataSet|example
    
*Example RESPONSE*
> HTTP/1.1 200 OK
```JSON
{
    "Id": "1",
    "Name": "example tag 1",
    "ParentId": "5",
    "Properties": {
    "Path": [
        {
        "Id": "5",
        "Level": "1",
        "Name": "example parent tag 5"
        }
        ],
    "Level": 2,
    "IsLeaf": "false",
    "DocumentCount": 33,
    "WordCount": 123
    }
}
```

### Get Tag List
Get all tags list from a given dataset. Provide 'withDetails=true' query parameter in order to get DocumentCount, WordCount values. Default value is 'false' because it takes time to calculate these properties.

*Example REQUEST*
> [GET /api/Tags?withDetails=false](#operation--api-Tags-get)
>
Header   |Value
---------|---
X-DataSet|example

*Example RESPONSE*
> HTTP/1.1 200 OK
```JSON
[
    {
    "Id": "1",
    "Name": "example tag 1",
    "ParentId": null,
    "Properties": {
        "Path": [],
        "Level": 1,
        "IsLeaf": true,
        "DocumentCount": 0,
        "WordCount": 0
    }
    },
    {
    "Id": "2",
    "Name": "example tag 2",
    "ParentId": null,
    "Properties": {
        "Path": [],
        "Level": 1,
        "IsLeaf": true,
        "DocumentCount": 0,
        "WordCount": 0
    }
    }
]
```

### Update Tag

*Example REQUEST*
> [PUT /api/Tags/`1`](#operation--api-Tags-put)
>
Header   |Value
---------|---
X-DataSet|example
```JSON
{
    "Id": "1",
    "Name": "example tag 1",
    "ParentId": null
}
```

*Example RESPONSE*
> HTTP/1.1 200 CREATED

### Remove Tag
Remove a tag from tag list. Default behavior is that only leaf elements can be deleted. You should provide 'force=true' query parameter in order to remove tags with child elements. 'cleanDocument'. Setting 'cleanDocuments=true' removes the specified tag also from its documents.

*Example REQUEST*
> [DELETE /api/Tags/`1`?force=false&cleanDocuments=false](#operation--api-Tags-delete)
>
Header   |Value
---------|---
X-DataSet|example

*Example RESPONSE*
> HTTP/1.1 200 OK