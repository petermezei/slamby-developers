## Statistics

Get simple usage statistics of the API.

_Example:_

```cs
var statisticsManager = new StatisticsManager(_configuration);
var resultStat = await statisticsManager.GetStatisticsAsync(2017);
```