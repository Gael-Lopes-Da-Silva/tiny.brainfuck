use crate::core::tokens::Tokens;

pub fn lexer(source: Vec<String>) -> Vec<Tokens> {
    let mut token_list = Vec::new();

    for line in source.iter() {
        for char in line.chars() {
            match char {
                '>' => token_list.push(Tokens::PTRINC),
                '<' => token_list.push(Tokens::PTRDEC),
                '+' => token_list.push(Tokens::INC),
                '-' => token_list.push(Tokens::DEC),
                '.' => token_list.push(Tokens::OUTPUT),
                ',' => token_list.push(Tokens::INPUT),
                '[' => token_list.push(Tokens::LOOPSTART),
                ']' => token_list.push(Tokens::LOOPEND),
                '#' => break,
                _ => {},
            }
        }
    }

    return token_list;
}
