## Processes

There are long running tasks in the Slamby API. These requests are served in async way. These methods returns with a Process object.

### Get Process

_Example:_

```cs
var processManager = new ProcessManager(_configuration);
var response = processManager.GetProcessAsync(processId));

```

### Get Processes

If the parameter is `true` then you get all processes, if it's `false` (default), then you get only the active processes.

_Example:_

```cs
var allStatus = true;
var processManager = new ProcessManager(_configuration);
var response = processManager.GetProcessesAsync(allStatus));

```

### Cancel a Process

_Example:_

```cs
var processManager = new ProcessManager(_configuration);
var cancelRespone = await processManager.CancelProcessAsync(process.Id);

```