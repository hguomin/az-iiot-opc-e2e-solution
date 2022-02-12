Dep

1. Install .NET 5
2. Install .Net Framework 4.8


Run

1. Start UA COM Server Wrapper directly, it will create certificates automatically and put in the %CommonApplicationData%\OPC Foundation\pki folder, also save username and password in %CommonApplicationData%\OPC Foundation\Accounts

2. In VS debug settings, set working directory and command line for OpcUaPublisher, press F5
2. OpcUaPublisher will also create cetificates under pki folder in the working folder automatically if no cetificates provided