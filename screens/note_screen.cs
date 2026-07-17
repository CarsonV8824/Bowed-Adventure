using Raylib_cs;
using System.Text.Json;
using System.Numerics;
using System.IO;

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
    public int NoteIndex { get; set; }
    private float _lengthOfNote;

    private List<Note> _noteList;

    private List<Finger> _fingerList;

    private bool _SpawnNote;
    private bool? _isDown;
    private Texture2D _DownSymbol;
    private Texture2D _UpSymbol;
    private readonly Action _toPauseMenu;
    private const int NoteSpeed = 1000;
    public int Score { get; set; }
    public int ExpectedScore { get; set; } = 0;
    private readonly Action _toResultWindow;


    public NoteScreen(Action pauseMenu, Action resultScreen)
    {
        
        string[] files = Directory.GetFiles("assets/pieces");
        int randomIndex = Random.Shared.Next(0, files.Length);
        var choosenFilePath = files[randomIndex];

        _json = File.ReadAllText(choosenFilePath);
        _jsonNotes = JsonSerializer.Deserialize<JsonNotes>(_json, JsonOptions) ?? throw new InvalidOperationException("Failed to load piece JSON.");
        _tempo = _jsonNotes.Piece.Tempo;
        _inverval = 60f / _tempo;
        NoteIndex = 0;
        _noteList = new List<Note>();
        _fingerList = new List<Finger>();
        InitFingers();
        _SpawnNote = true;
        _DownSymbol = Raylib.LoadTexture("assets/images/nene.png");
        _UpSymbol = Raylib.LoadTexture("assets/images/nene.png");
        _isDown = true;
        _toPauseMenu = pauseMenu;
        _toResultWindow = resultScreen;
    }

    private void InitFingers()
    {
        for (int i = 1; i < 5; i++)
        {
            _fingerList.Add(new Finger(i));
        }
    }

    public void ChangePiece(string Piece = "hot_cross_buns")
    {
        _json = File.ReadAllText($"assets/pieces/{Piece}.json");
        _jsonNotes = JsonSerializer.Deserialize<JsonNotes>(_json, JsonOptions) ?? throw new InvalidOperationException("Failed to load piece JSON.");
        _tempo = _jsonNotes.Piece.Tempo;
        _inverval = 60 / _tempo;
        _spawnTimer = 0;
        _lengthOfNote = 0;
        NoteIndex = 0;
        _noteList = new List<Note>();
        _SpawnNote = true;
        Score = 0;
        ExpectedScore = 0;
    }

    private void AddNote(Rectangle noteTexture)
    {
        int finger = Convert.ToInt32(_jsonNotes.Notes[NoteIndex].Finger);
        float xPos = (Raylib.GetScreenWidth() * (5 - finger) / 6f) - (noteTexture.Width / 2f);
        Note note = new Note(noteTexture, xPos, finger);
        _noteList.Add(note);
    }

    private void PrepareNotes()
    {
        const float spawnBaselineY = 200f;
        float size = _inverval * NoteSpeed;
        if (_SpawnNote)
        {
            string typeOfNote = _jsonNotes.Notes[NoteIndex].Length;
            _isDown = Convert.ToBoolean(_jsonNotes.Notes[NoteIndex].IsDown);


            switch (typeOfNote.ToLower())
            {
                case "quarter":
                    _lengthOfNote = _inverval;
                    break;
                case "eigth":
                case "eighth":
                    _lengthOfNote = _inverval / 2;
                    size /= 2;
                    break;
                case "sixteenth":
                    _lengthOfNote = _inverval / 4;
                    size /= 4;
                    break;
                case "half":
                    _lengthOfNote = _inverval * 2;
                    size *= 2;
                    break;
                default:
                    throw new Exception("JSON length of note could not be found in case in note_screen.cs in Update");
            }



        }

        if (_SpawnNote)
        {
            if (_jsonNotes.Notes[NoteIndex].Note != "rest")
            {
                // Keep all notes aligned by their leading edge so mixed lengths do not shift early/late.
                float spawnTopY = spawnBaselineY - size;
                Rectangle noteRect = new Rectangle(100, spawnTopY, new Vector2(100, size));
                AddNote(noteRect);
            }
            _SpawnNote = false;
        }

        if (_spawnTimer >= _lengthOfNote && _lengthOfNote != 0)
        {
            float completedNoteLength = _lengthOfNote;
            NoteIndex++;
            if (NoteIndex >= _jsonNotes.Notes.Count)
            {
                _SpawnNote = false;
                _toResultWindow.Invoke();
            }
            else
            {
                _SpawnNote = true;
                _lengthOfNote = 0;
            }

            // Keep any extra elapsed time so timing stays consistent across note-length changes.
            _spawnTimer -= completedNoteLength;

        }
    }

    public override void Update(float dt)
    {
        _spawnTimer += dt;

        PrepareNotes();

        foreach (Note note in _noteList)
        {
            float ySpeed = NoteSpeed * dt;
            note.Update(vel_y: ySpeed);
        }

        foreach (Finger finger in _fingerList)
        {
            finger.Update();
        }

        bool anyFingerPressed = _fingerList.Any(finger => finger.IsPressed);
        int countOfFingersPressed = _fingerList.Count(finger => finger.IsPressed);


        // collision logic
        foreach (Note note in _noteList.ToList())
        {
            if (note.Finger == 0)
            {
                float noteCenterY = note.GetCenterY();
                float pressLineY = 300f + (_fingerList[0].GetHeight() / 2f);
                float hitHeight = (note.GetHeight() + _fingerList[0].GetHeight()) / 2f;
                if (!anyFingerPressed)
                {
                    if (Math.Abs(noteCenterY - pressLineY) <= hitHeight && !note.Hit)
                    {
                        note.Hit = true;
                        Score++;
                    }
                }
                if (Math.Abs(noteCenterY - pressLineY) <= hitHeight && !note.Counted)
                {
                    note.Counted = true;
                    ExpectedScore++;
                    Console.WriteLine(ExpectedScore);
                }

                continue;
            }

            foreach (Finger finger in _fingerList.ToList())
            {

                // In update loop
                float noteCenterX = note.GetCenterX();
                float fingerCenterX = finger.GetCenterX();
                float noteCenterY = note.GetCenterY();
                float fingerCenterY = finger.GetCenterY();

                float hitWidth = (note.GetWidth() + finger.GetWidth()) / 2f;
                float hitHeight = (note.GetHeight() + finger.GetHeight()) / 2f;

                bool overlaps =
                    Math.Abs(noteCenterY - fingerCenterY) <= hitHeight &&
                    Math.Abs(noteCenterX - fingerCenterX) <= hitWidth;

                // Give score only once
                if (!note.Hit &&
                    finger.IsPressed &&
                    overlaps &&
                    countOfFingersPressed == 1 &&
                    note.Finger != 0)
                {
                    Score++;
                    note.Hit = true;
                }

                // Count expected score only once
                if (!note.Counted &&
                    noteCenterY >= Raylib.GetScreenHeight() - note.GetHeight() / 2f &&
                    note.Finger != 0)
                {
                    ExpectedScore++;
                    note.Counted = true;
                    Console.WriteLine(ExpectedScore);
                }
            }
        }

        if (Raylib.IsKeyPressed(KeyboardKey.Escape))
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

        if (_isDown == true)
        {
            const int posY = 0;
            int posX = Raylib.GetScreenWidth() - (int)_DownSymbol.Dimensions.X;
            Raylib.DrawTexture(_DownSymbol, posX, posY, Color.Black);

        }
        else if (_isDown == false)
        {
            const int posY = 0;
            int posX = Raylib.GetScreenWidth() - (int)_DownSymbol.Dimensions.X;
            Raylib.DrawTexture(_UpSymbol, posX, posY, Color.Blue);
        }

        string score = $"Score: {Score}";
        Raylib.DrawText(score, 0, 0, 20, Color.Black);


    }
}