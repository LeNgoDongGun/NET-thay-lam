using Microsoft.AspNetCore.Mvc;
public class ProductViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(string category)
    {
        var products = await GetProductsAsync(category);
        return View(products);
    }
    private Task<List<string>> GetProductsAsync(string category)
    {
        // Dữ liệu mẫu
        var allProducts = new Dictionary<string, List<string>>{
        { "Electronics", new List<string> { "Laptop", "Smartphone", "Headphones" }},
        { "Books", new List<string> { "C# Programming", "ASP.NET Core Guide","Design Patterns" } }
   };
        return Task.FromResult(allProducts.ContainsKey(category) ?
        allProducts[category] : new List<string>());
    }
}