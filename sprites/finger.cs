using System.Numerics;
using Raylib_cs;



class Finger : Sprite
{
    private const float Scale = 0.1f;
    private protected readonly Texture2D _pressedtexture;
    private protected readonly int _FingerNumber;
    private readonly Dictionary<int, KeyboardKey> fingerLookup = new()
    {
        [1] = KeyboardKey.H,
        [2] = KeyboardKey.J,
        [3] = KeyboardKey.K,
        [4] = KeyboardKey.L
    };

    public Finger(int finger_number)
    {
        _FingerNumber = finger_number;
        _texture = Raylib.LoadTexture("assets/images/finger.png");
        GetCoordsOfFingers(finger_number);
        
    }

    public override void Draw()
    {
        Raylib.DrawTextureEx(_texture, _posistion, 0, Scale, Color.White);
    }

    public override void Update(float vel_x = 0, float vel_y = 0)
    {
        if (Raylib.IsKeyPressed(fingerLookup[_FingerNumber]))
        {
            Console.WriteLine($"key {fingerLookup[_FingerNumber]}");
        }
    }

    private void GetCoordsOfFingers(int finger_number)
    {
        float scaledWidth = _texture.Width * Scale;
        float scaledHeight = _texture.Height * Scale;

        _posistion.X = (Raylib.GetScreenWidth() * finger_number / 5f) - (scaledWidth / 2f);
        _posistion.Y = Raylib.GetScreenHeight() - scaledHeight;
    }
}