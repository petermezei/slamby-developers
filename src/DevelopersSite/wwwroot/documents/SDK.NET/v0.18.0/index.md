# Slamby SDK .NET v0.18

Slamby .NET SDK and NuGet Package.
This project is open source. Please check the documentation and [join](http://www.slamby.com/Community) to the community group.

GitHub Slamby page: [www.github.com/slamby](https://github.com/slamby)
GitHub page:  [www.github.com/slamby/slamby-sdk-net](https://github.com/slamby/slamby-sdk-net)

## Changelog

### Features

- modified DocumentManagent GetFilteredDocumentsAsync function to handle `ScrollId`
- `[BREAKING CHANGES]` changed the following models: 
    - Order 
    - Pagination 
    - PaginatedList
    - DocumentSampleSettings
    - DocumentFilterSettings

---

## General

### Request Basics

Configuration example:

```cs
var configuration = new Configuration
    {
        ApiBaseEndpoint = new Uri("https://api.slamby.com/CLIENT_ID/"),
        ApiSecret = "API_KEY"
    };
```

You have to use this `configuration` object for every `Manager`.

You can find more details about the Authentication [here](/docs/api/{{docversion}}/index#authentication)

Slamby SDK.NET sends its version information to the API for version matching. Major and minor values should match in order to prevent version incompatibility. 

`ParallelLimit` property enables you to limit or maximize CPU usage in certain functions. Limit value should be greater than zero in order to be sent.

> **Note:** In some cases API can limit your value if your configuration cannot handle it efficiently

### Response Basics

Every request returns one of the following results:

```cs
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

```cs
IRawMessageSubscriber subscriber = new DebugSubscriber();
RawMessagePublisher.Instance.AddSubscriber(subscriber);

// API calls

RawMessagePublisher.Instance.RemoveSubscriber(subscriber);
```

_Output:_

```cs
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

### Async methods

In all the `Manager` class there are async methods. If you would like to use in a synchronized way, you can get the `Result` object of the task.

_Example:_

```cs
var dataset = manager.GetDataSetAsync().Result;
```
