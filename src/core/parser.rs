use crate::core::tokens::Tokens;
use std::option::Option;

pub fn parser(tokens: Vec<Tokens>) -> Option<Vec<Tokens>> {
    let mut loop_count = 0;

    for token in tokens.iter() {
        match token {
            Tokens::LOOPSTART => loop_count += 1,
            Tokens::LOOPEND => loop_count -= 1,
            _ => {},
        }
    }

    if loop_count == 0 {
        Some(tokens)
    } else {
        None
    }
}
