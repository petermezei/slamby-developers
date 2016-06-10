## Sampling

Statistical method to support sampling activity. Using sampling you can easily create **random samples** for experiments.

You can find more details about the Sampling mechanism [here.](http://developers.slamby.com/api/#sampling)

_Example:_

```cs
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