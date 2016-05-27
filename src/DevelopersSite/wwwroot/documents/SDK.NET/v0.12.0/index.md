#Slamby SDK .NET
Slamby .NET SDK and Nuget Package.
This project is open source. Please check the documentation and [join](http://www.slamby.com/Community) to the community group.

Github Slamby page: [www.github.com/slamby](https://github.com/slamby)
Github page:  [www.github.com/slamby/slamby-sdk-net](https://github.com/slamby/slamby-sdk-net)

## Changelog
### Features
- access the API version through the ClientResponse -> ApiVersion property
- change Tag Path property type to `List<PathItem>`
- TagManager -> DeleteTagAsync method new parameters: `force` and `cleanDocuments`
- ServiceManager
- ClassifierServiceManager
- ProcessesManager
- change: `Task<ClientResponseWithObject<IEnumerable<Tag>>> GetTagsAsync(bool withDetails = false);`
- change: `Task<ClientResponseWithObject<Tag>> GetTagAsync(string tagId, bool withDetails = false);`
- bulk tag import:  TagManager -> BulkTagsAsync
- remove non-existent Tags from Documents: TagManager -> CleanDocumentsAsync
- version information send

---


##General

### Request Basics
Configuration example:
```
var configuration = new Configuration
	{
	    ApiBaseEndpoint = new Uri("https://api.slamby.com/CLIENT_ID/"),
		ApiSecret = "API_KEY"
	};
```
You have to use this `configuration` object for every `Manager`.

You can find more details about the Authentication [here](http://developers.slamby.com/api/#authentication)

Slamby SDK.NET sends its version information to the API for version matching. Major and minor values should match in order to prevent version incompatibility. 

### Response Basics

Every request returns one of the following results:

```
public class ClientResponse
{
    public bool IsSuccessFul { get; set; }
    public HttpStatusCode HttpStatusCode { get; set; }
    public string ServerMessage { get; set; }
    public ErrorsModel Errors { get; set; }
    public string ApiVersion { get; set; }
}

public class ClientResponseWithObject<T> : ClientResponse
{
    public T ResponseObject { get; set; }
}
```

### Logging

Logging raw request and response message with `RawMessagePublisher`. 
Currently `DebugSubscriber` is available  which writes messages to **debug output**. Custom subscribers can be created via implementing `IRawMessageSubscriber` interface.

_Example:_
```
IRawMessageSubscriber subscriber = new DebugSubscriber();
RawMessagePublisher.Instance.AddSubscriber(subscriber);

// API calls

RawMessagePublisher.Instance.RemoveSubscriber(subscriber);
```

_Output:_
```
REQUEST #63592531131529252663115
----------------------
POST http://localhost:29689/api/tags
Headers:
Accept|application/json
Authorization|Slamby API_KEY
X-DataSet|test1

Content:  
{"Id":"999","Name":"tag","ParentTagId":null,"Properties":null}
```

###Async methods

In all the `Manager` class there are async methods. If you would like to use in a synchronized way, you can get the `Result` object of the task.

_Example:_

```
var dataset = manager.GetDataSetAsync().Result;
```

## Dataset
Slamby provides **Dataset** as a data storage. A dataset is a JSON document storage that allows to store schema free JSON objects, indexes and additional parameters. Inside your server you can create and manage multiple datasets.

You can find more details about the Datasets [here.](http://developers.slamby.com/api/#datasets)

### Create new Dataset

Create a new dataset by providing a sample JSON document and additional parameters.

_Example:_

```
var manager = new DataSetManager(configuration);
var dataset = new Models.DataSet
            {
                IdField = "id",
                Name = "name",
                NGramCount = 2,
                InterpretedFields = new List<string> { "title", "desc" },
                TagField = "tag",
                SampleDocument = new
                {
                    id = 10,
                    title = "Example Product Title",
                    desc = "Example Product Description",
                    tag = [1,2,3]
                }
            }
var response = await manager.CreateDataSetAsync(dataset);
if (!response.IsSuccessFul)
{
	// handle error with the help of the Errors property in the response
}
```	

### Get Dataset

Get information about a given dataset. A dataset can be accessed by its name.

_Example:_

```
var manager = new DataSetManager(configuration);
var dataset = await manager.GetDataSetAsync();
```	

### Get Dataset List

Get a list of the available datasets.

_Example:_

```
var manager = new DataSetManager(configuration);
var datasets = await manager.GetDataSetsAsync();
```	

### Remove Dataset

Removes a given dataset. All the stored data will be removed.

_Example:_

```
var manager = new DataSetManager(configuration);
var result = await manager.DeleteDataSetAsync("DATASET_NAME");
```	

## Document

Manage your **documents** easily. Create, edit, remove and running text analysis.

You can find more details about the Documents [here.](http://developers.slamby.com/api/#documents)

### Insert New Document

Insert a new document to a dataset using the predefined schema.

_Example:_

```
var manager = new DocumentManager(configuration, "DATASET_NAME");
var document = new
            {
                id = "42",
                title = "Example Product Title",
                desc = "Example Product Description",
                tag = [2,7]
            };
var result = await manager.CreateDocumentAsync(document);
```	

### Get Document

Get a document from a dataset.

_Example:_

```
var manager = new DocumentManager(configuration, "DATASET_NAME");
var result = await manager.GetDocumentAsync("DOCUMENT_ID");
```	

### Get Document List

Get a document from a dataset.

_Example:_

```
var manager = new DocumentManager(configuration, "DATASET_NAME");
var result = await manager.GetDocumentsAsync();
```	

### Edit Document

Edit an existing document in a dataset.

_Example:_

```
var manager = new DocumentManager(configuration, "DATASET_NAME");
var document = new
            {
                id = "45",
                title = "Example Modified Product Title",
                desc = "Example Modified Product Description",
                tag = "2"
            };
var result = await manager.UpdateDocumentAsync("45", document);
```	

### Copy To
Copying documents from a dataset to another one. You can specify the documents by id. You can copy documents to an existing dataset.
The selected documents will **remain in the source dataset** as well.

_Example:_

```
var manager = new DocumentManager(configuration, "DATASET_NAME");
var settings = new DocumentCopySettings()
{
    Ids = new List<string> { "5", "6", "7", "8", "9" },
    TargetDataSetName = "TARGET_DATASET_NAME"
};
var result = await manager.CopyDocumentsToAsync(settings);
```	

> **Tip:** You can use the `Documents/Sample` or the `Documents/Filter` methods to get document ids easily.

### Move To

Moving documents from a dataset to another one. You can specify documents by id. You can move documents to an existing dataset. 
The selected documents will be **removed from the source dataset**.

_Example:_

```
var manager = new DocumentManager(configuration, "DATASET_NAME");
var settings = new DocumentMoveSettings()
{
    Ids = new List<string> { "5", "6", "7", "8", "9" },
    TargetDataSetName = "TARGET_DATASET_NAME"
};
var result = await manager.MoveDocumentsToAsync(settings);
```	

> **Tip:** You can use the `Documents/Sample` or the `Documents/Filter` methods to get document ids easily.


## Tags

Manage tags to organize your data. Using tags create a tag cloud or a hierarchical tag tree.

You can find more details about the Tags [here.](http://developers.slamby.com/api/#tags)

### Create New Tag

Create a new tag in a dataset.

_Example:_

```
var manager = new TagManager(configuration, "DATASET_NAME");
var tag = new Tag
            {
                Id = "3",
                Name = "example tag 1",
				ParentId = null
            };
var result = await manager.CreateTagAsync(tag);
```	

### Get Tag

Get a tag by its Id.

_Example:_

```
var manager = new TagManager(configuration, "DATASET_NAME");
var result = await manager.GetTagAsync("3");
```	

### Get Tag List

Get all tags list from a given dataset.

_Example:_

```
var manager = new TagManager(configuration, "DATASET_NAME");
var result = await manager.GetTagsAsync();
```

### Update Tag

_Example:_

```
var manager = new TagManager(configuration, "DATASET_NAME");
var tag = new Tag
            {
                Id = "5",
                Name = "tag2"
            };
var result = await manager.UpdateTagAsync("3", tag);
```

> **Tip:** You can also change the `Id` of the object, as you can see in the example.

### Remove Tag

Remove a tag from tag list. By default, documents and datasets are not affected.
The method have two additional parameter:
- `force` - if true then the tag with children can be deleted, but be careful! All the children will be deleted also.
- `cleanDocuments` - if true then the removed tag will be also removed from the documents

_Example:_

```
var manager = new TagManager(configuration, "DATASET_NAME");
var result = await manager.DeleteTagAsync("5");
```

### Clean Documents

Remove all not existing tags from the documents.

_Example:_

```
var manager = new TagManager(configuration, "DATASET_NAME");
var result = await manager.CleanDocumentsAsync();
```

## Sampling

Statistical method to support sampling activity. Using sampling you can easily create **random samples** for experiments.
  
You can find more details about the Sampling mechanism [here.](http://developers.slamby.com/api/#sampling)
  
_Example:_
```
var manager = new DocumentManager(configuration, "DATASET_NAME");
var settings = new DocumentSampleSettings {
              Id = Guid.NewGuid().ToString(),
              IsStratified = false,
              Percent = 0,
              Size = 15000,
              TagIds = new List<string>(),
              Pagination = new Pagination {
                  Offset = 0,
                  Limit = 100,
                  OrderDirection = OrderDirectionEnum.Asc,
                  OrderByField = "id"
              }
};
var result = await manager.GetSampleDocumentsAsync(settings);
```
  

## Filter
  Powerful **search engine**. Build **smart** search functions or filters. Easily access to your datasets with **simple queries**, **logical expressions** and **wild cards**. Manage your language dependencies using **optimal tokenizer**.
  
You can find more details about the Filter mechanism [here.](http://developers.slamby.com/api/#filter)
  
 _Example:_

```
var manager = new DocumentManager(configuration, "DATASET_NAME");
var settings = new DocumentFilterSettings {
    TagIds = new List<string> { "1" },
    QueryDictionary = new Dictionary<string, string> {
        { "title", "michelin" }
    },
    Pagination = new Pagination {
        Limit = 100,
        Offset = 0,
        OrderByField = "title",
        OrderDirection = OrderDirectionEnum.Asc
    }
};
var result = await manager.GetFilteredDocumentsAsync(settings);
```

## Processes
There are long running tasks in the Slamby API. These requests are served in async way. These methods returns with a Process object.

### Get Process

_Example:_

```
var processManager = new ProcessManager(_configuration);
var response = processManager.GetProcessAsync(processId))

```

### Cancel a Process

_Example:_

```
var processManager = new ProcessManager(_configuration);
var cancelRespone = await processManager.CancelProcessAsync(process.Id);

```



## Services
You can quickly create a data processing service from the available service templates. Manage your data processing with services, run different tests, run more data management services parallelly.

### Create Service

_Example:_

```
var service = new Service()
            {
                Name = "Test service",
                Description = "This is a test service",
                Type = ServiceTypeEnum.Classifier
            };
var result = await serviceManager.CreateServiceAsync(service);
var createdService = result.ResponseObject;

```

> **Tip:** The ID of the Service is generated by the API.

### Get Service List

_Example:_

```
var serviceManager = new ServiceManager(_configuration);
var servicesResponse = await serviceManager.GetServicesAsync();

```

### Get Service

_Example:_

```
var serviceManager = new ServiceManager(_configuration);
var serviceResponse = await serviceManager.GetServiceAsync(serviceId);
```



### Update Service

You can modify only the `Name` and `Description` properties.

_Example:_
```
var serviceManager = new ServiceManager(_configuration);
createdService.Name = "Modified name";
createdService.Description = "Modified description";
var modifyResponse = await serviceManager.UpdateServiceAsync(createdService.Id, createdService);
```

### Delete Service

_Example:_
```
var serviceManager = new ServiceManager(_configuration);
var deleteResponse = await serviceManager.DeleteServiceAsync(createdService.Id);
```

## Classifier Services
Service for text classification. Create a classifier service from a selected dataset, specify your settings and use this service API endpoint to classify your incoming text.

### Get Classifier Service

_Example:_
```
var classifierServiceManager = new ClassifierServiceManager(_configuration);
var classifierServiceResponse = await classifierServiceManager.GetServiceAsync(serviceId);
```

### Prepare Classifier Service


_Example:_
```
var classifierServiceManager = new ClassifierServiceManager(_configuration);
var classifierPrepareSettings = new ClassifierPrepareSettings()
        {
            DataSetName = "example_dataset",
            NGramList = new List<int>() { 1, 2 }
        };
var prepareResponse = await classifierServiceManager.PrepareServiceAsync(serviceId, classifierPrepareSettings);
var process = prepareResponse.ResponseObject;
```


### Activate Classifier Service

_Example:_
```
var classifierServiceManager = new ClassifierServiceManager(_configuration);
var classifierActivateSettings = new Models.Services.ClassifierActivateSettings()
{
    NGramList = new List<int>() { 1 }
};
var activateResponse = await classifierServiceManager.ActivateServiceAsync(serviceId, classifierActivateSettings);
```

### Deactivate Classifier Service

_Example:_
```
var classifierServiceManager = new ClassifierServiceManager(_configuration);
deactivateResponse = await classifierServiceManager.DeactivateServiceAsync(classifierServiceId);
```

### Classifier Service Recommendation

_Example:_
```
var classifierServiceManager = new ClassifierServiceManager(_configuration);
var classifierRecommendationRequest = new Models.Services.ClassifierRecommendationRequest()
    {
        Text = "categorize this text",
        NeedTagInResult = true,
        Count = 5
    };
var recommendResponse = (await classifierServiceManager.RecommendServiceAsync(serviceId, classifierRecommendationRequest));
```

