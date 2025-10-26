using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Quanlythuvien.Models;

namespace Quanlythuvien.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KhachHangsController : Controller
    {
        private readonly QlthuVienContext _context;

        public KhachHangsController(QlthuVienContext context)
        {
            _context = context;
        }

        // GET: Admin/KhachHangs
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblKhachHangs.ToListAsync());
        }

        // GET: Admin/KhachHangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblKhachHang = await _context.TblKhachHangs
                .FirstOrDefaultAsync(m => m.MaKh == id);
            if (tblKhachHang == null)
            {
                return NotFound();
            }

            return View(tblKhachHang);
        }

        // GET: Admin/KhachHangs/Create
        public IActionResult Create()
        {
            int nextId = 1;
            if (_context.TblKhachHangs .Any())
            {
                nextId = _context.TblKhachHangs.Max(kh => kh.MaKh) + 1;
            }

            ViewBag.NextMaKh = nextId;
            return View();
        }

        // POST: Admin/KhachHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaKh,Tendangnhap,Matkhau,Hoten,Email,Sodienthoai,Trangthai")] TblKhachHang tblKhachHang)
        {
            if (ModelState.IsValid)
            {
                tblKhachHang.Tendangnhap = QLThuVien.Utilities.Function.TitleSlugGenerationAlias(tblKhachHang.Tendangnhap);
                if (_context.TblKhachHangs.Any(t => t.MaKh == tblKhachHang.MaKh))
                {
                    // Tự động gán mã mới nếu bị trùng
                    tblKhachHang.MaKh = _context.TblKhachHangs.Max(t => t.MaKh) + 1;
                }
                _context.Add(tblKhachHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblKhachHang);
        }

        // GET: Admin/KhachHangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblKhachHang = await _context.TblKhachHangs.FindAsync(id);
            if (tblKhachHang == null)
            {
                return NotFound();
            }
            return View(tblKhachHang);
        }

        // POST: Admin/KhachHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaKh,Tendangnhap,Matkhau,Hoten,Email,Sodienthoai,Trangthai")] TblKhachHang tblKhachHang)
        {
            if (id != tblKhachHang.MaKh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblKhachHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblKhachHangExists(tblKhachHang.MaKh))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tblKhachHang);
        }

        // GET: Admin/KhachHangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblKhachHang = await _context.TblKhachHangs
                .FirstOrDefaultAsync(m => m.MaKh == id);
            if (tblKhachHang == null)
            {
                return NotFound();
            }

            return View(tblKhachHang);
        }

        // POST: Admin/KhachHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblKhachHang = await _context.TblKhachHangs.FindAsync(id);
            if (tblKhachHang != null)
            {
                _context.TblKhachHangs.Remove(tblKhachHang);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblKhachHangExists(int id)
        {
            return _context.TblKhachHangs.Any(e => e.MaKh == id);
        }
    }
}
