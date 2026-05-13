using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ECommerce.Models;

namespace Ecommerce.Pages;

public class ProductModel : PageModel
{
    private readonly ProductService _productService;
    public List<Product> Products { get; set; } = new List<Product>();

    public Product SelectedProduct { get; set; }

    [BindProperty]
    public Product NewProduct { get; set; }

    [BindProperty(SupportsGet = true)]
    public string Keyword { get; set; }

    public ProductModel(ProductService productService)
    {
        _productService = productService;
    }

    public void OnGet(int? id)
    {
        var list = _productService.GetProducts();

        // 🔍 Tìm kiếm
        if (!string.IsNullOrEmpty(Keyword))
        {
            list = list
                .Where(p => p.Name.Contains(Keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        Products = list;

        Products = _productService.GetProducts();
        if (id != null)
        {
            SelectedProduct = _productService
                .GetProducts()
                .FirstOrDefault(p => p.Id == id);
        }
    }

    public IActionResult OnGetRemoveAll()
    {
        _productService.RemoveAll();
        Products = _productService.GetProducts();
        return Page();
    }


    public IActionResult OnGetLoadAll()
    {
        _productService.LoadAll();
        Products = _productService.GetProducts();
        return Page();
    }

    public IActionResult OnPost()
    {
        //Id tự tăng 
        NewProduct.Id = _productService.GetProducts().Count + 1;

        _productService.AddProduct(NewProduct);

        Products = _productService.GetProducts();

        return Page();
    }
}
