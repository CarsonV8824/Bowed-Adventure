using Raylib_cs;

class MainScreen : Screen
{
    private Label _MainLabel;

    public MainScreen()
    {
        const string text = "Welcome to Bowed Adventure";

        int x = Raylib.GetScreenWidth() / 2;
        int y = Raylib.GetScreenHeight() / 2;
        _MainLabel = new Label(x, y, text:text);
    }
    public override void Update(float dt)
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Enter))
        {
            return;
        }
    }

    public override void Draw()
    {
        _MainLabel.Draw();
    }
}