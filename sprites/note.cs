using System.Numerics;
using System.Runtime.CompilerServices;
using Raylib_cs;

public class Note
{
    private Rectangle _rectangle;
    public int Finger {get;set;}
    public bool Hit = false;
    public bool Counted = false;
    public Note(Rectangle texture, float xPos, int finger)
    {
        _rectangle = texture;
        _rectangle.X = xPos;
        Finger = finger;
    }

    public void Draw()
    {
        Raylib.DrawRectangleRec(_rectangle, Color.Gray);
    }

    public void Update(float vel_x = 0, float vel_y = 0)
    {
        _rectangle.X += vel_x;
        _rectangle.Y += vel_y;
    }

    public virtual Tuple<float, float> getCoordinates()
    {
        Tuple<float, float> returnTuple = Tuple.Create(_rectangle.X, _rectangle.Y);
        return returnTuple;

    }

    public float GetTopOfRectY()
    {
        return _rectangle.Y;
    }

    public float GetYCoord()
    {
        float return_y = _rectangle.Width / 2f;
        return _rectangle.Y + return_y;
    }

    public float GetXCoor()
    {
        return _rectangle.X;
    }

    public float GetCenterX()
    {
        return _rectangle.X + (_rectangle.Width / 2f);
    }

    public float GetCenterY()
    {
        return _rectangle.Y + (_rectangle.Height / 2f);
    }

    public float GetWidth()
    {
        return _rectangle.Width;
    }

    public float GetHeight()
    {
        return _rectangle.Height;
    }

}