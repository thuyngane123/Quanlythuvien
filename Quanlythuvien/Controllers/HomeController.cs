using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Quanlythuvien.Models;

namespace Quanlythuvien.Controllers
{
    public class HomeController : Controller
    {
        private readonly QlthuVienContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(QlthuVienContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.sach = _context.TblSaches.Take(7).ToList();
            var bestBooks = _context.TblSaches
       .Select(s => new {
           s.MaSach,
           s.TenSach,
           s.Anh,
           s.Soluong,
           LuotMuon = _context.TblMuonTras.Count(m => m.MaSach == s.MaSach)
       })
       .OrderByDescending(x => x.LuotMuon)
       .Take(6)
       .ToList();

            ViewBag.BestBooks = bestBooks;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
