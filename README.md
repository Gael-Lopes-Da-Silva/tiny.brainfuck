<h3 align="center">Brainfuck interpreter</h3>

---

<p align="center">
    :gear: This is my own implementation of the [brainfuck](https://en.wikipedia.org/wiki/Brainfuck) programming language in C#.
</p>

---

### :question: How to use

~~~console
$ Brainfuck.exe [<file path>]
$ Brainfuck.exe --version       # Display the interpreter version
$ Brainfuck.exe --github        # Display the github repo link
~~~

### :question: How to build

The simple way to build the project is to use Visual Studio, you just need to open the sln or csproj file and then make a build or run the project.

<details> <summary>Build manualy</summary>
<p>
    If you want to build manualy, you will need to install the latest dotnet SDK <a href="https://dotnet.microsoft.com/en-us/download">here</a>.
</p>

<p>
    Then you just need to run the following cli command. You will need to choose your OS if you want to run it.
</p>

```console
# windows
$ dotnet publish -c Release -o ./Build -r win-x64 --self-contained true
$ dotnet publish -c Release -o ./Build -r win-x86 --self-contained true

# linux
$ dotnet publish -c Release -o ./Build -r linux-x64 --self-contained true
$ dotnet publish -c Release -o ./Build -r linux-arm --self-contained true

# macos
$ dotnet publish -c Release -o ./Build -r osx-x64 --self-contained true

$ mv ./icon.png ./Build/
```
</details>

If you want to download a build, check the [Build](./Build/) folder.
