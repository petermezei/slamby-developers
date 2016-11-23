# Slamby API {{docversion}}

Slamby introduces Slamby Server (API). Build powerful data management service, store and analyze your data. This documentation shows you a quick overview of the available API endpoints, functions, technical details and business usage.

With Slamby you can:

* Store and manage your data easily
* Get high data security and privacy
* Use improved data analysis
* Create category recommendation engine
* Create similar product recommendation engine

## Changelog

- added License endpoint
- added Maintenance endpoint

### Features

- Released {{docversion}}

---

Once you've
[registered your client](http://slamby.com/register/) it's easy
to start working with Slamby API.

All endpoints are only accessible via https and are located at `europe.slamby.com` or `asia.slamby.com`.

```
    https://europe.slamby.com/CLIENT_ID
```

`Demo server access:`

URL |   `https://europe.slamby.com/demo/`
--- |   ---
Secret  |   `s3cr3t`

*For on-premise hosting please contact us: [support@slamby.com](mailto:support@slamby.com)*

> **Tip:** The `CLIENT_ID` is your unique identifier what you get after your server is ready to use.

## Authentication

The base of the authentication is the `API_KEY`.
You have to provide it in the authorization header. It is **required for every API call**.

The examples of the documentation are presuming that you provide the API key in the header.

>*Example*

Header   |Value
---------|---
Authorization|Slamby `API_KEY`

>**Tip:** You will get your `API_KEY` after your server is ready to use.

## Processor core management

Using your API you can set the required amount of processor core to each process. To set the suitable core number manually you can use `X-API-Parallel-Limit` in the request header.

>*Example*

Header | Value
--- | ---
X-API-Parallel-Limit | 2

This example set 2 processor cores to the given process.

`When this header is not set, the basic value is the available maximum core number.`


## API Version

Every response contains API version information in response HTTP `X-API-Version` header.

>*Example*

X-Api-Version: {{version}}

