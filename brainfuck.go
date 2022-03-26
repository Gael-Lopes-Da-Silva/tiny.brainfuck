package main

import (
	"bufio"
	"fmt"
	"io/ioutil"
	"os"
	"strings"
)

func parseInput(input string) string {
	var parsedInput string

	allowedCharacters := [8]rune{'>', '<', '+', '-', '.', ',', '[', ']'}
	splitedInput := strings.Split(input, "\n")

	for _, line := range splitedInput {
		if line == "" {
			continue
		} else if line[0] != '#' {
			for _, character := range line {
				if character == '#' {
					break
				} else {
					for _, allowed := range allowedCharacters {
						if allowed == character {
							parsedInput += string(character)
						}
					}
				}
			}
		}
	}

	return parsedInput
}

func interpret(input string) {
	var tape [30000]byte

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
			fmt.Printf("%c", tape[tapePtr])

		case ',':
			fmt.Printf("\nEnter a character: ")
			reader := bufio.NewReader(os.Stdin)
			character, err := reader.ReadByte()
			fmt.Printf("\n")

			if err != nil {
				fmt.Printf("\033[31m" + "ERROR" + "\033[0m" + ": Cannot read the character\n")
				os.Exit(2)
			}

			tape[tapePtr] = character

		case '[':
			if tape[tapePtr] == 0 {
				inputPtrTemp := inputPtr
				count := 1

				for count != 0 {
					if inputPtrTemp == len(input)-1 {
						fmt.Printf("\033[31m" + "ERROR" + "\033[0m" + ": Missing a closing bracket\n")
						os.Exit(3)
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
					if inputPtrTemp == 0 {
						fmt.Printf("\033[31m" + "ERROR" + "\033[0m" + ": Missing a opening bracket\n")
						os.Exit(4)
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
}

func main() {
	args := os.Args[1:]

	if len(args) > 0 {
		input, err := ioutil.ReadFile(args[0])
		if err != nil {
			fmt.Printf("\033[31m" + "ERROR" + "\033[0m" + ": Cannot open the file\n")
			os.Exit(1)
		}

		interpret(parseInput(string(input)))
	} else {
		fmt.Printf("Brainfuck interpreter v0.3\n\n")
		fmt.Printf("\033[90m" + "usage" + "\033[0m" + ": brainfuck.exe [<file path>]\n")
	}
}
