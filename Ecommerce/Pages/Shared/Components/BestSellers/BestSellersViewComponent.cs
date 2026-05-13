using Microsoft.AspNetCore.Mvc;
namespace ECommerce.Pages.Shared.Components.BestSellers
{
  public class BestSellersViewComponent : ViewComponent
  {
    public async Task<IViewComponentResult> InvokeAsync(int count)
    {

      // Dữ liệu sản phẩm bán chạy
      var allBestSellers = new List<string> { "Laptop", "Điện thoại", "Tai nghe", "Máy tính bảng", "Đồng hồ thông minh" };

      await Task.Delay(100); // Giả lập thời gian xử lý dữ liệu

      // Giới hạn chỉ lấy ra <count> sản phẩm
      var bestSellers = allBestSellers.Take(count).ToList();
      return View(bestSellers);
    }
  }
}