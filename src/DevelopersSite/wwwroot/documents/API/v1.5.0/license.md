## License

### Get License

Returns back with license information.

*Example REQUEST*
> [GET /api/License](swagger#operation--api-License-get)


*Example RESPONSE (truncated for brevity)*
> HTTP/1.1 200 OK
```json
{
  "IsValid": true,
  "Message": "",
  "Type": "OpenSource",
  "Base64": "PExpY2Vuc2U+CiAgPFR5cGU+T3BlblNvdXJjZTwvVHlwZT..."
}
```

### Change License

Replace or set your current license with a new one.

*Example REQUEST (truncated for brevity)*
> [POST /api/License](swagger#operation--api-License-post)
```json
{
	"License": "PExpY2Vuc2U+CiAgPFR5cGU+T3BlblNvdXJjZTwvVHlwZT..."
}
```

*Example RESPONSE*
> HTTP/1.1 200 OK