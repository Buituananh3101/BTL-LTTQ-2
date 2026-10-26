# Tài Liệu Thiết Kế Luồng Xử Lý Nghiệp Vụ (Activity Diagrams)
## Hệ Thống Quản Lý Kho Hàng Phú Quốc (`QuanLyKhoPhuQuocLTW`)

> **Mục đích tài liệu**: Chuẩn hóa toàn bộ các luồng hoạt động nghiệp vụ cốt lõi theo chuẩn UML 2.5 với phân làn trách nhiệm (Swimlanes 3 tầng: *Client/UI* - *Web App/Service* - *Database/Transaction*), phục vụ báo cáo học thuật LaTeX, bảo vệ đồ án và tham chiếu mã nguồn.

---

## 1. Quy Trình Nhập Hàng Từ Nhà Cung Cấp & Cập Nhật Giá Vốn (MAC)

### 1.1. Biểu đồ hoạt động (Mermaid)

```mermaid
flowchart TD
    subgraph User["1. Thủ kho / Giao diện người dùng (Client UI)"]
        A1([Bắt đầu]) --> A2["Mở form 'Tạo phiếu nhập hàng'"]
        A2 --> A3["Chọn Nhà cung cấp & Chi nhánh kho nhập"]
        A3 --> A4["F3 Tìm kiếm mặt hàng, nhập SL & Đơn giá nhập"]
        A4 --> A5["Bấm nút 'Hoàn thành'"]
    end

    subgraph System["2. Web App / Logic Xử lý (Backend Service)"]
        B1["Nhận Request POST Create"]
        B2{"Kiểm tra tính hợp lệ?<br>(Có NCC, Chi tiết > 0 dòng, SL > 0)"}
        B3["Trả về thông báo lỗi Validation"]
        B4["Mở Transaction CSDL (BeginTransactionAsync)"]
        B5["Tính lại Giá vốn bình quân gia quyền (MAC):<br>GiaVonMoi = (TonCu*GiaCu + SL*GiaNhap) / (TonCu + SL)"]
        B6["Tạo bản ghi PhieuNhap & ChiTietPhieuNhap (DA_XAC_NHAN)"]
        B7["Cập nhật công nợ phải trả Nhà cung cấp"]
        B8["Ghi Nhật ký thao tác (Audit Log)"]
        B9["Commit Transaction"]
        B10["Trả về kết quả thành công & Điều hướng danh sách"]
    end

    subgraph Database["3. Cơ sở dữ liệu & Tồn kho (Database Engine)"]
        C1[(Bảng PhieuNhap & ChiTietPhieuNhap)]
        C2[(Bảng HangHoa - Cập nhật GiaVon)]
        C3[(View vw_TonKhoHienTai - Tăng tồn kho)]
        C4[(Bảng NhatKyThaoTac)]
    end

    A5 --> B1
    B1 --> B2
    B2 -- Không hợp lệ --> B3
    B3 --> A4
    B2 -- Hợp lệ --> B4
    B4 --> B5
    B5 --> B6
    B6 --> B7
    B7 --> B8
    B8 --> B9
    B6 -.-> C1
    B5 -.-> C2
    B6 -.-> C3
    B8 -.-> C4
    B9 --> B10
    B10 --> A6([Kết thúc: Nhập hàng thành công])
```

---

## 2. Quy Trình Điều Chuyển Hàng Hóa Liên Chi Nhánh (2 Chặng)

### 2.1. Biểu đồ hoạt động (Mermaid)

