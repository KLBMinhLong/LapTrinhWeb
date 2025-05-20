using System.Collections.Generic;

namespace LapTrinhWeb.Models
{
    public class CauHoi
    {
        public int Id { get; set; }
        public int DeThiId { get; set; }
        public string NoiDung { get; set; } // Nội dung câu hỏi
        public string? HinhAnh { get; set; } // Đường dẫn ảnh minh họa (nếu có)
        public List<DapAn> DapAns { get; set; }
        public string DapAnDung { get; set; } // "A", "B", "C", "D"
        public string? GiaiThich { get; set; } // Giải thích đáp án
    }
}
