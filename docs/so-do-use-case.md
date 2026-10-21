# SƠ ĐỒ USE CASE & MA TRẬN PHÂN QUYỀN HỆ THỐNG (UML)
## Website Quản Lý Kho Hàng Phú Quốc (`QuanLyKhoPhuQuocLTW`)

> **Mục đích tài liệu**: Chuẩn hóa toàn bộ mô hình hóa chức năng hệ thống theo chuẩn UML 2.5 bằng Mermaid, cung cấp hình ảnh chất lượng cao phục vụ Báo cáo học thuật LaTeX (Mục 2.1), bảo vệ đồ án và tài liệu vận hành.

---

## 1. Biểu Đồ Use Case Tổng Quan Cấp Hệ Thống

```mermaid
flowchart LR
    classDef actor fill:#f8fafc,stroke:#1e293b,stroke-width:1.5px,color:#0f172a,font-weight:bold;
    classDef usecase fill:#ffffff,stroke:#0284c7,stroke-width:1.3px,color:#0f172a;
    classDef boundary fill:#f1f5f9,stroke:#64748b,stroke-width:1.5px,stroke-dasharray: 4 4;

    Admin["👤 Quản trị viên<br/>(Admin)"]:::actor
    Thukho["👤 Thủ kho<br/>(Warehouse Manager)"]:::actor
    Nhanvien["👤 Nhân viên kho / Bán hàng<br/>(Staff / Sales)"]:::actor

    %% Generalization (Kế thừa tác nhân)
    Admin -. "kế thừa" .-> Thukho
    Thukho -. "kế thừa" .-> Nhanvien

    subgraph Sys ["HỆ THỐNG QUẢN LÝ KHO HÀNG ĐA CHI NHÁNH (QuanLyKhoPhuQuocLTW)"]
        UC_Auth(["Đăng nhập & Chọn kho làm việc"]):::usecase
        UC_Sys(["Quản trị Người dùng & Phân quyền Kho"]):::usecase
        UC_Cat(["Quản lý Danh mục Hàng hóa & Giá"]):::usecase
        UC_Logistics(["Giao dịch Kho (Nhập / Trả / Hủy)"]):::usecase
        UC_TransferAudit(["Điều chuyển Kho & Kiểm kê Cân bằng"]):::usecase
        UC_Report(["Báo cáo Tồn kho Đa kho & Thẻ kho"]):::usecase
        UC_Sales(["Lập đơn bán & Xuất hàng"]):::usecase
    end

    %% Associations
    Nhanvien --- UC_Auth
    Nhanvien --- UC_Sales
    Nhanvien --- UC_Cat

    Thukho --- UC_Logistics
    Thukho --- UC_TransferAudit
    Thukho --- UC_Report

    Admin --- UC_Sys
```

---

## 2. Biểu Đồ Phân Hệ 1: Xác Thực & Quản Trị Hệ Thống

```mermaid
flowchart LR
    classDef actor fill:#f8fafc,stroke:#1e293b,stroke-width:1.5px,color:#0f172a,font-weight:bold;
    classDef usecase fill:#ffffff,stroke:#0284c7,stroke-width:1.3px,color:#0f172a;
    classDef subusecase fill:#f8fafc,stroke:#64748b,stroke-width:1px,color:#334155,stroke-dasharray: 3 3;

    Admin["👤 Quản trị viên"]:::actor
    User["👤 Người dùng hệ thống"]:::actor

    Admin -. "kế thừa" .-> User

    subgraph Subsys1 ["PHÂN HỆ XÁC THỰC & QUẢN TRỊ HỆ THỐNG"]
        UC11(["Đăng nhập hệ thống"]):::usecase
        UC12(["Lựa chọn Chi nhánh Kho làm việc"]):::usecase
        UC13(["Đổi mật khẩu cá nhân"]):::usecase
        UC14(["Quản lý Người dùng & Nhân viên"]):::usecase
        UC15(["Phân quyền Chi nhánh cho Tài khoản"]):::usecase
        UC16(["Quản lý Danh mục Chi nhánh Kho"]):::usecase
        UC17(["Xem Nhật ký thao tác (Audit Log)"]):::usecase

        UC_Verify(["Xác thực mật khẩu & Khóa tài khoản"]):::subusecase
        UC_Claims(["Gán Cookie & Claims Kho hiện hành"]):::subusecase
        UC_Toggle(["Khóa / Kích hoạt Chi nhánh Kho"]):::subusecase
    end

    User --- UC11
    User --- UC12
    User --- UC13

    Admin --- UC14
    Admin --- UC15
    Admin --- UC16
    Admin --- UC17

    %% Include & Extend
    UC11 -. "<<include>>" .-> UC_Verify
    UC11 -. "<<include>>" .-> UC_Claims
    UC14 -. "<<include>>" .-> UC15
    UC_Toggle -. "<<extend>>" .-> UC16
```

