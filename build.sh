set -xe

nim c -d:release --opt:speed --passL:-s --verbosity:0 --spellSuggest:0 --hints:off --outdir:./build ./brainfuck.nim
