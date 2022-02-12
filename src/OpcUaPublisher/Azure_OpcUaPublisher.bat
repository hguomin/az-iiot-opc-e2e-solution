cd /D "%~dp0"
echo off
echo "Starting OpcPublisher..."

opcpublisher.exe --aa --pf conf\publishednodes_sample.json --tc conf\telemetryconfiguration.json --c "[replace with you iot hub connection string]"

set /p temp="Press Enter to exist..."