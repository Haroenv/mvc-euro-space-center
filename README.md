# Euro Space Center

A website made for the course .net web solutions at Odisee UC Ghent. Course given by Kevin Picalausa 2016-2017.

## Setup

This code needs email credentials to be able to send some nice and exciting register and invite emails. 

To do this you should create `EuroSpaceCenter/EuroSpaceCenter/secret.xml` with content like this: 

```xml
<?xml version="1.0" encoding="utf-8"?>
<appSettings>
  <add key="emailAddress" value="email address to send from" />
  <add key="emailPass" value="yep, your password" />
  <add key="emailServer" value="your smtp address" />
  <add key="emailPort" value="smtp port (integer!)" />
</appSettings>
```

Be careful to also deploy this file :smile:

## License

MIT