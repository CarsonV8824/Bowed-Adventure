using Raylib_cs;

class VBoxLayout
{

    private int _screenWidth;

    private int _screenHeight;

    private List<Widget> _Widgets;
    
    public VBoxLayout()
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

        float rowHeight = _screenHeight / (widgetCount + 1f);

        for (int i = 0; i < widgetCount; i++)
        {
            Widget wid = _Widgets[i];
            Rectangle tempRect = wid.Bounds;
            float centerY = rowHeight * (i + 1);

            tempRect.X = _screenWidth / 2f - tempRect.Width / 2f;
            tempRect.Y = centerY - tempRect.Height / 2f;
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