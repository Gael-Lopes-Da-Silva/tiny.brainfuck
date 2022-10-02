<h3 align="center">Brainfuck interpreter</h3>

---

<p align="center">⚙️ This is my own implementation of the <a href="https://en.wikipedia.org/wiki/Brainfuck">brainfuck</a> programming language in C#.</p>

---

### ❓ How to use

~~~shell
$ Brainfuck.exe [<file path>]
$ Brainfuck.exe --version       # Display the interpreter version
$ Brainfuck.exe --github        # Display the github repo link
~~~

### ❓ How to build

First you need to install the latest dotnet SDK <a href="https://dotnet.microsoft.com/en-us/download">here</a>.

~~~shell
$ cd Source/

# windows
$ dotnet publish -c Release -o ../Build -r win-x64 --self-contained true
$ dotnet publish -c Release -o ../Build -r win-x86 --self-contained true

# linux
$ dotnet publish -c Release -o ../Build -r linux-x64 --self-contained true
$ dotnet publish -c Release -o ../Build -r linux-arm --self-contained true

# macos
$ dotnet publish -c Release -o ../Build -r osx-x64 --self-contained true

$ mv ./icon.png ../Build/
~~~

If you want to download a build, check the <a href="./Build/">Build</a> folder.
