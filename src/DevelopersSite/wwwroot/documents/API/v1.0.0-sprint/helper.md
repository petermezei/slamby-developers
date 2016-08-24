## Helper functions

A collection of helper methods.

### File Parser

File parsing and text extraction endpoint. Content must be provided as valid `base64` string.

*Example REQUEST*
> [POST /api/Helper/FileParser](#operation--api-Helper-FileParser-post)
```json
{
    "Content": "77u/QSBsb25nIHRpbWUgYWdvIGluIGEgZ2FsYXh5IGZhciwgZmFyIGF3YXkuLi4="
}
```

*Example RESPONSE*
> HTTP/1.1 200 OK
```JSON
{
  "Content": "A long time ago in a galaxy far, far away...\n",
  "Title": null,
  "Date": null,
  "Author": null,
  "Keywords": null,
  "ContentType": "text/plain; charset=UTF-8",
  "ContentLength": 47,
  "Language": "en"
}
```

`Available response parameters:`

Name  | Description
--- | ---
Content | Extracted plain text content
Title | File metadata title
Date  |  File metadata date
Author  | File metadata author
Keywords  | File metadata keywords
ContentType | Detected content type and character encoding
ContentLength | Plain text content length
Language  | Detected content language