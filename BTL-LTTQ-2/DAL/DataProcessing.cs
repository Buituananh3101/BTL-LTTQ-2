using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BTL_LTTQ_2.DAL
{
    /// <summary>
    /// Lớp tiện ích ADO.NET thuần quản lý kết nối và thực thi truy vấn tới CSDL SQL Server.
    /// </summary>
    public static class DataProcessing
    {
        private static string _strConnection;

        /// <summary>
        /// Chuỗi kết nối lấy từ file cấu hình App.config
        /// </summary>
        public static string strConnection
        {
            get
            {
                if (string.IsNullOrEmpty(_strConnection))
                {
                    var setting = ConfigurationManager.ConnectionStrings["QuanLyKhoConnection"];
                    if (setting != null)
                    {
                        _strConnection = setting.ConnectionString;
                    }
                    else
                    {
                        // Chuỗi kết nối dự phòng
                        _strConnection = @"Server=localhost\SQLEXPRESS;Database=QuanLyKhoPhuQuocLTW;Integrated Security=True;TrustServerCertificate=True;MultipleActiveResultSets=True";
                    }
                }
                return _strConnection;
            }
            set
            {
                _strConnection = value;
            }
        }

        /// <summary>
        /// Mở và trả về một đối tượng SqlConnection đã kết nối
        /// </summary>
        public static SqlConnection OpenConnect()
        {
            SqlConnection conn = new SqlConnection(strConnection);
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            return conn;
        }

        /// <summary>
        /// Thực thi câu lệnh SELECT trả về một DataTable chứa dữ liệu (Dùng SqlDataAdapter)
        /// </summary>
        public static DataTable ReadData(string cauTruyVan, SqlParameter[] thamSo = null)
        {
            DataTable bangDuLieu = new DataTable();
            using (SqlConnection conn = OpenConnect())
            {
                using (SqlCommand cmd = new SqlCommand(cauTruyVan, conn))
                {
                    if (thamSo != null && thamSo.Length > 0)
                    {
                        cmd.Parameters.AddRange(thamSo);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(bangDuLieu);
                    }
                }
            }
            return bangDuLieu;
        }

        /// <summary>
        /// Thực thi câu lệnh INSERT / UPDATE / DELETE trả về số dòng bị tác động
        /// </summary>
        public static int ChangeData(string cauLenh, SqlParameter[] thamSo = null)
        {
            int soDongAnhHuong = 0;
            using (SqlConnection conn = OpenConnect())
            {
                using (SqlCommand cmd = new SqlCommand(cauLenh, conn))
                {
                    if (thamSo != null && thamSo.Length > 0)
                    {
                        cmd.Parameters.AddRange(thamSo);
                    }
                    soDongAnhHuong = cmd.ExecuteNonQuery();
                }
            }
            return soDongAnhHuong;
        }

        /// <summary>
        /// Thực thi câu lệnh lấy giá trị đơn lẻ từ ô đầu tiên dòng đầu tiên (COUNT, SUM, MAX, ID tự tăng...)
        /// </summary>
        public static object ReadValue(string cauTruyVan, SqlParameter[] thamSo = null)
        {
            object ketQua = null;
            using (SqlConnection conn = OpenConnect())
            {
                using (SqlCommand cmd = new SqlCommand(cauTruyVan, conn))
                {
                    if (thamSo != null && thamSo.Length > 0)
                    {
                        cmd.Parameters.AddRange(thamSo);
                    }
                    ketQua = cmd.ExecuteScalar();
                }
            }
            return ketQua;
        }

        /// <summary>
        /// Thực thi tập hợp nhiều câu lệnh trong một Giao dịch (Transaction) an toàn
        /// </summary>
        public static void ChangeDataTransaction(Action<SqlConnection, SqlTransaction> hanhDong)
        {
            using (SqlConnection conn = OpenConnect())
            {
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        hanhDong(conn, trans);
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
