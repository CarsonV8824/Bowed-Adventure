using Raylib_cs;

public abstract class Widget
{
    public string? Text { get; set; }

    public int FontSize {get; set;} = 20;

    public Rectangle Bounds { get; set ; }

    public Color BackgroundColor { get; set; } = Color.LightGray;

    public float Roundness { get; set; } = 0.25f;
    public int Segments { get; set; } = 8;

    public Color BorderColor { get; set; } = Color.Black;
    public float BorderThickness { get; set; } = 2;    

    public bool IsLabel {get;set;} = false;

    protected Rectangle EnsureTextFits(Rectangle bounds, string text)
    {
        int textWidth = string.IsNullOrEmpty(text) ? 0 : Raylib.MeasureText(text, FontSize);

        if (textWidth > bounds.Width)
        {
            float centerX = bounds.X + bounds.Width / 2f;
            bounds.Width = textWidth;
            bounds.X = centerX - bounds.Width / 2f;
        }

        return bounds;
    }

    protected void ConstructorFormat(int x, int y, int width = 200, int height = 100, string? text = "", bool isLabel = false)
    {
        IsLabel = isLabel;

        int textWidth = string.IsNullOrEmpty(text) ? 0 : Raylib.MeasureText(text, FontSize);
        int setWidth = width > textWidth ? width : textWidth;

        Bounds = new Rectangle(
            x - setWidth / 2f,
            y - height / 2f,
            setWidth,
            height
        );

        Text = text;
    }

    public virtual void Draw()
    {
        Color color;
        if (!IsLabel)
        {
            color = BackgroundColor;
            
        } else
        {
            color = Color.Blank;
            BorderColor = Color.Blank;
        }
        string text = Text ?? string.Empty;
        Bounds = EnsureTextFits(Bounds, text);

        if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), Bounds) && !IsLabel)
            color = Color.Gray;

        if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), Bounds) && !IsLabel && Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), Bounds) &&
            Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            color = Color.Black;
        }

        Raylib.DrawRectangleRounded(Bounds, Roundness, Segments, color);

        Raylib.DrawRectangleRoundedLinesEx(
            Bounds,
            Roundness,
            Segments,
            BorderThickness,
            BorderColor
        );

        if (!string.IsNullOrEmpty(text))
        {
            int width = Raylib.MeasureText(text, FontSize);

            Raylib.DrawText(
                text,
                (int)(Bounds.X + Bounds.Width / 2 - width / 2),
                (int)(Bounds.Y + Bounds.Height / 2 - FontSize / 2),
                FontSize,
                Color.Black
            );
        }
    }

    public abstract void Update(float dt);
}