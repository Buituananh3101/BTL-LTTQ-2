using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace BTL_LTTQ_2.Classes
{
    /// <summary>
    /// Tiện ích hỗ trợ tìm kiếm trực tiếp trong danh sách ComboBox WinForms.
    /// Ban đầu hiển thị đầy đủ danh sách; khi gõ phím thì các phần tử trong ComboBox tự lọc theo từ khóa chứa ký tự (hỗ trợ có dấu và không dấu).
    /// Khi tìm thấy: ấn Enter tự động chọn phần tử đầu tiên (không cần bấm mũi tên xuống).
    /// Khi không tìm thấy: ấn Enter tự động khôi phục toàn bộ danh sách và chọn phần tử đầu tiên (-- Tất cả --).
    /// Giới hạn hiển thị tối đa MaxDropDownItems (mặc định 8 mục) và tự động bật thanh cuộn (scrollbar) để tránh tràn màn hình.
    /// </summary>
    public static class ComboboxHelper
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left, Top, Right, Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct COMBOBOXINFO
        {
            public int cbSize;
            public RECT rcItem;
            public RECT rcButton;
            public int stateButton;
            public IntPtr hwndCombo;
            public IntPtr hwndItem;
            public IntPtr hwndList;
        }

        [DllImport("user32.dll")]
        private static extern bool GetComboBoxInfo(IntPtr hWnd, ref COMBOBOXINFO pcbi);

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        private const uint SWP_NOMOVE = 0x0002;
        private const uint SWP_NOZORDER = 0x0004;
        private const uint SWP_NOACTIVATE = 0x0010;

        /// <summary>
        /// Cập nhật chiều cao hiển thị của phần thả xuống ComboBox (giới hạn tối đa MaxDropDownItems, mặc định 8 dòng).
        /// Khắc phục lỗi WinForms bỏ qua MaxDropDownItems khi IntegralHeight = true.
        /// </summary>
        public static void CapNhatDoCaoDropdown(ComboBox cbo, int itemCount)
        {
            if (cbo == null) return;

            int maxItems = cbo.MaxDropDownItems > 0 ? cbo.MaxDropDownItems : 8;
            int visibleItems = Math.Min(Math.Max(itemCount, 1), maxItems);
            int newHeight = visibleItems * cbo.ItemHeight + 2;

            cbo.IntegralHeight = false;
            cbo.DropDownHeight = newHeight;

            if (cbo.DroppedDown && cbo.IsHandleCreated)
            {
                COMBOBOXINFO info = new COMBOBOXINFO();
                info.cbSize = Marshal.SizeOf(info);
                if (GetComboBoxInfo(cbo.Handle, ref info) && info.hwndList != IntPtr.Zero)
                {
                    RECT r;
                    GetWindowRect(info.hwndList, out r);
                    int width = r.Right - r.Left;
                    if (width <= 0) width = cbo.DropDownWidth;
                    SetWindowPos(info.hwndList, IntPtr.Zero, 0, 0, width, newHeight, SWP_NOMOVE | SWP_NOZORDER | SWP_NOACTIVATE);
                }
            }
        }

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
            cbo.IntegralHeight = false;
            if (cbo.MaxDropDownItems <= 0) cbo.MaxDropDownItems = 8;

            // Tạo cột tìm kiếm không dấu nếu chưa có
            string searchCol = "_SearchCol";
            if (!dt.Columns.Contains(searchCol))
            {
                dt.Columns.Add(searchCol, typeof(string));
            }
            foreach (DataRow row in dt.Rows)
            {
                string textVal = row[displayCol]?.ToString() ?? "";
                string valStr = row[valueCol]?.ToString() ?? "";
                if (textVal.StartsWith("--") || valStr == "0" || string.IsNullOrEmpty(valStr))
                {
                    row[searchCol] = "tat ca all";
                }
                else
                {
                    row[searchCol] = BoDauTiengViet(textVal).ToLower() + " " + textVal.ToLower();
                }
            }

            DataView dv = new DataView(dt);
            cbo.DataSource = dv;
            cbo.DisplayMember = displayCol;
            cbo.ValueMember = valueCol;

            CapNhatDoCaoDropdown(cbo, dv.Count);

            bool isFiltering = false;

            cbo.DropDown += (s, e) =>
            {
                CapNhatDoCaoDropdown(cbo, dv.Count);
            };

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
                    if (!clean.StartsWith("tat ca") && !clean.StartsWith("all") && !clean.StartsWith("--"))
                    {
                        dv.RowFilter = $"{searchCol} LIKE '%{clean}%' AND {searchCol} <> 'tat ca all'";
                    }
                    else
                    {
                        dv.RowFilter = $"{searchCol} LIKE '%{clean}%'";
                    }
                }

                CapNhatDoCaoDropdown(cbo, dv.Count);
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
                    bool wasFiltered = !string.IsNullOrEmpty(dv.RowFilter);
                    if (!wasFiltered)
                    {
                        // Người dùng chỉ click chọn bình thường từ danh sách đầy đủ, không lọc
                        // Giữ nguyên 100% lựa chọn của người dùng
                        return;
                    }

                    if (dv.Count == 0)
                    {
                        // 1. Không tìm thấy gì mà Enter/Đóng -> Trả lại tất cả, chọn mục đầu tiên (Tất cả)
                        dv.RowFilter = "";
                        CapNhatDoCaoDropdown(cbo, dv.Count);
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

                        // Khôi phục lại toàn bộ danh sách để lần mở sau hiện hết
                        dv.RowFilter = "";
                        CapNhatDoCaoDropdown(cbo, dv.Count);

                        if (currentVal != null)
                        {
                            cbo.SelectedValue = currentVal;
                        }
                    }
                }
                catch
                {
                    dv.RowFilter = "";
                    CapNhatDoCaoDropdown(cbo, dv.Count);
                    if (cbo.Items.Count > 0) cbo.SelectedIndex = 0;
                }
                finally
                {
                    cbo.DroppedDown = false;
                    isFiltering = false;
                    onCommitted?.Invoke();
                    if (!cbo.Focused)
                    {
                        cbo.SelectionLength = 0;
                    }
                }
            };

            cbo.DropDownClosed += (s, e) =>
            {
                apDungLuaChon();
            };

            cbo.SelectionChangeCommitted += (s, e) =>
            {
                if (isFiltering) return;
                try
                {
                    object currentVal = cbo.SelectedValue;
                    if (!string.IsNullOrEmpty(dv.RowFilter))
                    {
                        dv.RowFilter = "";
                        CapNhatDoCaoDropdown(cbo, dv.Count);
                        if (currentVal != null)
                        {
                            cbo.SelectedValue = currentVal;
                        }
                    }
                }
                catch { }
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
                cbo.SelectionLength = 0;
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
            cbo.IntegralHeight = false;
            if (cbo.MaxDropDownItems <= 0) cbo.MaxDropDownItems = 8;

            cbo.BeginUpdate();
            cbo.Items.Clear();
            cbo.Items.AddRange(danhSachGoc.ToArray());
            cbo.EndUpdate();
            if (cbo.Items.Count > 0 && cbo.SelectedIndex < 0) cbo.SelectedIndex = 0;

            CapNhatDoCaoDropdown(cbo, cbo.Items.Count);

            bool isFiltering = false;

            cbo.DropDown += (s, e) =>
            {
                CapNhatDoCaoDropdown(cbo, cbo.Items.Count);
            };

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
                    {
                        if (item.StartsWith("--") && !textClean.StartsWith("tat ca") && !textClean.StartsWith("all") && !textClean.StartsWith("--"))
                            return false;
                        return item.IndexOf(text.Trim(), StringComparison.OrdinalIgnoreCase) >= 0 ||
                               BoDauTiengViet(item).IndexOf(textClean, StringComparison.OrdinalIgnoreCase) >= 0;
                    }).ToArray();
                    cbo.Items.AddRange(matched);
                }

                cbo.EndUpdate();
                CapNhatDoCaoDropdown(cbo, cbo.Items.Count);
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
                    bool wasFiltered = (cbo.Items.Count != danhSachGoc.Count);
                    if (!wasFiltered)
                    {
                        // Người dùng chỉ click chọn bình thường từ danh sách đầy đủ, không lọc
                        // Giữ nguyên 100% lựa chọn của người dùng
                        return;
                    }

                    if (cbo.Items.Count == 0)
                    {
                        // 1. Không tìm thấy gì mà Enter -> Trả lại tất cả, chọn mục đầu tiên
                        cbo.BeginUpdate();
                        cbo.Items.Clear();
                        cbo.Items.AddRange(danhSachGoc.ToArray());
                        cbo.EndUpdate();
                        CapNhatDoCaoDropdown(cbo, cbo.Items.Count);
                        if (cbo.Items.Count > 0)
                        {
                            cbo.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        // 2. Tìm thấy -> Tự động chọn phần tử đầu tiên nếu chưa chọn
                        if (cbo.SelectedIndex < 0)
                        {
                            cbo.SelectedIndex = 0;
                        }

                        string selectedVal = cbo.SelectedItem?.ToString();

                        cbo.BeginUpdate();
                        cbo.Items.Clear();
                        cbo.Items.AddRange(danhSachGoc.ToArray());
                        cbo.EndUpdate();
                        CapNhatDoCaoDropdown(cbo, cbo.Items.Count);

                        if (!string.IsNullOrEmpty(selectedVal) && danhSachGoc.Contains(selectedVal))
                        {
                            cbo.SelectedItem = selectedVal;
                        }
                    }
                }
                catch
                {
                    cbo.BeginUpdate();
                    cbo.Items.Clear();
                    cbo.Items.AddRange(danhSachGoc.ToArray());
                    cbo.EndUpdate();
                    CapNhatDoCaoDropdown(cbo, cbo.Items.Count);
                    if (cbo.Items.Count > 0) cbo.SelectedIndex = 0;
                }
                finally
                {
                    cbo.DroppedDown = false;
                    isFiltering = false;
                    onCommitted?.Invoke();
                    if (!cbo.Focused)
                    {
                        cbo.SelectionLength = 0;
                    }
                }
            };

            cbo.DropDownClosed += (s, e) =>
            {
                apDungLuaChon();
            };

            cbo.SelectionChangeCommitted += (s, e) =>
            {
                if (isFiltering) return;
                try
                {
                    string selectedText = cbo.SelectedItem?.ToString();
                    if (cbo.Items.Count != danhSachGoc.Count)
                    {
                        cbo.BeginUpdate();
                        cbo.Items.Clear();
                        cbo.Items.AddRange(danhSachGoc.ToArray());
                        cbo.EndUpdate();
                        CapNhatDoCaoDropdown(cbo, cbo.Items.Count);
                        if (!string.IsNullOrEmpty(selectedText) && danhSachGoc.Contains(selectedText))
                        {
                            cbo.SelectedItem = selectedText;
                        }
                    }
                }
                catch { }
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
                cbo.SelectionLength = 0;
            };

            cbo.Enter += (s, e) =>
            {
                cbo.BeginInvoke(new Action(() => cbo.SelectAll()));
            };
        }
    }
}
