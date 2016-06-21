## Tags

Manage tags to organize your data. Using tags create a tag cloud or a hierarchical tag tree.

You can find more details about the Tags [here](http://developers.slamby.com/api/#tags).

### Create New Tag

Create a new tag in a dataset.

_Example:_

```cs
var manager = new TagManager(configuration, "DATASET_NAME");
var tag = new Tag
            {
                Id = "3",
                Name = "example tag 1",
                ParentId = null
            };
var result = await manager.CreateTagAsync(tag);
```

### Get Tag

Get a tag by its Id.

_Example:_

```cs
var manager = new TagManager(configuration, "DATASET_NAME");
var result = await manager.GetTagAsync("3");
```

### Get Tag List

Get all tags list from a given dataset.

_Example:_

```cs
var manager = new TagManager(configuration, "DATASET_NAME");
var result = await manager.GetTagsAsync();
```

### Update Tag

Update a tag by new values.

_Example:_

```cs
var manager = new TagManager(configuration, "DATASET_NAME");
var tag = new Tag
            {
                Id = "5",
                Name = "tag2"
            };
var result = await manager.UpdateTagAsync("3", tag);
```

> **Tip:** You can also change the `Id` of the object, as you can see in the example.

### Remove Tag

Remove a tag from tag list. By default, documents and datasets are not affected.
The method have two additional parameter:

- `force` - if true then the tag with children can be deleted, but be careful! All the children will be deleted also.
- `cleanDocuments` - if true then the removed tag will be also removed from the documents

_Example:_

```cs
var manager = new TagManager(configuration, "DATASET_NAME");
var result = await manager.DeleteTagAsync("5");
```

### Bulk Tag

Replaces existing tags with the provided list. Recommended for tag list initalizing.

> **Tip:** You can limit used thread count for this function in the configuration object

_Example:_

```cs
var settings = new TagBulkSettings()
                    {
                        Tags = myImportTagList
                    };
var manager = new TagManager(configuration, "DATASET_NAME");
var result = await manager.BulkTagsAsync(settings);
```

### Clean Documents

Remove all not existing tags from the documents.

_Example:_

```cs
var manager = new TagManager(configuration, "DATASET_NAME");
var result = await manager.CleanDocumentsAsync();
```

### Words Occurences

Get words and the occurences of the words.


_Example:_

```cs
var manager = new TagManager(configuration, "DATASET_NAME");
var settings = new TagsExportWordsSettings {
    NGramList = new List<int> { 1, 2 },
    TagIdList = new List<string> { "123", "44" }
};
var resultProcess = await manager.WordsExportAsync(settings);
```