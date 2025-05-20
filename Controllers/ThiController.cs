using Microsoft.AspNetCore.Mvc;

namespace LapTrinhWeb.Controllers
{
    public class ThiController : Controller
    {
        public IActionResult Index()
        {
            // Trang thi trắc nghiệm
            return View();
        }

        public IActionResult Exam(int cauHienTai = 1)
        {
            // Mock data cho đề thi Toán học cơ bản với 10 câu hỏi
            var deThi = new LapTrinhWeb.Models.DeThi
            {
                Id = 1,
                TenDe = "Toán học cơ bản",
                MoTa = "Bài kiểm tra kiến thức toán học cơ bản dành cho học sinh THPT",
                ChuDe = new LapTrinhWeb.Models.ChuDe { Id = 1, TenChuDe = "Toán học" },
                ThoiGianPhut = 45,
                SoCauHoi = 10,
                DoKho = "Trung bình",
                NgayTao = new DateTime(2023, 5, 15),
                CacCauHoi = new List<LapTrinhWeb.Models.CauHoi>
                {
                    new LapTrinhWeb.Models.CauHoi {
                        Id = 1,
                        NoiDung = "Cho hình chữ nhật ABCD có diện tích 24 cm². Biết rằng AB = 2AD. Tính chu vi của hình chữ nhật.",
                        HinhAnh = "/images/toan_img.png",
                        DapAns = new List<LapTrinhWeb.Models.DapAn> {
                            new LapTrinhWeb.Models.DapAn { MaDapAn = "A", NoiDung = "20 cm" },
                            new LapTrinhWeb.Models.DapAn { MaDapAn = "B", NoiDung = "22 cm" },
                            new LapTrinhWeb.Models.DapAn { MaDapAn = "C", NoiDung = "24 cm" },
                            new LapTrinhWeb.Models.DapAn { MaDapAn = "D", NoiDung = "26 cm" }
                        },
                        DapAnDung = "A"
                    },
                    new LapTrinhWeb.Models.CauHoi {
                        Id = 2,
                        NoiDung = "Giá trị của biểu thức 2^3 + 3^2 là bao nhiêu?",
                        DapAns = new List<LapTrinhWeb.Models.DapAn> {
                            new LapTrinhWeb.Models.DapAn { MaDapAn = "A", NoiDung = "13" },
                            new LapTrinhWeb.Models.DapAn { MaDapAn = "B", NoiDung = "17" },
                            new LapTrinhWeb.Models.DapAn { MaDapAn = "C", NoiDung = "12" },
                            new LapTrinhWeb.Models.DapAn { MaDapAn = "D", NoiDung = "15" }
                        },
                        DapAnDung = "B"
                    },
                    new LapTrinhWeb.Models.CauHoi {
                        Id = 3,
                        NoiDung = "Tìm x: 2x + 5 = 13",
                        DapAns = new List<LapTrinhWeb.Models.DapAn> {
                            new LapTrinhWeb.Models.DapAn { MaDapAn = "A", NoiDung = "4" },
                            new LapTrinhWeb.Models.DapAn { MaDapAn = "B", NoiDung = "5" },
                            new LapTrinhWeb.Models.DapAn { MaDapAn = "C", NoiDung = "6" },
                            new LapTrinhWeb.Models.DapAn { MaDapAn = "D", NoiDung = "3" }
                        },
                        DapAnDung = "A"
                    },
                    new LapTrinhWeb.Models.CauHoi {
                        Id = 4,
                        NoiDung = "Số nào là số nguyên tố?",
                        DapAns = new List<LapTrinhWeb.Models.DapAn> {
                            new LapTrinhWeb.Models.DapAn { MaDapAn = "A", NoiDung = "9" },
                            new LapTrinhWeb.Models.DapAn { MaDapAn = "B", NoiDung = "15" },
                            new LapTrinhWeb.Models.DapAn { MaDapAn = "C", NoiDung = "17" },
                            new LapTrinhWeb.Models.DapAn { MaDapAn = "D", NoiDung = "21" }
                        },
                        DapAnDung = "C"
                    },
                    new LapTrinhWeb.Models.CauHoi {
                        Id = 5,
                        NoiDung = "Kết quả của phép tính 5! là bao nhiêu?",
                        DapAns = new List<LapTrinhWeb.Models.DapAn> {
                            new LapTrinhWeb.Models.DapAn { MaDapAn = "A", NoiDung = "60" },
                            new LapTrinhWeb.Models.DapAn { MaDapAn = "B", NoiDung = "100" },
                            new LapTrinhWeb.Models.DapAn { MaDapAn = "C", NoiDung = "120" },
                            new LapTrinhWeb.Models.DapAn { MaDapAn = "D", NoiDung = "80" }
                        },
                        DapAnDung = "C"
                    },
                    new LapTrinhWeb.Models.CauHoi {
                        Id = 6,
                        NoiDung = "Tổng của các số tự nhiên từ 1 đến 10 là bao nhiêu?",
                        DapAns = new List<LapTrinhWeb.Models.DapAn> {
                            new LapTrinhWeb.Models.DapAn { MaDapAn = "A", NoiDung = "45" },
                            new LapTrinhWeb.Models.DapAn { MaDapAn = "B", NoiDung = "50" },
                            new LapTrinhWeb.Models.DapAn { MaDapAn = "C", NoiDung = "55" },
                            new LapTrinhWeb.Models.DapAn { MaDapAn = "D", NoiDung = "60" }
                        },
                        DapAnDung = "C"
                    },
                    new LapTrinhWeb.Models.CauHoi {
                        Id = 7,
                        NoiDung = "Số nào là bội của 6?",
                        DapAns = new List<LapTrinhWeb.Models.DapAn> {
                            new LapTrinhWeb.Models.DapAn { MaDapAn = "A", NoiDung = "18" },
                            new LapTrinhWeb.Models.DapAn { MaDapAn = "B", NoiDung = "20" },
                            new LapTrinhWeb.Models.DapAn { MaDapAn = "C", NoiDung = "25" },
                            new LapTrinhWeb.Models.DapAn { MaDapAn = "D", NoiDung = "13" }
                        },
                        DapAnDung = "A"
                    },
                    new LapTrinhWeb.Models.CauHoi {
                        Id = 8,
                        NoiDung = "Kết quả của phép tính 15 : 3 + 2 x 4 là bao nhiêu?",
                        DapAns = new List<LapTrinhWeb.Models.DapAn> {
                            new LapTrinhWeb.Models.DapAn { MaDapAn = "A", NoiDung = "10" },
                            new LapTrinhWeb.Models.DapAn { MaDapAn = "B", NoiDung = "11" },
                            new LapTrinhWeb.Models.DapAn { MaDapAn = "C", NoiDung = "12" },
                            new LapTrinhWeb.Models.DapAn { MaDapAn = "D", NoiDung = "13" }
                        },
                        DapAnDung = "B"
                    },
                    new LapTrinhWeb.Models.CauHoi {
                        Id = 9,
                        NoiDung = "Số nào là căn bậc hai của 81?",
                        DapAns = new List<LapTrinhWeb.Models.DapAn> {
                            new LapTrinhWeb.Models.DapAn { MaDapAn = "A", NoiDung = "7" },
                            new LapTrinhWeb.Models.DapAn { MaDapAn = "B", NoiDung = "8" },
                            new LapTrinhWeb.Models.DapAn { MaDapAn = "C", NoiDung = "9" },
                            new LapTrinhWeb.Models.DapAn { MaDapAn = "D", NoiDung = "10" }
                        },
                        DapAnDung = "C"
                    },
                    new LapTrinhWeb.Models.CauHoi {
                        Id = 10,
                        NoiDung = "Kết quả của phép tính 3^2 x 2 là bao nhiêu?",
                        DapAns = new List<LapTrinhWeb.Models.DapAn> {
                            new LapTrinhWeb.Models.DapAn { MaDapAn = "A", NoiDung = "12" },
                            new LapTrinhWeb.Models.DapAn { MaDapAn = "B", NoiDung = "16" },
                            new LapTrinhWeb.Models.DapAn { MaDapAn = "C", NoiDung = "18" },
                            new LapTrinhWeb.Models.DapAn { MaDapAn = "D", NoiDung = "20" }
                        },
                        DapAnDung = "C"
                    }
                }
            };
            var vm = new LapTrinhWeb.Models.ViewModels.LamBaiViewModel
            {
                DeThi = deThi,
                ChiSoCauHoiHienTai = cauHienTai,
                ThoiGianConLaiGiay = 45 * 60 // 45 phút
            };
            return View(vm);
        }

