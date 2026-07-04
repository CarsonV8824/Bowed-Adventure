using System.Text.Json.Serialization;

public class Piece
{
    public uint Tempo { get; set; }

    [JsonPropertyName("time")]
    public required string TimeSignature { get; set; }

    public required string Title { get; set; }
    public required string Composer { get; set; }

}