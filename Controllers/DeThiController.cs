using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThiTracNghiem.Models;
using ThiTracNghiem.Data;
using Microsoft.AspNetCore.Authorization;

[Authorize(Roles = "admin")]
public class DeThiController : Controller
{

    private readonly AppDbContext _context;

    public DeThiController(AppDbContext context)
    {
        _context = context;
    }

    // tìm kiếm theo tên đề thi hoặc chủ đề, phân trang
    public async Task<IActionResult> Index(string searchString, int page = 1, int pageSize = 10)
    {
        var query = _context.DeThis
            .Include(d => d.ChuDe)
            .AsQueryable();

        if (!string.IsNullOrEmpty(searchString))
        {
            // Tìm kiếm theo cả tên đề thi và tên chủ đề
            query = query.Where(d => 
                d.TenDeThi.Contains(searchString) || 
                d.ChuDe.TenChuDe.Contains(searchString)
            );
        }

        int totalItems = await query.CountAsync();

        var deThis = await query
            .OrderByDescending(d => d.NgayTao)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
        ViewBag.CurrentFilter = searchString;

        return View(deThis);
    }


    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.ChuDeId = new SelectList(_context.ChuDes, "Id", "TenChuDe");
        return View();
    }

    [HttpPost]
    public IActionResult Create(DeThi model)
    {
        if (!ModelState.IsValid || model.SoLuongCauHoi <= 0)
        {
            ModelState.AddModelError("", "Vui lòng nhập số lượng câu hỏi hợp lệ.");
            ViewBag.ChuDeId = new SelectList(_context.ChuDes, "Id", "TenChuDe", model.ChuDeId);
            return View(model);
        }
        // Đếm số câu hỏi thực tế của chủ đề
        int tongSoCauHoi = _context.CauHois.Count(c => c.ChuDeId == model.ChuDeId);
        if (model.SoLuongCauHoi > tongSoCauHoi)
        {
            ModelState.AddModelError("", $"Chủ đề này chỉ có {tongSoCauHoi} câu hỏi. Không thể chọn {model.SoLuongCauHoi} câu.");
            ViewBag.ChuDeId = new SelectList(_context.ChuDes, "Id", "TenChuDe", model.ChuDeId);
            return View(model);
        }
        // Tạo đề thi mới
        var deThi = new DeThi
        {
            TenDeThi = model.TenDeThi,
            ThoiGianLamBai = model.ThoiGianLamBai,
            TrangThaiMo = model.TrangThaiMo,
            ChuDeId = model.ChuDeId,
            NgayTao = DateTime.Now,
            SoLuongCauHoi = model.SoLuongCauHoi
        };

        _context.DeThis.Add(deThi);
        _context.SaveChanges();



        _context.SaveChanges();
        return RedirectToAction("Index");
    }


    public IActionResult Details(int id)
    {
        var deThi = _context.DeThis
            .Include(d => d.ChuDe)
            .FirstOrDefault(d => d.Id == id);

        if (deThi == null) return NotFound();
        return View(deThi);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var deThi = _context.DeThis
            .FirstOrDefault(d => d.Id == id);

        if (deThi == null) return NotFound();

        var model = new DeThi
        {
            TenDeThi = deThi.TenDeThi,
            ThoiGianLamBai = deThi.ThoiGianLamBai,
            ChuDeId = deThi.ChuDeId,
            TrangThaiMo = deThi.TrangThaiMo,
            SoLuongCauHoi = deThi.SoLuongCauHoi
        };

        ViewBag.ChuDeId = new SelectList(_context.ChuDes, "Id", "TenChuDe", model.ChuDeId);
        ViewBag.DeThiId = deThi.Id;

        return View(model);
    }

    [HttpPost]
    public IActionResult Edit(int id, DeThi model)
    {
        // ràng buộc số lượng câu hỏi phải lớn hơn 0
        if (!ModelState.IsValid || model.SoLuongCauHoi <= 0)
        {
            ModelState.AddModelError("", "Vui lòng nhập số lượng câu hỏi hợp lệ.");
            ViewBag.ChuDeId = new SelectList(_context.ChuDes, "Id", "TenChuDe", model.ChuDeId);
            return View(model);
        }

        // Đếm tổng số câu hỏi có trong chủ đề được chọn
        int totalQuestions = _context.CauHois.Count(c => c.ChuDeId == model.ChuDeId);

        if (model.SoLuongCauHoi > totalQuestions)
        {
            ModelState.AddModelError("", $"Số lượng câu hỏi không được lớn hơn số câu hỏi hiện có ({totalQuestions}) trong chủ đề.");
            ViewBag.ChuDeId = new SelectList(_context.ChuDes, "Id", "TenChuDe", model.ChuDeId);
            return View(model);
        }

        var deThi = _context.DeThis.FirstOrDefault(d => d.Id == id);
        if (deThi == null) return NotFound();
        // var soCauHoi = model.SoLuongCauHoi; 
        // Cập nhật đề thi
        deThi.TenDeThi = model.TenDeThi;
        deThi.ThoiGianLamBai = model.ThoiGianLamBai;
        deThi.TrangThaiMo = model.TrangThaiMo;
        deThi.ChuDeId = model.ChuDeId;
        deThi.SoLuongCauHoi = model.SoLuongCauHoi;
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            // Kiểm tra xem đề thi có chi tiết đề thi không
            bool coChiTietDeThi = await _context.LichSuLamBais.AnyAsync(c => c.DeThiId == id);
            if (coChiTietDeThi)
            {
                // Nếu có chi tiết đề thi, không cho phép xóa và hiển thị thông báo
                TempData["ErrorMessage"] = "Không thể xóa đề thi này vì đã có dữ liệu bài thi liên quan.";
                return RedirectToAction(nameof(Index));
            }
            
            // Nếu không có chi tiết đề thi, tiến hành xóa
            var deThi = await _context.DeThis.FindAsync(id);
            if (deThi != null)
            {
                _context.DeThis.Remove(deThi);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Xóa đề thi thành công.";
            }
            
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            // Bắt các lỗi khác nếu có
            TempData["ErrorMessage"] = "Không thể xóa đề thi. Lỗi: " + ex.Message;
            return RedirectToAction(nameof(Index));
        }
    }

}
