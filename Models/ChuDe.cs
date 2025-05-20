using System;
using System.Collections.Generic;

namespace LapTrinhWeb.Models
{
    public class ChuDe
    {
        public int Id { get; set; }
        public string TenChuDe { get; set; } // Ví dụ: Toán học, Tiếng Anh
        public string MoTa { get; set; }
        public List<DeThi> DeThis { get; set; }
    }
}
