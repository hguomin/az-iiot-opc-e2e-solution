cd /D "%~dp0"
echo off
echo "Starting OpcPublisher..."

opcpublisher.exe --aa --pf conf/publishednodes_danfoss.json --tc conf/telemetryconfiguration.json --c "HostName=dfspoc-iothub.azure-devices.cn;SharedAccessKeyName=iothubowner;SharedAccessKey=jTC5N067VwRp+FLe61WQxhY9BNOeJLkvlXBY6uATgPE="

set /p temp="Press Enter to exist..."