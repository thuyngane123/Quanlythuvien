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
    public class NhanViensController : Controller
    {
        private readonly QlthuVienContext _context;

        public NhanViensController(QlthuVienContext context)
        {
            _context = context;
        }

        // GET: Admin/NhanViens
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblNhanViens.ToListAsync());
        }

        // GET: Admin/NhanViens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblNhanVien = await _context.TblNhanViens
                .FirstOrDefaultAsync(m => m.MaNv == id);
            if (tblNhanVien == null)
            {
                return NotFound();
            }

            return View(tblNhanVien);
        }

        // GET: Admin/NhanViens/Create
        public IActionResult Create()
        {
            int nextId = 1;
            if (_context.TblNhanViens.Any())
            {
                nextId = _context.TblNhanViens.Max(nv => nv.MaNv) + 1;
            }

            ViewBag.NextMaNv = nextId;

            return View();
        }

        // POST: Admin/NhanViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaNv,TenNv,Matkhau,Hoten,Diachi,Sodienthoai,Trangthai")] TblNhanVien tblNhanVien)
        {
            if (ModelState.IsValid)
            {
                tblNhanVien.TenNv = QLThuVien.Utilities.Function.TitleSlugGenerationAlias(tblNhanVien.TenNv);

                if (ModelState.IsValid)
                {
                    if (_context.TblNhanViens.Any(t => t.MaNv == tblNhanVien.MaNv))
                    {
                        // Tự động gán mã mới nếu bị trùng
                        tblNhanVien.MaNv = _context.TblNhanViens.Max(t => t.MaNv) + 1;
                    }
                }
                _context.Add(tblNhanVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblNhanVien);
        }

        // GET: Admin/NhanViens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblNhanVien = await _context.TblNhanViens.FindAsync(id);
            if (tblNhanVien == null)
            {
                return NotFound();
            }
            return View(tblNhanVien);
        }

        // POST: Admin/NhanViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaNv,TenNv,Matkhau,Hoten,Diachi,Sodienthoai,Trangthai")] TblNhanVien tblNhanVien)
        {
            if (id != tblNhanVien.MaNv)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblNhanVien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblNhanVienExists(tblNhanVien.MaNv))
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
            return View(tblNhanVien);
        }

        // GET: Admin/NhanViens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblNhanVien = await _context.TblNhanViens
                .FirstOrDefaultAsync(m => m.MaNv == id);
            if (tblNhanVien == null)
            {
                return NotFound();
            }

            return View(tblNhanVien);
        }

        // POST: Admin/NhanViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblNhanVien = await _context.TblNhanViens.FindAsync(id);
            if (tblNhanVien != null)
            {
                _context.TblNhanViens.Remove(tblNhanVien);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblNhanVienExists(int id)
        {
            return _context.TblNhanViens.Any(e => e.MaNv == id);
        }
    }
}
