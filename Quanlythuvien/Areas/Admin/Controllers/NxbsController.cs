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
    public class NxbsController : Controller
    {
        private readonly QlthuVienContext _context;

        public NxbsController(QlthuVienContext context)
        {
            _context = context;
        }

        // GET: Admin/Nxbs
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblNxbs.ToListAsync());
        }

        // GET: Admin/Nxbs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblNxb = await _context.TblNxbs
                .FirstOrDefaultAsync(m => m.MaNxb == id);
            if (tblNxb == null)
            {
                return NotFound();
            }

            return View(tblNxb);
        }

        // GET: Admin/Nxbs/Create
        public IActionResult Create()
        {
            int nextId = 1;
            if (_context.TblNxbs.Any())
            {
                nextId = _context.TblNxbs.Max(tl => tl.MaNxb) + 1;
            }

            ViewBag.NextMaNxb = nextId;
            return View();
        }

        // POST: Admin/Nxbs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaNxb,TenNxb,Diachi,Sodienthoai")] TblNxb tblNxb)
        {
            if (ModelState.IsValid)
            {
                if (_context.TblNxbs.Any(t => t.MaNxb == tblNxb.MaNxb))
                {
                    // Tự động gán mã mới nếu bị trùng
                    tblNxb.MaNxb = _context.TblNxbs.Max(t => t.MaNxb) + 1;
                }
                _context.Add(tblNxb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblNxb);
        }

        // GET: Admin/Nxbs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblNxb = await _context.TblNxbs.FindAsync(id);
            if (tblNxb == null)
            {
                return NotFound();
            }
            return View(tblNxb);
        }

        // POST: Admin/Nxbs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaNxb,TenNxb,Diachi,Sodienthoai")] TblNxb tblNxb)
        {
            if (id != tblNxb.MaNxb)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblNxb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblNxbExists(tblNxb.MaNxb))
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
            return View(tblNxb);
        }

        // GET: Admin/Nxbs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblNxb = await _context.TblNxbs
                .FirstOrDefaultAsync(m => m.MaNxb == id);
            if (tblNxb == null)
            {
                return NotFound();
            }

            return View(tblNxb);
        }

        // POST: Admin/Nxbs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblNxb = await _context.TblNxbs.FindAsync(id);
            if (tblNxb != null)
            {
                _context.TblNxbs.Remove(tblNxb);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblNxbExists(int id)
        {
            return _context.TblNxbs.Any(e => e.MaNxb == id);
        }
    }
}
