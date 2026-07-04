using Raylib_cs;
using System.Text.Json;
using System.Numerics;

class NoteScreen : Screen
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };
    private string _json;
    private JsonNotes _jsonNotes;
    private float _tempo;
    private float _inverval;
    private float _spawnTimer;
    private int _noteIndex;
    private float _lengthOfNote;

    private List<Note> _noteList;

    private bool _SpawnNote;


    public NoteScreen()
    {
        _json = File.ReadAllText("pieces/hot_cross_buns.json");
        _jsonNotes = JsonSerializer.Deserialize<JsonNotes>(_json, JsonOptions) ?? throw new InvalidOperationException("Failed to load piece JSON.");
        _tempo = _jsonNotes.Piece.Tempo;
        _inverval = 60 / _tempo;
        _noteIndex = 0;
        _noteList = new List<Note>();
        _SpawnNote = true;

    }

    private void AddNote(Vector2 noteVector, Texture2D noteTexture)
    {
        _noteList.Add(new Note(noteVector, noteTexture));
    }

    private void PrepareNotes()
    {
        if (_SpawnNote)
        {
            string typeOfNote = _jsonNotes.Notes[_noteIndex].Length;


            switch (typeOfNote.ToLower())
            {
                case "quarter":
                    _lengthOfNote = _inverval;
                    break;
                case "eigth":
                    _lengthOfNote = _inverval / 2;
                    break;
                default:
                    throw new Exception("JSON length of note could not be found in case in note_screen.cs in Update");
            }
        }

        if (_SpawnNote)
        {
            if (_jsonNotes.Notes[_noteIndex].Note != "rest")
            {
                Vector2 noteVector = new Vector2(100, 100);
                Texture2D noteTexture = Raylib.LoadTexture("assets/nene.png");
                AddNote(noteVector, noteTexture);
            }
            _SpawnNote = false;
        }

        if (_spawnTimer >= _lengthOfNote && _lengthOfNote != 0)
        {
            _noteIndex++;
            if (_noteIndex >= _jsonNotes.Notes.Count)
            {
                _SpawnNote = false;
            }
            else
            {
                _SpawnNote = true;
                _lengthOfNote = 0;
            }

            _spawnTimer = 0;

        }
    }

    public override void Update(float dt)
    {
        _spawnTimer += dt;

        PrepareNotes();

        foreach (Note note in _noteList)
        {
            float ySpeed = 100 * dt;
            note.Update(vel_y: ySpeed);
        }
    }

    public override void Draw()
    {
        foreach (Note note in _noteList)
        {
            note.Draw();
        }
    }
}