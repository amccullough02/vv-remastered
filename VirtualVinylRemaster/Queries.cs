namespace VirtualVinylRemaster;

public class Queries
{
    private List<Record> _records;
    private readonly InputOutput _inputOutput;

    public Queries()
    {
        _inputOutput = new InputOutput();
        _records = _inputOutput.GetRecords();
    }

    public void Refresh()
    {
        var inputOutput = new InputOutput();
        inputOutput.SaveAndLoad(_records);
        _records = inputOutput.GetRecords();
    }

    private void DisplayResults(List<Record> records, bool displayExtras)
    {
        Console.WriteLine("\n{0,-20} {1,-30} {2,-15} {3,-15} {4,-15} {5,-10} {6,-10}", "Artist", "Title", "Genre", "Play Length", "Condition", "Stock", "Cost");
        Console.WriteLine(new string('-', 115));
        
        var sortedRecords = records.OrderBy(record => record.Artist).ToList();
        var numRecords = 0;
        var costRecords = 0.00;

        foreach (var record in sortedRecords)
        {
            Console.WriteLine("{0,-20} {1,-30} {2,-15} {3,-15} {4,-15} {5,-10} {6,-10:C}", record.Artist, record.Title, 
                record.Genre, record.PlayLength, record.Condition, record.Stock, record.Cost);
            if (displayExtras)
            {
                numRecords += record.Stock;
                costRecords += record.Cost;
            }
        }

        if (!displayExtras) return;
        Console.WriteLine("\nTotal number of records: {0}", numRecords);
        Console.WriteLine("\nTotal value of records: £{0:F2}", costRecords);
    }

    public void SummaryReport()
    {
        this.DisplayResults(_records, true);
    }

    public void AbovePrice()
    {
        Console.Write("\nPlease enter the minimum price(£) -> ");
        string? priceThreshold = Console.ReadLine();
        float convertedPrice = float.Parse(priceThreshold ?? string.Empty);
        List<Record> filteredRecords = _records.Where(record => record.Cost > convertedPrice).ToList();
        
        Console.WriteLine("\n=== Displaying Records above £{0} ===", priceThreshold);
        
        this.DisplayResults(filteredRecords, false);
    }

    public void ByGenre()
    {
        Console.Write("\n=== Select Genre ===\n1. Rock\n2. Classical\n3. Pop\n4. Jazz\n5. Spoken Word\n\n" +
                      "Please enter your choice -> ");
        string? choice = Console.ReadLine();
        int genreChoice = int.Parse(choice ?? string.Empty);

        List<Record> filteredRecords;
        
        switch (genreChoice)
        {
            case 1:
                filteredRecords = _records.Where(record => record.Genre.Equals("Rock")).ToList();
                this.DisplayResults(filteredRecords, false);
                break;
            case 2:
                filteredRecords = _records.Where(record => record.Genre.Equals("Classical")).ToList();
                this.DisplayResults(filteredRecords, false);
                break;
            case 3:
                filteredRecords = _records.Where(record => record.Genre.Equals("Pop")).ToList();
                this.DisplayResults(filteredRecords, false);
                break;
            case 4:
                filteredRecords = _records.Where(record => record.Genre.Equals("Jazz")).ToList();
                this.DisplayResults(filteredRecords, false);
                break;
            case 5:
                filteredRecords = _records.Where(record => record.Genre.Equals("Spoken Word")).ToList();
                this.DisplayResults(filteredRecords, false);
                break;
            default:
                Console.WriteLine("Oops...");
                break;
        }
    }

