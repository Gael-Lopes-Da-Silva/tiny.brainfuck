use std::sync::Mutex;

pub static VERSION: &str = "v0.2";
pub static DEBUG: Mutex<bool> = Mutex::new(false);
pub static TAPE: Mutex<bool> = Mutex::new(false);
