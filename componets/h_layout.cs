using Raylib_cs;


class HBoxLayout
{

    private int _screenWidth;

    private int _screenHeight;

    private List<Widget> _Widgets;
    
    public HBoxLayout()
    {
        _Widgets = new List<Widget>();
    }

    private void ArrangeWidgets()
    {
        _screenWidth = Raylib.GetScreenWidth();
        _screenHeight = Raylib.GetScreenHeight();

        int widgetCount = _Widgets.Count;
        if (widgetCount == 0)
        {
            return;
        }

        float rowWidth = _screenWidth / (widgetCount + 1f);

        for (int i = 0; i < widgetCount; i++)
        {
            Widget wid = _Widgets[i];
            Rectangle tempRect = wid.Bounds;
            float centerX = rowWidth * (i + 1);

            tempRect.Y = _screenHeight / 2f - tempRect.Height / 2f;
            tempRect.X = centerX - tempRect.Width / 2f;
            wid.Bounds = tempRect;
        }
    }

    public void AddWidget(Widget widget)
    {
        _Widgets.Add(widget);
        ArrangeWidgets();
    }

    public void Update(float dt)
    {
        ArrangeWidgets();
        foreach (var wid in _Widgets)
        {
            wid.Update(dt);
        }
    }

    public void Draw()
    {
        foreach (var wid in _Widgets)
        {
            wid.Draw();
        }
    }
}
