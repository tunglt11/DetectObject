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
    public partial class frmGopAnhLoi : Form
    {
        List<DiVat> _DSDiVat;
        frmMain _frmMain;
        public frmGopAnhLoi(frmMain frmMain, List<DiVat> dsDiVat)
        {
            InitializeComponent();
            _DSDiVat = dsDiVat;
            _frmMain = frmMain;
        }

        private void frmGopAnhLoi_Load(object sender, EventArgs e)
        {
            _DSDiVat.Reverse();
            foreach (var diVat in _DSDiVat)
            {
                var pictureBox = new PictureBox();
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox.Dock = DockStyle.Fill;
                pictureBox.Image = CommonFunc.ConvertByteToImage(diVat.Image);
                pictureBox.Name = diVat.Loi.ToString();
                pictureBox.Click += PictureBox_Click;
                tableLayoutPanel1.Controls.Add(pictureBox);
            }
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            var pictureBox = sender as PictureBox;
            var dsDiVatCanXoa = _DSDiVat.Where(d => d.Loi != Convert.ToInt32(pictureBox.Name)).ToList();
            foreach (var diVat in dsDiVatCanXoa)
            {
                _frmMain.DSDiVat.Remove(diVat);
            }

            for (int i = 0; i < _frmMain.DSDiVat.Count; i++)
            {
                _frmMain.DSDiVat[i].Loi = i + 1;
            }
            _frmMain.bsDiVat.ResetBindings(false);
            _frmMain.DemLoi();
            this.Close();
        }
    }
}
