## Status

Returns with version, processor, memory and disk information about the API server.

_Example:_

```cs
var statusManager = new StatusManager(_configuration);
var resultStatus = await statusManager.GetStatusAsync();
```