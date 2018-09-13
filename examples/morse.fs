\ Helper words
defer m. ( -- )
defer m--- ( -- )
defer between-letters ( -- )
defer between-words ( -- )

: morse-letter
    toupper case
        [char] A of m. m---                  endof
        [char] B of m--- m. m. m.            endof
        [char] C of m--- m. m--- m.          endof
        [char] D of m--- m. m.               endof
        [char] E of m.                       endof
        [char] F of m. m. m--- m.            endof
        [char] G of m--- m--- m.             endof
        [char] H of m. m. m. m.              endof
        [char] I of m. m.                    endof
        [char] J of m. m--- m--- m---        endof
        [char] K of m--- m. m---             endof
        [char] L of m. m--- m. m.            endof
        [char] M of m--- m---                endof
        [char] N of m--- m.                  endof
        [char] O of m--- m--- m---           endof
        [char] P of m. m--- m--- m.          endof
        [char] Q of m--- m--- m. m---        endof
        [char] R of m. m--- m.               endof
        [char] S of m. m. m.                 endof
        [char] T of m---                     endof
        [char] U of m. m. m---               endof
        [char] V of m. m. m. m---            endof
        [char] W of m. m--- m---             endof
        [char] X of m--- m. m. m---          endof
        [char] Y of m--- m. m--- m---        endof
        [char] Z of m--- m--- m. m.          endof
        [char] 0 of m--- m--- m--- m--- m--- endof
        [char] 1 of m. m--- m--- m--- m---   endof
        [char] 2 of m. m. m--- m--- m---     endof
        [char] 3 of m. m. m. m--- m---       endof
        [char] 4 of m. m. m. m. m---         endof
        [char] 5 of m. m. m. m. m.           endof
        [char] 6 of m--- m. m. m. m.         endof
        [char] 7 of m--- m--- m. m. m.       endof
        [char] 8 of m--- m--- m--- m. m.     endof
        [char] 9 of m--- m--- m--- m--- m.   endof
    endcase ;

\ Public words
0 constant: START-WORD
1 constant: INSIDE-WORD

: space? ( c -- f ) bl = ;

: morse ( c-addr n -- )
    START-WORD -rot
    bounds ?do
        i c@ space? if
            INSIDE-WORD = if between-words then
            START-WORD
        else
            INSIDE-WORD = if between-letters then
            i c@ morse-letter
            INSIDE-WORD
        then
    loop
    drop ;

: print-dot [char] . emit ;
: print-dash [char] - emit ;
: space-between-letters space ;
: spaces-between-words 3 spaces ;

: print-morse
    ['] print-dot is m.
    ['] print-dash is m---
    ['] space-between-letters is between-letters
    ['] spaces-between-words is between-words ;

print-morse


\ ESP8266 specific code
GPIO load

13 variable: PIN

: times-dot-length ( n -- )
    250 * ms ;

: blink-dot ( -- )
    PIN GPIO_HIGH gpio-write
    1 times-dot-length
    PIN GPIO_LOW gpio-write
    1 times-dot-length ;

: blink-dash ( -- )
    PIN GPIO_HIGH gpio-write
    3 times-dot-length
    PIN GPIO_LOW gpio-write
    1 times-dot-length ;

: pause-between-letters ( -- )
    3 times-dot-length ;

: pause-between-words ( -- )
    7 times-dot-length ;

: blink-morse ( -- )
    ['] blink-dot is m.
    ['] blink-dash is m---
    ['] pause-between-letters is between-letters
    ['] pause-between-words is between-words ;

\blink-morse