    public void CheckAvailability()
    {
        bool singleFound = false;
        int stock = 0;
        Console.Write("\nEnter title of single: ");
        string? singleName = Console.ReadLine();

        foreach (var record in _records)
        {
            if (record.Title.Equals(singleName))
            {
                singleFound = true;
                stock = record.Stock;
            }
        }

        if (!singleFound)
        {
            Console.WriteLine("The single you're looking for doesn't exist, please try again.\n");
            this.CheckAvailability();
        }
        else
        {
            if (stock > 0)
            {
                Console.WriteLine("\n" + singleName + " was found.\n\nIt currently has " + stock + " copies in stock.");
            }
            else
            {
                Console.WriteLine("\n" + singleName + " is currently out of stock.");
            }
        }
        this.StockMenu(singleName ?? string.Empty);
    }

    private void StockMenu(string singleName)
    {
        Console.Write("\n1. Increase stock.\n2. Decrease stock.\n3. Return to main menu.\n\nSelect your option -> ");
        string? choice = Console.ReadLine();
        int parsedChoice = int.Parse(choice ?? string.Empty);

        switch (parsedChoice)
        {
            case 1:
                this.AddStock(singleName);
                break;
            case 2:
                this.DecreaseStock(singleName);
                break;
            case 3:
                Console.WriteLine("Returning to main menu.");
                break;
        }
    }

    private void AddStock(string singleName)
    {
        Console.Write("\nHow much stock do you want to add -> ");
        string? amount = Console.ReadLine();
        int parsedAmount = int.Parse(amount ?? string.Empty);

        foreach (var record in _records)
        {
            if (record.Title.Equals(singleName))
            {
                record.Stock += parsedAmount;
            }
        }
        this.DisplayResults(_records, false);
    }

    private void DecreaseStock(string singleName)
    {
        Console.Write("\nHow much stock do you want to remove -> ");
        string? amount = Console.ReadLine();
        int parsedAmount = int.Parse(amount ?? string.Empty);

        foreach (var record in _records)
        {
            if (record.Title.Equals(singleName))
            {
                record.Stock -= parsedAmount;
                if (record.Stock.Equals(0))
                {
                    Console.WriteLine("\nDISCLAIMER: " + singleName + " will now be out of stock.");
                }
            }
        }
        this.DisplayResults(_records, false);
    }

    public void BarChart()
    {
        Dictionary<string, int> genreStock = new Dictionary<string, int>();
        
        foreach (var record in _records)
        {
            string genre = record.Genre;
            int stock = record.Stock;

            if (genreStock.ContainsKey(genre))
            {
                genreStock[genre] += stock;
            }
            else
            {
                genreStock[genre] = stock;
            }
        }
        
        int maxStock = genreStock.Values.Max();

        Console.WriteLine("\nGenre Stock Bar Chart:");

        foreach (var kvp in genreStock)
        {
            string genre = kvp.Key;
            int stock = kvp.Value;
            
            int barLength = (int)((double)stock / maxStock * 40);

            Console.Write($"{genre,-15} | {new string('#', barLength)}\n");
        }
    }

    public void AddRecord()
    {
        Console.WriteLine("\nYou will now begin the process of adding a record to the database.\n");
        
        Console.Write("Enter the artists' name -> ");
        string? artistName = Console.ReadLine();
        Console.Write("\nEnter the title name -> ");
        string? titleName = Console.ReadLine();
        Console.Write("\nEnter the genre name -> ");
        string? genreName = Console.ReadLine();
        Console.Write("\nEnter the play length -> ");
        string? playLength = Console.ReadLine();
        Console.Write("\nEnter the condition -> ");
        string? condition = Console.ReadLine();
        Console.Write("\nEnter the amount of stock -> ");
        string? stock = Console.ReadLine();
        Console.Write("\nEnter the cost(£) -> ");
        string? cost = Console.ReadLine();

        int parsedStock = int.Parse(stock ?? string.Empty);
        double parsedCost = double.Parse(cost ?? string.Empty);

        if (artistName != null && titleName != null && genreName != null && playLength != null && condition != null)
        {
            Record record = new Record(artistName, titleName, genreName, playLength, condition, parsedStock, parsedCost);
        
            _records.Add(record);
        }

        this._inputOutput.SaveAndLoad(_records);
        this.DisplayResults(_records, false);
    }

}