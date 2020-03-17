using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectObject.Utils
{
    public static class CommonFunc
    {
        public static string ConvertDateTimeToInvariantInfo(DateTime date)
        {
            return date.ToString("yyMMddHHmmss", DateTimeFormatInfo.InvariantInfo);
        }

        public static DateTime ConvertInvariantInfoToDateTime(string date)
        {
            return DateTime.ParseExact(date, "yyMMddHHmmss", DateTimeFormatInfo.InvariantInfo);
        }

        public static byte[] ImageToByte(Bitmap img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        public static Bitmap ByteToImage(byte[] bytes)
        {
           return (Bitmap)((new ImageConverter()).ConvertFrom(bytes));
        }

        public static string FormatFileNameVideo()
        {
            return string.Format("{0}_{1}_{2}", Utilities.TenCuon, ConvertDateTimeToInvariantInfo(Utilities.ThoiDiemBatDauCuonMoi), ConvertDateTimeToInvariantInfo(DateTime.Now));
        }

        public static bool IsNumber(string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                try
                {
                    decimal.Parse(value);
                    return true;
                }
                catch { }
            }

            return false;
        }

        public static string FormatNumber(this decimal number)
        {
            return number.ToString("0,0.##");
        }

        public static string FormatNumber(this double number)
        {
            return number.ToString("0,0.##");
        }

        public static ThamSo GetThamSoTuFileName(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                fileName = Path.GetFileName(fileName).Replace(".mp4", "");
                string[] names = fileName.Split('_');
                if (names.Length == 3)
                {
                    return new ThamSo()
                    {
                        TenCuon = names[0],
                        ThoiGianBatDauCuon = ConvertInvariantInfoToDateTime(names[1]),
                        ThoiGianBatDauLuuVideo = ConvertInvariantInfoToDateTime(names[2])
                    };
                }
            }

            return null;
        }
    }
}
