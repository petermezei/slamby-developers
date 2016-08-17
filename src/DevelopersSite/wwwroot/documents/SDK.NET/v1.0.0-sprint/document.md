## Document

Manage your **documents** easily. Create, edit, remove and running text analysis.

You can find more details about the Documents [here.](/docs/api/{{docversion}}/document)

### Insert New Document

Insert a new document to a dataset using the predefined schema.

_Example:_

```cs
var manager = new DocumentManager(configuration, "DATASET_NAME");
var document = new
            {
                id = "42",
                title = "Example Product Title",
                desc = "Example Product Description",
                tag = [2,7]
            };
var result = await manager.CreateDocumentAsync(document);
```

### Get Document

Get a document from a dataset.

_Example:_

```cs
var manager = new DocumentManager(configuration, "DATASET_NAME");
var result = await manager.GetDocumentAsync("DOCUMENT_ID");
```

### Get Document List

Get document list from a dataset.

_Example:_

```cs
var manager = new DocumentManager(configuration, "DATASET_NAME");
var result = await manager.GetDocumentsAsync();
```

### Edit Document

Edit an existing document in a dataset. Partial document update is allowed. Only modified data should be specified in this case. The rest of the document will be unchanged.
 

_Example:_

```cs
var manager = new DocumentManager(configuration, "DATASET_NAME");
var document = new
            {
                id = "45",
                title = "Example Modified Product Title",
                desc = "Example Modified Product Description",
                tag = "2"
            };
var result = await manager.UpdateDocumentAsync("45", document);
```

### Copy To
Copying documents from a dataset to another one. You can specify the documents by id. You can copy documents to an existing dataset.
The selected documents will **remain in the source dataset** as well.

> **Tip:** You can limit used thread count for this function in the configuration object

_Example:_

```cs
var manager = new DocumentManager(configuration, "DATASET_NAME");
var settings = new DocumentCopySettings()
{
    Ids = new List<string> { "5", "6", "7", "8", "9" },
    TargetDataSetName = "TARGET_DATASET_NAME"
};
var result = await manager.CopyDocumentsToAsync(settings);
```

> **Tip:** You can use the `Documents/Sample` or the `Documents/Filter` methods to get document ids easily.

### Move To

Moving documents from a dataset to another one. You can specify documents by id. You can move documents to an existing dataset. 
The selected documents will be **removed from the source dataset**.

> **Tip:** You can limit used thread count for this function in the configuration object

_Example:_

```cs
var manager = new DocumentManager(configuration, "DATASET_NAME");
var settings = new DocumentMoveSettings()
{
    Ids = new List<string> { "5", "6", "7", "8", "9" },
    TargetDataSetName = "TARGET_DATASET_NAME"
};
var result = await manager.MoveDocumentsToAsync(settings);
```

> **Tip:** You can use the `Documents/Sample` or the `Documents/Filter` methods to get document ids easily.


### Bulk Documents

Inserts mass documents to a dataset using the predefined schema.

> **Tip:** You can limit used thread count for this function in the configuration object

_Example:_

```cs
var settings = new DocumentBulkSettings()
                    {
                        Documents = myNewDocumentsList
                    };
var manager = new DocumentManager(configuration, "DATASET_NAME");
var result = await manager.BulkDocumentsAsync(settings);
```