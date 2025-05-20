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

        public IActionResult Exam()
        {
            return View();
        }

        public IActionResult Results()
        {
            return View();
        }
    }
}
