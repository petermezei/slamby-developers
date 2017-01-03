## Document

Manage your **documents** easily. Create, edit, remove documents, manage tags and run text analysis.

Each document is related to a dataset. You have to specify which dataset you want to use in the `X-DataSet` header by the name of the dataset.

> **Tip:** If you use any of the Document methods without or an unexisting `X-DataSet` header you will get a `Missing X-DataSet header!` error.

Using document endpoint you can:

* Insert single document
* Insert multiple documents
* Acess your documents
* Modify your documents
* Run text analysis

> **Tip:** Store all the related information - such as text, prices, image URLs - and use powerful queries.

### Insert New Document

Insert a new document to a dataset using a predefined schema.

*Example REQUEST*
> [POST /api/Documents](swagger#operation--api-Documents-post)

Header   |Value
---------|---
X-DataSet|example

```json
{
    "id": 9,
    "title": "Example Product Title",
    "desc": "Example Product Description",
    "tags": [1,2,3]
}
```

*Example RESPONSE*
> HTTP/1.1 201 CREATED

### Insert Bulk Documents

Add multiple documents to a dataset.

*Example REQUEST*
> [POST /api/Documents/Bulk](swagger#operation--api-Documents-Bulk-post)

Header   |Value
---------|---
X-DataSet|example

```json
{
    "documents": [{
        "id": "Id1",
        "title": "Han Solo",
        "desc": "Example Product Description",
        "tags": [1,2,3]
    },
    {
        "id": "Id2",
        "title": "Luke",
        "desc": "Example Product Description",
        "tags": [1,2,3]
    },
    {
        "id": "Id3",
        "title": "Wookies",
        "desc": "Example Product Description",
        "tags": [1,2,3]
    }]
}
```

*Example RESPONSE*
> HTTP/1.1 200 OK

```json
{
  "Results": [
    {
      "StatusCode": 201,
      "Id": "Id1",
      "Error": ""
    },
    {
      "StatusCode": 201,
      "Id": "Id2",
      "Error": ""
    },
    {
      "StatusCode": 201,
      "Id": "Id3",
      "Error": ""
    }
  ]
}
```

### Get Document

Get a document from a dataset.

*Example REQUEST*
> [GET /api/Documents/`9`](swagger#operation--api-Documents--id--get)

Header   |Value
---------|---
X-DataSet|example

*Example RESPONSE*

> HTTP/1.1 200 OK
```json
{
    "id": 9,
    "title": "Example Product Title",
    "desc": "Example Product Description",
    "tags": [1,2,3]
}
```

### Edit Document

Edit (update) an existing document in a dataset.

*Example REQUEST*

> [PUT /api/Documents/`9`](swagger#operation--api-Documents--id--put)

Header   |Value
---------|---
X-DataSet|example

```json
{
    "id": 9,
    "title": "Example Modified Product Title",
    "desc": "Example Modified Product Description",
    "tags": [1,2,3,4,5,6,7,8,9]
}
```

*Example RESPONSE*

> HTTP/1.1 200 OK

Partial document update is allowed. Only modified data should be specified in this case. The rest of the document will be unchanged.

*Example REQUEST*

> [PUT /api/Documents/`9`](swagger#operation--api-Documents--id--put)

Header   |Value
---------|---
X-DataSet|example

```json
{
    "tags": [7,8,9]
}
```

*Example RESPONSE*

> HTTP/1.1 200 OK

### Delete Document

Delete an existing document in a dataset.

*Example REQUEST*

> [DELETE /api/Documents/`9`](swagger#operation--api-Documents--id--delete)

Header   |Value
---------|---
X-DataSet|example

*Example RESPONSE*

> HTTP/1.1 200 OK

### Copy To

Copying documents from a dataset to another one. You can specify the documents by id. You can copy documents to an existing dataset.

The selected documents will **remain in the source dataset** as well.

*Example REQUEST*

> [POST /api/Documents/Copy](swagger#operation--api-Documents-Copy-post)

Header   |Value
---------|---
X-DataSet|example

```json
{
    "TargetDataSetName": "TARGET_DATASET_NAME",
    "DocumentIdList": ["10", "11"]
}
```

*Example RESPONSE*

> HTTP/1.1 202 ACCEPTED

```json
{
    "Id": "f321abcf-354a-482c-ade4-4d8172f81a40",
    "Start": "2016-03-16T13:12:15.5520625Z",
    "End": "0001-01-01T00:00:00",
    "Percent": 0,
    "Status": "InProgress"
}
```

If the process is ready then the result of the process will contains a link to a detailed JSON. This JSON contains the result code for every document operation.

> **Tip:** You can use the [POST /api/Documents/Sample](swagger#operation--api-Documents-Sample-post) or the [POST /api/Documents/Filter](swagger#operation--api-Documents-Filter--scrollId--post) methods to get document ids easily.

### Move To

Moving documents from a dataset to another one. You can specify documents by id. You can move documents to an existing dataset. 
The selected documents will be **removed from the source dataset**.

*Example REQUEST*

> [POST /api/Documents/Move](swagger#operation--api-Documents-Move-post)

Header   |Value
---------|---
X-DataSet|example

```json
{
    "TargetDataSetName": "TARGET_DATASET_NAME",
    "DocumentIdList": ["10", "11"]
}
```

*Example RESPONSE*

> HTTP/1.1 202 ACCEPTED

```json
{
    "Id": "f321abcf-354a-482c-ade4-4d4433b81a40",
    "Start": "2016-03-16T13:12:15.5520625Z",
    "End": "0001-01-01T00:00:00",
    "Percent": 0,
    "Status": "InProgress"
}
```

If the process is ready then the result of the process will contains a link to a detailed JSON. This JSON contains the result code for every document operation.

> **Tip:** You can use the [POST /api/Documents/Sample](swagger#operation--api-Documents-Sample-post) or the [POST /api/Documents/Filter](#operation--api-Documents-Filter-post) methods to get document ids easily.
