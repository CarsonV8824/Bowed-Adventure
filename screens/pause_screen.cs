
using Raylib_cs;


class PauseScreen : Screen
{
    private readonly VBoxLayout _mainScreenLayout;
    private readonly Label _pauseLabel;
    private readonly Button _resumeButton;
    private readonly Button _menuButton;
    public PauseScreen(Action menuAction, Action resumeAction)
    {
        _mainScreenLayout = new VBoxLayout();
        _pauseLabel = new Label(text: "Pause");
        _mainScreenLayout.AddWidget(_pauseLabel);
        _resumeButton = new Button(text: "resume");
        _resumeButton.Clicked = resumeAction;
        _menuButton = new Button(text: "menu");
        _menuButton.Clicked = menuAction;
        _mainScreenLayout.AddWidget(_resumeButton);
        _mainScreenLayout.AddWidget(_menuButton);
    }
    public override void Update(float dt)
    {
        _mainScreenLayout.Update(dt);
    }

    public override void Draw()
    {
        _mainScreenLayout.Draw();
    }
}