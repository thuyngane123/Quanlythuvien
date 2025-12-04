using Microsoft.AspNetCore.Mvc;
using Quanlythuvien.Models;

namespace Quanlythuvien.Controllers
{
    public class CartController : Controller
    {
        private readonly QlthuVienContext _context;

        public CartController(QlthuVienContext context)
        {
            _context = context;
        }

        // GET: /Cart
        public IActionResult Index(int? MaSach, DateTime? NgayMuon, DateTime? NgayTra)
        {
            ViewBag.TenKh = HttpContext.Session.GetString("Tendangnhap") ?? "Khách lạ";

            if (MaSach != null)
            {
                var sach = _context.TblSaches.FirstOrDefault(x => x.MaSach == MaSach);
                if (sach == null) return NotFound();

                ViewBag.TenSach = sach.TenSach;
                ViewBag.MaSach = MaSach;
                ViewBag.NgayMuon = NgayMuon;
                ViewBag.NgayTra = NgayTra;
            }

            return View();
        }

        // POST nếu bạn muốn nhận dữ liệu từ form
        [HttpPost]
        public IActionResult CreateFromCart(int MaSach, DateTime NgayMuon, DateTime NgayTra)
        {
            // Lấy tên khách hàng từ Session
            var tenKh = HttpContext.Session.GetString("Tendangnhap") ?? "";

            if (string.IsNullOrEmpty(tenKh))
            {
                ViewBag.Message = "Vui lòng đăng nhập trước khi mượn sách!";
                return View("Index"); // quay lại trang Cart
            }

            // Tìm khách hàng theo tên
            var khachHang = _context.TblKhachHangs.FirstOrDefault(x => x.Hoten == tenKh);
            if (khachHang == null)
            {
                ViewBag.Message = "Không tìm thấy khách hàng!";
                return View("Index"); // quay lại trang Cart
            }

            // Tạo phiếu mượn mới
            var muonTra = new TblMuonTra
            {
                MaKh = khachHang.MaKh,
                MaSach = MaSach,
                Ngaymuon = DateOnly.FromDateTime(NgayMuon),
                Ngaytra = DateOnly.FromDateTime(NgayTra),
                Trangthai = "Đang mượn"
            };

            _context.TblMuonTras.Add(muonTra);
            _context.SaveChanges();

            // Giữ dữ liệu hiển thị lại giỏ mượn
            ViewBag.TenKh = tenKh;
            ViewBag.TenSach = _context.TblSaches.FirstOrDefault(x => x.MaSach == MaSach)?.TenSach;
            ViewBag.NgayMuon = NgayMuon;
            ViewBag.NgayTra = NgayTra;
            ViewBag.MaSach = MaSach;

            ViewBag.Message = "Mượn sách thành công!";

            return View("Index"); // hiển thị lại trang Cart với thông báo
        }

    }
}

