using System.Numerics;
using System.Runtime.CompilerServices;
using Raylib_cs;

public class Note
{
    private Rectangle _rectangle;
    public Note(Rectangle texture, float xPos)
    {
        _rectangle = texture;
        _rectangle.X = xPos;
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

    public float GetYCoord()
    {
        float return_y = _rectangle.Width / 2f;
        return _rectangle.Y + return_y;
    }

}