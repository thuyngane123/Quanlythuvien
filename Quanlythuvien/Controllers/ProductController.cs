using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quanlythuvien.Models;
using System.Collections.Generic;

namespace Quanlythuvien.Controllers
{
    public class ProductController : Controller
    {
        private readonly QlthuVienContext _context;

        public ProductController(QlthuVienContext context)
        {
            _context = context;
        }
        public IActionResult Index()

        {
            var list = _context.TblSaches
                .Include(s => s.MaTlNavigation)
                .Include(s => s.MaTgNavigation)
                .Include(s => s.MaNxbNavigation)
                .ToList();
            return View(list);

        }
        [Route("/product/{tenSach}-{MaSach:int}.html")]
        public async Task<IActionResult> Details(int? MaSach)
        {


            if (MaSach == null || _context.TblSaches == null)
            {
                return NotFound();
            }

            var sach = await _context.TblSaches
                .Include(s => s.MaTlNavigation)
                .Include(s => s.MaTgNavigation)
                .Include(s => s.MaNxbNavigation)
                .Include(s => s.MaTgs)
                .FirstOrDefaultAsync(s => s.MaSach == MaSach);

            if (sach == null)
            {
                return NotFound();
            }

            ViewBag.SachLienQuan = _context.TblSaches
                .Where(s => s.MaSach != MaSach && s.MaTl == sach.MaTl)
                .Take(5)
                .ToList();
            ViewBag.sach = _context.TblSaches
       .OrderByDescending(s => s.MaSach) // hoặc .Take(5) nếu chỉ lấy 5 cuốn
            .Take(5)
       .ToList();
            return View(sach);
        }
        


    }
}
