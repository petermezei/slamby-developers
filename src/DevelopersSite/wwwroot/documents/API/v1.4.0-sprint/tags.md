## Tags

Manage tags to organize your data. Use tags as a tag cloud, or build a category tree organizing tags into a hierarchy.

Each tag is related to a Dataset. You have to specify which dataset you want to use in the `X-DataSet` header by the name of the dataset.

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

>**Tip:** To create hierarchy you have to specify the ParentId of the tag. The ParentId is the Id of the parent of the tag. In the dataset, there must be an existing tag with the id given in the ParentId. If the tag is a root element, or you don't want to use hierarchy then just skip the property or set to `null`.

*Example REQUEST*

> [POST /api/Tags](swagger#operation--api-Tags-post)

Header   |Value
---------|---
X-DataSet|example

```json
{
    "Id": "1",
    "Name": "example tag 1",
    "ParentId": null
}
```

*Example RESPONSE*

> HTTP/1.1 201 CREATED

### Create Bulk Tags

Import tags using bulk method.

*Example REQUEST*

> [POST /api/Tags/Bulk](swagger#operation--api-Tags-Bulk-post)

Header  |   Value
--- |   ---
X-DataSet   |   example

```json
{
  "Tags": [
    {
      "Id": 1,
      "Name": "Jedi",
      "ParentId": null
    },
    {
      "Id": 2,
      "Name": "Luke",
      "ParentId": "1"
    },
    {
      "Id": 3,
      "Name": "Anakin",
      "ParentId": "1"
    },
    {
      "Id": 4,
      "Name": "Dark side of the Force",
      "ParentId": null
    },
    {
      "Id": 5,
      "Name": "Darth Vader",
      "ParentId": "4"
    },
    {
      "Id": 6,
      "Name": "Darth Bandon",
      "ParentId": "4"
    }
  ]
}
```

*Example RESPONSE*

> HTTP/1.1 200 OK

### Get Tag

Get a tag by its Id. Provide 'withDetails=true' query parameter in order to get DocumentCount, WordCount values. Default value is 'false' because it takes time to calculate these properties.

*Example REQUEST*

> [GET /api/Tags/`Id`?withDetails=false](swagger#operation--api-Tags-get)

Header   |Value
---------|---
X-DataSet|example
    
*Example RESPONSE*

> HTTP/1.1 200 OK

```json
{
    "Id": "1",
    "Name": "example tag 1",
    "ParentId": "5",
    "Properties": {
        "Paths": [
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

`Available response fields:`

Name    |   Description
--- |   ---
Id  |   Tag id
Name    |   Tag name
ParentId    |   Tag parent id, default value `null`
Paths    |   Object. When ParentId is given, it contains the full category path of the given tag.
Level   |   When we use ParentId this field shows us the position of the given tag. First category level is 1.
IsLeaf  |   Using a category tree, this boolean field shows us the position of the given tag. When IsLeaf is true, the given tag is located in a leaf node position in the category tree. When this value is false, it means this tag has children in the category tree.
DocumentCount   |   Related document number in the given dataset.
WordCount   |   Related word count number in the given dataset.

### Get Tag List

Get all tags list from a given dataset. Provide 'withDetails=true' query parameter in order to get DocumentCount, WordCount values. Default value is 'false' because it takes time to calculate these properties.

*Example REQUEST*

> [GET /api/Tags?withDetails=false](swagger#operation--api-Tags-get)

Header   |Value
---------|---
X-DataSet|example

*Example RESPONSE*

> HTTP/1.1 200 OK

```json
[
    {
        "Id": "1",
        "Name": "example tag 1",
        "ParentId": null,
        "Properties": {
            "Paths": [],
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
            "Paths": [],
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

> [PUT /api/Tags/`Id`](swagger#operation--api-Tags-put)

Header   |Value
---------|---
X-DataSet|example

```json
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

> [DELETE /api/Tags/`Id`?force=false&cleanDocuments=false](swagger#operation--api-Tags-delete)

Header   |Value
---------|---
X-DataSet|example

*Example RESPONSE*
> HTTP/1.1 200 OK