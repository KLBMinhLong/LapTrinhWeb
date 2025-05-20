namespace LapTrinhWeb.Models
{
    public class TraLoiNguoiDung
    {
        public int Id { get; set; }
        public int KetQuaThiId { get; set; }
        public int CauHoiId { get; set; }
        public string DapAnChon { get; set; } // "A", "B", "C", "D"
        public bool DungHaySai { get; set; }
    }
}
