using Raylib_cs;

enum WindowSelected
{
    MainMenu,
    PlayWindow,
    MapWindow,
    PauseMenu,
    ResultScreen
}

class Program
{
    private static WindowSelected _currentScreen;
    private static NoteScreen? noteScreen;
    private static ResultScreen? resultScreen;
    public static void Main(string[] args)
    {
        Raylib.InitWindow(800, 600, "Bowed Adventure");
        Raylib.SetTargetFPS(60);
        Raylib.SetExitKey(KeyboardKey.Null);

        _currentScreen = WindowSelected.MainMenu;
        MainScreen mainScreen = new MainScreen(ToPlayWindowFromMenu);
        noteScreen = new NoteScreen(ToPauseWindow, ToResultWindow);
        PauseScreen pauseScreen = new PauseScreen(ToMainWindow, ToPlayWindow);
        resultScreen = new ResultScreen(ToMainWindow);
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
                case WindowSelected.PauseMenu:
                    shownScreen = pauseScreen;
                    break;
                case WindowSelected.ResultScreen:
                    shownScreen = resultScreen;
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

    public static void ToPlayWindowFromMenu()
    {
        _currentScreen = WindowSelected.PlayWindow;
        noteScreen?.ChangePiece();
    }

    public static void ToMainWindow()
    {
        _currentScreen = WindowSelected.MainMenu;
    }

    public static void ToPauseWindow()
    {
        _currentScreen = WindowSelected.PauseMenu;
    }

    public static void ToResultWindow()
    {
        _currentScreen = WindowSelected.ResultScreen;
        if (resultScreen != null && noteScreen != null) 
        {
            var result = ((float) noteScreen.Score / (float) noteScreen.ExpectedScore) * 100;
            resultScreen.resultText.Text = $"Accuracy: {result}%";
        }
    }


}