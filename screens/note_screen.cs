using Raylib_cs;
using System.Text.Json;

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


    public NoteScreen()
    {
        _json = File.ReadAllText("pieces/hot_cross_buns.json");
        _jsonNotes = JsonSerializer.Deserialize<JsonNotes>(_json, JsonOptions) ?? throw new InvalidOperationException("Failed to load piece JSON.");
        _tempo = _jsonNotes.Piece.Tempo;
        _inverval = 60 / _tempo;
        _noteIndex = 0;
    }
    public override void Update(float dt)
    {
        _spawnTimer += dt;
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

        if (_spawnTimer >= _lengthOfNote)
        {
            _noteIndex++;
            _spawnTimer = 0;
            if (_noteIndex >= _jsonNotes.Notes.Count) 
            {
                _noteIndex = 0;
            }
        }
    }

    public override void Draw()
    {
        return;
    }
}