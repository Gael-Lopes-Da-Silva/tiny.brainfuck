using System;
using System.IO;
using System.Linq;

static class Brainfuck
{
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

  private static void Main(string[] Args)
  {
    if (Args.Length <= 0)
    {
      Console.WriteLine("Brainfuck Interpreter v0.2\n\nUsage: brainfuck.exe yourFile");
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
}
