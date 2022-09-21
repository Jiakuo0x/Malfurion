namespace Malfurion.Comm;

public static class JsonFile
{
    public static TConf Parse<TConf>(string jsonFilePath)
        where TConf : new()
    {
        if (File.Exists(jsonFilePath))
        {
            var fileContent = File.ReadAllText(jsonFilePath);
            var result = JsonConvert.DeserializeObject<TConf>(fileContent);
            if (result is null)
                return new();

            return result;
        }
        else
        {
            return new();
        }
    }
}