using DetectObject.Model;
using DetectObject.Utils;
using Emgu.CV;
using Emgu.CV.Structure;
using GeneralDef;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        List<PlayPanel> PlayPanels = new List<PlayPanel>();
        List<DeviceInfo> DeviceInfos = new List<DeviceInfo>();
        public List<DiVat> DSDiVat;
        public BindingSource bsDiVat = new BindingSource();
        bool IsScan = false;
        public frmMain()
        {
            InitializeComponent();
            CameraHelper = new CameraHelper(PlayPanels, DeviceInfos);
            DSDiVat = new List<DiVat>();
            #region Scan Object
            Thread thread = new Thread(new ThreadStart(ScanObject));
            thread.IsBackground = true;
            thread.Start();
            #endregion
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
            #endregion

            bsDiVat.DataSource = DSDiVat;
            dgvDSLoi.DataSource = bsDiVat;
        }

        private async void ScanObject()
        {
            while (true)
            {
                string[] fileArray = Directory.GetFiles(LocalSetting.m_strRecordSavePath, "*.mp4");
                if (fileArray.Length == 0 || !IsScan)
                {
                    Thread.Sleep(7000);
                }
                else
                {
                    Detecter detecter = new Detecter();
                    for (int i = 0; i < fileArray.Length; i++)
                    {
                        var capture = new VideoCapture(fileArray[i]);
                        try
                        {
                            var thamSo = CommonFunc.GetThamSoTuFileName(fileArray[i]);
                            var totalFrames = capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameCount);
                            double currentFrame = 0;
                            double fps = capture.GetCaptureProperty(Emgu.CV.CvEnum.CapProp.Fps);
                            double nextFrames = fps / 2;
                            double OneFPS = 1000 / fps;
                            while (currentFrame <= totalFrames)
                            {
                                capture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.PosFrames, currentFrame);
                                if (currentFrame > totalFrames)
                                {
                                    currentFrame = totalFrames;
                                }
                                else
                                {
                                    currentFrame += nextFrames;
                                }

                                Mat m = new Mat();
                                capture.Read(m);
                                if (!m.IsEmpty)
                                {
                                    var inputImage = m.ToImage<Bgr, byte>();
                                    if (detecter.DetectObject(inputImage))
                                    {
                                        var thoiDiemLoi = thamSo.ThoiGianBatDauLuuVideo.AddSeconds(currentFrame * OneFPS / 1000);
                                        var viTriLoi = (thoiDiemLoi - thamSo.ThoiGianBatDauCuon).TotalSeconds * Utilities.VanToc;
                                        DSDiVat.Add(new DiVat() { Loi = DSDiVat.Count + 1, ThoiGianLoi = thoiDiemLoi, ViTriLoi = viTriLoi, Cuon = thamSo.TenCuon, Image = CommonFunc.ConvertImageToByte(inputImage.Bitmap) });
                                        this.Invoke(new Action(() => { bsDiVat.ResetBindings(false); DemLoi(); }));
                                        pictureBox1.Image = inputImage.Bitmap;
                                    }
                                    else
                                    {
                                        inputImage.Dispose();
                                    }

                                    await Task.Delay(100);
                                }
                                else
                                {
                                    break;
                                }
                            }

                            capture.Dispose();
                            //chuyển những file video đã được scan sang thư mục scanned
                            string fileName = Path.GetFileName(fileArray[i]);
                            File.Copy(fileArray[i], LocalSetting.m_strScannedPath + fileName);
                            File.Delete(fileArray[i]);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            CameraHelper.StopRealPlay(Constant.Camera);
            CameraHelper.StopRecord(Constant.Camera);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            IsScan = false;
            CameraHelper.StopRecord(Constant.Camera);
            timer1.Enabled = false;
            timer1.Stop();
        }

        private void btnSangCuon_Click(object sender, EventArgs e)
        {
            if (CommonFunc.IsNumber(txtVanToc.Text))
            {
                IsScan = true;
                Utilities.VanToc = Convert.ToDouble(txtVanToc.Text);
                //Utilities.ThoiDiemBatDauCuonMoi = DateTime.Now;
                //Utilities.TenCuon = (TenCuon + 1).ToString();
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
    }
}
