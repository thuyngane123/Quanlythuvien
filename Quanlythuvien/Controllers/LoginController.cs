using Microsoft.AspNetCore.Mvc;
using Quanlythuvien.Models;

namespace Quanlythuvien.Controllers
{
    public class LoginController : Controller
    {
        private readonly QlthuVienContext _context;

        public LoginController(QlthuVienContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Vui lòng nhập tên đăng nhập và mật khẩu!";
                return View();
            }

            // Kiểm tra user
            var user = _context.TblKhachHangs
                .FirstOrDefault(x => x.Tendangnhap == username && x.Matkhau.Trim() == password.Trim());

            if (user == null)
            {
                ViewBag.Error = "Sai tên đăng nhập hoặc mật khẩu!";
                return View();
            }
          
            HttpContext.Session.SetString("Tendangnhap", user.Hoten);
            // Nếu đăng nhập đúng, chuyển hướng về trang chủ
            return RedirectToAction("Index", "Home");
        }
    }
}
