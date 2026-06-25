using System.ComponentModel.DataAnnotations;

namespace Magazyn.Models;

// kategoria produktu
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
}

// produkt w magazynie
public class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Nazwa wymagana")]
    [StringLength(200)]
    public string Name { get; set; }

    public int? CategoryId { get; set; }

    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }

    [Range(0, int.MaxValue)]
    public int MinQuantity { get; set; }

    [Range(0, 999999)]
    public decimal Price { get; set; }
}

// operacja magazynowa (przyjecie lub wydanie)
public class Operation
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string UserId { get; set; }
    public string Type { get; set; } // IN lub OUT
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
}

// uzytkownik systemu
public class User
{
    public string Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}
