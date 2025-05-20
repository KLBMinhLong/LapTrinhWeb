using System.Collections.Generic;

namespace LapTrinhWeb.Models.ViewModels
{
    public class LamBaiViewModel
    {
        public DeThi DeThi { get; set; }
        public int ChiSoCauHoiHienTai { get; set; }
        public List<TraLoiNguoiDung> CacTraLoiNguoiDung { get; set; }
        public int ThoiGianConLaiGiay { get; set; }
    }
}
