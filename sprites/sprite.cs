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

    public virtual void Update(int vel_x, int vel_y)
    {
        _posistion.X += vel_x;
        _posistion.Y += vel_y;
    }


}