---

## 3. Biểu Đồ Phân Hệ 2: Quản Lý Danh Mục & Giá Hàng Hóa

```mermaid
flowchart LR
    classDef actor fill:#f8fafc,stroke:#1e293b,stroke-width:1.5px,color:#0f172a,font-weight:bold;
    classDef usecase fill:#ffffff,stroke:#0284c7,stroke-width:1.3px,color:#0f172a;
    classDef subusecase fill:#f8fafc,stroke:#64748b,stroke-width:1px,color:#334155,stroke-dasharray: 3 3;

    Admin["👤 Quản trị viên"]:::actor
    Thukho["👤 Thủ kho"]:::actor

    Admin --- UC22
    Admin --- UC23
    Admin --- UC24

    Thukho --- UC21
    Thukho --- UC25

    subgraph Subsys2 ["PHÂN HỆ QUẢN LÝ DANH MỤC & GIÁ"]
        UC21(["Quản lý Danh mục Hàng hóa"]):::usecase
        UC22(["Quản lý Nhóm hàng & Đơn vị tính"]):::usecase
        UC23(["Quản lý Nhà cung cấp"]):::usecase
        UC24(["Quản lý Khách hàng"]):::usecase
        UC25(["Cấu hình Định mức Tồn kho Min/Max"]):::usecase

        UC_SKU(["Kiểm tra duy nhất Mã SKU / Barcode"]):::subusecase
        UC_Excel(["Nhập / Xuất dữ liệu Excel"]):::subusecase
        UC_Barcode(["In Tem Mã vạch / Barcode"]):::subusecase
    end

    UC21 -. "<<include>>" .-> UC_SKU
    UC21 -. "<<include>>" .-> UC25
    UC_Excel -. "<<extend>>" .-> UC21
    UC_Barcode -. "<<extend>>" .-> UC21
```

---

## 4. Biểu Đồ Phân Hệ 3: Nghiệp Vụ Biến Động Kho (Nhập / Trả / Hủy)

```mermaid
flowchart LR
    classDef actor fill:#f8fafc,stroke:#1e293b,stroke-width:1.5px,color:#0f172a,font-weight:bold;
    classDef usecase fill:#ffffff,stroke:#0284c7,stroke-width:1.3px,color:#0f172a;
    classDef subusecase fill:#f8fafc,stroke:#64748b,stroke-width:1px,color:#334155,stroke-dasharray: 3 3;

    Thukho["👤 Thủ kho"]:::actor
    Admin["👤 Quản trị viên"]:::actor

    Thukho --- UC31
    Thukho --- UC32
    Thukho --- UC33

    Admin --- UC31
    Admin --- UC32
    Admin --- UC33

    subgraph Subsys3 ["PHÂN HỆ BIẾN ĐỘNG KHO (NHẬP / TRẢ / HỦY)"]
        UC31(["Lập Phiếu Nhập hàng từ NCC"]):::usecase
        UC32(["Lập Phiếu Trả hàng nhập"]):::usecase
        UC33(["Lập Phiếu Xuất hủy"]):::usecase

        UC_F3(["Tìm kiếm & Quét Barcode (F3)"]):::subusecase
        UC_MAC(["Tính Giá vốn Bình quân Gia quyền"]):::subusecase
        UC_Card(["Ghi sổ Thẻ kho & Biến động tồn"]):::subusecase
        UC_Print(["In Chứng từ / Xuất PDF"]):::subusecase
    end

    UC31 -. "<<include>>" .-> UC_F3
    UC31 -. "<<include>>" .-> UC_MAC
    UC31 -. "<<include>>" .-> UC_Card

    UC32 -. "<<include>>" .-> UC_F3
    UC32 -. "<<include>>" .-> UC_Card

    UC33 -. "<<include>>" .-> UC_F3
    UC33 -. "<<include>>" .-> UC_Card

    UC_Print -. "<<extend>>" .-> UC31
    UC_Print -. "<<extend>>" .-> UC32
    UC_Print -. "<<extend>>" .-> UC33
```

