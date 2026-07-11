using System.Numerics;
using Raylib_cs;



class Finger : Sprite
{
    private const float Scale = 0.1f;
    private protected readonly Texture2D _pressedtexture;
    private protected readonly int _FingerNumber;
    private readonly Dictionary<int, KeyboardKey> fingerLookup = new()
    {
        [1] = KeyboardKey.A,
        [2] = KeyboardKey.S,
        [3] = KeyboardKey.D,
        [4] = KeyboardKey.F
    };
    public bool IsPressed { get; set; }

    public Finger(int finger_number)
    {
        IsPressed = false;
        _FingerNumber = finger_number;
        _texture = Raylib.LoadTexture("assets/images/finger.png");
        _pressedtexture = Raylib.LoadTexture("assets/images/pressed.png");
        GetCoordsOfFingers(finger_number);

    }

    public override void Draw()
    {
        Raylib.DrawTextureEx(_texture, _posistion, 0, Scale, Color.White);
        if (IsPressed)
        {
            for (int i = 0; i < 2; i++)
            {
                Vector2 pressedPosition = _posistion;
                pressedPosition.Y = 500 - (i * 100);
                Raylib.DrawTextureEx(_pressedtexture, pressedPosition, 0, Scale, Color.White);
            }

        }
    }

    public override void Update(float vel_x = 0, float vel_y = 0)
    {
        if (Raylib.IsKeyDown(fingerLookup[_FingerNumber]))
        {
            GetCoordsOfFingers(_FingerNumber);
            IsPressed = true;
            _posistion.Y = 300;
        }
        else
        {
            GetCoordsOfFingers(_FingerNumber);
            IsPressed = false;
        }
    }

    private void GetCoordsOfFingers(int finger_number)
    {
        float scaledWidth = _texture.Width * Scale;
        float scaledHeight = _texture.Height * Scale;

        if (!IsPressed)
        {
            _posistion.Y = Raylib.GetScreenHeight() - scaledHeight;
        }
        _posistion.X = (Raylib.GetScreenWidth() * finger_number / 6f) - (scaledWidth / 2f);

    }

    public float GetWidth()
    {
        return _texture.Width * Scale;
    }

    public float GetHeight()
    {
        return _texture.Height * Scale;
    }

    public float GetCenterX()
    {
        return _posistion.X + (GetWidth() / 2f);
    }

    public float GetCenterY()
    {
        return _posistion.Y + (GetHeight() / 2f);
    }

}