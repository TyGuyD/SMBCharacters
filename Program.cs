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
    List<Character> characters = [];

    try {
        StreamReader sr = new(file);
        sr.ReadLine();
        while (!sr.EndOfStream)
        {
            string? line = sr.ReadLine();
            if (line is not null)
            {
                Character character = new();
                string[] characterDetails = line.Split(',');
                character.Id = UInt64.Parse(characterDetails[0]);
                character.Name = characterDetails[1] ?? string.Empty;
                character.Description = characterDetails[2] ?? string.Empty;
                character.Species = characterDetails[3] ?? string.Empty;
                character.FirstAppearance = characterDetails[4] ?? string.Empty;
                character.Year = characterDetails[5] ?? string.Empty;
                characters.Add(character);
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

            List<string> LowerCaseNames = characters.Select(c => c.Name.ToLower()).ToList();
            if (!LowerCaseNames.Contains(Name.ToLower()))
            {
                UInt64 id = (characters.Count > 0) ? characters.Max(c => c.Id) + 1 : 1;
                Console.Write("Description: ");
                string? description = Console.ReadLine();
                Console.Write("Species: ");
                string species = Console.ReadLine()!;
                Console.Write("First Appearance: ");
                string firstAppearance = Console.ReadLine()!;
                Console.Write("Year: ");
                string year = Console.ReadLine()!;
                Character newCharacter = new(){
                    Id = id,
                    Name = Name,
                    Description = description ?? string.Empty,
                    Species = species,
                    FirstAppearance = firstAppearance,
                    Year = year
                };
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
            for (int i = 0; i < characters.Count; i++)
            {
               Character c = characters[i];
                Console.WriteLine($"ID: {i + 1}\nName: {c.Name}\nDescription: {c.Description}\nSpecies: {c.Species}\nAppearance: {c.FirstAppearance}\nYear: {c.Year}\n");
            }
        }
        
    } while (choice == "1" || choice == "2");
}

logger.Info("Program ended");
