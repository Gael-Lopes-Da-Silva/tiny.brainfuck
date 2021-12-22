import os, strutils

proc cleanSource(source: seq[string]): string
proc interpret(source: string): void
proc compile(source: string): void

proc main(paramsList: seq[string]): void =
  if len(paramsList) == 0:
    echo "Brainfuck interpreter v0.1\n"
    echo "usage: brainfuck int/com [<file>]"
    return

  elif len(paramsList) == 1:
    echo "You need to specifie the targeted brainfuck file or use int to interpret or com to compile the file."
    return

  elif len(paramsList) == 2:
    var file: File
    if file.open(paramsList[1]):
      let source: string = cleanSource(readFile(paramsList[1]).splitLines())
      file.close()

      if paramsList[0].toLower() == "int": interpret(source)
      elif paramsList[0].toLower() == "com": compile(source)
    else:
      echo "ERROR: file cannot be opened"
      return

proc cleanSource(source: seq[string]): string =
  var cleanedSource: string = ""
  for line in source:
    if line == "": continue
    elif line[0] != '#':
      for character in line:
        if character == '#': break
        elif character in ['>', '<', '+', '-', '.', ',', '[', ']']:
          cleanedSource.add(character)

  return cleanedSource

proc interpret(source: string): void =
  var
    tape: array[30000, int]
    cellPtr: int = 0
    sourceIndex: int = 0

  while sourceIndex < len(source):
    case source[sourceIndex]
    of '>':
      if cellPtr == len(tape)-1: cellPtr = 0
      else: inc cellPtr

    of '<':
      if cellPtr == 0: cellPtr = len(tape)-1
      else: dec cellPtr

    of '+':
      if tape[cellPtr] == 255: tape[cellPtr] = 0
      else: inc tape[cellPtr]

    of '-':
      if tape[cellPtr] == 0: tape[cellPtr] = 255
      else: dec tape[cellPtr]

    of '.':
      stdout.write tape[cellPtr].char()

    of ',':
      tape[cellPtr] = readChar(stdin).int()

    of '[':
      if tape[cellPtr] == 0:
        var
          sourceIndexTemp: int = sourceIndex
          count: int = 1

        while count != 0:
          if sourceIndexTemp == len(source)-1:
            echo "ERROR: matching closing brackets error"
            return
          else:
            inc sourceIndexTemp
            if source[sourceIndexTemp] == '[': inc count
            elif source[sourceIndexTemp] == ']': dec count
        
        sourceIndex = sourceIndexTemp

    of ']':
      if tape[cellPtr] != 0:
        var
          sourceIndexTemp: int = sourceIndex
          count: int = 1

        while count != 0:
          if sourceIndexTemp == 0:
            echo "ERROR: matching opening brackets error"
            return
          else:
            dec sourceIndexTemp
            if source[sourceIndexTemp] == '[': dec count
            elif source[sourceIndexTemp] == ']': inc count

        sourceIndex = sourceIndexTemp

    else: discard
    inc sourceIndex

proc compile(source: string): void =
  # todo: https://igor.io/2014/10/27/compiling-brainfuck.html
  # todo: https://github.com/igorw/naegleria/blob/master/src/compiler.php
  discard

if isMainModule:
  main(commandLineParams())
