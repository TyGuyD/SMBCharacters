using NLog;
string path = Directory.GetCurrentDirectory() + "//nlog.config";
var logger = LogManager.Setup().LoadConfigurationFromFile(path).GetCurrentClassLogger();

logger.Info("Program started");

string file = "mario.csv";

if (!File.Exists(file))
{
    logger.Error("File does not exist: {File}", file);
}
else
{
    List<UInt64> Ids = [];
    List<string> Names = [];
    List<string?> Descriptions = [];
    List<string> Species = [];
    List<string> FirstAppearance = [];
    List<string> Year = [];

    try {
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
    } 
    catch (Exception ex) {
        logger.Error(ex, "Error reading file: {File}", file);
    }

    string? choice;
    do
    {
        Console.WriteLine("1) Add Character");
        Console.WriteLine("2) Display All Characters");
        Console.WriteLine("Enter to quit");

        choice = Console.ReadLine();
        logger.Info("User selected option: {Choice}", choice);

        if (choice == "1")
        {
            Console.Write("Name: ");
            string Name = Console.ReadLine()!;

            List<string> LowerCaseNames = Names.ConvertAll(n => n.ToLower());
            if (!LowerCaseNames.Contains(Name.ToLower()))
            {
                UInt64 id = Ids.Max() + 1;
                Console.Write("Description: ");
                string? description = Console.ReadLine();
                Console.Write("Species: ");
                string species = Console.ReadLine()!;
                Console.Write("First Appearance: ");
                string firstAppearance = Console.ReadLine()!;
                Console.Write("Year: ");
                string year = Console.ReadLine()!;
                Ids.Add(id);
                Names.Add(Name);
                Descriptions.Add(description);
                Species.Add(species);
                FirstAppearance.Add(firstAppearance);
                Year.Add(year);
                using StreamWriter sw = new(file, append: true);
                sw.WriteLine($"{id},{Name},{description},{species},{firstAppearance},{year}");
                sw.Close();
                logger.Info("Added character: {Name}", Name);
            }
            else {
                Console.WriteLine("Character already exists.");
                logger.Warn("Attempted to add duplicate character: {Name}", Name);
            }

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
}

logger.Info("Program ended");
