This project is based on the Microsoft.Azure.IIoT.OpcUa.Edge.Publisher module from [Industrial-IoT](https://github.com/Azure/Industrial-IoT/) version [2.5.6](https://github.com/Azure/Industrial-IoT/releases/tag/2.5.6).

Based on the upstream code, plain user name and password authentication configuration support is added, configuration sample can refer to below publishednodes.json: 
```json
[
  {
    "EndpointUrl": "opc.tcp://127.0.0.1:48400/UA/ComServerWrapper",
    "UseSecurity": false,
    "OpcAuthenticationMode": "UsernamePassword",
    "OpcAuthenticationUsername": "your opc ua server user name",
    "OpcAuthenticationPassword": "your user password",
    "OpcNodes": [
      {
        "Id": "ns=2;s=0:Random.Real8",
        "OpcSamplingInterval": 1000,
        "OpcPublishingInterval": 1000
      }
    ]
  }
]
```
