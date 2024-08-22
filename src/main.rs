use std::env;
use std::fs::read_to_string;
use std::path::Path;

mod core;
mod options;
use core::colors::*;
use core::*;

fn main() {
    let args: Vec<String> = env::args().collect();

    if args.len() - 1 == 0 {
        println!("{FG_GREEN}{}{RESET}\n\n", "Mini.Brainfuck");
        println!(
            "{FG_BRIGHT_BLACK}{}{RESET}: {} --file <filename>\n",
            "USAGE", args[0]
        );
        println!(
            "{}\n{}\n{}\n{}\n{}",
            "  -f, --file       Open a file and interpret it's content",
            "  -i, --interpret  Interpret a given input",
            "  -d, --debug      Print a debug output of the interpreted input",
            "  -t, --tape       Print the tape after the interpretation of the input",
            "  -v, --version    Print the interpreter version"
        );
    } else {
        if args.contains(&String::from("--debug")) || args.contains(&String::from("-d")) {
            let mut debug = options::DEBUG.lock().unwrap();
            *debug = true;
        }

        if args.contains(&String::from("--tape")) || args.contains(&String::from("-t")) {
            let mut tape = options::TAPE.lock().unwrap();
            *tape = true;
        }

        if args.contains(&"--file".to_string()) || args.contains(&"-f".to_string()) {
            let index = args
                .iter()
                .position(|value| value == "--file" || value == "-f")
                .unwrap();
            let filename = args.get(index + 1);

            if filename.is_none() {
                println!("{FG_RED}{BOLD}{}{RESET}: No filename provided", "ERROR");
            } else {
                if !Path::new(filename.unwrap()).exists() {
                    println!(
                        "{FG_RED}{BOLD}{}{RESET}: File {FG_GREEN}\"{}\"{RESET} not found",
                        "ERROR",
                        filename.unwrap()
                    );
                    return;
                }

                let input: Vec<String> = read_to_string(filename.unwrap())
                    .unwrap()
                    .lines()
                    .map(String::from)
                    .collect();
                let parsed_input = parser::parser(lexer::lexer(input));

                if parsed_input.is_none() {
                    println!(
                        "{FG_RED}{BOLD}{}{RESET}: Loop not closed in source code",
                        "ERROR"
                    );
                    return;
                } else {
                    interpreter::interpreter(parsed_input.unwrap());
                }
            }

            return;
        } else if args.contains(&"--interpret".to_string()) || args.contains(&"-i".to_string()) {
            let index = args
                .iter()
                .position(|value| value == "--interpret" || value == "-i")
                .unwrap();
            let source = args.get(index + 1);

            if source.is_none() {
                println!("{FG_RED}{BOLD}{}{RESET}: No filename provided", "ERROR");
            } else {
                let input: Vec<String> = source.unwrap().lines().map(String::from).collect();
                let parsed_input = parser::parser(lexer::lexer(input));

                if parsed_input.is_none() {
                    println!(
                        "{FG_RED}{BOLD}{}{RESET}: Loop not closed in source code",
                        "ERROR"
                    );
                    return;
                } else {
                    interpreter::interpreter(parsed_input.unwrap());
                }
            }

            return;
        } else if args.contains(&"--version".to_string()) || args.contains(&"-v".to_string()) {
            println!("{}", options::VERSION);
            return;
        }

        println!(
            "{FG_RED}{BOLD}{}{RESET}: Argument {FG_GREEN}\"{}\"{RESET} not valid",
            "ERROR", args[1]
        );
    }
}
