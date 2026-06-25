using System.Text.Json;
using Magazyn.Models;

namespace Magazyn;

public class StoreData
{
    // listy danych z pliku JSON
    public List<Category> Categories { get; set; } = new();
    public List<Product> Products { get; set; } = new();
    public List<Operation> Operations { get; set; } = new();
}

public class UsersData
{
    // lista uzytkownikow z pliku JSON
    public List<User> Users { get; set; } = new();
}

public class Db
{
    private readonly string _path;
    private readonly string _usersPath;
    private static readonly JsonSerializerOptions _opts = new() { WriteIndented = true };

    public Db(IConfiguration cfg)
    {
        // bierzemy sciezki do plikow z konfiguracji
        _path = cfg["DataFile"] ?? "data/store.json";
        _usersPath = cfg["UsersFile"] ?? "data/users.json";
    }

    // wczytuje dane z pliku
    public StoreData Load()
    {
        var json = File.ReadAllText(_path);
        return JsonSerializer.Deserialize<StoreData>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }

    // zapisuje dane do pliku
    public void Save(StoreData data)
    {
        var json = JsonSerializer.Serialize(data, _opts);
        File.WriteAllText(_path, json);
    }

    // wczytuje uzytkownikow z pliku
    public UsersData LoadUsers()
    {
        var json = File.ReadAllText(_usersPath);
        return JsonSerializer.Deserialize<UsersData>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }

    // zapisuje uzytkownikow do pliku
    public void SaveUsers(UsersData data)
    {
        var json = JsonSerializer.Serialize(data, _opts);
        File.WriteAllText(_usersPath, json);
    }
}
