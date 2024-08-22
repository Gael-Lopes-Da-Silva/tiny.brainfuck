<div align="center">
	<h1>Mini Brainfuck</h1>
    <a href="https://github.com/Gael-Lopes-Da-Silva/mini_brainfuck">https://github.com/Gael-Lopes-Da-Silva/mini_brainfuck</a>
</div>


Description
------------------------------------------------------------------

This is my own implementation of an interpreter for the brainfuck (https://en.wikipedia.org/wiki/Brainfuck) programming language in Rust.


Usage
------------------------------------------------------------------

~~~
USAGE: target/debug/mini_brainfuck --file <filename>

  -f, --file       Open a file and interpret it's content
  -i, --interpret  Interpret a given input
  -d, --debug      Print a debug output of the interpreted input
  -t, --tape       Print the tape after the interpretation of the input
  -v, --version    Print the interpreter version
~~~


Build From Source
------------------------------------------------------------------

Make sure to have a ready to use installation of rust.

~~~
git clone https://github.com/Gael-Lopes-Da-Silva/mini_brainfuck.git
cd mini_brainfuck
cargo build
~~~