```mermaid
flowchart TD
    subgraph User["1. Thủ kho / Giao diện người dùng (Client UI)"]
        T1([Bắt đầu]) --> T2["Mở form 'Tạo phiếu chuyển kho'"]
        T2 --> T3["Chọn Kho xuất (nguồn) & Kho nhận (đích)"]
        T3 --> T4["F3 Thêm mặt hàng & Nhập số lượng cần chuyển"]
        T4 --> T5["Bấm nút 'Chuyển hàng'"]
        T8["Thủ kho nhận: Kiểm đếm thực tế khi hàng đến"]
        T9["Nhập số lượng thực nhận & Ghi chú hao hụt"]
        T10["Bấm nút 'Xác nhận nhận hàng'"]
    end

    subgraph System["2. Web App / Logic Xử lý (Backend Service)"]
        S1["Kiểm tra tồn khả dụng tại Kho xuất"]
        S2{"Tồn khả dụng >= SL chuyển?"}
        S3["Báo lỗi không đủ tồn kho khả dụng"]
        S4["Mở Transaction Chặng 1 (Xuất chuyển)"]
        S5["Lưu PhieuChuyenKho: Trạng thái DANG_CHUYEN"]
        S6["Trừ tồn kho xuất qua View tồn kho"]
        S7["Commit Transaction Chặng 1"]
        S8["Nhận Request Xác nhận nhận hàng"]
        S9["Mở Transaction Chặng 2 (Nhận chuyển)"]
        S10["Ghi nhận SL thực nhận & Hao hụt thất thoát"]
        S11["Cộng tồn kho nhận qua View tồn kho"]
        S12["Cập nhật trạng thái phiếu: HOAN_THANH"]
        S13["Commit Transaction Chặng 2"]
    end

    subgraph Database["3. Cơ sở dữ liệu & Tồn kho (Database Engine)"]
        D1[(vw_TonKhoHienTai - Trừ tồn Kho xuất)]
        D2[(PhieuChuyenKho - DANG_CHUYEN)]
        D3[(vw_TonKhoHienTai - Cộng tồn Kho nhận)]
        D4[(ChiTietPhieuChuyenKho - Ghi nhận Hao hụt)]
        D5[(PhieuChuyenKho - HOAN_THANH)]
    end

    T5 --> S1
    S1 --> S2
    S2 -- Không đủ --> S3
    S3 --> T4
    S2 -- Đủ tồn --> S4
    S4 --> S5
    S5 --> S6
    S6 -.-> D1
    S5 -.-> D2
    S6 --> S7
    S7 --> T8
    T8 --> T9
    T9 --> T10
    T10 --> S8
    S8 --> S9
    S9 --> S10
    S10 --> S11
    S11 --> S12
    S10 -.-> D4
    S11 -.-> D3
    S12 -.-> D5
    S12 --> S13
    S13 --> T11([Kết thúc: Điều chuyển hoàn tất])
```

---

## 3. Quy Trình Kiểm Kê & Tự Động Cân Bằng Kho

### 3.1. Biểu đồ hoạt động (Mermaid)

```mermaid
flowchart TD
    subgraph User["1. Thủ kho / Giao diện người dùng (Client UI)"]
        K1([Bắt đầu]) --> K2["Vào phân hệ 'Kiểm kho' -> '+ Tạo phiếu kiểm'"]
        K2 --> K3["Chọn Chi nhánh kho kiểm kê"]
        K5["Thực hiện đếm thực tế ngoài thực địa kho"]
        K6["Nhập 'Số lượng thực tế' vào từng dòng kiểm"]
        K8{"Người dùng chọn thao tác?"}
        K9["Bấm nút 'Lưu tạm'"]
        K10["Bấm nút 'Cân bằng kho'"]
    end

    subgraph System["2. Web App / Logic Xử lý (Backend Service)"]
        P1["Truy vấn View vw_TonKhoHienTai"]
        P2["Hiển thị danh sách mặt hàng & Tồn sổ sách"]
        P3["Tự động tính Chênh lệch (Thừa / Thiếu) và Giá trị lệch"]
        P4["Mở Transaction: Lưu PhieuKiemKe"]
        P5["Gán trạng thái PHIEU_TAM<br>(Giữ nguyên tồn kho, cho phép sửa tiếp)"]
        P6["Mở Transaction Cân bằng kho (BeginTransactionAsync)"]
        P7["Tự động sinh PhieuDieuChinh trạng thái DA_DUYET"]
        P8["Tạo ChiTietPhieuDieuChinh cho các dòng chênh lệch"]
        P9["Cập nhật trạng thái PhieuKiemKe: DA_CAN_BANG"]
        P10["Khóa cứng phiếu kiểm vĩnh viễn (Cấm sửa / xóa)"]
        P11["Commit Transaction Cân bằng"]
    end

    subgraph Database["3. Cơ sở dữ liệu & Tồn kho (Database Engine)"]
        Q1[(View vw_TonKhoHienTai)]
        Q2[(Bảng PhieuKiemKe - PHIEU_TAM)]
        Q3[(Bảng PhieuDieuChinh & ChiTiet - DA_DUYET)]
        Q4[(View vw_TonKhoHienTai - Cân bằng 100%)]
        Q5[(Bảng NhatKyThaoTac)]
    end

    K3 --> P1
    P1 -.-> Q1
    P1 --> P2
    P2 --> K5
    K5 --> K6
    K6 --> P3
    P3 --> K8
    K8 -- Lưu tạm --> K9
    K9 --> P4
    P4 --> P5
    P5 -.-> Q2
    P5 --> K11([Lưu tạm thành công: Phiếu ở trạng thái tạm])
    K8 -- Cân bằng kho --> K10
    K10 --> P6
    P6 --> P7
    P7 --> P8
    P8 --> P9
    P9 --> P10
    P7 -.-> Q3
    P8 -.-> Q4
    P10 -.-> Q5
    P10 --> P11
    P11 --> K12([Hoàn tất: Tồn sổ sách khớp Tồn thực tế])
```

---

## 4. Quy Trình Xuất Hủy Hàng Hỏng & Hết Hạn Sử Dụng

### 4.1. Biểu đồ hoạt động (Mermaid)

