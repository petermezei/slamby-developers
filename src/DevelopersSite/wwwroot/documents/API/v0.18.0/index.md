# Slamby API {{version}}

## Changelog

### Features

- modified paging experience with `ScrollId`
- added partial document update
- added `attachment` DataSet field type (document text extraction)

---

Slamby introduces Slamby Service (API). Build powerful data management center, store and analyze your data easily. This documentation shows you a quick overview about the available API endpoints, technical details and business usage.
With Slamby you can:
* Store and manage your data easily
* Get high data security and privacy
* Use improved data analysis

Once you've
[registered your client](http://slamby.com/register/) it's easy
to start working with Slamby API.

All endpoints are only accessible via https and are located at `europe.slamby.com` or `asia.slamby.com`.

```
    https://europe.slamby.com/CLIENT_ID
```

> **Tip:** The `CLIENT_ID` is your unique identifier what you get after your server is ready to use.

## Authentication
The base of the authentication is the `API_KEY`.
You have to provide it in the authorization header. It is **required for every API call**.
The examples of the documentation is preasuming that you provide the API key in the header.

>*Example*
>
Header   |Value
---------|---
Authorization|Slamby `API_KEY`

&nbsp;

>**Tip:** You will get your `API_KEY` after your server is ready to use.

## API Version

Every response contains API version information in response HTTP `X-Api-Version` header.

>*Example*
>
X-Api-Version: {{version}}

