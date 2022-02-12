Dep

1. Install .NET 5
2. Install .Net Framework 4.8

=====================
Run
1. Start UA COM Server Wrapper directly, it will create certificates automatically and put in the %CommonApplicationData%\OPC Foundation\pki folder, also save username and password in %CommonApplicationData%\OPC Foundation\Accounts

2. In VS debug settings, set working directory and command line for OpcUaPublisher, press F5
3. OpcUaPublisher will also create cetificates under pki folder in the working folder automatically if no cetificates provided

## OPC UA publisher container build and run

1. OpcUaPublisher docker build:  
Go to OpcUaPublisher folder, run below command
```bash
$ docker build -f <docker file> -t opcua-publisher .
```
2. OpcUaPublisher docker run:  
```bash
$ docker run -h <host name> --name <container name> -v "<foder contains configuration files>:/appdata" <OpcUaPublisher image name> --aa --pf /appdata/<configuration file> --c "<iot hub connection string>"
```
example as:
```
$ docker run -h my-opcua-publisher --name my-opcua-publisher -v "D:\Temp\opcua-publisher:/appdata"  opcua-publisher:latest --aa --pf /appdata/publishednodes_sample.json --c "HostName=myiothub.azure-devices.cn;SharedAccessKeyName=iothubowner;SharedAccessKey=jTC5N067VwRp+FLe61WQxhY9BNOeJLkvlXBY6usfsdgPE="
```
Note: /appdata is the working directory inside the container  
Now you can see a device named "my-opcua-publisher" was created and see data telemetry send to iot hub

3. docker push
```bash
$ docker login -u <you repo user name> -p <your repo user password> <your repo host name>
$ docker tag <local image> <remote image>
$ docker push <remote image>
```
example as:   
```bash
$ docker login -u acruser -p acrpassword myacr.azurecr.cn
$ docker tag opcua-publisher:latest myacr.azurecr.cn/iot/opcua-publisher:latest
$ docker push myacr.azurecr.cn/iot/opcua-publisher:latest
```

## OPC UA publisher run on Azure IoT Edge
After push the opcua-publisher container to Azure Container Registry, we can run it with Azure IoT Edge   

On the IoT Edge device, create a folder and put the configuration file to it, for example I will create  the `/opt/opcua-publisher` folder to place the configuration files

Set module in azure iot edge and select the opcua-publisher image, set your module name and use below "Container Create Option"

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
Routes set to `FROM /messages/* INTO $upstream` because the source use ModuleClient.SendEventAsync() to send message to iot hub