```mermaid
flowchart TD
    subgraph User["1. Thủ kho / Giao diện người dùng (Client UI)"]
        H1([Bắt đầu]) --> H2["Mở form 'Tạo phiếu xuất kho'"]
        H2 --> H3["Chọn Chi nhánh & Phân loại: XUAT_HUY"]
        H3 --> H4["F3 Thêm mặt hàng hỏng / hết date & Nhập SL"]
        H4 --> H5["Nhập lý do xuất hủy (Bể vỡ, rỉ sét, ẩm mốc...)"]
        H5 --> H6["Bấm 'Hoàn thành xuất hủy'"]
    end

    subgraph System["2. Web App / Logic Xử lý (Backend Service)"]
        M1["Nhận Request POST Xuất hủy"]
        M2{"Kiểm tra tồn khả dụng tại kho?<br>(Tồn hiện tại >= SL hủy)"}
        M3["Báo lỗi: Số lượng xuất hủy vượt quá tồn kho"]
        M4["Mở Transaction CSDL (BeginTransactionAsync)"]
        M5["Tạo PhieuXuat (LoaiXuat = 'XUAT_HUY', DA_XAC_NHAN)"]
        M6["Tạo ChiTietPhieuXuat (Ghi nhận đơn giá vốn và thành tiền)"]
        M7["Ghi Thẻ kho dòng Xuất hủy (Số lượng âm)"]
        M8["Ghi Nhật ký thao tác (Audit Log)"]
        M9["Commit Transaction"]
        M10["Trả về kết quả thành công & Cập nhật danh sách"]
    end

    subgraph Database["3. Cơ sở dữ liệu & Tồn kho (Database Engine)"]
        N1[(View vw_TonKhoHienTai - Trừ tồn kho)]
        N2[(Bảng PhieuXuat & ChiTietPhieuXuat)]
        N3[(Bảng NhatKyThaoTac)]
    end

    H6 --> M1
    M1 --> M2
    M2 -- Không đủ tồn --> M3
    M3 --> H4
    M2 -- Đủ tồn --> M4
    M4 --> M5
    M5 --> M6
    M6 --> M7
    M7 --> M8
    M6 -.-> N2
    M7 -.-> N1
    M8 -.-> N3
    M8 --> M9
    M9 --> M10
    M10 --> H7([Kết thúc: Hàng hỏng đã được trừ khỏi kho])
```

---

## 5. Quy Trình Trả Hàng Nhập Cho Nhà Cung Cấp

### 5.1. Biểu đồ hoạt động (Mermaid)

```mermaid
flowchart TD
    subgraph User["1. Thủ kho / Giao diện người dùng (Client UI)"]
        R1([Bắt đầu]) --> R2["Vào phân hệ 'Trả hàng nhập' -> '+ Tạo phiếu'"]
        R2 --> R3["Chọn Nhà cung cấp hoặc Phiếu nhập gốc"]
        R3 --> R4["Chọn mặt hàng cần trả & Nhập số lượng trả"]
        R4 --> R5["Chọn hình thức nhận lại: Giảm công nợ / Tiền mặt"]
        R5 --> R6["Bấm nút 'Hoàn thành trả hàng'"]
    end

    subgraph System["2. Web App / Logic Xử lý (Backend Service)"]
        E1["Nhận Request POST Trả hàng nhập"]
        E2{"Kiểm tra tồn kho khả dụng tại chi nhánh?"}
        E3["Báo lỗi: Tồn kho không đủ để xuất trả NCC"]
        E4["Mở Transaction CSDL (BeginTransactionAsync)"]
        E5["Tạo PhieuTraHangNhap & ChiTietPhieuTraHangNhap"]
        E6["Trừ tồn kho chi nhánh qua View vw_TonKhoHienTai"]
        E7["Sinh dòng Thẻ kho (Loại: Trả hàng nhập)"]
        E8["Cập nhật giảm trừ công nợ phải trả NCC"]
        E9["Ghi Nhật ký thao tác (Audit Log)"]
        E10["Commit Transaction"]
        E11["Trả về kết quả & In phiếu trả hàng"]
    end

    subgraph Database["3. Cơ sở dữ liệu & Tồn kho (Database Engine)"]
        F1[(Bảng PhieuTraHangNhap & ChiTiet)]
        F2[(View vw_TonKhoHienTai - Giảm tồn kho)]
        F3[(Bảng DonViCapHang - Giảm nợ)]
        F4[(Bảng NhatKyThaoTac)]
    end

    R6 --> E1
    E1 --> E2
    E2 -- Không đủ tồn --> E3
    E3 --> R4
    E2 -- Đủ tồn --> E4
    E4 --> E5
    E5 --> E6
    E6 --> E7
    E7 --> E8
    E8 --> E9
    E5 -.-> F1
    E6 -.-> F2
    E8 -.-> F3
    E9 -.-> F4
    E9 --> E10
    E10 --> E11
    E11 --> R7([Kết thúc: Trả hàng hoàn tất & Cân đối công nợ])
```
