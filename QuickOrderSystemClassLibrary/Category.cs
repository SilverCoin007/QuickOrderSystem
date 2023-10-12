namespace QuickOrderSystemClassLibrary;

public class Category
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? Name { get; set; }
    public string? ImageData { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
}