# Generating new json pieces

this file will expain and provide examples on how to make new JSON files for the game

## Template Style

each json file will store its own piece configuration settings to let the user play. here is the format. The two outer keys in the object are:

### Piece

- tempo: int

- time: str like 4/4 or 6/8

- title: str

- composer: str

### Notes

the notes part of the json is as long as the piece has that many notes. Notes will have an array with objects inside with these attributes:

- note: str (12 notes on the chromatic scale)

- finger: int

- pos: int

- playedString: str

- length: str (type of note. Example: quarter, eigth, sixteenth)

when doing rests, put in rest for note, then the type of rest in length

## Example

here is an example of a sample json file
```json
{
    "piece": {
        "tempo": 60,
        "time": "4/4",
        "title": "hot cross buns",
        "composer": "traditional"
    },
    "notes": [
        {
            "note": "f#",
            "finger": 3,
            "pos": 1,
            "playedString": "d",
            "length": "quarter"
        },
        {
            "note": "e",
            "finger": 3,
            "pos": 1,
            "playedString": "d",
            "length": "quarter"
        },
        {
            "note": "d",
            "finger": 3,
            "pos": 1,
            "playedString": "d",
            "length": "quarter"
        },
                {
            "note": "rest",
            "finger": 3,
            "pos": 1,
            "playedString": "d",
            "length": "quarter"
        },
        {
            "note": "f#",
            "finger": 3,
            "pos": 1,
            "playedString": "d",
            "length": "quarter"
        },
        {
            "note": "e",
            "finger": 3,
            "pos": 1,
            "playedString": "d",
            "length": "quarter"
        },
        {
            "note": "d",
            "finger": 3,
            "pos": 1,
            "playedString": "d",
            "length": "quarter"
        },
        {
            "note": "rest",
            "finger": 3,
            "pos": 1,
            "playedString": "d",
            "length": "quarter"
        },
        {
            "note": "d",
            "finger": 3,
            "pos": 1,
            "playedString": "d",
            "length": "eigth"
        },
        {
            "note": "d",
            "finger": 3,
            "pos": 1,
            "playedString": "d",
            "length": "eigth"
        },
        {
            "note": "d",
            "finger": 3,
            "pos": 1,
            "playedString": "d",
            "length": "eigth"
        },
        {
            "note": "d",
            "finger": 3,
            "pos": 1,
            "playedString": "d",
            "length": "eigth"
        },
                {
            "note": "e",
            "finger": 3,
            "pos": 1,
            "playedString": "d",
            "length": "eigth"
        },
        {
            "note": "e",
            "finger": 3,
            "pos": 1,
            "playedString": "d",
            "length": "eigth"
        },
        {
            "note": "e",
            "finger": 3,
            "pos": 1,
            "playedString": "d",
            "length": "eigth"
        },
        {
            "note": "e",
            "finger": 3,
            "pos": 1,
            "playedString": "d",
            "length": "eigth"
        },
        {
            "note": "f#",
            "finger": 3,
            "pos": 1,
            "playedString": "d",
            "length": "quarter"
        },
        {
            "note": "e",
            "finger": 3,
            "pos": 1,
            "playedString": "d",
            "length": "quarter"
        },
        {
            "note": "d",
            "finger": 3,
            "pos": 1,
            "playedString": "d",
            "length": "quarter"
        },
                {
            "note": "rest",
            "finger": 3,
            "pos": 1,
            "playedString": "d",
            "length": "quarter"
        }
    ]
}
```