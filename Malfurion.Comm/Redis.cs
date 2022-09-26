namespace Malfurion.Comm;

public static class Redis
{
    private static string? _connectStr;

    public static void InitConnectStr(string connectStr)
    {
        _connectStr = connectStr;
    }
    public static T Func<T>(string key, Func<T> func, TimeSpan expiry)
        where T : new()
    {
        var database = GetDatabase();
        var value = database.StringGet(key);
        if (value.HasValue)
            return JsonConvert.DeserializeObject<T>(value!) ?? new();

        var funcValue = func();
        if (funcValue is null) return new();

        database.StringSet(key, JsonConvert.SerializeObject(funcValue), expiry);
        return funcValue;
    }
    public static void SetValue<T>(string key, T value, TimeSpan expiry)
        => GetDatabase().StringSet(key, JsonConvert.SerializeObject(value), expiry);
    public static T GetValue<T>(string key)
        where T : new()
    {
        var value = GetDatabase().StringGet(key);
        if (value.IsNullOrEmpty) return new();
        return JsonConvert.DeserializeObject<T>(value!) ?? new();
    }
    private static IDatabase GetDatabase()
    {
        if (_connectStr is null)
            throw new RedisException("Not set the connection string.");

        var redis = ConnectionMultiplexer.Connect(_connectStr);
        return redis.GetDatabase();
    }
}