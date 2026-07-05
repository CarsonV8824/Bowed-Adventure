using Raylib_cs;

class Button : Widget
{
    public Action? Clicked {get; set; }
    public Button(int x, int y, int width=200, int height=100)
    {
        Rectangle tempRect;
        tempRect.X = x;
        tempRect.Y = y;
        tempRect.Width = width;
        tempRect.Height = height;
        Bounds = tempRect;
    }

    public void Update(float dt)
    {
        if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), Bounds) &&
            Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            Clicked?.Invoke();
        }
    }


    
}