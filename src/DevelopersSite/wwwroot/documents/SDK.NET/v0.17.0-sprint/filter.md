## Filter

  Powerful **search engine**. Build **smart** search functions or filters. Easily access to your datasets with **simple queries**, **logical expressions** and **wild cards**. Manage your language dependencies using **optimal tokenizer**.

You can find more details about the Filter mechanism [here.](http://developers.slamby.com/api/#filter)

 _Example:_

```cs
var manager = new DocumentManager(configuration, "DATASET_NAME");
var settings = new DocumentFilterSettings {
    Filter = new Filter {
        Query = "title:michelin",
    TagIds = new List<string> { "1" },
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