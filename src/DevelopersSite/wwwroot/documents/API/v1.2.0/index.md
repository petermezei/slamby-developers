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

## Setup

To complete your Slamby API installation follow the steps below:

- open the setup page at `http://api-url/setup`

- request an open-source license for test purpose (free)

- set the API secret

- set your license

- Now your API ready for use

> For more information visit [https://www.slamby.com/getting-started](https://www.slamby.com/getting-started)

## API update

There are two ways two update your Slamby API, depending whish hosting you have.

### Saas, or Microsoft Azure Marketplace

When you are using your Slamby API via Microsoft Azure Marketplace, or SaaS by Slamby then you can use our built-in update endpoint.

*Example REQUEST*
> GET /update

*Example RESPONSE*
> HTTP/1.1 200 OK
OK

> When the response is `OK` it means you have an update endpoint.

`Update your API`

*Example REQUEST*
> POST /update

*Example RESPONSE*
> HTTP/1.1 200 OK
```json
{
    "Log": "slamby_redis is up-to-date\nslamby_api_updater is up-to-date\nslamby_elasticsearch is up-to-date\nslamby_api is up-to-date\nslamby_nginx is up-to-date\n"
}
```

### On-premise update

When you installed Slamby API on your machine, then download and replace your `docker-compose file` with the latest version, then `docker-compose up -d`. 

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

