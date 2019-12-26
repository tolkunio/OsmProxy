DATE=date '+%Y-%m-%d %H:%M:%S'
echo "OsmProxy service started at ${DATE}" | systemd-cat -p info
/usr/bin/dotnet /home/tomurbekova/OsmProxy/publish/WebApi.dll