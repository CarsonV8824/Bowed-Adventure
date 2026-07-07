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

    private List<Finger> _fingerList;

    private bool _SpawnNote;
    private bool _isDown;
    private Texture2D _DownSymbol;
    private Texture2D _UpSymbol;
    private readonly Action _toPauseMenu;


    public NoteScreen(Action pauseMenu)
    {
        _json = File.ReadAllText("assets/pieces/hot_cross_buns.json");
        _jsonNotes = JsonSerializer.Deserialize<JsonNotes>(_json, JsonOptions) ?? throw new InvalidOperationException("Failed to load piece JSON.");
        _tempo = _jsonNotes.Piece.Tempo;
        _inverval = 60 / _tempo;
        _noteIndex = 0;
        _noteList = new List<Note>();
        _fingerList = new List<Finger>();
        InitFingers();
        _SpawnNote = true;
        _DownSymbol = Raylib.LoadTexture("assets/images/nene.png");
        _UpSymbol = Raylib.LoadTexture("assets/images/nene.png");
        _isDown = true;
        _toPauseMenu = pauseMenu;
    }

    private void InitFingers()
    {
        for (int i = 1; i < 5; i++)
        {
            _fingerList.Add(new Finger(i));
        }
    }

    public void ChangePiece(string Piece)
    {
        _json = File.ReadAllText($"assets/pieces/{Piece}.json");
        _jsonNotes = JsonSerializer.Deserialize<JsonNotes>(_json, JsonOptions) ?? throw new InvalidOperationException("Failed to load piece JSON.");
        _tempo = _jsonNotes.Piece.Tempo;
        _inverval = 60 / _tempo;
        _noteIndex = 0;
        _noteList = new List<Note>();
        _SpawnNote = true;
    }

    private void AddNote(Vector2 noteVector, Texture2D noteTexture)
    {
        int finger = Convert.ToInt32(_jsonNotes.Notes[_noteIndex].Finger);
        float xPos = (Raylib.GetScreenWidth() * (5 - finger) / 6f) - (noteTexture.Width / 2f);
        noteVector.X = xPos;
        Note note = new Note(noteVector, noteTexture);
        _noteList.Add(note);
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
                Texture2D noteTexture = Raylib.LoadTexture("assets/images/nene.png");
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

        foreach (Finger finger in _fingerList)
        {
            finger.Update();
        }

        if (Raylib.IsKeyPressed(KeyboardKey.Up))
        {
            _isDown = false;
            Console.WriteLine("works");
        }
        else if (Raylib.IsKeyPressed(KeyboardKey.Down))
        {
            Console.WriteLine("works");
            _isDown = true;
        } else if (Raylib.IsKeyPressed(KeyboardKey.Escape))
        {
            _toPauseMenu.Invoke();
        }

    }

    public override void Draw()
    {
        int index = 0;
        foreach (Note note in _noteList.ToList())
        {
            note.Draw();
            var (x, y) = note.getCoordinates();
            if (x > Raylib.GetScreenWidth() || y > Raylib.GetScreenHeight())
            {
                try
                {
                    _noteList.RemoveAt(index);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine($"Index out of range at line 151 in Draw() in note_screen.cs: {e}");
                    break;
                }

            }
            index++;

        }

        foreach (Finger finger in _fingerList.ToList())
        {
            finger.Draw();
        }

        if (_isDown)
        {
            const int posY = 0;
            int posX = Raylib.GetScreenWidth() - (int) _DownSymbol.Dimensions.X;
            Raylib.DrawTexture(_DownSymbol, posX, posY, Color.Black);

        }
        else
        {
            const int posY = 0;
            int posX = Raylib.GetScreenWidth() - (int) _DownSymbol.Dimensions.X;
            Raylib.DrawTexture(_UpSymbol, posX, posY, Color.Blue);
        }
    }
}