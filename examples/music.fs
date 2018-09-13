\ Notes

0  constant A
1  constant A#
1  constant Bb
2  constant B
3  constant C
4  constant C#
4  constant Db
5  constant D
6  constant D#
6  constant Eb
7  constant E
8  constant F
9  constant F#
9  constant Gb
10 constant G
11 constant G#
12 constant num-notes

: wrap-note ( note -- note' )
    num-notes mod ;

: print-note ( note -- )
    wrap-note case
    A  of ." A"     endof
    Bb of ." A#/Bb" endof
    B  of ." B"     endof
    C  of ." C"     endof
    C# of ." C#"    endof
    D  of ." D"     endof
    Eb of ." D#/Eb" endof
    E  of ." E"     endof
    F  of ." F"     endof
    F# of ." F#/Gb" endof
    G  of ." G"     endof
    G# of ." G#/Ab" endof
    endcase ;

\ Chords

0 constant major
1 constant minor
2 constant num-types-of-chords
num-notes num-types-of-chords * constant num-all-chords

: wrap-chord ( chord -- chord' )
    num-all-chords mod ;

: maj ( note -- chord )
    num-types-of-chords * major + ;

: min ( note -- chord )
    num-types-of-chords * minor + ;

: chord-root ( chord -- note )
    num-types-of-chords / ;

: chord-type ( chord -- chord-type )
    num-types-of-chords mod ;

: print-chord-type ( chord -- )
    chord-type case
    minor      of ." m"    endof
    \ diminished of ."  dim" endof
    endcase ;

: print-chord ( chord -- )
    dup chord-root print-note
    space
    print-chord-type ;

\ Scales

6 constant num-notes-in-scale
num-notes constant num-scales
create major-scale-degrees 0     , 2     , 4     , 5     , 7     , 9     ,
create major-scale-chords  0 maj , 2 min , 4 min , 5 maj , 7 maj , 9 min ,

: major-scale-note ( root-note degree -- note )
    cells major-scale-degrees + @ + wrap-note ;

: major-scale-chord ( root-note degree -- chord )
    cells major-scale-chords + @  \ root-note chord
    swap num-types-of-chords * + wrap-chord ;

: print-major-scale ( root-note -- )
    num-notes-in-scale 0 ?do
        cr
        dup i major-scale-note print-note
    loop ;

: print-major-scale-chords ( root-note -- )
    num-notes-in-scale 0 ?do
        cr
        dup i major-scale-chord print-chord
    loop ;

\ Related chords

variable related-chords

: chord-flag ( chord -- flag )
    1 swap lshift ;

: set-related-chord ( chord -- )
    chord-flag related-chords @ or related-chords ! ;

: is-related-chord ( chord  -- flag )
    chord-flag related-chords @ and ;

: clear-related-chords ( -- )
    0 related-chords ! ;

: find-related-chords ( chord -- )
    clear-related-chords
    cr
    num-scales 0 ?do
        num-notes-in-scale 0 ?do
            dup j i major-scale-chord = if
                num-notes-in-scale 0 ?do
                    k i major-scale-chord set-related-chord
                loop

                leave
            endif
        loop
    loop 
    
    ( chord ) num-all-chords bounds ?do
        dup i wrap-chord is-related-chord if
            dup i print-chord cr
        endif
        drop
    loop ;
