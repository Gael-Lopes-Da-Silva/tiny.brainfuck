<h3 align="center">
    Brainfuck interpreter
</h3>

---

<p align="center">
    :gear: This is my own implementation of the <a href="https://en.wikipedia.org/wiki/Brainfuck">brainfuck</a> programming language in Go.
</p>

---

### :question: How to use
~~~console
brainfuck your_file.b
brainfuck --version    or -v      # Input directly a string to interpret
brainfuck --github     or -g      # Print the interpreter version
brainfuck --interpret  or -i      # Give the GitHub link
~~~

### :question: How to build
To build the interpreter you will first need to download the Go compiler [here](https://go.dev/dl/).

If you want a precompiled executable, run this.
~~~console
go install github.com/gael-lopes-da-silva/brainfuck
~~~

If you want to build the interpreter from the source, run this.
~~~console
git clone https://github.com/Gael-Lopes-Da-Silva/Brainfuck
cd Brainfuck
go buidl .
~~~
