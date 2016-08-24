## Dataset

Slamby provides **Dataset** as a data storage. A dataset is a JSON document storage that allows storing schema-free JSON objects, indexes, and additional parameters. Inside your server, you can create and manage multiple datasets.

With dataset you can:
* Create multiple datasets
* Using schema-free JSON objects
* Set indexes for text processing
* Running text analysis on the stored data
* Run search queries

> **Tip:** With schema-free JSON storage you can easily use your existing data structures. Store item related data - such as image URLs, prices - and build powerful queries.

### Create new Dataset

You have two options to create a dataset. Define your data structure by sample JSON document, or use document Schema.

**Create dataset using sample JSON document**

Using sample JSON document is the easiest way to create a dataset. Just simply write a sample document that contains all  the necessary fields and sample values that show the expected field type.

In a sample document you can use the following types:
* Integer by using a number. `eg.: "id":9`
* String by using a string value. `eg.: "name":"Anakin Skywalker"`
* List by using a JSON list. `eg.: "tags":[1,2,3]`
* Object by using a JSON object. `eg.: "details":{...}`

*Example REQUEST*

> [POST /api/DataSets](#operation--api-DataSets-post)
```JSON
{
    "IdField": "id",
    "InterpretedFields": ["title", "desc"],
    "Name": "test1",
    "NGramCount": "3",
    "TagField": "tag",
    "SampleDocument": {
        "id": 9,
        "title": "Example Product Title",
        "desc": "Example Product Description",
        "tag": [1,2,3]
    }
}
```

`Available settings to create a dataset:`

Name    |   Default value   |   Description
--- |   --- |   ---
Name    |   empty   |   Name of the dataset. You can use a-z and 0-9. You can rename the dataset in the future.
NgramCount  |   3   |   Ngram settings. This number shows the n-gram number that is used by the dataset. We recommend using 3 as a value. Higher n-gram count means more precise text analysis, but higher server capacity.
IdField |   id  |   Which field is used as an id field in your sample document. The id field is required.
TagField    |   tag |   Which field is used as a tag field in your sample document. The tag field is required.
InterpretedFileds   |   title, desc  |   Interpreted fields are fields that we would like to use for text analysis in the future. We recommend using each text contained fields as interpreted fields.
SampleDocument  |   {...}   |   To create a dataset you can use a JSON sample document. In the sample, JSON document uses all the required fields and provide a required type. If you know perfectly which field types you would like to use please use DocumentSchema option and use directly schema.

**Create dataset using document Schema**

Using document Schema you can specify your required field types.

*Example REQUEST*

> [POST /api/DataSets/Schema](#operation--api-DataSets-Schema-post)
```JSON
{
    "IdField": "id",
    "InterpretedFields": [
        "title",
        "desc"
    ],
    "Name": "test2",
    "NGramCount": "3",
    "TagField": "tag",
    "Schema": {
        "type": "object",
        "properties": {
            "id": {
                "type": "integer"
            },
            "title": {
                "type": "string"
            },
            "desc": {
                "type": "string"
            },
            "tag": {
                "type": "array",
                "items": {
                    "type": "byte"
                }
            }
        }
    }
}
```

> `Tip:` for available field types check the following `Data Types` section.

*Example RESPONSE*
>HTTP/1.1 201 CREATED

##### Check the DataSet schema definition [here](#/definitions/DataSet)

### Data Types

Defining a dataset schema you can set your custom field type.

*Currently available field types:*

Name    |   Types
--- |   ---
String  |   `string`
Numeric |   `long`, `integer`, `short`, `byte`, `double`, `float`
Date    |   `date`
Boolean |   `boolean`
Array   |   `array`
Object  |   `object` for single JSON objects
Document|   `attachment` accepts valid `base64` encoded string

*Example schema*

```JSON
{
    "type": "object",
    "properties": {
        "name": {
            "type": "object",
            "properties": {
                "firstName": {
                    "type": "string"
                },
                "secondName": {
                    "type": "string"
                }
            },
        "age": {
            "type":"integer"
        },
        "sex": {
            "type":"boolean"
        },
        "luckyNumbers": {
            "type": "array",
            "items": {
                "type": "integer"
            }
        }
    }
}
```

#### Date Formats

You can define your custom date format to specify your needs.

For dataset date formats you can use the built-in [elastic-search custom formats](https://www.elastic.co/guide/en/elasticsearch/reference/2.2/mapping-date-format.html).

If you do not provide date format, default value is `"strict_date_optional_time||epoch_millis"`.

**Built in formats (excerpt)**

name    |   Description
--- |   ---
`epoch_millis`    |   A formatted for the number of milliseconds since the epoch. Note, that this timestamp allows a max length of 13 chars, so only dates between 1653 and 2286 are supported. You should use a different date formatter in that case. 
`epoch_second`    |   A formatter for the number of seconds since the epoch. Note, that this timestamp allows a max length of 10 chars, so only dates between 1653 and 2286 are supported. You should use a different date formatter in that case. 
`date_optional_time` or `strict_date_optional_time` |    A generic ISO DateTime parser where the date is mandatory and the time is optional.
`basic_date`  |   A basic formatter for a full date as four digit year, two digit month of a year, and two digit day of the month: yyyyMMdd.
`basic_date_time` |   A basic formatter that combines a basic date and time, separated by a T: yyyyMMdd'T'HHmmss.SSSZ.
`basic_date_time_no_millis`   |   A basic formatter that combines a basic date and time without millisecond, separated by a T: yyyyMMdd'T'HHmmssZ. 
`basic_ordinal_date`  |   A formatter for a full ordinal date, using a four digit year and three digit dayOfYear: yyyyDDD. 
...

#### Document Format

Property type must be set to `attachment` at DataSet schema creation. That is why it can be achieved via schema and not sample document definition. The content of this field must contain `base64` encoded binary content of a document such as .pdf, .doc. Uploaded document text will be extracted and will be used when this field is used in Classifier or Prc services.

##### Supported file formats

Behind parsing documents, there is an Apache Tika which provides extracted text. It supports a wide variety of document formats. For detailed supported format list please visit [Apache Tika format page](http://tika.apache.org/1.13/formats.html).

### Get Dataset

Get information about a given dataset. A dataset can be accessed by its name.

Returns with:

* Dataset basic information
* Dataset settings
* Schema sample document
* Dataset statistics

*Example REQUEST*

> [GET /api/DataSets/`example`](#operation--api-DataSets-get)

*Example RESPONSE*

> HTTP/1.1 200 OK
```JSON
{
    "Name": "example",
    "NGramCount": 3,
    "IdField": "id",
    "TagField": "tag",
    "InterpretedFields": [
        "title",
        "desc"
    ],
    "Statistics": {
        "DocumentsCount": 3
    },
    "SampleDocument": {
        "id": 1,
        "title": "Example title",
        "desc": "Example Description",
        "tag": [1,2,3]
    }
}
```

##### Check the DataSet schema definition [here](#/definitions/DataSet)

### Get Dataset List

Get a list of the available datasets.

Returns with:
* Dataset objects array

*Example REQUEST*

> [GET /api/DataSets](#operation--api-DataSets-get)

*Example RESPONSE*

> HTTP/1.1 200 OK
```JSON
[
    {
    "Name": "example",
    "NGramCount": 3,
    "IdField": "id",
    "TagField": "tags",
    "InterpretedFields": [
        "title",
        "desc"
    ],
    "Statistics": {
        "DocumentsCount": 3
    },
    "SampleDocument": {
        "id": 1,
        "title": "Example title",
        "desc": "Example Description",
        "tags": [1,2,3]
    }
    },
    {
    "Name": "example2",
    "NGramCount": 3,
    "IdField": "id",
    "TagField": "tags",
    "InterpretedFields": [
        "title",
        "desc"
    ],
    "Statistics": {
        "DocumentsCount": 3
    },
    "SampleDocument": {
        "id": 1,
        "title": "Example title",
        "desc": "Example Description",
        "tags": [1,2,3]
    }
    }
]
```

`Available fields in the dataset object`:

Name    |   Description
--- |   ---
Name    |   Dataset name (current)
NGramCount  |   Ngram settings of the given dataset.
IdField |   IdField of the given schema or sample document
TagField    |   Tagfield of the given schema or sample document
InterpretedFields   |   Interpreted fields of the given schema or sample document
Statistics  |   Dataset statistics. Basic field is DocumentsCount that shows the current document number of the given dataset
SampleDocument  |   Sample document when we used sample document to create a dataset. When we used schema to create it, SampleDocument value is null
Schema  |   Schema document when we used document Schema to create this dataset. When we used sample document to create it, Schema value is null

##### Check the DataSet schema definition [here](#/definitions/DataSet)

### Update Dataset

Updates a dataset. Currently only updating Dataset name is possible. As Dataset names are unique it will return with an error if the name is taken by another Dataset. 

*Example REQUEST*

> [PUT /api/DataSets/`example`](#operation--api-DataSets-put))
```JSON
{
    "Name": "new dataset name"
}
```

*Example RESPONSE*
>HTTP/1.1 200 OK


### Remove Dataset
Removes a given dataset. All the stored data will be removed.

*Example REQUEST*
> [DELETE /api/DataSets/`example`](#operation--api-DataSets-delete)

*Example RESPONSE*
> HTTP/1.1 200 OK