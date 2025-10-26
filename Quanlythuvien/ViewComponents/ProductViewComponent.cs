using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quanlythuvien.Models;

namespace Quanlythuvien.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {
        private readonly QlthuVienContext _context;

        public ProductViewComponent( QlthuVienContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            // Lấy toàn bộ sách từ bảng TblSach
            var sach = _context.TblSaches.ToList();
      
            return View(sach); // Gửi đến Default.cshtml
        }
    }
}
