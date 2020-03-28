using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectObject.DataService
{
    public class BaseService
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(BaseService));
        SQLiteConnection _con = new SQLiteConnection();
        public void OpenConection()
        {
            string _strConnect = "Data Source=DiVat.sqlite;Version=3;";
            _con.ConnectionString = _strConnect;
            _con.Open();
        }

        public void CloseConnection()
        {
            _con.Close();
        }

        public void CreateTable()
        {
            string sql = @"CREATE TABLE IF NOT EXISTS DiVat ([Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, Loi int, ThoiGianLoi DateTime, ViTriLoi Double, Cuon nvarchar(100))
                           CREATE TABLE IF NOT EXISTS MaxTenCuon (TenCuon int)";
            SQLiteConnection.CreateFile("DiVat.sqlite");
            OpenConection();
            SQLiteCommand command = new SQLiteCommand(sql, _con);
            command.ExecuteNonQuery();
            CloseConnection();
        }

        #region Load
        public DataSet Load(string sql)
        {
            var _m_DataAdapter = new SQLiteDataAdapter(sql, _con);
            var ds = new DataSet();
            try
            {
                OpenConection();
                _m_DataAdapter.Fill(ds);
                CloseConnection();
            }
            catch (Exception ex)
            {
                log.Error("Loi ket noi sql", ex);
                CloseConnection();
            }
            return ds;
        }
        #endregion

        public void ExcuteNonQuery(SQLiteCommand sqlCmd)
        {
            try
            {
                OpenConection();
                sqlCmd.ExecuteNonQuery();
                CloseConnection();
            }
            catch (Exception ex)
            {
                log.Error("Loi ket noi sql", ex);
                CloseConnection();
            }
        }
    }
}
