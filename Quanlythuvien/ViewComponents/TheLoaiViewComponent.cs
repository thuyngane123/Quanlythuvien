using Microsoft.AspNetCore.Mvc;
using Quanlythuvien.Models;
using System.Linq;

namespace Quanlythuvien.ViewComponents
{
    public class TheLoaiViewComponent : ViewComponent
    {
        private readonly QlthuVienContext _context;

        public TheLoaiViewComponent(QlthuVienContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var theloai = _context.TblTheLoais.ToList(); // Lấy danh sách thể loại
            return View(theloai);
        }
    }
}
