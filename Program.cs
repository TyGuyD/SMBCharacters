
string file = "mario.csv";

List<UInt64> Ids = [];
List<string> Names = [];
List<string?> Descriptions = [];
List<string> Species = [];
List<string> FirstAppearance = [];
List<string> Year = [];

StreamReader sr = new(file);

sr.ReadLine();
while (!sr.EndOfStream)
{
    string? line = sr.ReadLine();
    if (line is not null)
    {
        string[] characterDetails = line.Split(',');
        Ids.Add(UInt64.Parse(characterDetails[0]));
        Names.Add(characterDetails[1]);
        Descriptions.Add(characterDetails[2]);
        Species.Add(characterDetails[3]);
        FirstAppearance.Add(characterDetails[4]);
        Year.Add(characterDetails[5]);
    }
}
sr.Close();

string? choice;
do
{
    Console.WriteLine("1) Add Character");
    Console.WriteLine("2) Display All Characters");
    Console.WriteLine("Enter to quit");

    choice = Console.ReadLine();

    if (choice == "1")
    {
        
    }
    else if (choice == "2")
    {
        Console.WriteLine();
        for (int i = 0; i < Ids.Count; i++)
        {
            Console.WriteLine($"Id: {Ids[i]}");
            Console.WriteLine($"Name: {Names[i]}");
            Console.WriteLine($"Description: {Descriptions[i]}");
            Console.WriteLine($"Species: {Species[i]}");
            Console.WriteLine($"First Appearance: {FirstAppearance[i]}");
            Console.WriteLine($"Year: {Year[i]}");
            Console.WriteLine();
        }
    }
    
    
} while (choice == "1" || choice == "2");