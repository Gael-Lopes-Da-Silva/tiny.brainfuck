<div align="center">
	<h1>Brainfuck interpreter</h1>
</div>

### 🗒️ This is my own implementation of the [brainfuck](https://en.wikipedia.org/wiki/Brainfuck) programming language in Go.

## ❓ How to use
~~~
brainfuck your_file.b
brainfuck --version    or -v      # Print the interpreter version
brainfuck --github     or -g      # Give the GitHub link
brainfuck --interpret  or -i      # Input directly a string to interpret
~~~

## ❓ How to build
#### ❗ To build the interpreter you will first need to download the Go compiler [here](https://go.dev/dl/).

If you want a precompiled executable, run this.
~~~shell
go install github.com/gael-lopes-da-silva/brainfuck@latest
~~~

If you want to build the interpreter from the source, run this.
~~~shell
git clone https://github.com/Gael-Lopes-Da-Silva/Brainfuck
cd Brainfuck
go build .
~~~
