using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BTL_LTTQ_2.BLL
{
    /// <summary>
    /// Tiện ích hỗ trợ tìm kiếm trực tiếp trong danh sách ComboBox WinForms.
    /// Ban đầu hiển thị đầy đủ danh sách; khi gõ phím thì các phần tử trong ComboBox tự lọc theo từ khóa chứa ký tự (hỗ trợ có dấu và không dấu).
    /// Khi tìm thấy: ấn Enter tự động chọn phần tử đầu tiên (không cần bấm mũi tên xuống).
    /// Khi không tìm thấy: ấn Enter tự động khôi phục toàn bộ danh sách và chọn phần tử đầu tiên (-- Tất cả --).
    /// </summary>
    public static class ComboboxHelper
    {
        /// <summary>
        /// Chuẩn hóa bỏ dấu tiếng Việt phục vụ tìm kiếm linh hoạt (ví dụ: gõ 'nuoc' vẫn tìm thấy 'Nước')
        /// </summary>
        public static string BoDauTiengViet(string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;
            string normalized = text.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            foreach (char c in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }
            return sb.ToString().Normalize(NormalizationForm.FormC).Replace('đ', 'd').Replace('Đ', 'D');
        }

        /// <summary>
        /// Cài đặt tìm kiếm cho ComboBox có nguồn dữ liệu DataTable (sử dụng DataView để lọc trực tiếp các phần tử trong ComboBox).
        /// </summary>
        public static void CaiDatTimKiem(ComboBox cbo, DataTable dt, string displayCol, string valueCol, Action onCommitted = null)
        {
            if (cbo == null || dt == null) return;

            cbo.AutoCompleteMode = AutoCompleteMode.None;
            cbo.AutoCompleteSource = AutoCompleteSource.None;
            cbo.DropDownStyle = ComboBoxStyle.DropDown;

            // Tạo cột tìm kiếm không dấu nếu chưa có
            string searchCol = "_SearchCol";
            if (!dt.Columns.Contains(searchCol))
            {
                dt.Columns.Add(searchCol, typeof(string));
            }
            foreach (DataRow row in dt.Rows)
            {
                string textVal = row[displayCol]?.ToString() ?? "";
                row[searchCol] = BoDauTiengViet(textVal).ToLower() + " " + textVal.ToLower();
            }

            DataView dv = new DataView(dt);
            cbo.DataSource = dv;
            cbo.DisplayMember = displayCol;
            cbo.ValueMember = valueCol;

            bool isFiltering = false;

            cbo.TextUpdate += (s, e) =>
            {
                if (isFiltering) return;
                isFiltering = true;

                string text = cbo.Text;
                int selStart = cbo.SelectionStart;

                if (string.IsNullOrWhiteSpace(text))
                {
                    dv.RowFilter = "";
                }
                else
                {
                    string clean = BoDauTiengViet(text.Trim()).ToLower().Replace("'", "''").Replace("[", "[[]").Replace("%", "[%]");
                    dv.RowFilter = $"{searchCol} LIKE '%{clean}%'";
                }

                cbo.DroppedDown = true;
                Cursor.Current = Cursors.Default;
                cbo.Text = text;
                cbo.SelectionStart = selStart;
                cbo.SelectionLength = 0;

                isFiltering = false;
            };

            Action apDungLuaChon = () =>
            {
                if (isFiltering) return;
                isFiltering = true;

                try
                {
                    if (dv.Count == 0)
                    {
                        // 1. Không tìm thấy gì mà Enter/Đóng -> Trả lại tất cả, chọn mục đầu tiên (Tất cả)
                        dv.RowFilter = "";
                        if (cbo.Items.Count > 0)
                        {
                            cbo.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        // 2. Tìm thấy -> Tự động chọn phần tử đầu tiên nếu người dùng chưa chọn dòng khác
                        if (cbo.SelectedIndex < 0)
                        {
                            cbo.SelectedIndex = 0;
                        }

                        object currentVal = cbo.SelectedValue;
                        string currentText = cbo.Text;

                        // Khôi phục lại toàn bộ danh sách để lần mở sau hiện hết
                        dv.RowFilter = "";

                        if (currentVal != null)
                        {
                            cbo.SelectedValue = currentVal;
                        }
                        else if (!string.IsNullOrWhiteSpace(currentText))
                        {
                            int idx = cbo.FindStringExact(currentText);
                            if (idx >= 0) cbo.SelectedIndex = idx;
                            else if (cbo.Items.Count > 0) cbo.SelectedIndex = 0;
                        }
                        else if (cbo.Items.Count > 0)
                        {
                            cbo.SelectedIndex = 0;
                        }
                    }
                }
                catch
                {
                    dv.RowFilter = "";
                    if (cbo.Items.Count > 0) cbo.SelectedIndex = 0;
                }
                finally
                {
                    cbo.DroppedDown = false;
                    isFiltering = false;
                    onCommitted?.Invoke();
                    cbo.BeginInvoke(new Action(() => cbo.SelectAll()));
                }
            };

            cbo.DropDownClosed += (s, e) =>
            {
                apDungLuaChon();
            };

            cbo.SelectionChangeCommitted += (s, e) =>
            {
                onCommitted?.Invoke();
            };

            cbo.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    apDungLuaChon();
                }
            };

            cbo.Leave += (s, e) =>
            {
                apDungLuaChon();
            };

            cbo.Enter += (s, e) =>
            {
                cbo.BeginInvoke(new Action(() => cbo.SelectAll()));
            };
        }

        /// <summary>
        /// Cài đặt tìm kiếm cho ComboBox dạng danh sách chuỗi Items (Unbound).
        /// </summary>
        public static void CaiDatTimKiem(ComboBox cbo, List<string> danhSachGoc, Action onCommitted = null)
        {
            if (cbo == null || danhSachGoc == null) return;

            cbo.AutoCompleteMode = AutoCompleteMode.None;
            cbo.AutoCompleteSource = AutoCompleteSource.None;
            cbo.DropDownStyle = ComboBoxStyle.DropDown;

            cbo.BeginUpdate();
            cbo.Items.Clear();
            cbo.Items.AddRange(danhSachGoc.ToArray());
            cbo.EndUpdate();
            if (cbo.Items.Count > 0 && cbo.SelectedIndex < 0) cbo.SelectedIndex = 0;

            bool isFiltering = false;

            cbo.TextUpdate += (s, e) =>
            {
                if (isFiltering) return;
                isFiltering = true;

                string text = cbo.Text;
                int selStart = cbo.SelectionStart;

                cbo.BeginUpdate();
                cbo.Items.Clear();

                if (string.IsNullOrWhiteSpace(text))
                {
                    cbo.Items.AddRange(danhSachGoc.ToArray());
                }
                else
                {
                    string textClean = BoDauTiengViet(text.Trim()).ToLower();
                    var matched = danhSachGoc.Where(item =>
                        item.IndexOf(text.Trim(), StringComparison.OrdinalIgnoreCase) >= 0 ||
                        BoDauTiengViet(item).IndexOf(textClean, StringComparison.OrdinalIgnoreCase) >= 0
                    ).ToArray();
                    cbo.Items.AddRange(matched);
                }

                cbo.EndUpdate();
                cbo.DroppedDown = true;
                Cursor.Current = Cursors.Default;

                cbo.Text = text;
                cbo.SelectionStart = selStart;
                cbo.SelectionLength = 0;

                isFiltering = false;
            };

            Action apDungLuaChon = () =>
            {
                if (isFiltering) return;
                isFiltering = true;

                try
                {
                    if (cbo.Items.Count == 0)
                    {
                        // 1. Không tìm thấy gì mà Enter -> Trả lại tất cả
                        cbo.BeginUpdate();
                        cbo.Items.Clear();
                        cbo.Items.AddRange(danhSachGoc.ToArray());
                        cbo.EndUpdate();
                        if (cbo.Items.Count > 0)
                        {
                            cbo.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        // 2. Tìm thấy -> Tự động chọn phần tử đầu tiên nếu người dùng chưa chọn mục khác
                        if (cbo.SelectedIndex < 0)
                        {
                            cbo.SelectedIndex = 0;
                        }

                        string currentText = cbo.Text;

                        // Khôi phục lại toàn bộ danh sách để lần mở sau hiện hết
                        cbo.BeginUpdate();
                        cbo.Items.Clear();
                        cbo.Items.AddRange(danhSachGoc.ToArray());
                        cbo.EndUpdate();

                        cbo.Text = currentText;
                        int idx = cbo.FindStringExact(currentText);
                        if (idx >= 0)
                        {
                            cbo.SelectedIndex = idx;
                        }
                        else if (cbo.Items.Count > 0 && danhSachGoc.Count > 0 && danhSachGoc[0].StartsWith("--"))
                        {
                            cbo.SelectedIndex = 0;
                        }
                    }
                }
                catch
                {
                    cbo.BeginUpdate();
                    cbo.Items.Clear();
                    cbo.Items.AddRange(danhSachGoc.ToArray());
                    cbo.EndUpdate();
                    if (cbo.Items.Count > 0) cbo.SelectedIndex = 0;
                }
                finally
                {
                    cbo.DroppedDown = false;
                    isFiltering = false;
                    onCommitted?.Invoke();
                    cbo.BeginInvoke(new Action(() => cbo.SelectAll()));
                }
            };

            cbo.DropDownClosed += (s, e) =>
            {
                apDungLuaChon();
            };

            cbo.SelectionChangeCommitted += (s, e) =>
            {
                onCommitted?.Invoke();
            };

            cbo.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    apDungLuaChon();
                }
            };

            cbo.Leave += (s, e) =>
            {
                apDungLuaChon();
            };

            cbo.Enter += (s, e) =>
            {
                cbo.BeginInvoke(new Action(() => cbo.SelectAll()));
            };
        }
    }
}
