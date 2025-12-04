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
    public class SachesController : Controller
    {
        private readonly QlthuVienContext _context;

        public SachesController(QlthuVienContext context)
        {
            _context = context;
        }

        // GET: Admin/Saches
        public async Task<IActionResult> Index()
        {
            var qlthuVienContext = _context.TblSaches
                .Include(s => s.MaTlNavigation)
                .Include(s => s.MaNxbNavigation)
                .Include(s => s.MaTgs) // danh sách tác giả trực tiếp
            .Include(s => s.MaTgNavigation);

            return View(await qlthuVienContext.ToListAsync());
        }

        // GET: Admin/Saches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblSach = await _context.TblSaches
                .Include(t => t.MaNxbNavigation)
                .Include(t => t.MaTgNavigation)
                .Include(t => t.MaTlNavigation)
                .FirstOrDefaultAsync(m => m.MaSach == id);
            if (tblSach == null)
            {
                return NotFound();
            }

            return View(tblSach);
        }

        // GET: Admin/Saches/Create
        public IActionResult Create()
        {
            // Lấy mã lớn nhất + 1
            int maxId = _context.TblSaches.Max(s => (int?)s.MaSach) ?? 0;
            TblSach model = new TblSach { MaSach = maxId + 1 };

            // Dropdown
            ViewBag.MaTl = new SelectList(_context.TblTheLoais, "MaTl", "TenTl");
            ViewBag.MaNxb = new SelectList(_context.TblNxbs, "MaNxb", "TenNxb");
            ViewBag.MaTg = new SelectList(_context.TblTacGia, "MaTg", "TenTg");

            return View(model);
        }


        // POST: Admin/Saches/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TblSach model, int SelectedTgId)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra tác giả tồn tại
                var tacGia = await _context.TblTacGia.FindAsync(SelectedTgId);
                if (ModelState.IsValid)
                {
                    // Trực tiếp gán MaTg (giá trị lấy từ dropdown, chắc chắn tồn tại)
                    _context.Add(model);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            // Load lại dropdown nếu lỗi
            ViewBag.MaTl = new SelectList(_context.TblTheLoais, "MaTl", "TenTl", model.MaTl);
            ViewBag.MaNxb = new SelectList(_context.TblNxbs, "MaNxb", "TenNxb", model.MaNxb);
            
            ViewBag.MaTg = new SelectList(_context.TblTacGia, "MaTg", "TenTg", model.MaNxb);
            return View(model);
        }


        // GET: Admin/Saches/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var sach = await _context.TblSaches
                .Include(s => s.MaTgs) // load danh sách tác giả liên kết (để hiển thị tác giả đã chọn)
                .Include(s => s.MaTgNavigation) // load tác giả chính
                .FirstOrDefaultAsync(s => s.MaSach == id);

            if (sach == null) return NotFound();

            ViewBag.MaTl = new SelectList(_context.TblTheLoais, "MaTl", "TenTl", sach.MaTl);
            ViewBag.MaNxb = new SelectList(_context.TblNxbs, "MaNxb", "TenNxb", sach.MaNxb);

            // Dùng cho control multi-select trong View
            ViewBag.TacGiaList = _context.TblTacGia.ToList();

            return View(sach);
        }


        // POST: Admin/Saches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Admin/Saches/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TblSach model, int[] SelectedTgIds)
        {
            if (id != model.MaSach) return NotFound();

            if (ModelState.IsValid)
            {
                // 1. Tải lại bản ghi (tracking entity) để quản lý cả thông tin chính và collection N-N
                var sachToUpdate = await _context.TblSaches
                    .Include(s => s.MaTgs) // Cần tải collection để có thể Clear/Add
                    .FirstOrDefaultAsync(s => s.MaSach == id);

                if (sachToUpdate == null) return NotFound();

                
               
                sachToUpdate.TenSach = model.TenSach;
                sachToUpdate.Soluong = model.Soluong;
                sachToUpdate.Trangthai = model.Trangthai;
                sachToUpdate.MaTl = model.MaTl;
                sachToUpdate.MaNxb = model.MaNxb;
                
                // 3. Cập nhật quan hệ N-N (tblSachTacGia) - KHÔNG ĐỔI
                sachToUpdate.MaTgs.Clear();

                if (SelectedTgIds != null)
                {
                    foreach (var tgId in SelectedTgIds)
                    {
                        var tg = await _context.TblTacGia.FindAsync(tgId);
                        if (tg != null) sachToUpdate.MaTgs.Add(tg);
                    }
                }

                // Bỏ lệnh _context.Update(model) cũ, vì bạn đã cập nhật qua sachToUpdate

                // 4. Lưu tất cả thay đổi
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Load lại dữ liệu nếu lỗi
            ViewBag.MaTl = new SelectList(_context.TblTheLoais, "MaTl", "TenTl", model.MaTl);
            ViewBag.MaNxb = new SelectList(_context.TblNxbs, "MaNxb", "TenNxb", model.MaNxb);
            ViewBag.TacGiaList = _context.TblTacGia.ToList();
            return View(model);
        }
        // GET: Admin/Saches/Delete/5
        // GET: Admin/Saches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblSach = await _context.TblSaches
                .Include(t => t.MaNxbNavigation)
                .Include(t => t.MaTgNavigation)
                .Include(t => t.MaTlNavigation)
                .Include(t => t.MaTgs) // <<< THÊM: Tải danh sách tác giả liên kết (N-N)
                .FirstOrDefaultAsync(m => m.MaSach == id);

            if (tblSach == null)
            {
                return NotFound();
            }

            return View(tblSach);
        }
        // POST: Admin/Saches/Delete/5
        // POST: Admin/Saches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // 1. Tải bản ghi Sách VÀ các bản ghi liên kết (MaTgs)
            var tblSach = await _context.TblSaches
                .Include(s => s.MaTgs) // <<< PHẢI CÓ: Để EF Core biết các bản ghi trong tblSachTacGia cần xóa
                .FirstOrDefaultAsync(s => s.MaSach == id);

            if (tblSach != null)
            {
                // 2. Xóa bản ghi chính. EF Core sẽ tự động xóa các bản ghi trong MaTgs (tblSachTacGia) trước.
                _context.TblSaches.Remove(tblSach);
            }

            // 3. Lưu thay đổi
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}