use crate::core::colors::*;
use crate::core::tokens::Tokens;
use crate::options;
use std::io::{stdin, Read};

pub fn interpreter(source: Vec<Tokens>) {
    let mut tape: [u8; 30_000] = [0; 30_000];
    let mut tape_ptr = 0;
    let mut index = 0;

    while index < source.len() {
        match source[index] {
            Tokens::PTRINC => {
                if tape_ptr >= tape.len() - 1 {
                    tape_ptr = 0;
                } else {
                    tape_ptr += 1;
                }
            }
            Tokens::PTRDEC => {
                if tape_ptr <= 0 {
                    tape_ptr = tape.len() - 1;
                } else {
                    tape_ptr -= 1;
                }
            }
            Tokens::INC => {
                if tape[tape_ptr] >= 255 {
                    tape[tape_ptr] = 0;
                } else {
                    tape[tape_ptr] += 1;
                }
            }
            Tokens::DEC => {
                if tape[tape_ptr] <= 0 {
                    tape[tape_ptr] = 255;
                } else {
                    tape[tape_ptr] -= 1;
                }
            }
            Tokens::OUTPUT => {
                if options::DEBUG.lock().unwrap().clone() {
                    let mut char: String = String::from(tape[tape_ptr] as char);

                    match char.as_str() {
                        "\n" => char = "\\n".to_string(),
                        "\t" => char = "\\t".to_string(),
                        "\r" => char = "\\r".to_string(),
                        _ => {}
                    }

                    println!("{FG_CYAN}{}{RESET} => {FG_GREEN}\"{}\"{RESET}", tape[tape_ptr], char);
                } else {
                    print!("{}", tape[tape_ptr] as char);
                }
            }
            Tokens::INPUT => {
                let input = stdin()
                    .bytes()
                    .next()
                    .and_then(|result| result.ok())
                    .map(|byte| byte as u8);

                tape[tape_ptr] = input.unwrap();
            }
            Tokens::LOOPSTART => {
                if tape[tape_ptr] == 0 {
                    let mut iner_ptr = 1;
                    let mut iner_index = index;

                    while iner_ptr != 0 {
                        iner_index += 1;

                        match source[iner_index] {
                            Tokens::LOOPSTART => iner_ptr += 1,
                            Tokens::LOOPEND => iner_ptr -= 1,
                            _ => {}
                        }
                    }

                    index = iner_index;
                }
            }
            Tokens::LOOPEND => {
                if tape[tape_ptr] != 0 {
                    let mut iner_ptr = 1;
                    let mut iner_index = index;

                    while iner_ptr != 0 {
                        iner_index -= 1;

                        match source[iner_index] {
                            Tokens::LOOPSTART => iner_ptr -= 1,
                            Tokens::LOOPEND => iner_ptr += 1,
                            _ => {}
                        }
                    }

                    index = iner_index;
                }
            }
        }
        index += 1;
    }

    if options::TAPE.lock().unwrap().clone() {
        println!("{FG_BRIGHT_BLACK}{:?}{RESET}", tape);
    }
}
