namespace VirtualVinylRemaster;

public class Menu
{
    private bool _welcomed;
    public void MainMenu()
    {
        int intChoice;
        bool validChoice = false;

        do
        {
            if (!_welcomed)
            {
                Console.WriteLine("\n==== Year One University Project - C# Remaster ====\n" +
                                  "WELCOME TO VIRTUAL VINYL!\n-----------------------\n" +
                                  "1. Query Data\n2. Add a new record.\n3. Quit\n");
                _welcomed = true;
            }
            else
            {
                Console.WriteLine("\n1. Query Data\n2. Add a new record.\n3. Quit\n"); 
            }
            
            Console.Write("Please enter your choice -> ");
            string? choice = Console.ReadLine();



            if (int.TryParse(choice, out intChoice))
            {
                if (intChoice is >= 1 and <= 3)
                {
                    validChoice = true;
                }
                else
                {
                    Console.WriteLine("\n!!!Invalid input. Please enter 1, 2, or 3!!!");
                }
            }
            else
            {
                Console.WriteLine("\n!!!Invalid input. Please enter 1, 2, or 3!!!");
            }

        } while (!validChoice);

        switch (intChoice)
        {
            case 1:
                QueryMenu();
                QuitMenu();
                break;
            case 2:
                CreateNewRecord();
                QuitMenu();
                break;
            case 3:
                Quit();
                break;
            default:
                Console.WriteLine("You've met with a terrible fate - haven't you?");
                break;
        }
    }

    private void QueryMenu()
    {
        Queries queries = new Queries();
        int intChoice;
        bool validChoice = false;
    
        do
        {
            Console.WriteLine("\nPlease select an option:" +
                              "\n1. Summary report." +
                              "\n2. Record titles within a price threshold." +
                              "\n3. Records by genre." +
                              "\n4. Query availability." +
                              "\n5. Bar Chart\n");
            
            Console.Write("Please enter your choice -> ");
            string? choice = Console.ReadLine();
            
            if (int.TryParse(choice, out intChoice))
            {
                if (intChoice is >= 1 and <= 5)
                {
                    validChoice = true;
                }
                else
                {
                    Console.WriteLine("\n!!!Invalid input. Please enter 1, 2, 3, 4, or 5!!!");
                }
            }
            else
            {
                Console.WriteLine("\n!!!Invalid input. Please enter 1, 2, 3, 4 or 5!!!");
            }
            
        } while (!validChoice);
        
        switch (intChoice)
        {
            case 1:
                queries.SummaryReport();
                break;
            case 2:
                queries.AbovePrice();
                break;
            case 3:
                queries.ByGenre();
                break;
            case 4:
                queries.CheckAvailability();
                break;
            case 5:
                queries.BarChart();
                break;
            default:
                Console.WriteLine("You've met with a terrible fate - haven't you?");
                break;
        }
    }

    private void CreateNewRecord()
    {
        Queries queries = new Queries();
        queries.AddRecord();
    }

    private void QuitMenu()
    {
        bool validChoice = false;
        int intChoice;

        do
        {
            Console.Write("\nReturn to main menu, or quit?\n" +
                          "\n1. Return to main menu." +
                          "\n2. Quit application.\n");
            Console.Write("\nPlease enter your choice -> ");

            string? choice = Console.ReadLine();

            if (int.TryParse(choice, out intChoice))
            {
                if (intChoice is >= 1 and <= 3)
                {
                    validChoice = true;
                }
            }
        } while (!validChoice);

        switch (intChoice)
        {
            case 1:
                MainMenu();
                break;
            case 2:
                Quit();
                break;
        }
    }
    
    private void Quit()
    {
        Console.WriteLine("Exiting the program. Goodbye!");
    }
    
}