using System.IO;
using Newtonsoft.Json;

public static class JSONHandler
{
    public static void SaveToJson<T>(string filePath, T data)
    {
        string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
        File.WriteAllText(filePath, jsonData);
        Console.WriteLine("Data saved to JSON.");
    }

    public static T LoadFromJson<T>(string filePath)
    {
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(jsonData);
        }
        else
        {
            Console.WriteLine("JSON file not found.");
            return default;
        }
    }
public static CollectionManager LoadFromJson(string filePath)
    {
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<CollectionManager>(jsonData);
        }
        else
        {
            System.Console.WriteLine("JSON file not found.");
            return new CollectionManager();
        }
    }
}
