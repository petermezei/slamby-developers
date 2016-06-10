## Services
Slamby introduces services. You can quickly create a data processing service from the available service templates. Manage your data processing with services, run different tests, run more data management services parallelly.

**Service definition:** a data management service with custom settings, dedicated resources and available API endpoint.

With services you can:
* Create a service
* Get your services list
* Get a service
* Remove a service
* Manage processes

### Get Service
You can get general information about a service using the Id of the service

*Example REQUEST*
> [GET /api/Services/`GUID`](#operation--api-Services-get)

*Example RESPONSE*
> HTTP/1.1 200 OK
```JSON
{
    "Id": "57c845dc-6aa4-475c-bbf2-0d682f471f32",
    "Name": "Example name of a service",
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
> [POST /api/Services](#operation--api-Services-post)
```JSON
{
    "Name": "Example name of a service",
    "Description": "This is an example service description",
    "Type" : "Classifier"
}
```

*Example RESPONSE*
> HTTP/1.1 201 CREATED

### Update Service
You can update only the Name and the Description field.
*Example REQUEST*
> [PUT /api/Services/`GUID`](#operation--api-Services-put)
```JSON
{
    "Name": "Updated example name of a service",
    "Description": "This is an updated example service description"
}
```

*Example RESPONSE*
> HTTP/1.1 200 CREATED


### Remove Service
You remove a service anytime. If it's in Activated status then it will be Deactivated first. If it's in Busy status then it will be cancelled first.

*Example REQUEST*
> [DELETE /api/Services/`GUID`](#operation--api-Services-delete)

*Example RESPONSE*
> HTTP/1.1 200 OK


## Classifier Service
Service for text classification. Create a classifier service from a selected dataset, specify your settings and use this service API endpoint to classify your incoming text.

> Currently Slamby provides `Slamby Twister` as a highly accurate classification algorithm designed for e-commerce market.

### Get Classifier Service
You can get classifier specified information about a classifier service with the Id of the service

*Example REQUEST*
> [GET /api/Services/Classifier/`GUID`](#operation--api-Services-Classifier-get)

*Example RESPONSE*
> HTTP/1.1 200 OK
```JSON
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
3. Provide the tag ids the you are going to use during the training,
4. Start the training process.

> For Training process Slamby is using `Slamby Twister` as its own classification algorithm. 

This request is a long running task so the API do it in async way. Therefore the response is a Process.

> `N-gram seetings`: each dataset has an n-gram setting. For set the required n-gram the minimum value is 1, the maximum value equals with the maximum n-gram number of the given dataset. Using a [1,2,3] n-gram settings means during the training process the classifier is going to create 1,2,3 n-gram dictionaries. [Learn more about N-gram](https://en.wikipedia.org/wiki/N-gram)

*Example REQUEST*
> [POST /api/Services/Classifier/`GUID`/Prepare](#operation--api-Services-Classifier-Prepare-post)
```JSON
{
    "DataSetName" : "test dataset",
    "NGramList": [1,2],
    "TagIdList": ["tag1Id","tag2Id"]
}
```

*Example RESPONSE*
> HTTP/1.1 202 ACCEPTED
```JSON
{
    "Id": "d335edaf-354a-482c-ade4-4d8172f81a40",
    "Start": "2016-03-16T13:12:15.5520625Z",
    "End": "0001-01-01T00:00:00",
    "Percent": 0,
    "Status": "InProgress"
}
```

### Activate Classifier Service
Each service has two status: active, deactive. When a preparation/training process is ready, the service has a deactivated status. A deactivated service is ready, but its not loaded into memory and the API is not able to process the incoming requests. To use a service set the status to Activated. After the activation process the service is ready to use, all the required files are loaded and stored in memory, the API endpoint is active.

*Example REQUEST*
> [POST /api/Services/Classifier/`GUID`/Activate](#operation--api-Services-Classifier-Activate-post)
```JSON
{
    "NGramList": [1,2],
    "EmphasizedTagIdList": null,
    "TagIdList": null
}
```

*Example RESPONSE*
> HTTP/1.1 200 OK

### Deactivate Classifier Service
When a service is not needed for continous usage you can deactivate it. After deactivating a service, all the settings and files remain, but they are not using any resources (memory, cores). You can store your deactivated services and activate them anytime.

*Example REQUEST*
> [POST /api/Services/Classifier/`GUID`/Deactivate](#operation--api-Services-Classifier-Deactivate-post)

*Example RESPONSE*
> HTTP/1.1 200 OK

### Recommend
Built-in text classification engine. Uses the prepared Classifier dictionaries and calculations. High speed and classification capability. Built-in n-gram analyzer.

*Example Request*
> [POST /api/Services/Classifier/`GUID`/Recommend](#operation--api-Services-Classifier-Recommend-post)
```JSON
{
    "Text": "Lorem Ipsum Dolorem",
    "Count": "2",
    "UseEmphasizing": false,
    "NeedTagInResults": true
}
```

*Example Response*
```JSON
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

### Export dictionaries

*Example Request*
> [POST /api/Services/Classifier/`GUID`/ExportDictionaries](#operation--api-Services-Classifier-ExportDictionaries-post)
```JSON
{
    "NGramList": [1],
    "TagIdList": null
}
```

> **Tip:** If you skip the `TagIdList` or set it to `null` than the API will use all the leaf tags

*Example Response*
```JSON
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

### Get Prc Service

*Example REQUEST*
> [GET /api/Services/Prc/`GUID`](#operation--api-Services-Prc-get)

*Example RESPONSE*
> HTTP/1.1 200 OK
```JSON
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
> [POST /api/Services/Prc/`GUID`/Prepare](#operation--api-Services-Prc-Prepare-post)
```JSON
{
    "DataSetName" : "test dataset",
    "TagIdList": ["tag1Id","tag2Id"]
}
```

*Example RESPONSE*
> HTTP/1.1 202 ACCEPTED
```JSON
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

*Example REQUEST*
> [POST /api/Services/Prc/`GUID`/Activate](#operation--api-Services-Prc-Activate-post)
```JSON
{
    "FieldsForRecommendation": ["title"]
}
```

*Example RESPONSE*
> HTTP/1.1 200 OK

### Deactivate Prc Service

*Example REQUEST*
> [POST /api/Services/Prc/`GUID`/Deactivate](#operation--api-Services-Prc-Deactivate-post)

*Example RESPONSE*
> HTTP/1.1 200 OK

### Recommend

*Example Request*
> [POST /api/Services/Prc/`GUID`/Recommend](#operation--api-Services-Prc-Recommend-post)
```JSON
{
    "Text": "Lorem Ipsum Dolorem",
    "Count": "2",
    "Filter": null,
    "Weights": null,
    "TagId": "tag1Id",
    "NeedDocumentInResult": true
}
```

*Example Response*
```JSON
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

### Export dictionaries

*Example Request*
> [POST /api/Services/Prc/`GUID`/ExportDictionaries](#operation--api-Services-Prc-ExportDictionaries-post)
```JSON
{
    "TagIdList": null
}
```

> **Tip:** If you skip the `TagIdList` or set it to `null` than the API will use all the leaf tags

*Example Response*
```JSON
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