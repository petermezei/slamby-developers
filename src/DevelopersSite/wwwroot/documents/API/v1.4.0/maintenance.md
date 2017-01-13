## Maintenance

### Change Secret

Changes API secret. 

> WARNING! Changing API secret could prevent others from accessing API functions!

*Example REQUEST*
> [POST /api/Maintenance/ChangeSecret](swagger#operation--api-Maintenance-ChangeSecret-post)
```json
{
  "Secret": "MyNewS3cr3t"
}
```

*Example RESPONSE*
> HTTP/1.1 200 OK
