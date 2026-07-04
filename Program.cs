using Raylib_cs;
using System.Numerics;


class Program
{
    static void Main()
    {
        Raylib.InitWindow(800, 600, "Bowed Adventure");
        Raylib.SetTargetFPS(60);

        Screen currentScreen = new MainScreen();
        
        while (!Raylib.WindowShouldClose())
        {
            currentScreen.Update();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.RayWhite);

            currentScreen.Draw();

            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
    }
}