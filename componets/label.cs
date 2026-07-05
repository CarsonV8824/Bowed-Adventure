using Raylib_cs;

class Label : Widget
{
    public Label(int x, int y, int width=200, int height=100, string text="")
    {
        ConstructorFormat(x, y, width, height, text, true);
    }
}