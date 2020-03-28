using DetectObject.DataService;
using DetectObject.Model;
using DetectObject.Utils;
using Emgu.CV;
using Emgu.CV.Structure;
using GeneralDef;
using log4net;
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
        private static readonly ILog log = LogManager.GetLogger(typeof(frmMain));
        public List<DiVat> DSDiVat;
        public BindingSource bsDiVat = new BindingSource();
        bool IsScan = false;
        VideoCapture videoCapture = null;
        double ViTriLoiMoiNhat = 0;
        PictureBox pictureBoxCamera;
        Rectangle _ROI;
        public frmMain()
        {
            InitializeComponent();
            DSDiVat = new List<DiVat>();
            BindingData();
            CommonFunc.SetSavePath();
            #region ROI
            _ROI = new Rectangle();
            var roi = ConfigurationManager.AppSettings[Constant.ROI].Split(',');
            _ROI.X = Convert.ToInt32(roi[0]);
            _ROI.Y = Convert.ToInt32(roi[1]);
            _ROI.Width = Convert.ToInt32(roi[2]);
            _ROI.Height = Convert.ToInt32(roi[3]);
            #endregion
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            #region Setup camera
            pictureBoxCamera = new PictureBox();
            pictureBoxCamera.Name = "camera";
            pictureBoxCamera.Dock = DockStyle.Fill;
            pictureBoxCamera.SizeMode = PictureBoxSizeMode.StretchImage;
            tlpCamera.Controls.Add(pictureBoxCamera);
            AnHienTimKiem(true);
            #endregion
        }

        private void ScanObject()
        {
            Action ResetUI = new Action(() => { /*bsDiVat.ResetBindings(true);*/ DemLoi(); });
            try
            {
                Mat m = new Mat();
                videoCapture = new VideoCapture(ConfigurationManager.AppSettings[Constant.Camera], VideoCapture.API.Ffmpeg);
                if (videoCapture.IsOpened)
                {
                    while (true)
                    {
                        videoCapture.Read(m);
                        if (!m.IsEmpty)
                        {
                            var img = m.ToImage<Bgr, byte>();
                            img.ROI = _ROI;
                            Image<Bgr, byte> inputImage = img.CopyBlank();
                            img.CopyTo(inputImage);
                            pictureBoxCamera.Image = inputImage.Bitmap;
                            if (IsScan)
                            {
                                Scan(inputImage, ResetUI);
                            }
                            else
                            {
                                img.Dispose();
                            }
                            Task.Delay(200);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Loi o chuc nang ScanObject", ex);
            }
            finally
            {
                StartCapture();
            }
        }

        private void Scan(Image<Bgr, byte> inputImage, Action ResetUI)
        {
            Task.Run(() => 
            {
                int heightOfObject;
                string savedImagePath;
                if (Detecter.DetectObject(inputImage, out heightOfObject, out savedImagePath))
                {
                    var thoiDiemLoi = DateTime.Now;
                    var viTriLoiHienTai = (thoiDiemLoi - Utilities.ThoiDiemBatDauCuonMoi).TotalSeconds * Utilities.VanToc;
                    if (viTriLoiHienTai - ViTriLoiMoiNhat <= Utilities.DoCao1KhungHinhThucTe)
                    {
                        ViTriLoiMoiNhat = viTriLoiHienTai;
                        var diVat = new DiVat() { Loi = DSDiVat.Count + 1, ThoiGianLoi = thoiDiemLoi, ViTriLoi = viTriLoiHienTai, TenCuon = Utilities.TenCuon, ImagePath = savedImagePath };
                        DSDiVat.Add(diVat);
                        var savedFilePath = LocalSetting.m_strDataPath + Utilities.ThuMucLuuLoi + "\\" + Utilities.TenCuon + ".txt";
                        string content = JsonConvert.SerializeObject(diVat);
                        if (File.Exists(savedFilePath))
                        {
                            content = "," + content;
                        }
                        
                        //var diVatService = new DiVatService();
                        //diVatService.LuuDiVat(diVat);

                        this.Invoke(ResetUI);
                        SaveContent(savedFilePath, content);
                    }
                }
                inputImage.Dispose();
            });
        }

        private void SaveContent(string savedFilePath, string content)
        {
            try
            {
                File.AppendAllText(savedFilePath, content);
            }
            catch(Exception ex)
            {
                log.Error("Loi luu noi dung", ex);
                Task.Delay(1500);
                SaveContent(savedFilePath, content);
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
            btnGopLoi.Enabled = true;
            AnHienTimKiem(true);
        }

        private void btnSangCuon_Click(object sender, EventArgs e)
        {
            if (CommonFunc.IsNumber(txtVanToc.Text))
            {
                IsScan = true;
                ViTriLoiMoiNhat = 0;
                btnSangCuon.Enabled = false;
                btnStop.Enabled = true;
                btnGopLoi.Enabled = false;
                Utilities.VanToc = Convert.ToDouble(txtVanToc.Text);
                Utilities.ThoiDiemBatDauCuonMoi = DateTime.Now;
                Utilities.TenCuon = LayTenCuonHienTai();
                Utilities.ThuMucLuuLoi = TaoThuMucLuuLoi();
                DSDiVat = new List<DiVat>();
                DemLoi();
                pictureBox1.Image = null;
                BindingData();
                AnHienTimKiem(false);
            }
            else
            {
                MessageBox.Show("Trường vận tốc không đúng định dạng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string LayTenCuonHienTai()
        {
            int cuon = 1;
            if (File.Exists(LocalSetting.m_strDataPath + "TenCuon.txt"))
            {
                var name = File.ReadLines(LocalSetting.m_strDataPath + "TenCuon.txt").ToList();
                cuon = Convert.ToInt32(name[0]) + 1;
            }
            File.WriteAllText(LocalSetting.m_strDataPath + "TenCuon.txt", cuon.ToString());

            return "C" + cuon;
        }

        private string TaoThuMucLuuLoi()
        {
            string tenThucMuc = DateTime.Now.ToString("MMddyyyy");
            if (!Directory.Exists(LocalSetting.m_strDataPath + tenThucMuc))
            {
                Directory.CreateDirectory(LocalSetting.m_strDataPath + tenThucMuc);
            }

            return tenThucMuc;
        }

        private void AnHienTimKiem(bool hienThi)
        {
            lbNgay.Visible = hienThi;
            lbCuon.Visible = hienThi;
            dtPickerNgayTimKiem.Visible = hienThi;
            cbCuon.Visible = hienThi;
            btnTimKiem.Visible = hienThi;
            if (hienThi)
            {
                TimKiem();
            }
        }

        //private void TaoVideo()
        //{
        //    CameraHelper.StopRecord(Constant.Camera);
        //    CameraHelper.StopRealPlay(Constant.Camera);
        //    CameraHelper.AddCamera(tlpCamera, Constant.Camera, new Point(0, 0));
        //    CameraHelper.StartRealPlay(Constant.Camera);
        //    CameraHelper.StartRecord(Constant.Camera);
        //}

        private void timer1_Tick(object sender, EventArgs e)
        {
            //TaoVideo();
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
            var diVat = DSDiVat.FirstOrDefault(d => d.Loi == Convert.ToInt32(dgvDSLoi.CurrentRow.Cells["Loi"].Value) && d.TenCuon == (string)dgvDSLoi.CurrentRow.Cells["TenCuon"].Value);
            pictureBox1.Image = System.Drawing.Image.FromFile(diVat.ImagePath);
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
                dsChonDiVat.Add(DSDiVat.FirstOrDefault(d => d.Loi == (int)row.Cells["Loi"].Value && d.TenCuon == (string)row.Cells["TenCuon"].Value));
            }

            for (int i= 0; i < dsChonDiVat.Count; i++)
            {
                if (i == 0)
                    continue;
                if (dsChonDiVat[i].TenCuon != dsChonDiVat[i - 1].TenCuon)
                {
                    MessageBox.Show("Không thể gộp ảnh từ cuộn khác nhau", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            var frmGopAnh = new frmGopAnhLoi(this, dsChonDiVat);
            frmGopAnh.Show(this);
        }

        private void btnBatdau_Click(object sender, EventArgs e)
        {
            btnSangCuon.Enabled = true;
            btnBatdau.Enabled = false;
            #region Scan Object
            StartCapture();
            #endregion
        }

        private void StartCapture()
        {
            Thread thread = new Thread(new ThreadStart(ScanObject));
            thread.IsBackground = true;
            thread.Start();
        }

        private void BindingData()
        {
            bsDiVat.DataSource = DSDiVat;
            dgvDSLoi.DataSource = bsDiVat;
        }

        private void dtPickerNgayTimKiem_ValueChanged(object sender, EventArgs e)
        {
            TimKiem();
        }

        private void TimKiem()
        {
            var selectedDate = LocalSetting.m_strDataPath + dtPickerNgayTimKiem.Value.ToString("MMddyyyy");
            var directories = Directory.GetDirectories(LocalSetting.m_strDataPath);
            var dsCuon = new List<string>();
            if (directories != null)
            {
                for (int i = 0; i < directories.Length; i++)
                {
                    if (directories[i] == selectedDate)
                    {
                        var files = Directory.GetFiles(directories[i]);
                        for (int j = 0; j < files.Length; j++)
                        {
                            dsCuon.Add(Path.GetFileName(files[j]).Replace(".txt", ""));
                        }
                        break;
                    }
                }
            }

            cbCuon.DataSource = dsCuon;
            cbCuon.Update();
            cbCuon.ResetText();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            var selectedDate = LocalSetting.m_strDataPath + dtPickerNgayTimKiem.Value.ToString("MMddyyyy");
            var directories = Directory.GetDirectories(LocalSetting.m_strDataPath);
            bool isFound = false;
            DSDiVat = new List<DiVat>();
            if (directories != null)
            {
                for (int i = 0; i < directories.Length; i++)
                {
                    if (directories[i] == selectedDate)
                    {
                        var files = Directory.GetFiles(directories[i]);
                        for (int j = 0; j < files.Length; j++)
                        {
                            if (string.IsNullOrWhiteSpace(cbCuon.Text))
                            {
                                var json = "[" + File.ReadAllText(files[j]) + "]";
                                DSDiVat.AddRange(JsonConvert.DeserializeObject<List<DiVat>>(json, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));
                                isFound = true;
                            }
                            else
                            {
                                if (Path.GetFileName(files[j].Replace(".txt", "")) == cbCuon.Text)
                                {
                                    Utilities.TenCuon = cbCuon.Text;
                                    Utilities.ThuMucLuuLoi = dtPickerNgayTimKiem.Value.ToString("MMddyyyy");
                                    var json = "[" + File.ReadAllText(files[j]) + "]";
                                    if (!string.IsNullOrWhiteSpace(json))
                                    {
                                        DSDiVat = JsonConvert.DeserializeObject<List<DiVat>>(json, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                                        BindingData();
                                    }
                                    return;
                                }
                            }
                        }
                    }
                }
            }

            BindingData();
            if (!isFound)
            {
                MessageBox.Show("Không tìm thấy kết quả nào.", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnTai_Click(object sender, EventArgs e)
        {
            bsDiVat.ResetBindings(false);
        }
    }
}
