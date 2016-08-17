## Helper functions

### File Parser

File parsing and text extraction endpoint. Content must be provided as valid `base64` string.

*Example REQUEST*
> [POST /api/Helper/FileParser](#operation--api-Helper-FileParser-post)
```json
{
    "Content": "IkdvZCBTYXZlIHRoZSBRdWVlbiIgKGFsdGVybmF0aXZlbHkgIkdvZCBTYXZlIHRoZSBLaW5nIg=="
}
```

*Example RESPONSE*
> HTTP/1.1 200 OK
```JSON
{
  "Content": "\"God Save the Queen\" (alternatively \"God Save the King\"\n",
  "Title": null,
  "Date": null,
  "Author": null,
  "Keywords": null,
  "ContentType": "text/plain; charset=ISO-8859-1",
  "ContentLength": 55,
  "Language": "en"
}
```
