import os

let paramsList: seq[string] = commandLineParams()

if len(paramsList) == 0:
  echo "Brainfuck interpreter v0.1\n\nusage: brainfuck [<file>]"

elif len(paramsList) == 1:
  let source: string = readFile(paramsList[0])

  var
    stack: array[30000, int]
    stackIndex: int = 0
    sourceIndex: int = 0

  while sourceIndex != len(source)-1:
    case source[sourceIndex]
    of '>':
      if stackIndex == len(stack)-1: stackIndex = 0
      else: inc stackIndex

    of '<':
      if stackIndex == 0: stackIndex = len(stack)-1
      else: dec stackIndex

    of '+':
      if stack[stackIndex] == 255: stack[stackIndex] = 0
      else: inc stack[stackIndex]

    of '-':
      if stack[stackIndex] == 0: stack[stackIndex] = 255
      else: dec stack[stackIndex]

    of '.':
      stdout.write stack[stackIndex].char()

    of ',':
      stack[stackIndex] = readChar(stdin).int()

    of '[':
      if stack[stackIndex] == 0:
        var
          sourceIndexTemp: int = sourceIndex
          count: int = 1

        while true:
          if count == 0:
            sourceIndex = sourceIndexTemp
            break
          elif sourceIndexTemp == len(source)-1: assert true, "ERROR: matching brackets error"

          inc sourceIndexTemp
          if source[sourceIndexTemp] == '[': inc count
          elif source[sourceIndexTemp] == ']': dec count

    of ']':
      if stack[stackIndex] != 0:
        var
          sourceIndexTemp: int = sourceIndex
          count: int = 1

        while true:
          if count == 0:
            sourceIndex = sourceIndexTemp
            break
          elif sourceIndexTemp == 0: assert true, "ERROR: matching brackets error"
            
          dec sourceIndexTemp
          if source[sourceIndexTemp] == '[': dec count
          elif source[sourceIndexTemp] == ']': inc count

    else: discard

    inc sourceIndex
