using Microsoft.AspNetCore.Mvc;
using WeddingRestaurant.Models;

namespace WeddingRestaurant.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const string CartSessionKey = "CartSession";

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Hiển thị giỏ hàng
        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        // Thêm món vào giỏ
        public IActionResult AddToCart(int id)
        {
            var monAn = _context.MonAns.FirstOrDefault(m => m.Id == id);
            if (monAn == null) return NotFound();

            var cart = GetCart();
            var existingItem = cart.FirstOrDefault(i => i.MonAnId == id);

            if (existingItem != null)
                existingItem.SoLuong++;
            else
                cart.Add(new CartItem
                {
                    MonAnId = monAn.Id,
                    TenMonAn = monAn.TenMonAn,
                    GiaTien = monAn.GiaTien,
                    SoLuong = 1
                });

            SaveCart(cart);
            return RedirectToAction("Index", "Cart");
        }

        // Xóa một món khỏi giỏ
        public IActionResult Remove(int id)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(i => i.MonAnId == id);
            if (item != null) cart.Remove(item);
            SaveCart(cart);
            return RedirectToAction("Index");
        }

        // Xóa toàn bộ giỏ hàng
        public IActionResult Clear()
        {
            SaveCart(new List<CartItem>());
            return RedirectToAction("Index");
        }

        // ========== Helper ==========

        private List<CartItem> GetCart()
        {
            var sessionData = HttpContext.Session.GetString(CartSessionKey);
            return sessionData != null
                ? System.Text.Json.JsonSerializer.Deserialize<List<CartItem>>(sessionData)
                : new List<CartItem>();
        }

        private void SaveCart(List<CartItem> cart)
        {
            var data = System.Text.Json.JsonSerializer.Serialize(cart);
            HttpContext.Session.SetString(CartSessionKey, data);
        }
    }
}
