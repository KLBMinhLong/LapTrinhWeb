using System;
using System.Collections.Generic;

namespace LapTrinhWeb.Models
{
    public class DeThi
    {
        public int Id { get; set; }
        public string TenDe { get; set; }
        public string MoTa { get; set; }
        public int ChuDeId { get; set; }
        public ChuDe ChuDe { get; set; }
        public int ThoiGianPhut { get; set; }
        public int SoCauHoi { get; set; }
        public string DoKho { get; set; }
        public DateTime NgayTao { get; set; }
        public List<CauHoi> CacCauHoi { get; set; }
    }
}
