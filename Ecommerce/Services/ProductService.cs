using ECommerce.Models;
public class ProductService
{
    private static List<Product> products = new List<Product>()
 {
new Product(){ Id = 1, Name = "Ao Lamine Yamal", Description = "Dien thoai cua Apple", Price = 1000 },
new Product(){ Id = 2, Name = "Samsung Galaxy", Description = "Dien thoai cua Samsung", Price = 500 },
new Product(){ Id = 3, Name = "Sony Xperia", Description = "Dien thoai cua Sony",Price = 800 }
 };
    public List<Product> GetProducts()
    {
        return products;
    }

    public void RemoveAll()
    {
        products.Clear();
    }

    public void LoadAll()
    {
        products = new List<Product>()
        {
            new Product(){ Id = 1, Name = "Ao Lamine Yamal", Description = "Dien thoai cua Apple", Price = 1000 },
            new Product(){ Id = 2, Name = "Samsung Galaxy", Description = "Dien thoai cua Samsung", Price = 500 },
            new Product(){ Id = 3, Name = "Sony Xperia", Description = "Dien thoai cua Sony", Price = 800 }
        };
    }

    public void AddProduct(Product p)
    {
        products.Add(p);
    }
}