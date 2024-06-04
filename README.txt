                            Brainfuck
        https://github.com/Gael-Lopes-Da-Silva/Brainfuck

Description
------------------------------------------------------------------

This is my own implementation of an interpreter for the
brainfuck (https://en.wikipedia.org/wiki/Brainfuck) programming
language in Go.


Usage
------------------------------------------------------------------

brainfuck your_file.b
brainfuck --version    or -v      # Print the interpreter version
brainfuck --github     or -g      # Give the GitHub link
brainfuck --interpret  or -i      # Input directly a string to
                                    interpret

Installation
------------------------------------------------------------------

To build the interpreter you will first need to download the Go
compiler here:
https://go.dev/dl/

If you want a precompiled executable, run this:
go install github.com/gael-lopes-da-silva/brainfuck@latest

If you want to build the interpreter from the source, run this:
git clone https://github.com/Gael-Lopes-Da-Silva/Brainfuck
cd Brainfuck
go build .
