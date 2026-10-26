using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace BTL_LTTQ_2.Classes
{
    /// <summary>
    /// Tiện ích hỗ trợ tìm kiếm trực tiếp trong danh sách ComboBox WinForms.
    /// Ban đầu hiển thị đầy đủ danh sách; khi gõ phím thì các phần tử trong ComboBox tự lọc theo từ khóa chứa ký tự (hỗ trợ có dấu và không dấu).
    /// Khi tìm thấy: ấn Enter tự động chọn phần tử đầu tiên (không cần bấm mũi tên xuống).
    /// Khi không tìm thấy: ấn Enter tự động khôi phục toàn bộ danh sách; combobox bộ lọc chọn phần tử đầu tiên (-- Tất cả --),
    /// combobox nhập liệu (không có mục "-- Tất cả --") khôi phục lại lựa chọn trước đó.
    /// Giới hạn hiển thị tối đa MaxDropDownItems (mặc định 8 mục) và tự động bật thanh cuộn (scrollbar) để tránh tràn màn hình.
    /// Quản lý dọn dẹp các sự kiện cũ để tránh lặp sự kiện khi nạp lại dữ liệu ComboBox nhiều lần.
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

        // Bảng lưu trữ binder để tự động giải phóng event handler cũ khi nạp lại ComboBox
        private static readonly ConditionalWeakTable<ComboBox, IDisposable> _binders = new ConditionalWeakTable<ComboBox, IDisposable>();

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
        /// Thoát ký tự đặc biệt cho giá trị đặt trong mệnh đề LIKE '...' của DataView.RowFilter.
        /// Bọc các ký tự [ ] * % trong cặp ngoặc vuông (khớp đúng ký tự, không phải ký tự đại diện)
        /// và nhân đôi dấu nháy đơn để tránh lỗi cú pháp biểu thức khi người dùng gõ ký tự đặc biệt.
        /// </summary>
        public static string ThoatKyTuLikeRowFilter(string giaTri)
        {
            if (string.IsNullOrEmpty(giaTri)) return string.Empty;

            StringBuilder sb = new StringBuilder(giaTri.Length + 8);
            foreach (char c in giaTri)
            {
                switch (c)
                {
                    case '[':
                    case ']':
                    case '*':
                    case '%':
                        sb.Append('[').Append(c).Append(']');
                        break;
                    case '\'':
                        sb.Append("''");
                        break;
                    default:
                        sb.Append(c);
                        break;
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Kiểm tra một mục hiển thị có phải mục giữ chỗ dạng "-- Tất cả ... --" của combobox bộ lọc hay không
        /// </summary>
        private static bool LaMucTatCa(string text)
        {
            if (string.IsNullOrEmpty(text)) return false;
            string lower = text.ToLower();
            return text.StartsWith("--") || lower.Contains("tất cả") || lower.Contains("tat ca");
        }

        /// <summary>
        /// Lớp quản lý vòng đời sự kiện tìm kiếm ComboBox cho nguồn dữ liệu DataTable
        /// </summary>
        private class DataTableSearchBinder : IDisposable
        {
            private readonly ComboBox _cbo;
            private readonly DataView _dv;
            private readonly string _searchCol;
            private readonly Action _onCommitted;
            private readonly bool _chonMucDauKhiKhongKhop;
            private bool _isFiltering = false;

            // Lựa chọn hợp lệ gần nhất (khôi phục khi gõ từ khóa không khớp mục nào ở combobox nhập liệu)
            private int _chiSoTruoc = -1;
            private object _giaTriTruoc = null;
            private string _textTruoc = string.Empty;

            private readonly EventHandler _onDropDown;
            private readonly EventHandler _onTextUpdate;
            private readonly EventHandler _onDropDownClosed;
            private readonly EventHandler _onSelectionChangeCommitted;
            private readonly EventHandler _onSelectedIndexChanged;
            private readonly KeyEventHandler _onKeyDown;
            private readonly EventHandler _onLeave;
            private readonly EventHandler _onEnter;

            public DataTableSearchBinder(ComboBox cbo, DataView dv, string searchCol, Action onCommitted, bool chonMucDauKhiKhongKhop)
            {
                _cbo = cbo;
                _dv = dv;
                _searchCol = searchCol;
                _onCommitted = onCommitted;
                _chonMucDauKhiKhongKhop = chonMucDauKhiKhongKhop;
                GhiNhoLuaChon();

                _onDropDown = (s, e) => CapNhatDoCaoDropdown(_cbo, _dv.Count);
                _onTextUpdate = (s, e) => XuLyTextUpdate();
                _onDropDownClosed = (s, e) => ApDungLuaChon();
                _onSelectionChangeCommitted = (s, e) => XuLySelectionChangeCommitted();
                _onSelectedIndexChanged = (s, e) =>
                {
                    // Chỉ ghi nhớ khi lựa chọn được gán từ code (không phải lúc người dùng đang gõ lọc)
                    if (!_isFiltering && !_cbo.Focused && string.IsNullOrEmpty(_dv.RowFilter))
                    {
                        GhiNhoLuaChon();
                    }
                };
                _onKeyDown = (s, e) =>
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        e.SuppressKeyPress = true;
                        ApDungLuaChon();
                    }
                    else if (e.KeyCode == Keys.Escape)
                    {
                        e.SuppressKeyPress = true;
                        KhoiPhucDanhSach();
                        _cbo.DroppedDown = false;
                    }
                };
                _onLeave = (s, e) =>
                {
                    ApDungLuaChon();
                    _cbo.SelectionLength = 0;
                };
                _onEnter = (s, e) =>
                {
                    if (string.IsNullOrEmpty(_dv.RowFilter))
                    {
                        GhiNhoLuaChon();
                    }
                    _cbo.BeginInvoke(new Action(() => _cbo.SelectAll()));
                };

                _cbo.DropDown += _onDropDown;
                _cbo.TextUpdate += _onTextUpdate;
                _cbo.DropDownClosed += _onDropDownClosed;
                _cbo.SelectionChangeCommitted += _onSelectionChangeCommitted;
                _cbo.SelectedIndexChanged += _onSelectedIndexChanged;
                _cbo.KeyDown += _onKeyDown;
                _cbo.Leave += _onLeave;
                _cbo.Enter += _onEnter;
            }

            private void GhiNhoLuaChon()
            {
                _chiSoTruoc = _cbo.SelectedIndex;
                _giaTriTruoc = _cbo.SelectedIndex >= 0 ? _cbo.SelectedValue : null;
                _textTruoc = _cbo.Text ?? string.Empty;
            }

            /// <summary>
            /// Không khớp mục nào: combobox bộ lọc chọn lại mục đầu (-- Tất cả --),
            /// combobox nhập liệu khôi phục lựa chọn và nội dung trước đó (không tự chọn bừa mục đầu tiên).
            /// </summary>
            private void XuLyKhongKhop()
            {
                if (_chonMucDauKhiKhongKhop)
                {
                    if (_cbo.Items.Count > 0) _cbo.SelectedIndex = 0;
                    return;
                }

                if (_chiSoTruoc >= 0 && _giaTriTruoc != null)
                {
                    _cbo.SelectedValue = _giaTriTruoc;
                }
                else
                {
                    _cbo.SelectedIndex = -1;
                    if (_cbo.SelectedIndex != -1) _cbo.SelectedIndex = -1; // Khắc phục lỗi WinForms đôi khi cần gán 2 lần với combobox có DataSource
                    _cbo.Text = _textTruoc ?? string.Empty;
                }
            }

            private void XuLyTextUpdate()
            {
                if (_isFiltering) return;
                _isFiltering = true;

                try
                {
                    string text = _cbo.Text;
                    int selStart = _cbo.SelectionStart;

                    if (string.IsNullOrWhiteSpace(text))
                    {
                        _dv.RowFilter = "";
                    }
                    else
                    {
                        string tuKhoa = BoDauTiengViet(text.Trim()).ToLower();
                        string clean = ThoatKyTuLikeRowFilter(tuKhoa);
                        if (!tuKhoa.StartsWith("tat ca") && !tuKhoa.StartsWith("all") && !tuKhoa.StartsWith("--"))
                        {
                            _dv.RowFilter = $"{_searchCol} LIKE '%{clean}%' AND {_searchCol} <> 'tat ca all'";
                        }
                        else
                        {
                            _dv.RowFilter = $"{_searchCol} LIKE '%{clean}%'";
                        }
                    }

                    CapNhatDoCaoDropdown(_cbo, _dv.Count);
                    _cbo.DroppedDown = true;
                    Cursor.Current = Cursors.Default;
                    _cbo.Text = text;
                    _cbo.SelectionStart = selStart;
                    _cbo.SelectionLength = 0;
                }
                catch
                {
                    // Biểu thức lọc lỗi: trả lại toàn bộ danh sách thay vì làm treo combobox
                    _dv.RowFilter = "";
                    CapNhatDoCaoDropdown(_cbo, _dv.Count);
                }
                finally
                {
                    _isFiltering = false;
                }
            }

            private void XuLySelectionChangeCommitted()
            {
                if (_isFiltering) return;
                try
                {
                    object currentVal = _cbo.SelectedValue;
                    if (!string.IsNullOrEmpty(_dv.RowFilter))
                    {
                        _dv.RowFilter = "";
                        CapNhatDoCaoDropdown(_cbo, _dv.Count);
                        if (currentVal != null)
                        {
                            _cbo.SelectedValue = currentVal;
                        }
                    }
                }
                catch { }
                GhiNhoLuaChon();
                _onCommitted?.Invoke();
            }

            private void KhoiPhucDanhSach()
            {
                if (!string.IsNullOrEmpty(_dv.RowFilter))
                {
                    _dv.RowFilter = "";
                    CapNhatDoCaoDropdown(_cbo, _dv.Count);
                }
            }

            private void ApDungLuaChon()
            {
                if (_isFiltering) return;

                bool wasFiltered = !string.IsNullOrEmpty(_dv.RowFilter);
                if (!wasFiltered)
                {
                    _cbo.DroppedDown = false;
                    return;
                }

                _isFiltering = true;

                try
                {
                    if (_dv.Count == 0)
                    {
                        // 1. Không tìm thấy gì mà Enter/Đóng -> Trả lại tất cả.
                        //    Combobox bộ lọc chọn mục đầu tiên (Tất cả); combobox nhập liệu khôi phục lựa chọn trước đó
                        _dv.RowFilter = "";
                        CapNhatDoCaoDropdown(_cbo, _dv.Count);
                        XuLyKhongKhop();
                    }
                    else
                    {
                        // 2. Tìm thấy -> Tự động chọn phần tử đầu tiên nếu người dùng chưa chọn dòng khác
                        if (_cbo.SelectedIndex < 0)
                        {
                            _cbo.SelectedIndex = 0;
                        }

                        object currentVal = _cbo.SelectedValue;

                        // Khôi phục lại toàn bộ danh sách để lần mở sau hiện hết
                        _dv.RowFilter = "";
                        CapNhatDoCaoDropdown(_cbo, _dv.Count);

                        if (currentVal != null)
                        {
                            _cbo.SelectedValue = currentVal;
                        }
                    }
                    GhiNhoLuaChon();
                }
                catch
                {
                    _dv.RowFilter = "";
                    CapNhatDoCaoDropdown(_cbo, _dv.Count);
                    XuLyKhongKhop();
                }
                finally
                {
                    _cbo.DroppedDown = false;
                    _isFiltering = false;
                    _onCommitted?.Invoke();
                    if (!_cbo.Focused)
                    {
                        _cbo.SelectionLength = 0;
                    }
                }
            }

            public void Dispose()
            {
                if (_cbo != null && !_cbo.IsDisposed)
                {
                    _cbo.DropDown -= _onDropDown;
                    _cbo.TextUpdate -= _onTextUpdate;
                    _cbo.DropDownClosed -= _onDropDownClosed;
                    _cbo.SelectionChangeCommitted -= _onSelectionChangeCommitted;
                    _cbo.SelectedIndexChanged -= _onSelectedIndexChanged;
                    _cbo.KeyDown -= _onKeyDown;
                    _cbo.Leave -= _onLeave;
                    _cbo.Enter -= _onEnter;
                }
            }
        }

        /// <summary>
        /// Lớp quản lý vòng đời sự kiện tìm kiếm ComboBox cho nguồn dữ liệu List danh sách chuỗi
        /// </summary>
        private class ListSearchBinder : IDisposable
        {
            private readonly ComboBox _cbo;
            private readonly List<string> _danhSachGoc;
            private readonly Action _onCommitted;
            private readonly bool? _chonMucDauKhiKhongKhop;
            private bool _isFiltering = false;

            // Lựa chọn hợp lệ gần nhất (khôi phục khi gõ từ khóa không khớp mục nào ở combobox nhập liệu)
            private int _chiSoTruoc = -1;
            private string _mucTruoc = null;
            private string _textTruoc = string.Empty;

            private readonly EventHandler _onDropDown;
            private readonly EventHandler _onTextUpdate;
            private readonly EventHandler _onDropDownClosed;
            private readonly EventHandler _onSelectionChangeCommitted;
            private readonly EventHandler _onSelectedIndexChanged;
            private readonly KeyEventHandler _onKeyDown;
            private readonly EventHandler _onLeave;
            private readonly EventHandler _onEnter;

            public ListSearchBinder(ComboBox cbo, List<string> danhSachGoc, Action onCommitted, bool? chonMucDauKhiKhongKhop)
            {
                _cbo = cbo;
                _danhSachGoc = danhSachGoc;
                _onCommitted = onCommitted;
                _chonMucDauKhiKhongKhop = chonMucDauKhiKhongKhop;
                GhiNhoLuaChon();

                _onDropDown = (s, e) => CapNhatDoCaoDropdown(_cbo, _cbo.Items.Count);
                _onTextUpdate = (s, e) => XuLyTextUpdate();
                _onDropDownClosed = (s, e) => ApDungLuaChon();
                _onSelectionChangeCommitted = (s, e) => XuLySelectionChangeCommitted();
                _onSelectedIndexChanged = (s, e) =>
                {
                    // Chỉ ghi nhớ khi lựa chọn được gán từ code (không phải lúc người dùng đang gõ lọc)
                    if (!_isFiltering && !_cbo.Focused && _cbo.Items.Count == _danhSachGoc.Count)
                    {
                        GhiNhoLuaChon();
                    }
                };
                _onKeyDown = (s, e) =>
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        e.SuppressKeyPress = true;
                        ApDungLuaChon();
                    }
                    else if (e.KeyCode == Keys.Escape)
                    {
                        e.SuppressKeyPress = true;
                        KhoiPhucDanhSach();
                        _cbo.DroppedDown = false;
                    }
                };
                _onLeave = (s, e) =>
                {
                    ApDungLuaChon();
                    _cbo.SelectionLength = 0;
                };
                _onEnter = (s, e) =>
                {
                    if (_cbo.Items.Count == _danhSachGoc.Count)
                    {
                        GhiNhoLuaChon();
                    }
                    _cbo.BeginInvoke(new Action(() => _cbo.SelectAll()));
                };

                _cbo.DropDown += _onDropDown;
                _cbo.TextUpdate += _onTextUpdate;
                _cbo.DropDownClosed += _onDropDownClosed;
                _cbo.SelectionChangeCommitted += _onSelectionChangeCommitted;
                _cbo.SelectedIndexChanged += _onSelectedIndexChanged;
                _cbo.KeyDown += _onKeyDown;
                _cbo.Leave += _onLeave;
                _cbo.Enter += _onEnter;
            }

            private void GhiNhoLuaChon()
            {
                _chiSoTruoc = _cbo.SelectedIndex;
                _mucTruoc = _cbo.SelectedIndex >= 0 ? _cbo.SelectedItem?.ToString() : null;
                _textTruoc = _cbo.Text ?? string.Empty;
            }

            /// <summary>
            /// Không khớp mục nào: combobox bộ lọc (mục đầu dạng "-- Tất cả --") chọn lại mục đầu,
            /// combobox nhập liệu khôi phục lựa chọn và nội dung trước đó (không tự chọn bừa mục đầu tiên).
            /// </summary>
            private void XuLyKhongKhop()
            {
                bool chonMucDau = _chonMucDauKhiKhongKhop ?? (_danhSachGoc.Count > 0 && LaMucTatCa(_danhSachGoc[0]));
                if (chonMucDau)
                {
                    if (_cbo.Items.Count > 0) _cbo.SelectedIndex = 0;
                    return;
                }

                if (_chiSoTruoc >= 0 && !string.IsNullOrEmpty(_mucTruoc) && _cbo.Items.Contains(_mucTruoc))
                {
                    _cbo.SelectedItem = _mucTruoc;
                }
                else
                {
                    _cbo.SelectedIndex = -1;
                    _cbo.Text = _textTruoc ?? string.Empty;
                }
            }

            private void XuLyTextUpdate()
            {
                if (_isFiltering) return;
                _isFiltering = true;

                bool dangCapNhat = false;
                try
                {
                    string text = _cbo.Text;
                    int selStart = _cbo.SelectionStart;

                    _cbo.BeginUpdate();
                    dangCapNhat = true;
                    _cbo.Items.Clear();

                    if (string.IsNullOrWhiteSpace(text))
                    {
                        _cbo.Items.AddRange(_danhSachGoc.ToArray());
                    }
                    else
                    {
                        string textClean = BoDauTiengViet(text.Trim()).ToLower();
                        var matched = _danhSachGoc.Where(item =>
                        {
                            if ((item.StartsWith("--") || item.ToLower().Contains("tất cả") || item.ToLower().Contains("tat ca"))
                                && !textClean.StartsWith("tat ca") && !textClean.StartsWith("all") && !textClean.StartsWith("--"))
                                return false;
                            return item.IndexOf(text.Trim(), StringComparison.OrdinalIgnoreCase) >= 0 ||
                                   BoDauTiengViet(item).IndexOf(textClean, StringComparison.OrdinalIgnoreCase) >= 0;
                        }).ToArray();
                        _cbo.Items.AddRange(matched);
                    }

                    _cbo.EndUpdate();
                    dangCapNhat = false;
                    CapNhatDoCaoDropdown(_cbo, _cbo.Items.Count);
                    _cbo.DroppedDown = true;
                    Cursor.Current = Cursors.Default;

                    _cbo.Text = text;
                    _cbo.SelectionStart = selStart;
                    _cbo.SelectionLength = 0;
                }
                finally
                {
                    if (dangCapNhat) _cbo.EndUpdate();
                    _isFiltering = false;
                }
            }

            private void XuLySelectionChangeCommitted()
            {
                if (_isFiltering) return;
                try
                {
                    string selectedText = _cbo.SelectedItem?.ToString();
                    if (_cbo.Items.Count != _danhSachGoc.Count)
                    {
                        _cbo.BeginUpdate();
                        _cbo.Items.Clear();
                        _cbo.Items.AddRange(_danhSachGoc.ToArray());
                        _cbo.EndUpdate();
                        CapNhatDoCaoDropdown(_cbo, _cbo.Items.Count);
                        if (!string.IsNullOrEmpty(selectedText) && _danhSachGoc.Contains(selectedText))
                        {
                            _cbo.SelectedItem = selectedText;
                        }
                    }
                }
                catch { }
                GhiNhoLuaChon();
                _onCommitted?.Invoke();
            }

            private void KhoiPhucDanhSach()
            {
                if (_cbo.Items.Count != _danhSachGoc.Count)
                {
                    _cbo.BeginUpdate();
                    _cbo.Items.Clear();
                    _cbo.Items.AddRange(_danhSachGoc.ToArray());
                    _cbo.EndUpdate();
                    CapNhatDoCaoDropdown(_cbo, _cbo.Items.Count);
                }
            }

            private void ApDungLuaChon()
            {
                if (_isFiltering) return;

                bool wasFiltered = (_cbo.Items.Count != _danhSachGoc.Count);
                if (!wasFiltered)
                {
                    _cbo.DroppedDown = false;
                    return;
                }

                _isFiltering = true;

                try
                {
                    if (_cbo.Items.Count == 0)
                    {
                        // 1. Không tìm thấy gì mà Enter -> Trả lại tất cả.
                        //    Combobox bộ lọc chọn mục đầu tiên (Tất cả); combobox nhập liệu khôi phục lựa chọn trước đó
                        _cbo.BeginUpdate();
                        _cbo.Items.Clear();
                        _cbo.Items.AddRange(_danhSachGoc.ToArray());
                        _cbo.EndUpdate();
                        CapNhatDoCaoDropdown(_cbo, _cbo.Items.Count);
                        XuLyKhongKhop();
                    }
                    else
                    {
                        // 2. Tìm thấy -> Tự động chọn phần tử đầu tiên nếu chưa chọn
                        if (_cbo.SelectedIndex < 0)
                        {
                            _cbo.SelectedIndex = 0;
                        }

                        string selectedVal = _cbo.SelectedItem?.ToString();

                        _cbo.BeginUpdate();
                        _cbo.Items.Clear();
                        _cbo.Items.AddRange(_danhSachGoc.ToArray());
                        _cbo.EndUpdate();
                        CapNhatDoCaoDropdown(_cbo, _cbo.Items.Count);

                        if (!string.IsNullOrEmpty(selectedVal) && _danhSachGoc.Contains(selectedVal))
                        {
                            _cbo.SelectedItem = selectedVal;
                        }
                    }
                    GhiNhoLuaChon();
                }
                catch
                {
                    _cbo.BeginUpdate();
                    _cbo.Items.Clear();
                    _cbo.Items.AddRange(_danhSachGoc.ToArray());
                    _cbo.EndUpdate();
                    CapNhatDoCaoDropdown(_cbo, _cbo.Items.Count);
                    XuLyKhongKhop();
                }
                finally
                {
                    _cbo.DroppedDown = false;
                    _isFiltering = false;
                    _onCommitted?.Invoke();
                    if (!_cbo.Focused)
                    {
                        _cbo.SelectionLength = 0;
                    }
                }
            }

            public void Dispose()
            {
                if (_cbo != null && !_cbo.IsDisposed)
                {
                    _cbo.DropDown -= _onDropDown;
                    _cbo.TextUpdate -= _onTextUpdate;
                    _cbo.DropDownClosed -= _onDropDownClosed;
                    _cbo.SelectionChangeCommitted -= _onSelectionChangeCommitted;
                    _cbo.SelectedIndexChanged -= _onSelectedIndexChanged;
                    _cbo.KeyDown -= _onKeyDown;
                    _cbo.Leave -= _onLeave;
                    _cbo.Enter -= _onEnter;
                }
            }
        }

        /// <summary>
        /// Cài đặt tìm kiếm cho ComboBox có nguồn dữ liệu DataTable (sử dụng DataView để lọc trực tiếp các phần tử trong ComboBox).
        /// </summary>
        /// <param name="chonMucDauKhiKhongKhop">
        /// Hành vi khi từ khóa gõ vào không khớp mục nào (Enter/rời ô): true = chọn mục đầu tiên; false = khôi phục lựa chọn trước đó.
        /// Mặc định (null) tự nhận biết: mục đầu là "-- Tất cả ... --" (combobox bộ lọc) thì chọn mục đầu, ngược lại khôi phục lựa chọn trước đó.
        /// </param>
        public static void CaiDatTimKiem(ComboBox cbo, DataTable dt, string displayCol, string valueCol, Action onCommitted = null, bool? chonMucDauKhiKhongKhop = null)
        {
            if (cbo == null || dt == null) return;

            // Dọn dẹp binder cũ nếu có
            if (_binders.TryGetValue(cbo, out IDisposable oldBinder))
            {
                oldBinder?.Dispose();
                _binders.Remove(cbo);
            }

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
                if (textVal.StartsWith("--") || (valStr == "0" && (textVal.StartsWith("--") || textVal.ToLower().Contains("tất cả") || textVal.ToLower().Contains("tat ca"))) || textVal.ToLower().Contains("tất cả") || textVal.ToLower().Contains("tat ca"))
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

            bool chonMucDau = chonMucDauKhiKhongKhop ?? (dt.Rows.Count > 0 && dt.Rows[0][searchCol]?.ToString() == "tat ca all");
            DataTableSearchBinder binder = new DataTableSearchBinder(cbo, dv, searchCol, onCommitted, chonMucDau);
            _binders.Add(cbo, binder);
        }

        /// <summary>
        /// Cài đặt tìm kiếm cho ComboBox dạng danh sách chuỗi Items (Unbound).
        /// </summary>
        /// <param name="chonMucDauKhiKhongKhop">
        /// Hành vi khi từ khóa gõ vào không khớp mục nào (Enter/rời ô): true = chọn mục đầu tiên; false = khôi phục lựa chọn trước đó.
        /// Mặc định (null) tự nhận biết: mục đầu là "-- Tất cả ... --" (combobox bộ lọc) thì chọn mục đầu, ngược lại khôi phục lựa chọn trước đó.
        /// </param>
        public static void CaiDatTimKiem(ComboBox cbo, List<string> danhSachGoc, Action onCommitted = null, bool? chonMucDauKhiKhongKhop = null)
        {
            if (cbo == null || danhSachGoc == null) return;

            // Dọn dẹp binder cũ nếu có
            if (_binders.TryGetValue(cbo, out IDisposable oldBinder))
            {
                oldBinder?.Dispose();
                _binders.Remove(cbo);
            }

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

            ListSearchBinder binder = new ListSearchBinder(cbo, danhSachGoc, onCommitted, chonMucDauKhiKhongKhop);
            _binders.Add(cbo, binder);
        }
    }
}
