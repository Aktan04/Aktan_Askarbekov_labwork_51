namespace ProductShop.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }
    public DateTime DateOfCreation { get; set; }
    public DateTime? DateOfEditing { get; set; }
    public string? Description { get; set; }
    public string Avatar { get; set; }
    
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public int BrandId { get; set; }
    public Brand Brand { get; set; }
}