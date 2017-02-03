## Services

Slamby introduces services. You can create a data processing service from the available service templates. Manage your data processing with services, run different tests, run more data management services parallelly.

**Service definition:** a data management service with custom settings, dedicated resources, and available API endpoint.

*Available service types:*

- Text classification service
- Similar product recommendation service

With services you can:

* Create a service
* Get your services list
* Get a service
* Remove a service
* Manage processes
* Create category recommendation engine
* Create similar product recommendation engine

> **Tip:** Unlike Id Service Alias is defined by the user and it is also unique among services. Alias name should be URL-encoded (e.g.: "My Service" -> "My%20Service") when used in URL.

### Get Services

You can query general information about services. 

*Example REQUEST*

> [GET /api/Services](swagger#operation--api-Services-get)

*Example RESPONSE*

> HTTP/1.1 200 OK

```json
[
    {
        "Id": "57c845dc-6aa4-475c-bbf2-0d682f471f32",
        "Name": "Example name of a service",
        "Alias": "The Service"
        "Description": "This is an example service description",
        "Status": "New",
        "Type": "Classifier",
        "ProcessIdList": [
            "e251dbbf-04ff-4d34-a959-90dc4a602142",
            "d335edaf-354a-482c-ade4-4d8172f81a40"
        ],
        "ActualProcessId": null
    },
    {
        "Id": "02f92528-5ae3-4496-9dd1-e3d69327a5a0",
        "Name": "Example name of a service 2",
        "Alias": "The Service 2"
        "Description": "This is an example service description",
        "Status": "New",
        "Type": "Prc",
        "ProcessIdList": [            
        ],
        "ActualProcessId": null
    }
]
```

### Get Service

You can get general information about a service using the Id or Alias of the service. 

*Example REQUEST*

> [GET /api/Services/`GUID or Alias`](swagger#operation--api-Services--id--get)

*Example RESPONSE*

> HTTP/1.1 200 OK

```json
{
    "Id": "57c845dc-6aa4-475c-bbf2-0d682f471f32",
    "Name": "Example name of a service",
    "Alias": "The Service"
    "Description": "This is an example service description",
    "Status": "New",
    "Type": "Classifier",
    "ProcessIdList": [
        "e251dbbf-04ff-4d34-a959-90dc4a602142",
        "d335edaf-354a-482c-ade4-4d8172f81a40"
    ],
    "ActualProcessId": null
}
```

### Create New Service

Create a new Service

*Example REQUEST*

> [POST /api/Services](swagger#operation--api-Services-post)

```json
{
    "Name": "Example name of a service",
    "Description": "This is an example service description",
    "Type" : "Classifier",
    "Alias" : "The Service"
}
```

*Example RESPONSE*

> HTTP/1.1 201 CREATED

### Update Service

You can update only the Name and the Description field. If specified Alias exists on another Service then it will be removed first.

*Example REQUEST*

> [PUT /api/Services/`GUID or Alias`](swagger#operation--api-Services--id--put)

```json
{
    "Name": "Updated example name of a service",
    "Description": "This is an updated example service description",
    "Alias": "My Updated Service"
}
```

*Example RESPONSE*

> HTTP/1.1 200 CREATED

### Remove Service

You remove a service anytime. If it's in Activated status then it will be Deactivated first. If it's in Busy status then it will be canceled first. 

> `WARNING:` If you use Alias for deletion then make sure the Alias is on the right service!

*Example REQUEST*

> [DELETE /api/Services/`GUID or Alias`](swagger#operation--api-Services--id--delete)

*Example RESPONSE*

> HTTP/1.1 200 OK

## Classifier Service

Service for text classification. Create a classifier service from a selected dataset, specify your settings and use this service API endpoint to classify your incoming text.

> Currently Slamby provides `Slamby Twister` as a highly accurate classification algorithm designed for e-commerce market.

### Get Classifier Service

You can get classifier specified information about a classifier service with the Id of the service

*Example REQUEST*

> [GET /api/Services/Classifier/`GUID or Alias`](swagger#operation--api-Services-Classifier--id--get)

*Example RESPONSE*

> HTTP/1.1 200 OK

```json
{
    "Id": "83326e75-dd16-4e5c-a66d-5ea5197bd8e0",
    "Name": "Example name of a classifier service",
    "Description": "This is an example classifier service description",
    "Status": "New",
    "Type": "Classifier",
    "ProcessIdList": null,
    "ActualProcessId": null,
    "PrepareSettings": null,
    "ActivateSettings": null
}
```

### Prepare Classifier Service

Training Process Steps:

1. Give a suitable name to the service,
2. Set the ngram values,
3. Provide the tag ids you are going to use during the training,
4. Start the training process.

> For Training process Slamby is using `Slamby Twister` as its own classification algorithm. 

This request is a long running task so the API do it in an async way. Therefore the response is a Process.

> `N-gram settings`: each dataset has an n-gram setting. For set the required n-gram the minimum value is 1, the maximum value equals the maximum n-gram number of the given dataset. Using a [1,2,3] n-gram settings means during the training process the classifier is going to create 1,2,3 n-gram dictionaries. [Learn more about N-gram](https://en.wikipedia.org/wiki/N-gram)

*Example REQUEST*

> [POST /api/Services/Classifier/`GUID or Alias`/Prepare](swagger#operation--api-Services-Classifier--id--Prepare-post)

```json
{
    "DataSetName" : "test dataset",
    "NGramList": [1,2],
    "TagIdList": ["tag1Id","tag2Id"],
    "CompressLevel": 1,
    "CompressSettings": {...}
}
```

`Settings` explanation

Name    |   Desc
--- |   ---
DataSetName |   Dataset as a training database, as a source database.
NGramList   |   Ngram model for prepare process.
TagIdList   |   Tag ID list that will be used during the training. Default value is `null` that means all the lead nodes.
CompressLevel   |   Built-in compress function. During the training process compress makes the training faster and more efficient. The output files are smaller, that makes the classification process faster. Using compress a smaller server size can serve the same training dataset as a bigger machine. CompressLevel is a built-in compress function, values can be: 1, 2 or 3. 1 is a smaller, 2 is a medium compress level, 3 is a higher compress level.
CompressSettings    |   Customized compress process. Instead of using predefined compress levels, you can use more detailed settings. For this please contact our support.

*Example RESPONSE*

> HTTP/1.1 202 ACCEPTED

```json
{
    "Id": "d335edaf-354a-482c-ade4-4d8172f81a40",
    "Start": "2016-03-16T13:12:15.5520625Z",
    "End": "0001-01-01T00:00:00",
    "Percent": 0,
    "Status": "InProgress"
}
```

### Activate Classifier Service

Each service has two statuses: active, deactive. When a preparation/training process is ready, the service has a deactivated status. A deactivated service is ready, but it's not loaded into memory and the API is not able to process the incoming requests. To use a service set the status to Activated. After the activation process, the service is ready to use, all the required files are loaded and stored in memory, the API endpoint is active.

This request can be a long running task so the API do it in an async way. Therefore the response is a Process.

*Example REQUEST*

> [POST /api/Services/Classifier/`GUID or Alias`/Activate](swagger#operation--api-Services-Classifier--id--Activate-post)

```json
{
    "NGramList": [1,2],
    "EmphasizedTagIdList": null,
    "TagIdList": null
}
```

*Example RESPONSE*

> HTTP/1.1 202 ACCEPTED

```json
{
  "Id": "d46850e8-1a22-43f5-b304-d22e6b7e5e6a",
  "Start": "2016-06-28T10:53:36.9553132Z",
  "End": "0001-01-01T00:00:00",
  "Percent": 0,
  "Description": "Activating Classifier service Example name of a classifier service...",
  "Status": "InProgress",
  "Type": "ClassifierActivate",
  "ErrorMessages": [],
  "ResultMessage": null
}
```

### Deactivate Classifier Service

When a service is not needed for continuous usage you can deactivate it. After deactivating a service, all the settings and files remain, but they are not using any resources (memory, cores). You can store your deactivated services and activate them anytime.

*Example REQUEST*

> [POST /api/Services/Classifier/`GUID or Alias`/Deactivate](swagger#operation--api-Services-Classifier--id--Deactivate-post)

*Example RESPONSE*

> HTTP/1.1 200 OK

### Recommend

Built-in text classification engine. Uses the prepared Classifier dictionaries and calculations. High speed and classification capability. Built-in n-gram analyzer.

*Example Request*

> [POST /api/Services/Classifier/`GUID or Alias`/Recommend](swagger#operation--api-Services-Classifier--id--Recommend-post)

```json
{
    "Text": "Lorem Ipsum Dolorem",
    "Count": "2",
    "UseEmphasizing": false,
    "NeedTagInResult": true
}
```

*Example Response*

```json
[
    {
        "TagId": "324",
        "Score": 0.35175663155586434,
        "Tag": {
            "Id": "324",
            "Name": "Tag name",
            "ParentId": "16",
            "Properties": null
        }
    },
    {
        "TagId": "232",
        "Score": 0.30277479057126688,
        "Tag": {
            "Id": "232",
            "Name": "Tag name",
            "ParentId": "24",
            "Properties": null
        }
    }
]
```

#### Emphasizing

During the recommendation process, if there are any emphasized tag in the TOP N result list then Slamby checks if the given document contains the name of the emphasized tag.
If yes, then Slamby changes the order of the recommended tags and moves the emphasized results to the first place.
If there are more than one emphasized tag on the list that meeting with the criteria then all of them will be moved to the top (with keeping their orders).
If the `IsEmphasized` parameter is true in a result element, it means that the actual result was emphasized. The score is the original in each case.
With the usage of the score, the original order can be restored anytime.

> **Tip:** You have to define the emphasized tags during the activation of the service 

### Export dictionaries

*Example Request*

> [POST /api/Services/Classifier/`GUID or Alias`/ExportDictionaries](swagger#operation--api-Services-Classifier--id--ExportDictionaries-post)

```json
{
    "NGramList": [1],
    "TagIdList": null
}
```

> **Tip:** If you skip the `TagIdList` or set it to `null` then the API will use all the leaf tags

*Example Response*
```json
{
    "Id": "345e1c79-dc78-427f-8ad1-facce75f6ae3",
    "Start": "2016-04-18T13:29:15.3728991Z",
    "End": "2016-04-18T13:29:39.3144202Z",
    "Percent": 0,
    "Description": "Exporting dictionaries from Classifier service prc...",
    "Status": "Finished",
    "Type": "ClassifierExportDictionaries",
    "ErrorMessages": [],
    "ResultMessage": "Successfully exported dictionaries from Classifier service prc!\nExport file can be download from here: https://api.slamby.com/demo-api/files/345e1c79-dc78-427f-8ad1-facce75f6ae3.zip"
}
```

## Prc Service

Similar product/document recommendation engine.

### Get Prc Service

*Example REQUEST*

> [GET /api/Services/Prc/`GUID or Alias`](swagger#operation--api-Services-Prc--id--get)

*Example RESPONSE*

> HTTP/1.1 200 OK

```json
{
    "PrepareSettings": null,
    "ActivateSettings": null,
    "Id": "996f91f4-f1ca-428b-af1a-929ccf3b0243",
    "Name": "prc",
    "Description": null,
    "Status": "New",
    "Type": "Prc",
    "ProcessIdList": [],
    "ActualProcessId": null
}
```

### Prepare Prc Service

*Example REQUEST*

> [POST /api/Services/Prc/`GUID or Alias`/Prepare](swagger#operation--api-Services-Prc--id--Prepare-post)

```json
{
    "DataSetName" : "test dataset",
    "TagIdList": ["tag1Id","tag2Id"]
}
```

`Parameter List`:

Field Name   |   Description
---   |   ---
DataSetName   |   Source dataset that will be the part of the preparation.
TagIdList   |   Available tag ids from the source dataset that will be analyzed and stored during the preparation process. Using a PRC Service you will be able to use just the prepared tags.

*Example RESPONSE*

> HTTP/1.1 202 ACCEPTED

```json
{
    "Id": "ceed2045-70fb-4785-b483-aadb9bf9a992",
    "Start": "2016-04-18T13:09:49.6032716Z",
    "End": "0001-01-01T00:00:00",
    "Percent": 0,
    "Description": "Preparing Prc service prc...",
    "Status": "InProgress",
    "Type": "PrcPrepare",
    "ErrorMessages": [],
    "ResultMessage": ""
}
```

### Activate Prc Service

This request can be a long running task so the API do it in an async way. Therefore the response is a Process.

*Example REQUEST*

> [POST /api/Services/Prc/`GUID or Alias`/Activate](swagger#operation--api-Services-Prc--id--Activate-post)

```json
{
    "FieldsForRecommendation": ["title"]
}
```

*Example RESPONSE*

> HTTP/1.1 202 ACCEPTED

```json
{
  "Id": "181b445c-3c91-44bb-bcf7-759a7e40a98c",
  "Start": "2016-06-28T10:49:52.5726793Z",
  "End": "0001-01-01T00:00:00",
  "Percent": 0,
  "Description": "Activating Prc service prc...",
  "Status": "InProgress",
  "Type": "PrcActivate",
  "ErrorMessages": [],
  "ResultMessage": null
}
```

### Index on PRC Service

After PRC Service Activation the service is available for real-time analysis that might take time. For real-time quick analysis, you can use PRC Index function. It has 2 main part: a first full index, then a reputable partial index.

During the indexing process, PRC Index analyze all the available documents inside the source dataset and store the results in an index database. Using PRC Index, a quick real-time analysis is available.

#### Full Index

First index stage. After a PRC Service is activated its ready for text analysis. To boost its speed we suggest using PRC Index. During the first full index, using the available PRC Service, your server analysis all of your documents and save the results in an index database.

#### Partial Index

When a first full index is ready, you can synchronize your index dataset by partial index update. Using partial index process, just the new documents will be analyzed and stored. We suggest using partial PRC Index frequently.

### Deactivate Prc Service

*Example REQUEST*

> [POST /api/Services/Prc/`GUID or Alias`/Deactivate](swagger#operation--api-Services-Prc--id--Deactivate-post)

*Example RESPONSE*

> HTTP/1.1 200 OK

### Recommend (PRC)

When a PRC service is activated use it for matchmaking. The basic method is `recommend`. For start pick a text for analysis and send it to your PRC service. You can identify your service using either `service id` or `service alias`.

Basic algorithm:

1. Read input text,

2. Analyzing the text in the context of the required tag,

3. Picking the meaningful keywords,

4. Run a pre-search query using the given filter object,

5. Start a powerful search in the given dataset part,

6. Run a post-weighting process on the result,

7. Returns the most similar documents from the given dataset.

Typical usage:

- Matchmaking,

- Keyword extraction,

- Duplicate detection.

*Example Request*

> [POST /api/Services/Prc/`GUID or Alias`/Recommend](swagger#operation--api-Services-Prc--id--Recommend-post)

```json
{
    "Text": "Lorem Ipsum Dolorem",
    "Count": "2",
    "Filter": {
        "TagIdList" : ["1"],
        "Query" : "status:1 AND title:michael"
    },
    "Weights": [
        {
            "Query": "price>1000",
            "value": 7
        },
        {
            "Query": "lang:84",
            "value": 9
        }
    ],
    "TagId": "tag1Id",
    "NeedDocumentInResult": true
}
```

*Example Response*

```json
[
    {
        "DocumentId": "1777237",
        "Score": 0.89313365295595715,
        "Document": {
            "id": "1777237",
            "tag_id": "tag1Id",
            "title": "Lorem",
            "body": "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor"
        }
    },
    {
        "DocumentId": "6507461",
        "Score": 0.7894283811358983,
        "Document": {
            "ad_id": "6507461",
            "tag_id": "tag1Id",
            "title": "Duis aute irure dolorem",
            "body": "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur."
        }
    }
]
```

`Request parameters`

Field   |   Description
---   |   ---
Text   |   Text to analyze. Recommend method analyses this given text.
Count   |   Result document number in response.
Filter   |   Not required. Filter object. We can use each available field from the dataset to pre-filter the potential result documents before matchmaking.
Weights   |   Not required. Weights object. We can use each available dataset field, a preferred value and a weighted score between 0 and 10 to boost the result.
TagId   |   Matchmaking within this given tag. Not required. Once it is given, the results will be from this tagId. Once it's empty, Slamby will analyze and predict the most appropriate tag, but the results will be from the entire dataset.
NeedDocumentInResult   |   `true` or `false`, when it true result documents can be found in the response.

`Filter Object`

Use it for pre-filter your potential documents before matchmaking. More precise results, custom filters, higher performance.

*Filter Object Structure*

```JSON
{
    "Query":"fieldName:fieldValue"
}
```

`Demo Filter Object`

```JSON
{
    "Query":"status:active"
}
```

`Weights Object`

Use it for boosting your results. You can access to each available document field, set custom values and weights.

*Weights Object Structure*

```JSON
[
    {
        "Query": "fieldName:targetValue",
        "value": "weight between 0-10"
    },
    {
        "Query": "fieldName:targetValue",
        "value": "weight between 0-10"
    },
    {
        ...
    }
]
```

`Demo Weight Query`

```JSON
[
    {
        "Query": "city:Budapest",
        "value": "7"
    },
    {
        "Query": "position:engineer",
        "value": "9"
    }
]
```

`Response parameters`

Field   |   Description
---   |   ---
DocumentId   |   
Score   |   Result score between 0 and 2. It reflects a relevance order between the documents in ASC order. Score range 0-0.99 means the result documents have many meaningful words in common, but not all the meaningful words match. Score 1 means all the meaningful words match, potentially duplicate documents. Score range 1-2 means the result documents contains the meaningful words more frequently than the source text.
Document Object   |   When `NeedDocumentInResult` is true, this is the complete document object we store.

### Keywords Extraction

With this function, you can easily extract the relevant keywords (according to the given tag) from your text, with the help of PRC service.

*Example Request*

> [POST /api/Services/Prc/`GUID`/Keywords?isStrict=false](swagger#operation--api-Services-Prc--id--Keywords-post)
```json
{
    "Text": "Lorem Ipsum Dolorem",
    "TagId": "tag1Id"
}
```

`Request parameters`

Field   |   Description
---   |   ---
Text   |   Text to analyze. Keywords method analyses this given text.
TagId   |   Keywords extraction within this given tag. Not required. Once it is given, the results will be from this tagId. Once it's empty, Slamby will analyze and predict the most appropriate tag.
IsStrict (query parameter)   |   `true` or `false`, when it true then you will get lesser keywords. The score limit is stricter. The default value is `false`.


*Example Response*
```json
[
  {
    "Word": "lorem",
    "Score": 75.457626224664608
  },
  {
    "Word": "ipsum",
    "Score": 40.333326695449692
  }
]
```

### Export dictionaries

*Example Request*

> [POST /api/Services/Prc/`GUID or Alias`/ExportDictionaries](swagger#operation--api-Services-Prc--id--ExportDictionaries-post)

```json
{
    "TagIdList": null
}
```

> **Tip:** If you skip the `TagIdList` or set it to `null` then the API will use all the leaf tags

*Example Response*

```json
{
    "Id": "345e1c79-dc78-427f-8ad1-facce75f6ae3",
    "Start": "2016-04-18T13:29:15.3728991Z",
    "End": "2016-04-18T13:29:39.3144202Z",
    "Percent": 0,
    "Description": "Exporting dictionaries from Prc service prc...",
    "Status": "Finished",
    "Type": "PrcExportDictionaries",
    "ErrorMessages": [],
    "ResultMessage": "Successfully exported dictionaries from Prc service prc!\nExport file can be download from here: https://api.slamby.com/demo-api/files/345e1c79-dc78-427f-8ad1-facce75f6ae3.zip"
}
```

### Index

Built-in index engine for PRC service. When you have a prepared PRC service, you can run index process to analyze all the available matching results and store it in an index database. When the indexing process finished you can use PRC `recommend by id` method.

Two kinds of index available: first start a full index then use the partial index to update your index database with smaller dataset changes.

*Example Request*

> [POST /api/Services/Prc/`GUID or Alias`/Index](swagger#operation--api-Services-Prc--id--Index-post)

```JSON
{
    "Filter": {
        "Query" : "status:active",
        "TagIdList": ["tag1Id","tag2Id"]
    }
}
```

> **Tip:** If you skip the `TagIdList` or set it to `null` then the API will use all the *prepared tags*.

*Example Response*

> HTTP/1.1 202 Accepted
```json
{
  "Id": "f8e8860b-9e40-4d75-be57-a1f36fdb3c1e",
  "Start": "2016-08-17T13:19:31.6187032Z",
  "End": "0001-01-01T00:00:00",
  "Percent": 0,
  "Description": "Indexing Prc service sampla_dataset...",
  "Status": "InProgress",
  "Type": "PrcIndex",
  "ErrorMessages": [],
  "ResultMessage": null
}
```

### Index Partial

Partial indexing all the documents modified since last `Index` or `IndexPartial` call. It can call after an `Index` once called.

*Example Request*

> [POST /api/Services/Prc/`GUID or Alias`/IndexPartial](swagger#operation--api-Services-Prc--id--IndexPartial-post)

*Example Response*

> HTTP/1.1 202 Accepted

```json
{
  "Id": "07499fad-6bec-4c66-a2bf-065ae2e7d853",
  "Start": "2016-08-17T13:24:47.0345313Z",
  "End": "0001-01-01T00:00:00",
  "Percent": 0,
  "Description": "Partial indexing Prc service sampla_dataset...",
  "Status": "InProgress",
  "Type": "PrcIndexPartial",
  "ErrorMessages": [],
  "ResultMessage": null
}
```

### Recommend By Id

PRC Recommend method using a predefined index database. To use it you need a `DocumentId` to identify the source document; all the other fields are the same like using `Recommend` endpoint.

*Example Request*

> [POST /api/Services/Prc/`GUID or Alias`/RecommendById](swagger#operation--api-Services-Prc--id--RecommendById-post)

```json
{
    "DocumentId": "987654",
    "Count": "2",
    "Filter": null,
    "Weights": null,
    "TagId": "tag1Id",
    "NeedDocumentInResult": true
}
```

> When `TagId` is null, the API server predicts the most suitable tag using the source document.

*Example Response*

```json
[
    {
        "DocumentId": "1777237",
        "Score": 0.89313365295595715,
        "Document": {
            "id": "1777237",
            "tag_id": "tag1Id",
            "title": "Lorem",
            "body": "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor"
        }
    },
    {
        "DocumentId": "6507461",
        "Score": 0.7894283811358983,
        "Document": {
            "ad_id": "6507461",
            "tag_id": "tag1Id",
            "title": "Duis aute irure dolorem",
            "body": "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur."
        }
    }
]
```