using System;
using System.Collections.Generic;

namespace LapTrinhWeb.Models
{
    public class KetQuaThi
    {
        public int Id { get; set; }
        public int DeThiId { get; set; }
        public string NguoiDungId { get; set; } // Nếu có đăng nhập
        public DateTime ThoiGianNop { get; set; }
        public int SoCauDung { get; set; }
        public int SoCauSai { get; set; }
        public double DiemSo { get; set; }
        public List<TraLoiNguoiDung> CacTraLoi { get; set; }
    }
}
