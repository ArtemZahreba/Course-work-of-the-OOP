using System;

public class UserInterface
{
    private CollectionManager collectionManager;
    private string jsonFilePath = "philatelists.json"; // Path for JSON file

    public UserInterface(CollectionManager collectionManager)
    {
        this.collectionManager = collectionManager;
    }

    public void ShowMainMenu()
    {
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("Main Menu:");
            Console.WriteLine("1. Manage Philatelists");
            Console.WriteLine("2. Add Philatelist");
            Console.WriteLine("3. Update Philatelist");
            Console.WriteLine("4. Remove Philatelist");
            Console.WriteLine("5. Save to JSON");
            Console.WriteLine("6. Load from JSON");
            Console.WriteLine("0. Exit");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ManagePhilatelists();
                    break;
                case "2":
                    AddPhilatelist();
                    SaveToJson(); // Save after adding
                    break;
                case "3":
                    UpdatePhilatelist();
                    SaveToJson(); // Save after updating
                    break;
                case "4":
                    RemovePhilatelist();
                    SaveToJson(); // Save after removing
                    break;
                case "5":
                    SaveToJson(); // Save manually
                    break;
                case "6":
                    LoadFromJson(); // Load manually
                    break;
                case "0":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private void ManagePhilatelists()
    {
        bool back = false;
        while (!back)
        {
            Console.WriteLine("Philatelists:");
            collectionManager.ViewPhilatelists();
            Console.WriteLine("Enter the number of the philatelist to manage their collection, or 'B' to go back:");

            string input = Console.ReadLine();
            if (input.ToLower() == "b")
            {
                back = true;
            }
            else if (int.TryParse(input, out int index))
            {
                var philatelist = collectionManager.GetPhilatelistByIndex(index - 1);
                if (philatelist != null)
                {
                    ManagePhilatelistCollection(philatelist);
                }
                else
                {
                    Console.WriteLine("Invalid selection. Please try again.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number or 'B' to go back.");
            }
        }
    }

    private void ManagePhilatelistCollection(Philatelist philatelist)
    {
        bool back = false;
        while (!back)
        {
            Console.WriteLine($"Managing Collection for {philatelist.Name}");
            Console.WriteLine("1. View Collection");
            Console.WriteLine("2. Add Stamp");
            Console.WriteLine("3. Remove Stamp");
            Console.WriteLine("4. Go Back");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ViewCollection(philatelist);
                    break;
                case "2":
                    AddStampToPhilatelist(philatelist);
                    SaveToJson(); // Save after adding stamp
                    break;
                case "3":
                    RemoveStampFromPhilatelist(philatelist);
                    SaveToJson(); // Save after removing stamp
                    break;
                case "4":
                    back = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private void AddPhilatelist()
    {
        Console.Write("Enter name: ");
        string name = Console.ReadLine();
        Console.Write("Enter country: ");
        string country = Console.ReadLine();
        Console.Write("Enter contact details: ");
        string contactDetails = Console.ReadLine();

        Philatelist philatelist = new Philatelist(name, country, contactDetails);
        collectionManager.AddPhilatelist(philatelist);
    }

    private void UpdatePhilatelist()
    {
        Console.Write("Enter the name of the philatelist to update: ");
        string name = Console.ReadLine();

        Console.Write("Enter new name: ");
        string newName = Console.ReadLine();
        Console.Write("Enter new country: ");
        string country = Console.ReadLine();
        Console.Write("Enter new contact details: ");
        string contactDetails = Console.ReadLine();

        Philatelist updatedPhilatelist = new Philatelist(newName, country, contactDetails);
        collectionManager.UpdatePhilatelist(name, updatedPhilatelist);
    }

    private void RemovePhilatelist()
    {
        Console.Write("Enter the name of the philatelist to remove: ");
        string name = Console.ReadLine();
        collectionManager.RemovePhilatelist(name);
    }

    private void ViewCollection(Philatelist philatelist)
    {
        if (philatelist.RareStamps.Count > 0)
        {
            foreach (var stamp in philatelist.RareStamps)
            {
                Console.WriteLine(stamp);
            }
        }
        else
        {
            Console.WriteLine("No stamps in this collection.");
        }
    }

    private void AddStampToPhilatelist(Philatelist philatelist)
    {
        Console.Write("Enter stamp country: ");
        string country = Console.ReadLine();
        Console.Write("Enter face value: ");
        decimal faceValue = decimal.Parse(Console.ReadLine());
        Console.Write("Enter year of issue: ");
        int yearOfIssue = int.Parse(Console.ReadLine());
        Console.Write("Enter circulation: ");
        int circulation = int.Parse(Console.ReadLine());
        Console.Write("Enter features: ");
        string features = Console.ReadLine();

        Stamp stamp = new Stamp(country, faceValue, yearOfIssue, circulation, features);
        philatelist.AddRareStamp(stamp);
    }

    private void RemoveStampFromPhilatelist(Philatelist philatelist)
    {
        Console.WriteLine("Select the stamp to remove:");
        for (int i = 0; i < philatelist.RareStamps.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {philatelist.RareStamps[i]}");
        }

        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= philatelist.RareStamps.Count)
        {
            philatelist.RareStamps.RemoveAt(index - 1);
            Console.WriteLine("Stamp removed.");
        }
        else
        {
            Console.WriteLine("Invalid selection.");
        }
    }

    private void SaveToJson()
    {
        JSONHandler.SaveToJson(jsonFilePath, collectionManager);
    }

    private void LoadFromJson()
    {
        collectionManager = JSONHandler.LoadFromJson<CollectionManager>(jsonFilePath);
    }
}
