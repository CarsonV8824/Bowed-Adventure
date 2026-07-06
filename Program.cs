using Raylib_cs;

enum WindowSelected
{
    MainMenu,
    PlayWindow,
    PauseMenu
}

class Program
{
    private static WindowSelected _currentScreen;
    static void Main()
    {
        Raylib.InitWindow(800, 600, "Bowed Adventure");
        Raylib.SetTargetFPS(60);

        _currentScreen = WindowSelected.MainMenu;
        MainScreen mainScreen = new MainScreen(ToPlayWindow);
        NoteScreen noteScreen = new NoteScreen();
        Screen shownScreen = mainScreen;
        while (!Raylib.WindowShouldClose())
        {
            float dt = Raylib.GetFrameTime();
            switch (_currentScreen)
            {
                case WindowSelected.MainMenu:
                    shownScreen = mainScreen;
                break;
                case WindowSelected.PlayWindow:
                    shownScreen = noteScreen;
                    break;
            }
            shownScreen.Update(dt);

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.RayWhite);

            shownScreen.Draw();

            Raylib.EndDrawing();
        }
        Raylib.CloseWindow();
    }

    public static void ToPlayWindow()
    {
        _currentScreen = WindowSelected.PlayWindow;
    }

    public static void ToMainWindow()
    {
        _currentScreen = WindowSelected.MainMenu;
    }


}