GPIO load

variable: PIN
\ 13 is D7 on the development board
13 PIN !

PIN @ GPIO_OUT gpio-mode

: times-dot-length ( n -- )
    250 * ms ;

: led-on ( -- ) PIN @ GPIO_HIGH gpio-write ;
: led-off ( -- ) PIN @ GPIO_LOW gpio-write ;

: m. ( -- )
    led-on 1 times-dot-length
    led-off 1 times-dot-length ;

: m--- ( -- )
    led-on 3 times-dot-length
    led-off 1 times-dot-length ;

