using Raylib_cs;

class ResultScreen : Screen
{
    public int? PlayerResult {get;set;}
    public int? ComputerResult {get;set;}
    public VBoxLayout mainLayout;
    public Label resultText;
    public Button ToMainMenuBtn;

    public ResultScreen(Action toMainMenu)
    {
        mainLayout = new VBoxLayout();
        resultText = new Label();
        mainLayout.AddWidget(resultText);
        ToMainMenuBtn = new Button(text: "Main Menu");
        ToMainMenuBtn.Clicked = toMainMenu;
        mainLayout.AddWidget(ToMainMenuBtn);
    }

    public override void Draw()
    {
        mainLayout.Draw();
    }

    public override void Update(float dt)
    {
        mainLayout.Update(dt);
    }
}