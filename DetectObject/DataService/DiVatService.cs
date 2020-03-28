using DetectObject.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectObject.DataService
{
    public class DiVatService : BaseService
    {
        public List<DiVat> LayDSDiVat()
        {
            var dsDiVat = new List<DiVat>();
            string sql = @"SELECT * FROM DiVat";
            var ds = Load(sql);
            if (ds.Tables.Count > 0)
            { 
                foreach(DataRow row in ds.Tables[0].Rows)
                {
                    dsDiVat.Add(new DiVat()
                    {
                        TenCuon = row["Cuon"].ToString(),
                        ThoiGianLoi = (DateTime)row["ThoiGianLoi"],
                        ViTriLoi = (double)row["ViTriLoi"],
                        Loi = (int)row["Loi"]
                    });
                }
            }

            return dsDiVat;
        }

        public bool LuuDiVat(DiVat diVat)
        {
            var cmd = new SQLiteCommand("INSERT INTO DiVat VALUES (@Loi, @ThoiGianLoi, @ViTriLoi, @TenCuon, @ImagePath)");
            cmd.Parameters.Add("Loi", DbType.Int32).Value = diVat.Loi;
            cmd.Parameters.Add("ThoiGianLoi", DbType.String).Value = diVat.ThoiGianLoi;
            cmd.Parameters.Add("ViTriLoi", DbType.Double).Value = diVat.ViTriLoi;
            cmd.Parameters.Add("TenCuon", DbType.String).Value = diVat.TenCuon;
            cmd.Parameters.Add("ImagePath", DbType.String).Value = diVat.ImagePath;
            ExcuteNonQuery(cmd);
            return true;
        }
    }
}
