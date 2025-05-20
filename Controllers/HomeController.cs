using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LapTrinhWeb.Models;

namespace LapTrinhWeb.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        // Mock data cho danh sách đề thi
        var danhSachDeThi = new List<DeThi>
        {
            new DeThi
            {
                Id = 1,
                TenDe = "Toán học cơ bản",
                MoTa = "Bài kiểm tra kiến thức toán học cơ bản dành cho học sinh THPT",
                ChuDe = new ChuDe { Id = 1, TenChuDe = "Toán học" },
                ThoiGianPhut = 45,
                SoCauHoi = 30,
                DoKho = "Trung bình",
                NgayTao = new DateTime(2023, 5, 15)
            },
            new DeThi
            {
                Id = 2,
                TenDe = "Ngữ pháp tiếng Anh",
                MoTa = "Kiểm tra ngữ pháp và từ vựng tiếng Anh cơ bản",
                ChuDe = new ChuDe { Id = 2, TenChuDe = "Tiếng Anh" },
                ThoiGianPhut = 30,
                SoCauHoi = 25,
                DoKho = "Dễ",
                NgayTao = new DateTime(2023, 6, 20)
            },
            new DeThi
            {
                Id = 3,
                TenDe = "Vật lý đại cương",
                MoTa = "Bài kiểm tra kiến thức vật lý đại cương cho học sinh lớp 12",
                ChuDe = new ChuDe { Id = 3, TenChuDe = "Vật lý" },
                ThoiGianPhut = 60,
                SoCauHoi = 40,
                DoKho = "Khó",
                NgayTao = new DateTime(2023, 7, 10)
            },
            new DeThi
            {
                Id = 4,
                TenDe = "Hóa học hữu cơ",
                MoTa = "Bài kiểm tra về hóa học hữu cơ và các phản ứng cơ bản",
                ChuDe = new ChuDe { Id = 4, TenChuDe = "Hóa học" },
                ThoiGianPhut = 50,
                SoCauHoi = 35,
                DoKho = "Trung bình",
                NgayTao = new DateTime(2023, 8, 5)
            }
        };
        return View(danhSachDeThi);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult QuizDetail(int id = 1)
    {
        // Lấy mock data từ danh sách đề thi (giống Index)
        var danhSachDeThi = new List<DeThi>
        {
            new DeThi
            {
                Id = 1,
                TenDe = "Toán học cơ bản",
                MoTa = "Bài kiểm tra kiến thức toán học cơ bản dành cho học sinh THPT",
                ChuDe = new ChuDe { Id = 1, TenChuDe = "Toán học" },
                ThoiGianPhut = 45,
                SoCauHoi = 30,
                DoKho = "Trung bình",
                NgayTao = new DateTime(2023, 5, 15),
            },
            new DeThi
            {
                Id = 2,
                TenDe = "Ngữ pháp tiếng Anh",
                MoTa = "Kiểm tra ngữ pháp và từ vựng tiếng Anh cơ bản",
                ChuDe = new ChuDe { Id = 2, TenChuDe = "Tiếng Anh" },
                ThoiGianPhut = 30,
                SoCauHoi = 25,
                DoKho = "Dễ",
                NgayTao = new DateTime(2023, 6, 20),
                
            },
            new DeThi
            {
                Id = 3,
                TenDe = "Vật lý đại cương",
                MoTa = "Bài kiểm tra kiến thức vật lý đại cương cho học sinh lớp 12",
                ChuDe = new ChuDe { Id = 3, TenChuDe = "Vật lý" },
                ThoiGianPhut = 60,
                SoCauHoi = 40,
                DoKho = "Khó",
                NgayTao = new DateTime(2023, 7, 10),
            },
            new DeThi
            {
                Id = 4,
                TenDe = "Hóa học hữu cơ",
                MoTa = "Bài kiểm tra về hóa học hữu cơ và các phản ứng cơ bản",
                ChuDe = new ChuDe { Id = 4, TenChuDe = "Hóa học" },
                ThoiGianPhut = 50,
                SoCauHoi = 35,
                DoKho = "Trung bình",
                NgayTao = new DateTime(2023, 8, 5),  
            }
        };
        var deThi = danhSachDeThi.FirstOrDefault(d => d.Id == id);
        var vm = new LapTrinhWeb.Models.ViewModels.ChiTietDeThiViewModel { DeThi = deThi };
        return View(vm);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
