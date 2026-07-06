using System.Numerics;
using Raylib_cs;

public abstract class Sprite
{
    private protected Vector2 _posistion;
    private protected Texture2D _texture;

    public virtual void Draw()
    {
        Raylib.DrawTextureV(_texture, _posistion, Color.White);
    }

    public virtual void Update(float vel_x=0, float vel_y=0)
    {
        _posistion.X += vel_x;
        _posistion.Y += vel_y;
    }

    public virtual Tuple<float, float> getCoordinates()
    {
        Tuple<float, float> returnTuple = Tuple.Create(_posistion.X, _posistion.Y);
        return returnTuple;

    }


}