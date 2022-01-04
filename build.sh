set -xe

# you can replace win-x64 by linux-x64 or osx.10.11-x64
# you can also replace x64 by x86 for x86 computers

dotnet publish -c Release -r win-x64 -p:PublishSingleFile=true -p:PublishTrimmed=true --self-contained true
