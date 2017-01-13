## License

Returns with version, processor, memory and disk information about the API server.

### Get License

Returns back with license information.

_Example:_

```cs
var licenseManager = new LicenseManager(_configuration);
var license = await licenseManager.GetLicenseAsync();
```


### Change License

Replace or set your current license with a new one. License is a `base64` encoded string which is provided by Slamby.

```cs
var licenseManager = new LicenseManager(_configuration);
var model = new ChangeLicense() 
    { 
        License = "PExpY2Vuc2U+CiAgPFR5cGU+T3BlblNvdXJjZTwvV...(truncated for brevity)" 
    };
var license = await licenseManager.SetLicenseAsync(model);
```