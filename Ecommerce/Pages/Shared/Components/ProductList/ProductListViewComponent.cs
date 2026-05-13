using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Pages.Shared.Components.ProductList
{
   public class ProductListViewComponent : ViewComponent
   {
      private readonly ProductService _productService;

      public ProductListViewComponent(ProductService productService)
      {
         _productService = productService;
      }

      public async Task<IViewComponentResult> InvokeAsync(string keyword, bool sapXepTang = true)
      {
         var products = _productService.GetProducts();

         // 🔍 tìm kiếm
         if (!string.IsNullOrEmpty(keyword))
         {
            products = products
                .Where(p => p.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();
         }

         // 🔽 sắp xếp
         if (sapXepTang)
            products = products.OrderBy(p => p.Price).ToList();
         else
            products = products.OrderByDescending(p => p.Price).ToList();

         return View(products);
      }
   }
}