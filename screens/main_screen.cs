using Raylib_cs;

class MainScreen : Screen
{
    public override void Update()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Enter))
        {
            return;
        }
    }

    public override void Draw()
    {
        const string text = "Welcome to Bowed Adventure";
        const int BaseWidth = 1280;
        const int BaseFontSize = 32;

        float scale = (float)Raylib.GetScreenWidth() / BaseWidth;
        int fontSize = (int)(BaseFontSize * scale);

        fontSize = Math.Max(fontSize, 12);

        int textWidth = Raylib.MeasureText(text, fontSize);

        int x = (Raylib.GetScreenWidth() - textWidth) / 2;
        int y = Raylib.GetScreenHeight() / 2;

        Raylib.DrawText(text, x, y, fontSize, Color.Black);
    }
}