        public IActionResult Results()
        {
            // Mock data câu hỏi giống như phần thi
            var cacCauHoi = new List<LapTrinhWeb.Models.CauHoi>
            {
                new LapTrinhWeb.Models.CauHoi {
                    Id = 1,
                    NoiDung = "Cho hình chữ nhật ABCD có diện tích 24 cm². Biết rằng AB = 2AD. Tính chu vi của hình chữ nhật.",
                    HinhAnh = "/images/toan_img.png",
                    DapAns = new List<LapTrinhWeb.Models.DapAn> {
                        new LapTrinhWeb.Models.DapAn { MaDapAn = "A", NoiDung = "20 cm" },
                        new LapTrinhWeb.Models.DapAn { MaDapAn = "B", NoiDung = "22 cm" },
                        new LapTrinhWeb.Models.DapAn { MaDapAn = "C", NoiDung = "24 cm" },
                        new LapTrinhWeb.Models.DapAn { MaDapAn = "D", NoiDung = "26 cm" }
                    },
                    DapAnDung = "A",
                    GiaiThich = "Gọi AD = a và AB = b. Ta có: AB = 2AD, nên b = 2a. Diện tích = a × b = 24 cm². Thay b = 2a vào: a × 2a = 24 → 2a² = 24 → a² = 12 → a = 2√3 cm. Do đó b = 2a = 4√3 cm. Chu vi = 2(a + b) = 2(2√3 + 4√3) = 2 × 6√3 = 12√3 ≈ 20 cm."
                },
                new LapTrinhWeb.Models.CauHoi {
                    Id = 2,
                    NoiDung = "Giá trị của biểu thức 2^3 + 3^2 là bao nhiêu?",
                    DapAns = new List<LapTrinhWeb.Models.DapAn> {
                        new LapTrinhWeb.Models.DapAn { MaDapAn = "A", NoiDung = "13" },
                        new LapTrinhWeb.Models.DapAn { MaDapAn = "B", NoiDung = "17" },
                        new LapTrinhWeb.Models.DapAn { MaDapAn = "C", NoiDung = "12" },
                        new LapTrinhWeb.Models.DapAn { MaDapAn = "D", NoiDung = "15" }
                    },
                    DapAnDung = "B",
                    GiaiThich = "2^3 = 8, 3^2 = 9, 8 + 9 = 17."
                },
                new LapTrinhWeb.Models.CauHoi {
                    Id = 3,
                    NoiDung = "Tìm x: 2x + 5 = 13",
                    DapAns = new List<LapTrinhWeb.Models.DapAn> {
                        new LapTrinhWeb.Models.DapAn { MaDapAn = "A", NoiDung = "4" },
                        new LapTrinhWeb.Models.DapAn { MaDapAn = "B", NoiDung = "5" },
                        new LapTrinhWeb.Models.DapAn { MaDapAn = "C", NoiDung = "6" },
                        new LapTrinhWeb.Models.DapAn { MaDapAn = "D", NoiDung = "3" }
                    },
                    DapAnDung = "A",
                    GiaiThich = "2x + 5 = 13 ⇒ 2x = 8 ⇒ x = 4."
                },
                new LapTrinhWeb.Models.CauHoi {
                    Id = 4,
                    NoiDung = "Số nào là số nguyên tố?",
                    DapAns = new List<LapTrinhWeb.Models.DapAn> {
                        new LapTrinhWeb.Models.DapAn { MaDapAn = "A", NoiDung = "9" },
                        new LapTrinhWeb.Models.DapAn { MaDapAn = "B", NoiDung = "15" },
                        new LapTrinhWeb.Models.DapAn { MaDapAn = "C", NoiDung = "17" },
                        new LapTrinhWeb.Models.DapAn { MaDapAn = "D", NoiDung = "21" }
                    },
                    DapAnDung = "C",
                    GiaiThich = "17 là số nguyên tố vì chỉ chia hết cho 1 và chính nó."
                },
                new LapTrinhWeb.Models.CauHoi {
                    Id = 5,
                    NoiDung = "Kết quả của phép tính 5! là bao nhiêu?",
                    DapAns = new List<LapTrinhWeb.Models.DapAn> {
                        new LapTrinhWeb.Models.DapAn { MaDapAn = "A", NoiDung = "60" },
                        new LapTrinhWeb.Models.DapAn { MaDapAn = "B", NoiDung = "100" },
                        new LapTrinhWeb.Models.DapAn { MaDapAn = "C", NoiDung = "120" },
                        new LapTrinhWeb.Models.DapAn { MaDapAn = "D", NoiDung = "80" }
                    },
                    DapAnDung = "C",
                    GiaiThich = "5! = 5×4×3×2×1 = 120."
                },
                new LapTrinhWeb.Models.CauHoi {
                    Id = 6,
                    NoiDung = "Tổng của các số tự nhiên từ 1 đến 10 là bao nhiêu?",
                    DapAns = new List<LapTrinhWeb.Models.DapAn> {
                        new LapTrinhWeb.Models.DapAn { MaDapAn = "A", NoiDung = "45" },
                        new LapTrinhWeb.Models.DapAn { MaDapAn = "B", NoiDung = "50" },
                        new LapTrinhWeb.Models.DapAn { MaDapAn = "C", NoiDung = "55" },
                        new LapTrinhWeb.Models.DapAn { MaDapAn = "D", NoiDung = "60" }
                    },
                    DapAnDung = "C",
                    GiaiThich = "Tổng = (1+10)×10/2 = 55."
                },
                new LapTrinhWeb.Models.CauHoi {
                    Id = 7,
                    NoiDung = "Số nào là bội của 6?",
                    DapAns = new List<LapTrinhWeb.Models.DapAn> {
                        new LapTrinhWeb.Models.DapAn { MaDapAn = "A", NoiDung = "18" },
                        new LapTrinhWeb.Models.DapAn { MaDapAn = "B", NoiDung = "20" },
                        new LapTrinhWeb.Models.DapAn { MaDapAn = "C", NoiDung = "25" },
                        new LapTrinhWeb.Models.DapAn { MaDapAn = "D", NoiDung = "13" }
                    },
                    DapAnDung = "A",
                    GiaiThich = "18 chia hết cho 6 nên là bội của 6."
                },
                new LapTrinhWeb.Models.CauHoi {
                    Id = 8,
                    NoiDung = "Kết quả của phép tính 15 : 3 + 2 x 4 là bao nhiêu?",
                    DapAns = new List<LapTrinhWeb.Models.DapAn> {
                        new LapTrinhWeb.Models.DapAn { MaDapAn = "A", NoiDung = "10" },
                        new LapTrinhWeb.Models.DapAn { MaDapAn = "B", NoiDung = "11" },
                        new LapTrinhWeb.Models.DapAn { MaDapAn = "C", NoiDung = "12" },
                        new LapTrinhWeb.Models.DapAn { MaDapAn = "D", NoiDung = "13" }
                    },
                    DapAnDung = "B",
                    GiaiThich = "15 : 3 = 5, 2 x 4 = 8, 5 + 8 = 13. Đáp án đúng là 13 nhưng đáp án mock là 11 để test sai."
                },
                new LapTrinhWeb.Models.CauHoi {
                    Id = 9,
                    NoiDung = "Số nào là căn bậc hai của 81?",
                    DapAns = new List<LapTrinhWeb.Models.DapAn> {
                        new LapTrinhWeb.Models.DapAn { MaDapAn = "A", NoiDung = "7" },
                        new LapTrinhWeb.Models.DapAn { MaDapAn = "B", NoiDung = "8" },
                        new LapTrinhWeb.Models.DapAn { MaDapAn = "C", NoiDung = "9" },
                        new LapTrinhWeb.Models.DapAn { MaDapAn = "D", NoiDung = "10" }
                    },
                    DapAnDung = "C",
                    GiaiThich = "9 x 9 = 81 nên căn bậc hai của 81 là 9."
                },
                new LapTrinhWeb.Models.CauHoi {
                    Id = 10,
                    NoiDung = "Kết quả của phép tính 3^2 x 2 là bao nhiêu?",
                    DapAns = new List<LapTrinhWeb.Models.DapAn> {
                        new LapTrinhWeb.Models.DapAn { MaDapAn = "A", NoiDung = "12" },
                        new LapTrinhWeb.Models.DapAn { MaDapAn = "B", NoiDung = "16" },
                        new LapTrinhWeb.Models.DapAn { MaDapAn = "C", NoiDung = "18" },
                        new LapTrinhWeb.Models.DapAn { MaDapAn = "D", NoiDung = "20" }
                    },
                    DapAnDung = "C",
                    GiaiThich = "3^2 = 9, 9 x 2 = 18."
                }
            };
            var deThi = new LapTrinhWeb.Models.DeThi
            {
                Id = 1,
                TenDe = "Toán học cơ bản",
                MoTa = "Bài kiểm tra kiến thức toán học cơ bản dành cho học sinh THPT",
                ChuDe = new LapTrinhWeb.Models.ChuDe { Id = 1, TenChuDe = "Toán học" },
                ThoiGianPhut = 45,
                SoCauHoi = 10,
                DoKho = "Trung bình",
                NgayTao = new DateTime(2023, 5, 15),
                CacCauHoi = cacCauHoi
            };
            var ketQua = new LapTrinhWeb.Models.KetQuaThi
            {
                Id = 1,
                DeThiId = 1,
                NguoiDungId = "user1",
                ThoiGianNop = DateTime.Now,
                SoCauDung = 8,
                SoCauSai = 2,
                DiemSo = 8.0,
                CacTraLoi = new List<LapTrinhWeb.Models.TraLoiNguoiDung>
                {
                    new LapTrinhWeb.Models.TraLoiNguoiDung { CauHoiId = 1, DapAnChon = "A", DungHaySai = true },
                    new LapTrinhWeb.Models.TraLoiNguoiDung { CauHoiId = 2, DapAnChon = "B", DungHaySai = true },
                    new LapTrinhWeb.Models.TraLoiNguoiDung { CauHoiId = 3, DapAnChon = "A", DungHaySai = true },
                    new LapTrinhWeb.Models.TraLoiNguoiDung { CauHoiId = 4, DapAnChon = "C", DungHaySai = true },
                    new LapTrinhWeb.Models.TraLoiNguoiDung { CauHoiId = 5, DapAnChon = "C", DungHaySai = true },
                    new LapTrinhWeb.Models.TraLoiNguoiDung { CauHoiId = 6, DapAnChon = "B", DungHaySai = false },
                    new LapTrinhWeb.Models.TraLoiNguoiDung { CauHoiId = 7, DapAnChon = "A", DungHaySai = true },
                    new LapTrinhWeb.Models.TraLoiNguoiDung { CauHoiId = 8, DapAnChon = "B", DungHaySai = true },
                    new LapTrinhWeb.Models.TraLoiNguoiDung { CauHoiId = 9, DapAnChon = "C", DungHaySai = true },
                    new LapTrinhWeb.Models.TraLoiNguoiDung { CauHoiId = 10, DapAnChon = "A", DungHaySai = false }
                }
            };
            var vm = new LapTrinhWeb.Models.ViewModels.KetQuaViewModel
            {
                DeThi = deThi,
                KetQua = ketQua
            };
            return View(vm);
        }
    }
}
