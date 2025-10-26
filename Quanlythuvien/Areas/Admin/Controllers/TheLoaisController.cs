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
    public class TheLoaisController : Controller
    {
        private readonly QlthuVienContext _context;

        public TheLoaisController(QlthuVienContext context)
        {
            _context = context;
        }

        // GET: Admin/TheLoais
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblTheLoais.ToListAsync());
        }

        // GET: Admin/TheLoais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblTheLoai = await _context.TblTheLoais
                .FirstOrDefaultAsync(m => m.MaTl == id);
            if (tblTheLoai == null)
            {
                return NotFound();
            }

            return View(tblTheLoai);
        }

        // GET: Admin/TheLoais/Create
        public IActionResult Create()
        {
            int nextId = 1;
            if (_context.TblTheLoais.Any())
            {
                nextId = _context.TblTheLoais.Max(tl => tl.MaTl) + 1;
            }

            ViewBag.NextMaTL = nextId;
            return View();
        }

        // POST: Admin/TheLoais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaTl,TenTl,Mota")] TblTheLoai tblTheLoai)
        {
            if (ModelState.IsValid)
            {
                if (_context.TblTheLoais.Any(t => t.MaTl == tblTheLoai.MaTl))
                {
                    // Tự động gán mã mới nếu bị trùng
                    tblTheLoai.MaTl = _context.TblTheLoais.Max(t => t.MaTl) + 1;
                }
                _context.Add(tblTheLoai);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblTheLoai);
        }

        // GET: Admin/TheLoais/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblTheLoai = await _context.TblTheLoais.FindAsync(id);
            if (tblTheLoai == null)
            {
                return NotFound();
            }
            return View(tblTheLoai);
        }

        // POST: Admin/TheLoais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaTl,TenTl,Mota")] TblTheLoai tblTheLoai)
        {
            if (id != tblTheLoai.MaTl)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblTheLoai);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblTheLoaiExists(tblTheLoai.MaTl))
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
            return View(tblTheLoai);
        }

        // GET: Admin/TheLoais/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblTheLoai = await _context.TblTheLoais
                .FirstOrDefaultAsync(m => m.MaTl == id);
            if (tblTheLoai == null)
            {
                return NotFound();
            }

            return View(tblTheLoai);
        }

        // POST: Admin/TheLoais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblTheLoai = await _context.TblTheLoais.FindAsync(id);
            if (tblTheLoai != null)
            {
                _context.TblTheLoais.Remove(tblTheLoai);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblTheLoaiExists(int id)
        {
            return _context.TblTheLoais.Any(e => e.MaTl == id);
        }
    }
}
