using Raylib_cs;
using System.Text.Json;

class NoteScreen : Screen
{

    public NoteScreen()
    {
        string json = File.ReadAllText("pieces/hot_cross_buns.json");
    }
    public override void Update()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Enter))
        {
            return;
        }
    }

    public override void Draw()
    {
        return;
    }
}