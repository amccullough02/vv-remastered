namespace VirtualVinylRemaster;

public class IO
{
    private List<Record> records = new();

    private void LoadRecordsFromCsv()
    {
        try
        {
            var relativePath = "../../../data/records.txt";
            var lines = File.ReadAllLines(relativePath);
            foreach (var line in lines.Skip(2))
            {
                var values = line.Split(',');
                if (values.Length == 7)
                {
                    if (int.TryParse(values[5].Trim(), out int stock) && double.TryParse(values[6].Trim(), out double cost))
                    {
                        records.Add(new Record
                        {
                            Artist = values[0].Trim(),
                            Title = values[1].Trim(),
                            Genre = values[2].Trim(),
                            PlayLength = values[3].Trim(),
                            Condition = values[4].Trim(),
                            Stock = stock,
                            Cost = cost   
                        });
                    }
                    else
                    {
                        Console.WriteLine("Error parsing stock or cost value. Invalid format.");
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred: {e.Message}");
        }
    }

    public List<Record> GetRecords()
    {
        this.LoadRecordsFromCsv();
        return records;
    }

}