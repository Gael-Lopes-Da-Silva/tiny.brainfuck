set -xe

dotnet publish -r win-x64 -p:PublishSingleFile=true -p:PublishTrimmed=true --self-contained true
