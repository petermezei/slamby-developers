## Sampling

Statistical method to support sampling activity. Using sampling you can easily create **random samples** for experiments.

You can find more details about the Sampling mechanism [here](/docs/API/{{docversion}}/sampling).

_Example:_

```cs
var manager = new DocumentManager(configuration, "DATASET_NAME");
var settings = new DocumentSampleSettings {
    Id = Guid.NewGuid().ToString(),
    IsStratified = false,
    Percent = 0,
    Size = 1500,
    TagIds = new List<string> { "1", "2", "5" },
    Fields = new List<string> { "id", "name" }
};
var result = await manager.GetSampleDocumentsAsync(settings);
```