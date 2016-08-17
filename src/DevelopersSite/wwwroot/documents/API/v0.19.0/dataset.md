## Dataset
Slamby provides **Dataset** as a data storage. A dataset is a JSON document storage that allows to store schema free JSON objects, indexes and additional parameters. Inside your server you can create and manage multiple datasets.

With dataset you can:
* Create multiple datasets
* Using schema free JSON objects
* Set indexes for text processing
* Running text analysis on the stored data

> **Tip:** With schema free JSON storage you can easily store your existing schemas. Store document related data - such as image urls, prices - and build powerful queries.

### Create new Dataset

Create a new dataset by providing a sample JSON document and additional parameters.

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

Create a new dataset by providing a schema JSON document and additional parameters.

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
`epoch_millis`    |   A formatter for the number of milliseconds since the epoch. Note, that this timestamp allows a max length of 13 chars, so only dates between 1653 and 2286 are supported. You should use a different date formatter in that case. 
`epoch_second`    |   A formatter for the number of seconds since the epoch. Note, that this timestamp allows a max length of 10 chars, so only dates between 1653 and 2286 are supported. You should use a different date formatter in that case. 
`date_optional_time` or `strict_date_optional_time` |    A generic ISO datetime parser where the date is mandatory and the time is optional.
`basic_date`  |   A basic formatter for a full date as four digit year, two digit month of year, and two digit day of month: yyyyMMdd.
`basic_date_time` |   A basic formatter that combines a basic date and time, separated by a T: yyyyMMdd'T'HHmmss.SSSZ.
`basic_date_time_no_millis`   |   A basic formatter that combines a basic date and time without millis, separated by a T: yyyyMMdd'T'HHmmssZ. 
`basic_ordinal_date`  |   A formatter for a full ordinal date, using a four digit year and three digit dayOfYear: yyyyDDD. 
...

#### Document Format

Property type must be set to `attachment` at DataSet schema creation. That is why it can be achieved via schema and not sample document definition. Content of this field must contain `base64` encoded binary content of a document such as .pdf, .doc. Uploaded document text will be extracted and wil be used when this field is used in Classifier or Prc services.

##### Supported file formats

Behind parsing documents there is an Apache Tika which provides extracted text. It supports wide variety of document formats. For detailed supported format list please visit [Apache Tika format page](http://tika.apache.org/1.13/formats.html).

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

##### Check the DataSet schema definition [here](#/definitions/DataSet)

### Update Dataset

Updates a dataset. Currently only updating Dataset name is possible. As Dataset names are unique it will return with an error if name is taken by another Dataset. 

*Example REQUEST*

> [PUT /api/DataSets](#operation--api-DataSets-put))
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