using Raylib_cs;

class MainScreen : Screen
{
    private Label _MainLabel;

    private Button _PlayButton;

    private VBoxLayout _MainScreenLayout;

    public MainScreen()
    {

        _MainScreenLayout = new VBoxLayout();

        const string mainText = "Welcome to Bowed Adventure";

        _MainLabel = new Label(text:mainText);

        _MainScreenLayout.AddWidget(_MainLabel);

        const string playText = "Play";

        _PlayButton = new Button(text:playText);

        _PlayButton.Clicked = test;

        _MainScreenLayout.AddWidget(_PlayButton);

    }

    private static void test()
    {
        Console.WriteLine("works");
    }

    public override void Update(float dt)
    {
        _MainScreenLayout.Update(dt);
    }

    public override void Draw()
    {
        _MainScreenLayout.Draw();
    }
}