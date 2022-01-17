<h3 align="center">Brainfuck interpreter</h3>

---

<p align="center">⚙️ This is my own implementation of the <a href="https://en.wikipedia.org/wiki/Brainfuck">brainfuck</a> programming language in C#. It can get very slow because I'm currently learning but it's simple.</p>

---

### ❓ How to use

~~~shell
$ brainfuck.exe yourFile
~~~

### ❓ How to compile

First, if you haven't, download the [.NET sdk](https://dotnet.microsoft.com/en-us/download/visual-studio-sdks).

If you are on windows and don't know how to run build.sh, please download [git](https://git-scm.com/downloads).

After that, run this command:
~~~shell
$ bash build.sh
~~~

Then, you can find the compiled file in bin/Release/net[.net version]/[your os]/publish.
Just grab the file without the .pdb file.
