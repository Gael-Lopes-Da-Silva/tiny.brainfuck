import os, strutils

proc interpret(source: string): void =
  var
    cells: array[30000, int]
    cellPtr: int = 0
    sourcePtr: int = 0

  while sourcePtr < len(source):
    case source[sourcePtr]
    of '>':
      if cellPtr == len(cells)-1: cellPtr = 0
      else: inc cellPtr

    of '<':
      if cellPtr == 0: cellPtr = len(cells)-1
      else: dec cellPtr

    of '+':
      if cells[cellPtr] == 255: cells[cellPtr] = 0
      else: inc cells[cellPtr]

    of '-':
      if cells[cellPtr] == 0: cells[cellPtr] = 255
      else: dec cells[cellPtr]

    of '.':
      stdout.write cells[cellPtr].char()

    of ',':
      cells[cellPtr] = readChar(stdin).int()

    of '[':
      if cells[cellPtr] == 0:
        var
          sourcePtrTemp: int = sourcePtr
          count: int = 1

        while count != 0:
          if sourcePtrTemp == len(source)-1:
            assert true, "ERROR: matching closing brackets error"
          else:
            inc sourcePtrTemp
            if source[sourcePtrTemp] == '[': inc count
            elif source[sourcePtrTemp] == ']': dec count
        
        sourcePtr = sourcePtrTemp

    of ']':
      if cells[cellPtr] != 0:
        var
          sourcePtrTemp: int = sourcePtr
          count: int = 1

        while count != 0:
          if sourcePtrTemp == 0:
            assert true, "ERROR: matching opening brackets error"
          else:
            dec sourcePtrTemp
            if source[sourcePtrTemp] == '[': dec count
            elif source[sourcePtrTemp] == ']': inc count

        sourcePtr = sourcePtrTemp

    else: discard
    inc sourcePtr

proc compile(source: string): void =
  discard

proc cleanup(source: seq[string]): string =

  var cleanedSource: string = ""
  for line in source:
    if line == "": continue
    elif line[0] != '#':
      for character in line:
        if character in ['>', '<', '+', '-', '.', ',', '[', ']']:
          if character == '#': break
          cleanedSource.add(character)
  
  return cleanedSource

proc main(paramsList: seq[string]): int =
  if len(paramsList) == 0:
    echo "Brainfuck interpreter v0.1\n\nusage: brainfuck [<file>]"

  elif len(paramsList) == 1:
    echo "You need to specifie the targeted brainfuck file."

  elif len(paramsList) == 2:
    let source: string = cleanup(readFile(paramsList[1]).splitLines())

    if paramsList[0].toLower() == "int": interpret(source)
    elif paramsList[0].toLower() == "com": compile(source)
    elif paramsList[0] == "clr": echo cleanup(readFile(paramsList[1]).splitLines())
  
  return 0

assert main(commandLineParams()) == 0, "ERROR: non zero error"
