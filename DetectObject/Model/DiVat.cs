using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectObject.Model
{
    public class DiVat
    {
        public int Loi { get; set; }
        public DateTime ThoiGianLoi { get; set; }
        public double ViTriLoi { get; set; }
        public string Cuon { get; set; }

        public string ImagePath { get; set; }
    }
}
