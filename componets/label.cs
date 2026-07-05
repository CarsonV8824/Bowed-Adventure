using Raylib_cs;

class Label : Widget
{
    public Label(int x=0, int y=0, int width=200, int height=100, string text="")
    {
        ConstructorFormat(x, y, width, height, text, true);
    }

    public override void Update(float dt)
    {
        return;
    }
}