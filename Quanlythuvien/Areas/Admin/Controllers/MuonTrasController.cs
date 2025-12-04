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
    public class MuonTrasController : Controller
    {
        private readonly QlthuVienContext _context;

        public MuonTrasController(QlthuVienContext context)
        {
            _context = context;
        }

        // GET: Admin/MuonTras
        public async Task<IActionResult> Index()
        {
            ViewData["MaKh"] = new SelectList(_context.TblKhachHangs, "MaKh", "TenKh");
            ViewData["MaSach"] = new SelectList(_context.TblSaches, "MaSach", "TenSach");

            var qlthuVienContext = _context.TblMuonTras.Include(t => t.MaKhNavigation).Include(t => t.MaSachNavigation);
            return View(await qlthuVienContext.ToListAsync());
        }

        // GET: Admin/MuonTras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblMuonTra = await _context.TblMuonTras
                .Include(t => t.MaKhNavigation)
                .Include(t => t.MaSachNavigation)
                .FirstOrDefaultAsync(m => m.MaMuon == id);
            if (tblMuonTra == null)
            {
                return NotFound();
            }

            return View(tblMuonTra);
        }

        // GET: Admin/MuonTras/Create
        public IActionResult Create()
        {
            ViewData["MaKh"] = new SelectList(_context.TblKhachHangs, "MaKh", "Hoten");
            ViewData["MaSach"] = new SelectList(_context.TblSaches, "MaSach", "TenSach");
            int nextId = 1;
            if (_context.TblMuonTras.Any())
            {
                nextId = _context.TblMuonTras.Max(tl => tl.MaMuon) + 1;
            }

            ViewBag.NextMaMuon = nextId;
            return View();
        }

        // POST: Admin/MuonTras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaMuon,MaKh,MaSach,Ngaymuon,Ngaytra,Trangthai")] TblMuonTra tblMuonTra)
        {

            if (!ModelState.IsValid)
            {
                // Gửi lỗi ra View để xem
                ViewBag.ModelErrors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
            }

            if (ModelState.IsValid)
            {
                    if (_context.TblMuonTras.Any(t => t.MaMuon == tblMuonTra.MaMuon))
                    {
                        // Tự động gán mã mới nếu bị trùng
                        tblMuonTra.MaMuon = _context.TblMuonTras.Max(t => t.MaMuon) + 1;
                    }
                    _context.Add(tblMuonTra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaKh"] = new SelectList(_context.TblKhachHangs, "MaKh", "Hoten", tblMuonTra.MaKh);
            ViewData["MaSach"] = new SelectList(_context.TblSaches, "MaSach", "TenSach", tblMuonTra.MaSach);
            return View(tblMuonTra);
        }

        // GET: Admin/MuonTras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblMuonTra = await _context.TblMuonTras.FindAsync(id);
            if (tblMuonTra == null)
            {
                return NotFound();
            }
            ViewData["MaKh"] = new SelectList(_context.TblKhachHangs, "MaKh", "Hoten", tblMuonTra.MaKh);
            ViewData["MaSach"] = new SelectList(_context.TblSaches, "MaSach", "TenSach", tblMuonTra.MaSach);
            return View(tblMuonTra);
        }

        // POST: Admin/MuonTras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaMuon,MaKh,MaSach,Ngaymuon,Ngaytra,Trangthai")] TblMuonTra tblMuonTra)
        {
            if (id != tblMuonTra.MaMuon)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblMuonTra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblMuonTraExists(tblMuonTra.MaMuon))
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
            ViewData["MaKh"] = new SelectList(_context.TblKhachHangs, "MaKh", "Hoten", tblMuonTra.MaKh);
            ViewData["MaSach"] = new SelectList(_context.TblSaches, "MaSach", "TenSach", tblMuonTra.MaSach);
            return View(tblMuonTra);
        }
        public async Task<IActionResult> TraSach(int id)
        {
            var muonTra = await _context.TblMuonTras.FindAsync(id);

            if (muonTra == null)
                return NotFound();

            // Cập nhật trạng thái
            muonTra.Trangthai = "Đã trả";
            muonTra.Ngaytra = DateOnly.FromDateTime(DateTime.Now);

            _context.Update(muonTra);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        // GET: Admin/MuonTras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblMuonTra = await _context.TblMuonTras
                .Include(t => t.MaKhNavigation)
                .Include(t => t.MaSachNavigation)
                .FirstOrDefaultAsync(m => m.MaMuon == id);
            if (tblMuonTra == null)
            {
                return NotFound();
            }

            return View(tblMuonTra);
        }

        // POST: Admin/MuonTras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblMuonTra = await _context.TblMuonTras.FindAsync(id);
            if (tblMuonTra != null)
            {
                _context.TblMuonTras.Remove(tblMuonTra);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblMuonTraExists(int id)
        {
            return _context.TblMuonTras.Any(e => e.MaMuon == id);
        }
    }
}
