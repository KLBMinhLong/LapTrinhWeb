namespace LapTrinhWeb.Models
{
    public class DapAn
    {
        public int Id { get; set; }
        public int CauHoiId { get; set; }
        public string MaDapAn { get; set; } // "A", "B", "C", "D"
        public string NoiDung { get; set; } // Nội dung đáp án
    }
}
