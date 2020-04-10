using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DetectObject.Model;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace DetectObject.Utils
{
    public class Detecter
    {
        public static bool DetectObject(Image<Bgr, byte> imgInput, out int heighOfObject, out string savedImagePath)
        {
            var sobel = imgInput.Convert<Gray, byte>().Canny(150, 250);
            heighOfObject = 0;
            savedImagePath = string.Empty;
            Mat SE = CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Cross, new Size(50, 1), new Point(-1, -1));
            sobel = sobel.MorphologyEx(Emgu.CV.CvEnum.MorphOp.Close, SE, new Point(-1, -1), 2, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(255));
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            Mat m = new Mat();
            CvInvoke.FindContours(sobel, contours, m, Emgu.CV.CvEnum.RetrType.List, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);
            List<Rectangle> list = new List<Rectangle>();
            for (int i = 0; i < contours.Size; i++)
            {
                Rectangle brect = CvInvoke.BoundingRectangle(contours[i]);
                //if (brect.Width > Utilities.Size.Width && brect.Height > Utilities.Size.Height)
                if (CvInvoke.ContourArea(contours[i]) > Utilities.LIMIT_AREA)
                {
                    list.Add(brect);
                }
            }

            contours.Dispose();
            Rectangle totalRec = new Rectangle();
            int minX = int.MaxValue;
            int maxX = 0;
            int minY = int.MaxValue;
            int maxY = 0;
            foreach (var r in list)
            {
                if (r.X < minX) minX = r.X;
                if (r.X + r.Width > maxX) maxX = r.X + r.Width;
                if (r.Y < minY) minY = r.Y;
                if (r.Y + r.Height > maxY) maxY = r.Y + r.Height;
            }

            if (list.Count > 0)
            {
                heighOfObject = minY;
                totalRec.X = minX;
                totalRec.Y = minY;
                totalRec.Width = maxX - minX;
                totalRec.Height = maxY - minY;
                //CvInvoke.Rectangle(imgInput, totalRec, new MCvScalar(0, 0, 255), 2);
                var savedFolderPath = LocalSetting.m_strDataPath + Utilities.ThuMucLuuLoi + "\\" + Utilities.TenCuon + "\\";
                savedImagePath = savedFolderPath + CommonFunc.ConvertDateTimeToInvariantInfo(DateTime.Now) + ".jpg";
                if (!Directory.Exists(savedFolderPath))
                {
                    Directory.CreateDirectory(savedFolderPath);
                }
                imgInput.Save(savedImagePath);
            }

            return list.Count > 0;
        }

        public static bool DetectSpot(Image<Bgr, byte> imgInput, out int heighOfObject, out string savedImagePath)
        {
            heighOfObject = 0;
            savedImagePath = string.Empty;
            
            //convert image to gray binary & threshoding
            Image<Gray, byte> imgOutput = imgInput.Convert<Gray, byte>().ThresholdBinary(new Gray(127), new Gray(255));            
                        
            //find contours
            using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint())
            {
                CvInvoke.FindContours(imgOutput, contours, null, Emgu.CV.CvEnum.RetrType.List, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);
                if (contours.Size > 0)
                {
                    for (int i = 0; i < contours.Size; i++)
                    {
                        if (CvInvoke.ContourArea(contours[i]) > Utilities.LIMIT_AREA)     //skip small area spot
                        {
                            var savedFolderPath = LocalSetting.m_strDataPath + Utilities.ThuMucLuuLoi + "\\" + Utilities.TenCuon + "\\";
                            savedImagePath = savedFolderPath + CommonFunc.ConvertDateTimeToInvariantInfo(DateTime.Now) + ".jpg";
                            if (!Directory.Exists(savedFolderPath))
                            {
                                Directory.CreateDirectory(savedFolderPath);
                            }
                            imgInput.Save(savedImagePath);
                            heighOfObject = CvInvoke.BoundingRectangle(contours[i]).Height;
                            return true;
                        }
                    }
                }
            }                
            return false;
        }
    }
}
