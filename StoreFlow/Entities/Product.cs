namespace StoreFlow.Entities;

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Stok { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public List<Order> Orders { get; set; }


}
