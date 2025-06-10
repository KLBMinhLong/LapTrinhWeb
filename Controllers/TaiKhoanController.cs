using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThiTracNghiem.Data;
using ThiTracNghiem.Models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using ThiTracNghiem.Helpers;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ThiTracNghiem.Controllers
{
    public class TaiKhoanController : Controller
    {
        private readonly AppDbContext _context;

        public TaiKhoanController(AppDbContext context)
        {
            _context = context;
        }

        // GET: TaiKhoan

        // GET: TaiKhoan/Details/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taiKhoan = await _context.TaiKhoans
                .FirstOrDefaultAsync(m => m.TenTaiKhoan == id);
            if (taiKhoan == null)
            {
                return NotFound();
            }

            return View(taiKhoan);
        }

        // GET: TaiKhoan/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: TaiKhoan/Create
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenTaiKhoan,MatKhau,Email,NgaySinh,GioiTinh,SoDienThoai,VaiTro,TrangThaiKhoa,ThoiGianTao,AnhDaiDienUrl")] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                // Mã hóa mật khẩu
                taiKhoan.MatKhau = MaHoaHelper.MaHoaSHA256(taiKhoan.MatKhau);
                _context.Add(taiKhoan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(taiKhoan);
        }

        // GET: TaiKhoan/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taiKhoan = await _context.TaiKhoans.FindAsync(id);
            if (taiKhoan == null)
            {
                return NotFound();
            }
            return View(taiKhoan);
        }

        // POST: TaiKhoan/Edit/5
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TenTaiKhoan,MatKhau,Email,NgaySinh,GioiTinh,SoDienThoai,VaiTro,TrangThaiKhoa,ThoiGianTao,AnhDaiDienUrl")] TaiKhoan taiKhoan)
        {
            if (id != taiKhoan.TenTaiKhoan)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taiKhoan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaiKhoanExists(taiKhoan.TenTaiKhoan))
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
            return View(taiKhoan);
        }

        // GET: TaiKhoan/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taiKhoan = await _context.TaiKhoans
                .FirstOrDefaultAsync(m => m.TenTaiKhoan == id);
            if (taiKhoan == null)
            {
                return NotFound();
            }

            return View(taiKhoan);
        }

        // POST: TaiKhoan/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var taiKhoan = await _context.TaiKhoans.FindAsync(id);
            if (taiKhoan != null)
            {
                _context.TaiKhoans.Remove(taiKhoan);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaiKhoanExists(string id)
        {
            return _context.TaiKhoans.Any(e => e.TenTaiKhoan == id);
        }
        // GET: /TaiKhoan/DangKy
        public IActionResult DangKy()
        {
            return View();
        }

        // POST: /TaiKhoan/DangKy
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DangKy(TaiKhoan model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Kiểm tra tài khoản đã tồn tại
            var existing = await _context.TaiKhoans.FindAsync(model.TenTaiKhoan);
            if (existing != null)
            {
                ModelState.AddModelError("", "Tên tài khoản đã tồn tại.");
                return View(model);
            }

            model.VaiTro = "user"; // mặc định là useruser
            model.ThoiGianTao = DateTime.Now;
            model.TrangThaiKhoa = false;

            if (_context.TaiKhoans.Any(tk => tk.TenTaiKhoan == model.TenTaiKhoan))
            {
                ModelState.AddModelError("TenTaiKhoan", "Tên tài khoản đã tồn tại");
                return View(model);
            }

            if (_context.TaiKhoans.Any(tk => tk.Email == model.Email))
            {
                ModelState.AddModelError("Email", "Email đã được sử dụng");
                return View(model);
            }
            // Mã hóa mật khẩu
            model.MatKhau = MaHoaHelper.MaHoaSHA256(model.MatKhau);

            _context.TaiKhoans.Add(model);
            _context.SaveChanges();

            ViewBag.ThongBao = "Đăng ký thành công! Bạn có thể đăng nhập ngay.";

            return View(model);
        }

        public IActionResult DangNhapGoogle()
        {
            var redirectUrl = Url.Action("GoogleCallback", "TaiKhoan");
            var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        // Callback sau khi đăng nhập Google thành công
        public async Task<IActionResult> GoogleCallback()
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            
            if (!result.Succeeded)
            {
                ViewBag.Loi = "Đăng nhập Google thất bại.";
                return RedirectToAction("DangNhap");
            }

            var claims = result.Principal.Identities.FirstOrDefault()?.Claims;
            var email = claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var name = claims?.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            var googleId = claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(email))
            {
                ViewBag.Loi = "Không thể lấy thông tin email từ Google.";
                return RedirectToAction("DangNhap");
            }

            // Kiểm tra user đã tồn tại trong database chưa
            var existingUser = await _context.TaiKhoans.FirstOrDefaultAsync(x => x.Email == email);
            
            if (existingUser == null)
            {
                // Tạo tài khoản mới cho user Google
                var newUser = new TaiKhoan
                {
                    TenTaiKhoan = email, // Sử dụng email làm username
                    Email = email,
                    MatKhau = MaHoaHelper.MaHoaSHA256(email), // Sử dụng email làm password
                    NgaySinh = DateTime.Now.AddYears(-18), // Mặc định
                    GioiTinh = "Khác",
                    SoDienThoai = "",
                    VaiTro = "user",
                    TrangThaiKhoa = false,
                    ThoiGianTao = DateTime.Now,
                    AnhDaiDienUrl = claims?.FirstOrDefault(x => x.Type == "picture")?.Value
                };

                _context.TaiKhoans.Add(newUser);
                await _context.SaveChangesAsync();
                existingUser = newUser;
            }

            // Đăng nhập user
            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, existingUser.TenTaiKhoan),
                new Claim(ClaimTypes.Email, existingUser.Email),
                new Claim(ClaimTypes.Role, existingUser.VaiTro)
            };

            var identity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            // Lưu thông tin vào Session
            HttpContext.Session.SetString("UserName", existingUser.TenTaiKhoan);
            HttpContext.Session.SetString("VaiTro", existingUser.VaiTro);

            // Điều hướng
            if (existingUser.VaiTro.ToLower() == "admin")
            {
                return RedirectToAction("Dashboard", "Admin");
            }
            
            return RedirectToAction("Index", "Home");
        }

        // GET: /TaiKhoan/DangNhap
        public IActionResult DangNhap() => View();

        // POST: /TaiKhoan/DangNhap
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DangNhap(string tenTaiKhoan, string matKhau)
        {
            // Kiểm tra đầu vào
            if (string.IsNullOrEmpty(tenTaiKhoan) || string.IsNullOrEmpty(matKhau))
            {
                ViewBag.Loi = "Vui lòng nhập đầy đủ tên tài khoản và mật khẩu.";
                return View();
            }

            // Tìm user trong database
            var user = await _context.TaiKhoans.FindAsync(tenTaiKhoan);

            // Mã hóa mật khẩu người dùng nhập để so sánh
            string matKhauBam = MaHoaHelper.MaHoaSHA256(matKhau);

            // Kiểm tra thông tin đăng nhập
            if (user == null || user.MatKhau != matKhauBam || user.TrangThaiKhoa)
            {
                ViewBag.Loi = "Tài khoản không đúng hoặc đã bị khóa.";
                return View();
            }

            // Sử dụng cùng một scheme với Google authentication
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.TenTaiKhoan),
                new Claim(ClaimTypes.Email, user.Email ?? ""),
                new Claim(ClaimTypes.Role, user.VaiTro)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            // Sử dụng cùng scheme với cấu hình trong Program.cs
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            // Lưu thông tin vào Session
            HttpContext.Session.SetString("UserName", user.TenTaiKhoan);
            HttpContext.Session.SetString("VaiTro", user.VaiTro);
            
            // Điều hướng sau khi đăng nhập thành công
            if (user.VaiTro.ToLower() == "admin")
            {
                return RedirectToAction("Dashboard", "Admin");
            }
            
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> DangXuat()
        {
            // Sử dụng cùng scheme để đăng xuất
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("DangNhap");
        }

        public IActionResult ThongTinCaNhan()
        {
            var tenTaiKhoan = HttpContext.Session.GetString("UserName");

            if (string.IsNullOrEmpty(tenTaiKhoan)) return RedirectToAction("DangNhap");

            var tk = _context.TaiKhoans.FirstOrDefault(t => t.TenTaiKhoan == tenTaiKhoan);
            if (tk == null) return NotFound();

            var viewModel = new ThongTinCaNhanViewModel
            {
                TenTaiKhoan = tk.TenTaiKhoan,
                Email = tk.Email,
                NgaySinh = tk.NgaySinh,
                GioiTinh = tk.GioiTinh,
                SoDienThoai = tk.SoDienThoai
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult ThongTinCaNhan(ThongTinCaNhanViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var tk = _context.TaiKhoans.FirstOrDefault(t => t.TenTaiKhoan == model.TenTaiKhoan);
            if (tk == null) return NotFound();

            tk.Email = model.Email;
            tk.NgaySinh = model.NgaySinh;
            tk.GioiTinh = model.GioiTinh;
            tk.SoDienThoai = model.SoDienThoai;

            // Mã hóa mật khẩu người dùng nhập mới
            if (!string.IsNullOrEmpty(model.MatKhauMoi))
            {
                tk.MatKhau = MaHoaHelper.MaHoaSHA256(model.MatKhauMoi);
            }

            _context.SaveChanges();

            ViewBag.ThongBao = "Cập nhật thành công!";
            return View(model);
        }

        // GET: /TaiKhoan/QuenMatKhau
        public IActionResult QuenMatKhau()
        {
            return View();
        }

        // POST: /TaiKhoan/QuenMatKhau
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> QuenMatKhau(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ViewBag.Loi = "Vui lòng nhập email.";
                return View();
            }
            var user = await _context.TaiKhoans.FirstOrDefaultAsync(tk => tk.Email == email);
            if (user == null)
            {
                ViewBag.Loi = "Không tìm thấy tài khoản với email này.";
                return View();
            }
            // Tạo mật khẩu mới ngẫu nhiên
            var newPassword = GenerateRandomPassword(8);
            user.MatKhau = MaHoaHelper.MaHoaSHA256(newPassword);
            await _context.SaveChangesAsync();

            // Gửi email mật khẩu mới
            try
            {
                await EmailHelper.SendEmailAsync(email, "Cấp lại mật khẩu ThiTracNghiem", $"Mật khẩu mới của bạn là: <b>{newPassword}</b>");
                ViewBag.ThongBao = "Mật khẩu mới đã được gửi tới email của bạn.";
            }
            catch(Exception ex)
            {
                // Xử lý lỗi gửi email
                ViewBag.Loi = "Không gửi được email. Vui lòng thử lại sau." + ex.Message;
            }
            return View();
        }

        private string GenerateRandomPassword(int length)
        {
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnpqrstuvwxyz23456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
    public static class MaHoaHelper
    {
        public static string MaHoaSHA256(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                var builder = new StringBuilder();
                foreach (var b in bytes)
                    builder.Append(b.ToString("x2"));

                return builder.ToString();
            }
        }
    }
    
}
