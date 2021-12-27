using System;
using System.IO;
using System.Linq;

public static class Brainfuck
{
  private static void Main(string[] Args)
  {
    if (Args.Length <= 0)
    {
      Console.WriteLine("Brainfuck Interpreter v0.2");
      Console.WriteLine(@"
Usage: brainfuck.exe yourFile

Tutorial:
  Tape:
  _________________________
  | 0 | 0 | 0 | 0 | 0 | 0 |
    ^

  > : Move the pointer to the right
  _________________________
  | 0 | 0 | 0 | 0 | 0 | 0 |
        ^

  < : Move the pointer to the left
  _________________________
  | 0 | 0 | 0 | 0 | 0 | 0 |
    ^

  + : Increment the pointed bit
  _________________________
  | 1 | 0 | 0 | 0 | 0 | 0 |
    ^

  - : Decrement the pointed bit
  _________________________
  | 0 | 0 | 0 | 0 | 0 | 0 |
    ^

  . : Show the pointed bit as a character
  _________________________
  | 1 | 0 | 0 | 0 | 0 | 0 |
    ^
  Output: ☺

  , : Get user input as character and put it in the pointed bit as byte
  _________________________
  | 1 | 0 | 0 | 0 | 0 | 0 |
    ^
  Output: Enter a character:

  [ : If the pointed bit is 0 move to the closed matching bracket, else continue
  ] : If the pointed bit is not 0 move to the previous opening matching bracket, else continue");
    }
    else if (Args.Length >= 1)
    {
      try
      {
        string Source = CleanSource(File.ReadAllLines(Args[0]));
        Interpret(Source);
      }
      catch (FileNotFoundException)
      {
        Console.WriteLine("ERROR: Cannot open the targeted file");
      }
    }
  }

  public static string CleanSource(string[] Source)
  {
    string CleanedSource = "";
    char[] AllowedCharacter = {'>', '<', '+', '-', '.', ',', '[', ']'};

    foreach (string Line in Source)
    {
      if (Line == "")
      {
        continue;
      }
      else if (Line[0] != '#')
      {
        foreach (char Character in Line)
        {
          if (Character == '#')
          {
            break;
          }
          else if (AllowedCharacter.Contains(Character))
          {
            CleanedSource += Character;
          }
        }
      }
    }

    return CleanedSource;
  }

  public static void Interpret(string Source)
  {
    byte[] Tape = new byte[30000];
    int CellPointer = 0;
    int SourceIndex = 0;

    while (SourceIndex < Source.Length)
    {
      switch (Source[SourceIndex])
      {
        case '>':
        {
          if (CellPointer >= Tape.Length-1)
          {
            CellPointer = 0;
          }
          else
          {
            ++CellPointer;
          }
        }
        break;

        case '<':
        {
          if (CellPointer <= 0)
          {
            CellPointer = Tape.Length-1;
          }
          else
          {
            --CellPointer;
          }
        }
        break;

        case '+':
        {
          if (Tape[CellPointer] >= 255)
          {
            Tape[CellPointer] = 0;
          }
          else
          {
            ++Tape[CellPointer];
          }
        }
        break;

        case '-':
        {
          if (Tape[CellPointer] <= 0)
          {
            Tape[CellPointer] = 255;
          }
          else
          {
            --Tape[CellPointer];
          }
        }
        break;

        case '.':
        {
          Console.Write((char)Tape[CellPointer]);
        }
        break;

        case ',':
        {
          Console.Write("\nEnter a character: ");
          Tape[CellPointer] = (byte)Console.ReadKey().KeyChar;
          Console.Write("\n");
        }
        break;

        case '[':
        {
          if (Tape[CellPointer] == 0)
          {
            int SourceIndexTemp = SourceIndex;
            int Count = 1;

            while (Count != 0)
            {
              if (SourceIndexTemp == Source.Length-1)
              {
                Console.WriteLine("ERROR: Cannot find a matching closing bracket");
                return;
              }
              else
              {
                ++SourceIndexTemp;
                if (Source[SourceIndexTemp] == '[')
                {
                  ++Count;
                }
                else if (Source[SourceIndexTemp] == ']')
                {
                  --Count;
                }
              }
            }

            SourceIndex = SourceIndexTemp;
          }
        }
        break;

        case ']':
        {
          if (Tape[CellPointer] != 0)
          {
            int SourceIndexTemp = SourceIndex;
            int Count = 1;

            while (Count != 0)
            {
              if (SourceIndexTemp == 0)
              {
                Console.WriteLine("ERROR: Cannot find a matching opening bracket");
                return;
              }
              else
              {
                --SourceIndexTemp;
                if (Source[SourceIndexTemp] == '[')
                {
                  --Count;
                }
                else if (Source[SourceIndexTemp] == ']')
                {
                  ++Count;
                }
              }
            }

            SourceIndex = SourceIndexTemp;
          }
        }
        break;
      }

      ++SourceIndex;
    }
  }
}
