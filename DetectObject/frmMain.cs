using DetectObject.DataService;
using DetectObject.Model;
using DetectObject.Utils;
using Emgu.CV;
using Emgu.CV.Structure;
using GeneralDef;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
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
        Rectangle _ROI;
        
        // ket noi chuong den
        SerialPort serialPort;

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
            #region Toc do
            string vanToc = ConfigurationManager.AppSettings[Constant.VanToc];
            string[] size = ConfigurationManager.AppSettings[Constant.DienTichVet].Split(',');
            lbVanToc.Text = vanToc + " mét/phút";
            Utilities.VanToc = Convert.ToDouble(vanToc) / 60;
            Utilities.DoCao1KhungHinhThucTe = Convert.ToDouble(ConfigurationManager.AppSettings[Constant.ChieuCaoKhungHinh]);
            Utilities.Size = new Size(Convert.ToInt32(size[0]), Convert.ToInt32(size[1]));
            #endregion

            //gioi han kich thuoc di vat phat hien
            Utilities.LIMIT_AREA = double.Parse(ConfigurationManager.AppSettings["LIMIT_AREA"]);
            log.Info("LIMIT_AREA: " + Utilities.LIMIT_AREA);

            //quy uoc gioi han dut giay
            Utilities.MAX_CONTOURS = int.Parse(ConfigurationManager.AppSettings["MAX_CONTOURS"]);            
            log.Info("MAX_CONTOURS: " + Utilities.MAX_CONTOURS);

            // mo cong COM cho chuong den
            try
            {
                string comName = ConfigurationManager.AppSettings.Get("COM");
                serialPort = new SerialPort(comName, 9600, Parity.None, 8);
                serialPort.Open();
                log.Info(comName + " is opened...");
            }catch(Exception ex)
            {
                log.Error(ex.Message);
            }            
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {
            StartCapture();
            AnHienTimKiem(true);
        }

        private void ScanObject()
        {
            Action ResetUI = new Action(() => { /*bsDiVat.ResetBindings(true);*/ DemLoi(); });
            try
            {
                HienThiTrangThai("Đang kết nối đến camera");
                log.Info("Dang ket noi camera");
                Mat m = new Mat();
                videoCapture = new VideoCapture(ConfigurationManager.AppSettings[Constant.Camera], VideoCapture.API.Ffmpeg);
                if (videoCapture.IsOpened)
                {
                    log.Info("Ket noi den camera thanh cong.");
                    HienThiTrangThai("Kết nối đến camera thành công");
                    LockButton(btnQuet, true);
                    while (true)
                    {
                        videoCapture.Read(m);
                        if (!m.IsEmpty)
                        {
                            if (IsScan)
                            {
                                var img = m.ToImage<Bgr, byte>();
                                img.ROI = _ROI;
                                Image<Bgr, byte> inputImage = img.CopyBlank();
                                img.CopyTo(inputImage);
                                Scan(inputImage, ResetUI);
                            }

                            Task.Delay(200);
                        }
                    }
                }
                else
                {
                    log.Info("Khong the ket noi den camera");
                }
            }
            catch (Exception ex)
            {
                HienThiTrangThai("Lỗi kết nối đến camera");
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
                    if (viTriLoiHienTai - ViTriLoiMoiNhat >= Utilities.DoCao1KhungHinhThucTe)
                    {
                        //thong bao chuong den
                        new Thread(new ThreadStart(ThongBaoChuongDen)).Start();
                        ViTriLoiMoiNhat = viTriLoiHienTai;
                        var diVat = new DiVat() { Loi = DSDiVat.Count + 1, ThoiGianLoi = thoiDiemLoi, ViTriLoi = viTriLoiHienTai, TenCuon = Utilities.TenCuon, ImagePath = savedImagePath };
                        DSDiVat.Add(diVat);
                        var savedFilePath = LocalSetting.m_strDataPath + Utilities.ThuMucLuuLoi + "\\" + Utilities.TenCuon + ".txt";
                        string content = JsonConvert.SerializeObject(diVat);
                        if (File.Exists(savedFilePath))
                        {
                            content = "," + content;
                        }
                        
                        this.Invoke(ResetUI);
                        SaveContent(savedFilePath, content);
                    }
                }
                inputImage.Dispose();
            });
        }

        private void ThongBaoChuongDen()
        {
            try
            {
                if(serialPort==null && !serialPort.IsOpen)
                {
                    log.Info("COM port is NOT ready");
                    return;
                }

                log.Info("Thong bao chuong den");
                
                serialPort.WriteLine("on");
                Thread.Sleep(1000);

                serialPort.WriteLine("off");
                Thread.Sleep(500);

                serialPort.WriteLine("on");
                Thread.Sleep(1000);

                serialPort.WriteLine("off");
            }
            catch(Exception ex)
            {
                log.Error(ex.Message);
            }            
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

        private void HienThiTrangThai(string content)
        {
            this.Invoke(new Action(() => { lbTrangThai.Text = content; }));
        }

        private void LockButton(Button btn, bool status)
        {
            this.Invoke(new Action(() => { btn.Enabled = status; }));
        }
       

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //CameraHelper.StopRealPlay(Constant.Camera);
            //CameraHelper.StopRecord(Constant.Camera);
            try
            {
                log.Info("Closing COM port...");
                serialPort.Close();                
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }
            
        }

        private void btnDung_Click(object sender, EventArgs e)
        {
            IsScan = false;
            btnQuet.Enabled = false;
            btnTamDung.Enabled = false;
            btnDung.Enabled = false;
            btnSangCuon.Enabled = true;
            btnXoa.Enabled = true;
            HienThiTrangThai("Đang dừng quét");
            AnHienTimKiem(true);
            log.Info("Dung");
        }

        private void btnSangCuon_Click(object sender, EventArgs e)
        {
            IsScan = true;
            ViTriLoiMoiNhat = 0;
            btnSangCuon.Enabled = false;
            btnTamDung.Enabled = true;
            btnDung.Enabled = true;
            btnQuet.Enabled = false;
            btnXoa.Enabled = false;
            Utilities.ThoiDiemBatDauCuonMoi = DateTime.Now;
            Utilities.TenCuon = LayTenCuonHienTai();
            Utilities.ThuMucLuuLoi = TaoThuMucLuuLoi();
            lbTenCuon.Text = Utilities.TenCuon;
            DSDiVat = new List<DiVat>();
            DemLoi();
            pictureBox1.Image = null;
            BindingData();
            HienThiTrangThai("Đang quét");
            AnHienTimKiem(false);
            log.Info("Sang cuon");
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

        private void btnIn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chúng tôi đang phát triển tính năng này. Vui lòng đợi thêm vài ngày. Cảm ơn!", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            var dialog = MessageBox.Show("Bạn có muốn xóa những lỗi được chọn không?", "Xóa lỗi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialog == DialogResult.Yes)
            {
                var htTenCuonDaXoa = new Hashtable();
                var htTenCuonSauKhiXoa = new Hashtable();
                var htIndex = new Hashtable();
                for (int i = 0; i < DSDiVat.Count; i++)
                {
                    htIndex[DSDiVat[i].Loi + DSDiVat[i].TenCuon] = i;
                }

                //xóa lỗi được chọn trên gridview
                foreach (DataGridViewRow row in dgvDSLoi.SelectedRows)
                {
                    int loi = (int)row.Cells["Loi"].Value;
                    string tenCuon = (string)row.Cells["TenCuon"].Value;
                    if (!htTenCuonDaXoa.ContainsKey(tenCuon))
                    {
                        htTenCuonDaXoa[tenCuon] = "";
                    }
                    DSDiVat.RemoveAt((int)htIndex[loi + tenCuon]);
                }

                var grDiVat = DSDiVat.GroupBy(g => g.TenCuon).Select(g => new { TenCuon = g.Key, DSLoi = g.ToList() }).ToList();
                foreach (var dsDiVat in grDiVat)
                {
                    if (htTenCuonDaXoa.ContainsKey(dsDiVat.TenCuon))
                    {
                        // đánh lại số thứ tự lỗi
                        for(int i = 0; i < dsDiVat.DSLoi.Count; i++)
                        {
                            dsDiVat.DSLoi[i].Loi = i + 1;
                        }

                        //ghi lỗi vào file
                        var content = JsonConvert.SerializeObject(dsDiVat.DSLoi).Replace("[", "").Replace("]", "");
                        File.WriteAllText(LocalSetting.m_strDataPath + Utilities.ThuMucLuuLoi + "\\" + dsDiVat.TenCuon + ".txt", content);
                    }
                    htTenCuonSauKhiXoa[dsDiVat.TenCuon] = "";
                }

                //khi xóa hết lỗi của một cuộn thì cuộn đó lưu file trống
                foreach (DictionaryEntry dic in htTenCuonDaXoa)
                {
                    if (!htTenCuonSauKhiXoa.ContainsKey(dic.Key))
                    {
                        File.WriteAllText(LocalSetting.m_strDataPath + Utilities.ThuMucLuuLoi + "\\" + dic.Key + ".txt", string.Empty);
                    }
                }

                bsDiVat.ResetBindings(false);
                DemLoi();
            }
        }

        private void btnQuet_Click(object sender, EventArgs e)
        {
            IsScan = true;
            HienThiTrangThai("Đang quét");
            //trường hợp lúc khởi động chương trình
            if (!btnTamDung.Enabled && !btnDung.Enabled && !btnSangCuon.Enabled)
            {
                btnXoa.Enabled = false;
                Utilities.ThoiDiemBatDauCuonMoi = DateTime.Now;
                Utilities.TenCuon = LayTenCuonHienTai();
                Utilities.ThuMucLuuLoi = TaoThuMucLuuLoi();
                lbTenCuon.Text = Utilities.TenCuon;
                DSDiVat = new List<DiVat>();
                BindingData();
                AnHienTimKiem(false);
            }
            else
            {
                //trường hợp tạm dừng chương trình sau đó tiếp tục quét
                var khoangThoiGianTamDungChuongTrinhDenQuetLai = DateTime.Now - Utilities.ThoiDiemTamDung;

                //cập nhập thời điểm bắt đầu cuộn mới để tính độ dài cuộn (s = v * t [t = thời điểm hiện tại - thời điểm bắt đầu cuộn])
                Utilities.ThoiDiemBatDauCuonMoi += khoangThoiGianTamDungChuongTrinhDenQuetLai; 
            }

            btnTamDung.Enabled = true;
            btnDung.Enabled = true;
            btnQuet.Enabled = false;
            btnSangCuon.Enabled = false;
            log.Info("Quet");
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
                Utilities.ThuMucLuuLoi = dtPickerNgayTimKiem.Value.ToString("MMddyyyy");
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
                                    lbTenCuon.Text = Utilities.TenCuon;
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

        private void dgvDSLoi_SelectionChanged(object sender, EventArgs e)
        {
            var diVat = DSDiVat.FirstOrDefault(d => d.Loi == Convert.ToInt32(dgvDSLoi.CurrentRow.Cells["Loi"].Value) && d.TenCuon == (string)dgvDSLoi.CurrentRow.Cells["TenCuon"].Value);
            if (diVat != null)
            {
                pictureBox1.Image = System.Drawing.Image.FromFile(diVat.ImagePath);
            }
        }

        private void btnTamDung_Click(object sender, EventArgs e)
        {
            btnQuet.Enabled = true;
            btnDung.Enabled = true;
            btnTamDung.Enabled = false;
            btnSangCuon.Enabled = false;
            IsScan = false;
            Utilities.ThoiDiemTamDung = DateTime.Now;
            HienThiTrangThai("Tạm dừng quét");
            log.Info("Tam dung");
        }
    }
}
