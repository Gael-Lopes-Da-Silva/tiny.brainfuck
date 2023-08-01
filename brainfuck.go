package main

import (
	"bufio"
	"fmt"
	"os"
	"strings"
)

const VERSION = "v0.5"

const RESET = "\033[0m"
const RED = "\033[31m"
const GREEN = "\033[32m"
const GREY = "\033[90m"

func contains(array []rune, character rune) bool {
	for _, element := range array {
		if element == character {
			return true
		}
	}
	return false
}

func parse_input(input string) string {
	output := ""
	allowedCharacter := []rune{'>', '<', '+', '-', '.', ',', '[', ']'}

	for _, line := range strings.Split(input, "\n") {
		if line == "" {
			continue
		} else if line[0] != '#' {
			for _, character := range line {
				if character == '#' {
					break
				} else if contains(allowedCharacter, character) {
					output += string(character)
				}
			}
		}
	}

	return output
}

func interpret(input string) int {
	tape := [30000]byte{}
	tapePtr := 0
	inputPtr := 0

	for inputPtr < len(input) {
		switch input[inputPtr] {
		case '>':
			if tapePtr >= len(tape)-1 {
				tapePtr = 0
			} else {
				tapePtr++
			}
		case '<':
			if tapePtr <= 0 {
				tapePtr = len(tape) - 1
			} else {
				tapePtr--
			}
		case '+':
			if tape[tapePtr] >= 255 {
				tape[tapePtr] = 0
			} else {
				tape[tapePtr]++
			}
		case '-':
			if tape[tapePtr] <= 0 {
				tape[tapePtr] = 255
			} else {
				tape[tapePtr]--
			}
		case '.':
			fmt.Print(string(tape[tapePtr]))
		case ',':
			fmt.Print("Type a character: ")
			reader := bufio.NewReader(os.Stdin)
			characater, error := reader.ReadByte()

			if error != nil {
				fmt.Print(RED + "ERROR" + RESET + ": Cannot read the key\n")
				return 1
			}

			tape[tapePtr] = characater
		case '[':
			if tape[tapePtr] == 0 {
				inputPtrTemp := inputPtr
				count := 1

				for count != 0 {
					if inputPtrTemp >= len(input)-1 {
						fmt.Print(RED + "ERROR" + RESET + ": Missing closing bracket\n")
						return 1
					} else {
						inputPtrTemp++

						if input[inputPtrTemp] == '[' {
							count++
						} else if input[inputPtrTemp] == ']' {
							count--
						}
					}
				}

				inputPtr = inputPtrTemp
			}
		case ']':
			if tape[tapePtr] != 0 {
				inputPtrTemp := inputPtr
				count := 1

				for count != 0 {
					if inputPtrTemp <= 0 {
						fmt.Print(RED + "ERROR" + RESET + ": Missing opening bracket\n")
						return 1
					} else {
						inputPtrTemp--

						if input[inputPtrTemp] == '[' {
							count--
						} else if input[inputPtrTemp] == ']' {
							count++
						}
					}
				}

				inputPtr = inputPtrTemp
			}
		}

		inputPtr++
	}

	return 0
}

func main() {
	args := os.Args[1:]
	argc := len(args)

	if argc <= 0 {
		fmt.Print(
			GREEN+"Brainfuck interpreter\n"+RESET,
			"\n",
			GREY+"USAGE"+RESET+": brainfuck your_file.b\n",
			"\n",
			"--interpret -i     Input directly a string to interpret\n",
			"--version   -v     Print the interpreter version\n",
			"--github    -g     Give the GitHub link\n",
		)
	} else {
		switch args[0] {
		case "--version", "-v":
			fmt.Println(VERSION)
		case "--github", "-g":
			fmt.Print("https://github.com/Gael-Lopes-Da-Silva/Brainfuck\n")
		case "--interpret", "-i":
			os.Exit(interpret(parse_input(args[1])))
		default:
			fileContent, error := os.ReadFile(args[0])
			if error == nil {
				os.Exit(interpret(parse_input(string(fileContent))))
			} else {
				fmt.Print(RED + "ERROR" + RESET + ": File not found\n")
			}
		}
	}
}