---

## 5. Biểu Đồ Phân Hệ 4: Điều Chuyển Kho & Kiểm Kê Cân Bằng

```mermaid
flowchart LR
    classDef actor fill:#f8fafc,stroke:#1e293b,stroke-width:1.5px,color:#0f172a,font-weight:bold;
    classDef usecase fill:#ffffff,stroke:#0284c7,stroke-width:1.3px,color:#0f172a;
    classDef subusecase fill:#f8fafc,stroke:#64748b,stroke-width:1px,color:#334155,stroke-dasharray: 3 3;

    Thukho["👤 Thủ kho"]:::actor
    Nhanvien["👤 Nhân viên kho"]:::actor

    Thukho --- UC41
    Thukho --- UC42
    Thukho --- UC43
    Thukho --- UC45

    Nhanvien --- UC44

    subgraph Subsys4 ["PHÂN HỆ ĐIỀU CHUYỂN KHO & KIỂM KÊ CÂN BẰNG"]
        UC41(["Lập Phiếu Chuyển kho"]):::usecase
        UC42(["Xác nhận Nhận hàng Chuyển"]):::usecase
        UC43(["Lập Phiếu Kiểm kê Kho"]):::usecase
        UC44(["Nhập Số lượng Kiểm đếm Thực tế"]):::usecase
        UC45(["Thực thi Cân bằng Kho (Transaction)"]):::usecase

        UC_CheckAvail(["Kiểm tra Tồn kho Nguồn khả dụng"]):::subusecase
        UC_Adjust(["Tự động sinh Phiếu Điều chỉnh"]):::subusecase
        UC_Lock(["Khóa Phiếu Kiểm kê vĩnh viễn"]):::subusecase
        UC_BlankPrint(["In Phiếu Kiểm đếm trắng"]):::subusecase
    end

    UC41 -. "<<include>>" .-> UC_CheckAvail
    UC43 -. "<<include>>" .-> UC44
    UC45 -. "<<include>>" .-> UC_Adjust
    UC45 -. "<<include>>" .-> UC_Lock
    UC_BlankPrint -. "<<extend>>" .-> UC43
```

---

## 6. Biểu Đồ Phân Hệ 5: Báo Cáo & Thống Kê Tồn Kho

```mermaid
flowchart LR
    classDef actor fill:#f8fafc,stroke:#1e293b,stroke-width:1.5px,color:#0f172a,font-weight:bold;
    classDef usecase fill:#ffffff,stroke:#0284c7,stroke-width:1.3px,color:#0f172a;
    classDef subusecase fill:#f8fafc,stroke:#64748b,stroke-width:1px,color:#334155,stroke-dasharray: 3 3;

    Admin["👤 Quản trị viên"]:::actor
    Thukho["👤 Thủ kho"]:::actor

    Admin --- UC51
    Admin --- UC52
    Admin --- UC53

    Thukho --- UC51
    Thukho --- UC52
    Thukho --- UC53

    subgraph Subsys5 ["PHÂN HỆ BÁO CÁO & THỐNG KÊ TỒN KHO"]
        UC51(["Xem Báo cáo Tồn kho Pivot Đa kho"]):::usecase
        UC52(["Xem Báo cáo Thẻ kho Chi tiết"]):::usecase
        UC53(["Giám sát Cảnh báo Tồn Min / Max"]):::usecase

        UC_Filter(["Lọc đa chiều (Kho, Nhóm, Ngày, F3)"]):::subusecase
        UC_CalcRunning(["Tính Tồn Lũy kế Tịnh tiến"]):::subusecase
        UC_ExcelReport(["Xuất Báo cáo Excel (ClosedXML)"]):::subusecase
        UC_PrintReport(["In Báo cáo trực tiếp"]):::subusecase
    end

    UC51 -. "<<include>>" .-> UC_Filter
    UC52 -. "<<include>>" .-> UC_CalcRunning
    UC52 -. "<<include>>" .-> UC_Filter
    UC_ExcelReport -. "<<extend>>" .-> UC51
    UC_ExcelReport -. "<<extend>>" .-> UC52
    UC_PrintReport -. "<<extend>>" .-> UC51
    UC_PrintReport -. "<<extend>>" .-> UC52
```
