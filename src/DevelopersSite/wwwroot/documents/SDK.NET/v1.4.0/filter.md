## Filter

  Powerful **search engine**. Build **smart** search functions or filters. Easily access to your datasets with **simple queries**, **logical expressions** and **wild cards**. Manage your language dependencies using **optimal tokenizer**.

You can find more details about the Filter mechanism [here](/docs/api/{{docversion}}/filter).

 _Example:_

```cs
var manager = new DocumentManager(configuration, "DATASET_NAME");
var settings = new DocumentFilterSettings {
    Filter = new Filter {
        Query = "title:michelin",
        TagIdList = new List<string> { "1" },
    },
    Order = new Order {
        OrderByField = "title",
        OrderDirection = OrderDirectionEnum.Asc
    },
    Pagination = new Pagination {
        Limit = 100
    },
    FieldList = new List<string> { "*" }
};
var result = await manager.GetFilteredDocumentsAsync(settings, null);

// Get next items 
var nextResult = await manager.GetFilteredDocumentsAsync(null, result.ScrollId);
```