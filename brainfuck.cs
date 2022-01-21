using System;
using System.IO;
using System.Linq;

public static class Brainfuck
{
  private static string CleanSource(string[] Source)
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

  private static void ThrowError(string Error)
  {
    Console.ForegroundColor = ConsoleColor.Red;
    Console.Write("ERROR: ");
    Console.ResetColor();
    Console.WriteLine(Error);
    Environment.Exit(1);
  }

  private static void Interpret(string Source)
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
                ThrowError("Cannot find a matching closing bracket");
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
                ThrowError("Cannot find a matching opening bracket");
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

  public static void Main(string[] Args)
  {
    if (Args.Length <= 0)
    {
      Console.WriteLine("Brainfuck Interpreter v0.3\n");
      Console.ForegroundColor = ConsoleColor.DarkGray;
      Console.Write("Usage: ");
      Console.ResetColor();
      Console.WriteLine("brainfuck.exe [your file path]");
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
        ThrowError("Cannot open the targeted file");
      }
    }
  }
}
