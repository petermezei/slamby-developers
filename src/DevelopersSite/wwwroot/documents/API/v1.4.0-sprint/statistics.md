## Statistics

Get simple usage statistics of the API.

*Example REQUEST*

> [GET /api/Statictics/2017/01](swagger#operation--api-Status-get)


The `year` and the `month` parameters are optional.
If you give a `year` then you will get the statistics from the given year only.
If you give a `year` and a `month` then you will get the statistics from the given year and the given month only.

*Example RESPONSE*

> HTTP/1.1 200 OK

```json
{
  "Sum": 7,
  "Statistics": {
    "2017-01": {
      "Actions": [
        {
          "Name": "Classifier/Get",
          "Count": 4
        },
        {
          "Name": "Status/Get",
          "Count": 2
        },
        {
          "Name": "Statistics/Get",
          "Count": 1
        }
      ],
      "Sum": 7
    }
  }
}
```

`Response object properties`

Name    |   Description
--- |   ---
Sum  |   The sum of all requests of all time.
Statistics  |   Statistics object (dictionary), describes the usage statistics of periods (months). The keys are the months.
Statistics[].Sum | The sum of all requests count in the period of the current statistics object.
Statistics[].Actions    |   List of actions. An action is an event which is measured by the statistics. Currently it is equivalent with the API endpoints.