# Euro Space Center

A website made for the course .net web solutions at Odisee UC Ghent. Course given by Kevin Picalausa 2016-2017.

## Setup

This code needs email credentials to be able to send some nice and exciting register and invite emails. 

To do this you should create `EuroSpaceCenter/EuroSpaceCenter/secret.xml` with content like this: 

```xml
<?xml version="1.0" encoding="utf-8"?>
<appSettings>
  <add key="mailgunKey" value="mailgun api key" />
  <add key="mailgunDomain" value="domain used for mailgun (like mail.haroen.me) --> make sure that postmaster@doman exists" />
</appSettings>
```

Be careful to also deploy this file :smile:

## License

MIT