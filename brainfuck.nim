import os

let paramsList: seq[string] = commandLineParams()

if len(paramsList) == 0: echo "Brainfuck interpreter v0.1\n\nusage: brainfuck [<file>]"
elif len(paramsList) == 1:
  let input: string = readFile(paramsList[0])

  var
    source: seq[char]
    stack: array[30000, int]
    stackIndex: int = 0
    sourceIndex: int = 0
    loopIndex: seq[int]

  for character in input:
    source.add(character)

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
      if stack[stackIndex] == 0: stack[stackIndex] = 0
      else: dec stack[stackIndex]
    of '.': stdout.write stack[stackIndex].char()
    of ',': stack[stackIndex] = readChar(stdin).int()
    of '[': loopIndex.add(sourceIndex)
    of ']':
      if stack[stackIndex] == 0: loopIndex.delete(loopIndex[^1])
      else: sourceIndex = loopIndex[^1]
    else: discard

    inc sourceIndex
