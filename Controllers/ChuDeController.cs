using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThiTracNghiem.Data;
using Microsoft.AspNetCore.Authorization;

namespace ThiTracNghiem
{
    [Authorize(Roles = "admin")]
    public class ChuDeController : Controller
    {
        private readonly AppDbContext _context;

        public ChuDeController(AppDbContext context)
        {
            _context = context;
        }

        // Phân Trang và Tìm Kiếm Theo Tên Chủ Đề
        public async Task<IActionResult> Index(string searchString, int page = 1, int pageSize = 5)
        {
            var query = _context.ChuDes.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(cd => cd.TenChuDe.Contains(searchString));
            }

            int totalItems = await query.CountAsync();

            var chuDes = await query
                .OrderBy(cd => cd.TenChuDe)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            ViewBag.CurrentFilter = searchString;

            return View(chuDes);
        }


        // GET: ChuDe/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chuDe = await _context.ChuDes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chuDe == null)
            {
                return NotFound();
            }

            return View(chuDe);
        }

        // GET: ChuDe/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ChuDe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TenChuDe")] ChuDe chuDe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chuDe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chuDe);
        }

        // GET: ChuDe/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chuDe = await _context.ChuDes.FindAsync(id);
            if (chuDe == null)
            {
                return NotFound();
            }
            return View(chuDe);
        }

        // POST: ChuDe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TenChuDe")] ChuDe chuDe)
        {
            if (id != chuDe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chuDe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChuDeExists(chuDe.Id))
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
            return View(chuDe);
        }

        // GET: ChuDe/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chuDe = await _context.ChuDes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chuDe == null)
            {
                return NotFound();
            }

            return View(chuDe);
        }

        // POST: ChuDe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                // Kiểm tra xem có đề thi nào thuộc chủ đề này không
                bool coDeThiLienQuan = await _context.DeThis.AnyAsync(d => d.ChuDeId == id);
                
                // Kiểm tra xem có câu hỏi nào thuộc chủ đề này không
                bool coCauHoiLienQuan = await _context.CauHois.AnyAsync(c => c.ChuDeId == id);

                if (coDeThiLienQuan || coCauHoiLienQuan)
                {
                    string thongBaoLoi = "Không thể xóa chủ đề này vì ";
                    if (coDeThiLienQuan && coCauHoiLienQuan)
                        thongBaoLoi += "đã có đề thi và câu hỏi liên quan.";
                    else if (coDeThiLienQuan)
                        thongBaoLoi += "đã có đề thi liên quan.";
                    else
                        thongBaoLoi += "đã có câu hỏi liên quan.";
                        
                    TempData["ErrorMessage"] = thongBaoLoi;
                    return RedirectToAction(nameof(Index));
                }

                // Nếu không có đề thi hoặc câu hỏi liên quan, tiến hành xóa
                var chuDe = await _context.ChuDes.FindAsync(id);
                if (chuDe != null)
                {
                    _context.ChuDes.Remove(chuDe);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Xóa chủ đề thành công.";
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Bắt các lỗi khác nếu có
                TempData["ErrorMessage"] = "Không thể xóa chủ đề. Lỗi: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        private bool ChuDeExists(int id)
        {
            return _context.ChuDes.Any(e => e.Id == id);
        }
    }
}
