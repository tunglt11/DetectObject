using DetectObject.Model;
using DetectObject.Utils;
using Emgu.CV;
using Emgu.CV.Structure;
using GeneralDef;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DetectObject
{
    public partial class frmMain : Form
    {
        public CameraHelper CameraHelper;
        //List<PlayPanel> PlayPanels = new List<PlayPanel>();
        //List<DeviceInfo> DeviceInfos = new List<DeviceInfo>();
        public List<DiVat> DSDiVat;
        public BindingSource bsDiVat = new BindingSource();
        bool IsScan = false;
        VideoCapture videoCapture = null;
        double ViTriLoiMoiNhat = 0;
        PictureBox pictureBoxCamera;
        public frmMain()
        {
            InitializeComponent();
            //CameraHelper = new CameraHelper(PlayPanels, DeviceInfos);
            BindingData();
            CommonFunc.SetSavePath();
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            #region Setup camera
            //Parallel.Invoke(() => { CameraHelper.SetupCamera(Constant.Camera); });
            //if (!CameraHelper.IsSetupBefore())
            //{
            //    //Chơ setup camera
            //    Thread.Sleep(2000);
            //}
            //if (CameraHelper.FindDeviceInfo(Constant.Camera) != null)
            //{
            //    CameraHelper.AddCamera(tlpCamera, Constant.Camera, new Point(0, 0));
            //    CameraHelper.StartRealPlay(Constant.Camera);
            //}
            //else
            //{
            //    MessageBox.Show("Không thể kết nối camera.", "Lỗi kết nối camera", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            pictureBoxCamera = new PictureBox();
            pictureBoxCamera.Name = "camera";
            pictureBoxCamera.Dock = DockStyle.Fill;
            pictureBoxCamera.SizeMode = PictureBoxSizeMode.StretchImage;
            tlpCamera.Controls.Add(pictureBoxCamera);
            #endregion
        }

        private async void ScanObject()
        {
            Action ResetUI = new Action(() => { bsDiVat.ResetBindings(false); DemLoi(); });
            Detecter detecter = new Detecter();
            try
            {
                Mat m = new Mat();
                videoCapture = new VideoCapture(ConfigurationManager.AppSettings[Constant.Camera]);
                if (videoCapture != null)
                {
                    while (true)
                    {
                        videoCapture.Read(m);
                        var inputImage = m.ToImage<Bgr, byte>();
                        pictureBoxCamera.Image = inputImage.Bitmap;
                        if (IsScan)
                        {
                            int heightOfObject;
                            if (detecter.DetectObject(inputImage, out heightOfObject))
                            {
                                var thoiDiemLoi = DateTime.Now;
                                var viTriDiVatTrenAnh = Utilities.DoCao1KhungHinhThucTe * heightOfObject / inputImage.Height;
                                var viTriLoi = (thoiDiemLoi - Utilities.ThoiDiemBatDauCuonMoi).TotalSeconds * Utilities.VanToc + viTriDiVatTrenAnh;
                                if (viTriLoi - ViTriLoiMoiNhat >= Utilities.DoCao1KhungHinhThucTe)
                                {
                                    DSDiVat.Add(new DiVat() { Loi = DSDiVat.Count + 1, ThoiGianLoi = thoiDiemLoi, ViTriLoi = viTriLoi, Cuon = Utilities.TenCuon, Image = CommonFunc.ResizeImage(CommonFunc.ConvertImageToByte(inputImage.Bitmap), 360) });
                                    this.Invoke(ResetUI);
                                    pictureBox1.Image = inputImage.Bitmap;
                                }
                            }
                        }
                        else
                        {
                            Thread.Sleep(500);
                        }
                        inputImage.Dispose();
                        await Task.Delay(200);
                    }
                }
                else
                {
                    MessageBox.Show("Không thể kết nối camera.", "Lỗi kết nối camera", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //CameraHelper.StopRealPlay(Constant.Camera);
            //CameraHelper.StopRecord(Constant.Camera);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            IsScan = false;
            btnSangCuon.Enabled = true;
            btnStop.Enabled = false;
            var json = JsonConvert.SerializeObject(DSDiVat);
            File.WriteAllText(LocalSetting.m_strDataPath + CommonFunc.ConvertDateTimeToInvariantInfo(DateTime.Now) + ".txt", json, Encoding.UTF8);
            //videoCapture.Dispose();
            //videoCapture = null;
            //CameraHelper.StopRecord(Constant.Camera);
            //timer1.Enabled = false;
            //timer1.Stop();
        }

        private void btnSangCuon_Click(object sender, EventArgs e)
        {
            if (CommonFunc.IsNumber(txtVanToc.Text))
            {
                IsScan = true;
                ViTriLoiMoiNhat = 0;
                btnSangCuon.Enabled = false;
                btnStop.Enabled = true;
                Utilities.VanToc = Convert.ToDouble(txtVanToc.Text);
                Utilities.ThoiDiemBatDauCuonMoi = DateTime.Now;
                Utilities.TenCuon = "A".ToString();
                BindingData();
                //timer1.Enabled = true;
                //timer1.Start();
                //TaoVideo();
            }
            else
            {
                MessageBox.Show("Trường vận tốc không đúng định dạng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TaoVideo()
        {
            CameraHelper.StopRecord(Constant.Camera);
            CameraHelper.StopRealPlay(Constant.Camera);
            CameraHelper.AddCamera(tlpCamera, Constant.Camera, new Point(0, 0));
            CameraHelper.StartRealPlay(Constant.Camera);
            CameraHelper.StartRecord(Constant.Camera);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TaoVideo();
        }

        private void btnXemAnh_Click(object sender, EventArgs e)
        {
            if (dgvDSLoi.SelectedRows.Count > 1)
            {
                MessageBox.Show("Vui lòng chỉ chọn 1 lỗi để xem ảnh.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                return;
            }

            if (dgvDSLoi.CurrentRow.Selected)
            {
                var diVat = DSDiVat.FirstOrDefault(d => d.Loi == Convert.ToInt32(dgvDSLoi.CurrentRow.Cells["Loi"].Value));
                var frmHienThiAnh = new frmHienThiAnhLoi(diVat);
                frmHienThiAnh.Show(this);
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn lỗi nào để xem ảnh.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DemLoi()
        {
            lbDiVat.Text = DSDiVat.Count.ToString();
        }

        private void dgvDSLoi_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var diVat = DSDiVat.FirstOrDefault(d => d.Loi == Convert.ToInt32(dgvDSLoi.CurrentRow.Cells["Loi"].Value));
            var frmHienThiAnh = new frmHienThiAnhLoi(diVat);
            frmHienThiAnh.Show(this);
        }

        private void dgvDSLoi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            int index = e.RowIndex;
            dgvDSLoi.Rows[index].Selected = true;
        }

        private void btnGopLoi_Click(object sender, EventArgs e)
        {
            if (btnStop.Enabled == true)
            {
                MessageBox.Show("Không thể gộp lỗi khi chương trình đang chạy.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dgvDSLoi.SelectedRows.Count <= 1)
            {
                MessageBox.Show("Bạn phải chọn ít nhất 2 lỗi để gộp ảnh.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dgvDSLoi.SelectedRows.Count > 6)
            {
                MessageBox.Show("Bạn không thể chọn hơn 6 lỗi.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var dsChonDiVat = new List<DiVat>();
            int maxIndex = dgvDSLoi.SelectedRows[0].Index;
            foreach (DataGridViewRow row in dgvDSLoi.SelectedRows)
            {
                if (row.Index != maxIndex)
                {
                    MessageBox.Show("Vui lòng chọn các lỗi liên tiếp nhau.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                maxIndex -= 1;
                dsChonDiVat.Add(DSDiVat.FirstOrDefault(d => d.Loi == (int)row.Cells["Loi"].Value));
            }

            var frmGopAnh = new frmGopAnhLoi(this, dsChonDiVat);
            frmGopAnh.Show(this);
        }

        private void btnBatdau_Click(object sender, EventArgs e)
        {
            btnSangCuon.Enabled = true;
            btnBatdau.Enabled = false;
            #region Scan Object
            Thread thread = new Thread(new ThreadStart(ScanObject));
            thread.IsBackground = true;
            thread.Start();
            #endregion
        }

        private void BindingData()
        {
            DSDiVat = new List<DiVat>();
            bsDiVat.DataSource = DSDiVat;
            dgvDSLoi.DataSource = bsDiVat;
        }
    }
}
