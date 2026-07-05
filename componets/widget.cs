using Raylib_cs;

public abstract class Widget
{
    public string? Text { get; set; }

    public Rectangle Bounds { get; set; }

    public Color BackgroundColor { get; set; } = Color.LightGray;

    public float Roundness { get; set; } = 0.25f;
    public int Segments { get; set; } = 8;

    public Color BorderColor { get; set; } = Color.Black;
    public float BorderThickness { get; set; } = 2;    

    public virtual void Draw()
    {
        Color color = BackgroundColor;

        if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), Bounds))
            color = Color.Gray;

        Raylib.DrawRectangleRounded(Bounds, Roundness, Segments, color);

        Raylib.DrawRectangleRoundedLinesEx(
            Bounds,
            Roundness,
            Segments,
            BorderThickness,
            BorderColor
        );

        if (!string.IsNullOrEmpty(Text))
        {
            int fontSize = 20;
            int width = Raylib.MeasureText(Text, fontSize);

            Raylib.DrawText(
                Text,
                (int)(Bounds.X + Bounds.Width / 2 - width / 2),
                (int)(Bounds.Y + Bounds.Height / 2 - fontSize / 2),
                fontSize,
                Color.Black
            );
        }
    }
}