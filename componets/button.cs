using Raylib_cs;

class Button : Widget
{
    public Action? Clicked {get; set; }
    public Button(int x=0, int y=0, int width=200, int height=100, string? text="")
    {
        ConstructorFormat(x, y, width, height, text);
    }

    public override void Update(float dt)
    {
        if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), Bounds) &&
            Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            Clicked?.Invoke();
        }
    }

}