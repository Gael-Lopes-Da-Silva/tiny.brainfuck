/* 
@author: Gael Lopes Da Silva
@project: Brainfuck Interpreter
@github: https://github.com/Gael-Lopes-Da-Silva/Brainfuck
@gitlab: https://gitlab.com/Gael-Lopes-Da-Silva/Brainfuck
*/

public class Brainfuck
{
    private static void WriteColoredLine(string type, ConsoleColor color, string message)
    {
        Console.ForegroundColor = color;
        Console.Write(type.ToUpper());
        Console.ResetColor();
        Console.WriteLine(message);
    }

    private static string ParseInput(string[] input)
    {
        string parsedInput = "";
        char[] allowedCharacter = { '>', '<', '+', '-', '.', ',', '[', ']' };

        foreach (string line in input)
        {
            if (line == "")
            {
                continue;
            }
            else if (line[0] != '#')
            {
                foreach (char character in line)
                {
                    if (character == '#')
                    {
                        break;
                    }
                    else if (allowedCharacter.Contains(character))
                    {
                        parsedInput += character;
                    }
                }
            }
        }

        return parsedInput;
    }

    private static void Interpret(string input)
    {
        byte[] tape = new byte[30000];

        int tapePtr = 0;
        int inputPtr = 0;

        while (inputPtr < input.Length)
        {
            switch (input[inputPtr])
            {
                case '>':
                    if (tapePtr >= tape.Length - 1)
                    {
                        tapePtr = 0;
                    }
                    else
                    {
                        ++tapePtr;
                    }
                    break;

                case '<':
                    if (tapePtr <= 0)
                    {
                        tapePtr = tape.Length - 1;
                    }
                    else
                    {
                        --tapePtr;
                    }
                    break;

                case '+':
                    if (tape[tapePtr] >= 255)
                    {
                        tape[tapePtr] = 0;
                    }
                    else
                    {
                        tape[tapePtr]++;
                    }
                    break;

                case '-':
                    if (tape[tapePtr] <= 0)
                    {
                        tape[tapePtr] = 255;
                    }
                    else
                    {
                        tape[tapePtr]--;
                    }
                    break;

                case '.':
                    Console.Write((char)tape[tapePtr]);
                    break;

                case ',':
                    Console.Write("\nEnter a character: ");
                    tape[tapePtr] = (byte)Console.ReadKey().KeyChar;
                    Console.Write("\n");
                    break;

                case '[':
                    if (tape[tapePtr] == 0)
                    {
                        int inputPtrTemp = inputPtr;
                        int count = 1;

                        while (count != 0)
                        {
                            if (inputPtrTemp >= input.Length - 1)
                            {
                                WriteColoredLine("ERROR", ConsoleColor.Red, ": Missing closing bracket");
                                return;
                            }
                            else
                            {
                                inputPtrTemp++;

                                if (input[inputPtrTemp] == '[')
                                {
                                    count++;
                                }
                                else if (input[inputPtrTemp] == ']')
                                {
                                    count--;
                                }
                            }
                        }

                        inputPtr = inputPtrTemp;
                    }
                    break;

                case ']':
                    if (tape[tapePtr] != 0)
                    {
                        int inputPtrTemp = inputPtr;
                        int count = 1;

                        while (count != 0)
                        {
                            if (inputPtrTemp <= 0)
                            {
                                WriteColoredLine("ERROR", ConsoleColor.Red, ": Missing opening bracket");
                                return;
                            }
                            else
                            {
                                inputPtrTemp--;

                                if (input[inputPtrTemp] == '[')
                                {
                                    count--;
                                }
                                else if (input[inputPtrTemp] == ']')
                                {
                                    count++;
                                }
                            }
                        }

                        inputPtr = inputPtrTemp;
                    }
                    break;
            }

            inputPtr++;
        }
    }

    public static void Main(string[] args)
    {
        if (args.Length <= 0)
        {
            Console.WriteLine("Brainfuck Interpreter\n");
            WriteColoredLine("USAGE", ConsoleColor.DarkGray, ": brainfuck.exe [<your file>]\n");

            WriteColoredLine("--version  -v", ConsoleColor.Green, ": Display version");
            WriteColoredLine("--github   -g", ConsoleColor.Green, ": Give the github link");
        }
        else if (args.Length >= 1)
        {
            switch (args[0])
            {
                case "--version":
                case "-v":
                    Console.WriteLine("v0.4");
                    break;

                case "--github":
                case "-g":
                    Console.WriteLine("https://github.com/Gael-Lopes-Da-Silva/Brainfuck");
                    break;

                default:
                    try
                    {
                        string[] input = File.ReadAllLines(args[0]);
                        Interpret(ParseInput(input));
                    }
                    catch (FileNotFoundException)
                    {
                        WriteColoredLine("ERROR", ConsoleColor.Red, $": Cannot open '{args[0]}'");
                    }
                    break;
            }
        }
    }
}
