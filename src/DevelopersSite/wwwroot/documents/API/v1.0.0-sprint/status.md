## Status

*Example REQUEST*

> [GET /api/Status](#operation--api-Status-get))

*Example RESPONSE*
> HTTP/1.1 200 OK
```JSON
{
    "ProcessorCount": 4,
    "AvailableFreeSpace": 47895.53,
    "ApiVersion": "0.14.0",
    "CpuUsage": 0.6,
    "TotalMemory": 996.08,
    "FreeMemory": 36.3
}
```

`Available status parameters`

Name    |   Description
--- |   ---
ApiVersion  |   Slamby Server API version number.
ProcessorCount  |   Integer. Available processor thread number in your Slamby Server.
CpuUsage    |   Actual CPU usage in percentage. 5 sec average value.
TotalMemory |   Available memory size in MB.
FreeMemory  |   Currently available free memory in MB. 5 sec average value.
AvailableFreeSpace  |   Available free storage space in MB. 5 sec average value.