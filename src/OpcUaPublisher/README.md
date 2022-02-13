# OPC UA Publisher Application

This project is based on the Microsoft.Azure.IIoT.OpcUa.Edge.Publisher module from Azure [Industrial-IoT](https://github.com/Azure/Industrial-IoT/) version [2.5.6](https://github.com/Azure/Industrial-IoT/releases/tag/2.5.6).

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

## Getting started

This application can run as standalone executable app or docker container on Windows or Linux.

### Run in command line
See official [docs](https://github.com/Azure/Industrial-IoT/blob/main/docs/modules/publisher-commandline.md#opc-publisher-command-line-arguments-for-version-25-and-below).

Sample as below, `publishednodes.json` is the configuration file:  
```bash
opcpublisher.exe --aa --pf publishednodes.json --c "IoT Hub connection string"
```

### Build and run in container
Change command line to this folder and use Dockerfiles in the `Docker` directory to build container image. Below command will build a container named as `opcua-publisher:latest` in your local dev machine.
```bash
$ docker build -f ./Docker/Dockerfile.linux-amd64 -t opcua-publisher .
```

Then you can run it as standalone container:  
```bash
$ docker run -h my-opcua-publisher --name my-opcua-publisher -v "D:\Temp\opcua-publisher:/appdata"  opcua-publisher:latest --aa --pf /appdata/publishednodes.json --c "IoT Hub connection string"
```
Before runing above command line sample, the configuration file `publishednodes.json` need put into the folder `D:\Temp\opcua-publisher`.

#### Run in Azure IoT Edge
In order to deploy this application to IoT Edge, you need to push the container image to docker hub or Azure Container Registry.

In below sample, the Azure Container Registry `gmdevcr.azurecr.io` will be used as container image repo, replace it when using your container registry.

```bash
$ docker login -u <user name> -p <password> gmdevcr.azurecr.io
$ docker tag opcua-publisher:latest gmdevcr.azurecr.io/opcua-publisher:latest
$ docker push gmdevcr.azurecr.io/opcua-publisher:latest
```

Now it's ready to deploy it to Azure IoT Edge device as a edge module, in the iot edge device, create a folder and put the configuration file `publishednodes.json` into it. `/opt/opcua-publisher` is used in below sample. 

when seting the iot edge module in azure portal, set `Image URI` to `gmdevcr.azurecr.io/opcua-publisher:latest`, the `Container Create Options` set as below and a route with value `FROM /messages/* INTO $upstream` needed.
```json
{
    "Hostname": "opcua-publisher",
    "Cmd": [
        "--pf=./publishednodes.json",
        "--aa"
    ],
    "HostConfig": {
        "Binds": [
            "/opt/opcua-publisher:/appdata"
        ]
    }
}
```

then the it will run as a edge module in Azure IoT Edge.

