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
    public class TacGiumsController : Controller
    {
        private readonly QlthuVienContext _context;

        public TacGiumsController(QlthuVienContext context)
        {
            _context = context;
        }

        // GET: Admin/TacGiums
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblTacGia.ToListAsync());
        }

        // GET: Admin/TacGiums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblTacGium = await _context.TblTacGia
                .FirstOrDefaultAsync(m => m.MaTg == id);
            if (tblTacGium == null)
            {
                return NotFound();
            }

            return View(tblTacGium);
        }

        // GET: Admin/TacGiums/Create
        public IActionResult Create()
        {
            int nextId = 1;
            if (_context.TblTacGia.Any())
            {
                nextId = _context.TblTacGia.Max(kh => kh.MaTg) + 1;
            }

            ViewBag.NextMaTg = nextId;
            return View();
        }

        // POST: Admin/TacGiums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaTg,TenTg,Mota")] TblTacGium tblTacGium)
        {
            if (ModelState.IsValid)
            {
                if (_context.TblTacGia.Any(t => t.MaTg == tblTacGium.MaTg))
                {
                    // Tự động gán mã mới nếu bị trùng
                    tblTacGium.MaTg = _context.TblTacGia.Max(t => t.MaTg) + 1;
                }
                _context.Add(tblTacGium);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblTacGium);
        }

        // GET: Admin/TacGiums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblTacGium = await _context.TblTacGia.FindAsync(id);
            if (tblTacGium == null)
            {
                return NotFound();
            }
            return View(tblTacGium);
        }

        // POST: Admin/TacGiums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaTg,TenTg,Mota")] TblTacGium tblTacGium)
        {
            if (id != tblTacGium.MaTg)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblTacGium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblTacGiumExists(tblTacGium.MaTg))
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
            return View(tblTacGium);
        }

        // GET: Admin/TacGiums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblTacGium = await _context.TblTacGia
                .FirstOrDefaultAsync(m => m.MaTg == id);
            if (tblTacGium == null)
            {
                return NotFound();
            }

            return View(tblTacGium);
        }

        // POST: Admin/TacGiums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblTacGium = await _context.TblTacGia.FindAsync(id);
            if (tblTacGium != null)
            {
                _context.TblTacGia.Remove(tblTacGium);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblTacGiumExists(int id)
        {
            return _context.TblTacGia.Any(e => e.MaTg == id);
        }
    }
}
