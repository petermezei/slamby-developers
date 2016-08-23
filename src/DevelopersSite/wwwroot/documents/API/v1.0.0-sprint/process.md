## Processes

There are long running tasks in the Slamby API. These requests are served in an async way. These methods return with `HTTP/1.1 202 ACCEPTED`and with a Process object.

> **Tip:** You can cancel a process anytime during its progress

### Get Active Process List

Get all running processes.

*Example REQUEST*

> [GET /api/Processes/](#operation--api-Processes-get)

*Example RESPONSE*

> HTTP/1.1 200 OK

```JSON
[
  {
    "Id": "abaa9144-2b1b-4539-a322-4157b8bde3f2",
    "Start": "2016-08-23T15:57:53.292296Z",
    "End": "0001-01-01T00:00:00",
    "Percent": 14.07,
    "Description": "Exporting dictionaries from Classifier service Yoda.",
    "Status": "InProgress",
    "Type": "ClassifierExportDictionaries",
    "ErrorMessages": [],
    "ResultMessage": null
  }
]
```

To get historical process list use `allStatus=true` URL parameter.

### Get-Process information

Get process details by its Id.

*Example REQUEST*

> [GET /api/Processes/`GUID`](#operation--api-Processes-get)

*Example RESPONSE*

> HTTP/1.1 200 OK

```JSON
{
    "Id": "958c1bdd-cd21-48f6-b9ec-c232271adec5",
    "Start": "2016-04-18T16:04:04.2156558Z",
    "End": "0001-01-01T00:00:00",
    "Percent": 0,
    "Description": "Exporting words from 1 tag(s) of dataset test...",
    "Status": "InProgress",
    "Type": "TagsExportWords",
    "ErrorMessages": [],
    "ResultMessage": null
}
```

### Cancel Process

Cancel the process. Only Process with status `InProgress` can be canceled. The method returns with `HTTP/1.1 202 ACCEPTED` because it can take time to cancel a process. After it's accepted, the status of the process will be `Cancelling`. You have to check periodically that whether the process status is already `Canceled`.

*Example REQUEST*

> [POST /api/Processes/`GUID`/Cancel](#operation--api-Processes-Cancel-post)

*Example RESPONSE*

> HTTP/1.1 202 ACCEPTED

##### For the parameters explanation check the Process schema definition [here](#/definitions/Process)