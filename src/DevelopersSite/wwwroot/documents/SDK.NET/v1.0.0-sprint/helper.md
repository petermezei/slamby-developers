## Helper

### File Parser

File parsing and text extraction endpoint. Content must be provided as valid `base64` string. 
Binary files can be easily loaded with [File.ReadAllBytes](https://msdn.microsoft.com/en-us/library/system.io.file.readallbytes(v=vs.110).aspx) and converted to base64 string using [Convert.ToBase64String](https://msdn.microsoft.com/en-us/library/dhx0d524(v=vs.110).aspx) methods.

_Example:_

```cs
var manager = new HelperManager(configuration);
var fileParser = new FileParser
    {
        Content = "IkdvZCBTYXZlIHRoZSBRdWVlbiIgKGFsdGVybmF0aXZlbHkgIkdvZCBTYXZlIHRoZSBLaW5nIg=="
    };
var result = await manager.FileParserAsync(tag);
```