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
    
    public Record(string artist, string title, string genre, string playLength, string condition, int stock, double cost)
    {
        Artist = artist;
        Title = title;
        Genre = genre;
        PlayLength = playLength;
        Condition = condition;
        Stock = stock;
        Cost = cost;
    }

    public override string ToString()
    {
        return $"Artist: {Artist}, Title: {Title}, Genre: {Genre}, Play Length: {PlayLength}, Condition: {Condition}, Stock: {Stock}, Cost: {Cost:C}";
    }
}