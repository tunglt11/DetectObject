using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectObject.Utils
{
    public static class Utilities
    {
        public static DateTime ThoiDiemBatDauCuonMoi = DateTime.MinValue;
        public static DateTime ThoiDiemTamDung = DateTime.MinValue;
        public static string TenCuon = string.Empty;
        public static double VanToc = 0;// đơn vị: m/s
        public static double DoCao1KhungHinhThucTe = 0.6; // đơn vị: m (theo cach tinh thuc te la: 1.36, thu don vi nho hon de lay nhieu hinh loi hon)
        public static string ThuMucLuuLoi = string.Empty;
        public static System.Drawing.Size Size = new System.Drawing.Size(0, 0);
    }

    public class ThamSo
    { 
        public string TenCuon { get; set; }
        public DateTime ThoiGianBatDauCuon { get; set; }
        public DateTime ThoiGianBatDauLuuVideo { get; set; }
    }
}
