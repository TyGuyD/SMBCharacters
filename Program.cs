// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

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