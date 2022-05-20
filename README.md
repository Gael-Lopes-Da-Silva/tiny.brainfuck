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

First install the latest dotnet SDK <a href="https://dotnet.microsoft.com/en-us/download">here</a>.

Then run these commands:
~~~shell
$ dotnet tool install -g dotnet-script
$ dotnet script ./build.csx
~~~
