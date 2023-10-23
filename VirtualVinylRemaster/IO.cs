namespace VirtualVinylRemaster;

public class IO
{
    private List<Record> records = new();

    private void LoadRecordsFromCsv()
    {
        try
        {
            records.Clear();
            var relativePath = "../../../data/records.csv";
            var lines = File.ReadAllLines(relativePath);
            foreach (var line in lines)
            {
                var values = line.Split(',');
                if (values.Length == 7)
                {
                    if (int.TryParse(values[5].Trim(), out int stock) && double.TryParse(values[6].Trim(), out double cost))
                    {
                        records.Add(new Record(
                            values[0].Trim(),
                            values[1].Trim(),
                            values[2].Trim(),
                            values[3].Trim(),
                            values[4].Trim(),
                            stock,
                            cost)
                        );
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

    private void SaveRecordsToCsv(List<Record> records)
    {
        try
        {
            var relativePath = "../../../data/records.csv";
            using (StreamWriter writer = new StreamWriter(relativePath))
            {
                foreach (var record in records)
                {
                    string csvLine = string.Join(",", record.Artist, record.Title, record.Genre, record.PlayLength,
                        record.Condition, record.Stock, record.Cost);
                    
                    writer.WriteLine(csvLine);
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

    public void SaveAndLoad(List<Record> records)
    {
        this.SaveRecordsToCsv(records);
        this.LoadRecordsFromCsv();
    }

}