using DetectObject.Model;
using DetectObject.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DetectObject
{
    public partial class frmHienThiAnhLoi : Form
    {
        DiVat _DiVat;
        public frmHienThiAnhLoi(DiVat diVat)
        {
            this._DiVat = diVat;
            InitializeComponent();
        }

        private void frmHienThiAnhLoi_Shown(object sender, EventArgs e)
        {
            lbCuon.Text = _DiVat.Cuon;
            lbThoiGian.Text = _DiVat.ThoiGianLoi.ToString();
            lbViTri.Text = _DiVat.ViTriLoi + " m";
            pictureBox1.Image = Image.FromFile(_DiVat.ImagePath);
        }
    }
}
