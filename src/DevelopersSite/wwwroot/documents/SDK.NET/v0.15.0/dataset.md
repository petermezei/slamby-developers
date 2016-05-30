## Dataset

Slamby provides **Dataset** as a data storage. A dataset is a JSON document storage that allows to store schema free JSON objects, indexes and additional parameters. Inside your server you can create and manage multiple datasets.

You can find more details about the Datasets [here.](http://developers.slamby.com/api/#datasets)

### Create new Dataset

Create a new dataset by providing a sample JSON document and additional parameters.

_Example:_

```cs
var manager = new DataSetManager(configuration);
var dataset = new Models.DataSet
            {
                IdField = "id",
                Name = "name",
                NGramCount = 2,
                InterpretedFields = new List<string> { "title", "desc" },
                TagField = "tag",
                SampleDocument = new
                {
                    id = 10,
                    title = "Example Product Title",
                    desc = "Example Product Description",
                    tag = [1,2,3]
                }
            }
var response = await manager.CreateDataSetAsync(dataset);
if (!response.IsSuccessFul)
{
    // handle error with the help of the Errors property in the response
}
```	

Create a new dataset by providing a JSON schema and additional parameters.

_Example:_

```cs
var manager = new DataSetManager(configuration);
var dataset = new Models.DataSet
            {
                IdField = "id",
                Name = "name",
                NGramCount = 2,
                InterpretedFields = new List<string> { "title", "desc" },
                TagField = "tag",
                Schema = new
                {
                    id = new 
                    {
                        type = "integer"
                    },
                    title = new 
                    {
                        type = "string"
                    },
                    desc =  new 
                    {
                        type = "string"
                    },
                    tag =  new 
                    {
                        type = "array",
                        items = new
                        {
                            type = "byte"
                        }
                    }
                }
            }
var response = await manager.CreateDataSchemaSetAsync(dataset);
if (!response.IsSuccessFul)
{
	// handle error with the help of the Errors property in the response
}
```	

### Get Dataset

Get information about a given dataset. A dataset can be accessed by its name.

_Example:_

```cs
var manager = new DataSetManager(configuration);
var dataset = await manager.GetDataSetAsync();
```

### Get Dataset List

Get a list of the available datasets.

_Example:_

```cs
var manager = new DataSetManager(configuration);
var datasets = await manager.GetDataSetsAsync();
```

### Remove Dataset

Removes a given dataset. All the stored data will be removed.

_Example:_

```cs
var manager = new DataSetManager(configuration);
var result = await manager.DeleteDataSetAsync("DATASET_NAME");
```