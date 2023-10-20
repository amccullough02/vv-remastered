namespace VirtualVinylRemaster;

public class Record
{
    public string Artist { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Genre { get; set; } = null!;
    public string PlayLength { get; set; } = null!;
    public string Condition { get; set; } = null!;
    public int Stock { get; set; }
    public double Cost { get; set; }

    public override string ToString()
    {
        return $"Artist: {Artist}, Title: {Title}, Genre: {Genre}, Play Length: {PlayLength}, Condition: {Condition}, Stock: {Stock}, Cost: {Cost:C}";
    }
}