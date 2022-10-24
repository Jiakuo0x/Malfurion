# How to use

Configurate your 'Appsettings.json'.
```json
"Kong": {
    "AdminApiAddress": "http://localhost:8001",
    "ServiceName": "test",
    "ServiceAddress": "192.168.3.46:5000"
  }
```

Use the package in your web program.
```csharp
builder.Services.Configure<KongConf>(builder.Configuration.GetSection("Kong"));
builder.Services.AddMalfurionWebKong();
```