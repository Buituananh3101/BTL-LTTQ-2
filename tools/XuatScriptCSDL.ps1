# =========================================================================================
# XUẤT SCRIPT TẠO CSDL ĐẦY ĐỦ (cấu trúc + dữ liệu mẫu) RA docs/TaoCSDL_DayDu.sql
# Dùng SMO đi kèm SQL Server. Chạy trên máy đã có CSDL QuanLyKhoPhuQuocLTTQ hoàn chỉnh:
#   powershell -ExecutionPolicy Bypass -File tools\XuatScriptCSDL.ps1
# Người khác chỉ cần mở docs/TaoCSDL_DayDu.sql trong SSMS và chạy (F5) là có CSDL để chạy app.
# =========================================================================================
param(
    [string]$Server = "localhost\SQLEXPRESS",
    [string]$Database = "QuanLyKhoPhuQuocLTTQ",
    [string]$Out = "$PSScriptRoot\..\docs\TaoCSDL_DayDu.sql"
)
$ErrorActionPreference = 'Stop'
[void][System.Reflection.Assembly]::LoadWithPartialName("Microsoft.SqlServer.Smo")
[void][System.Reflection.Assembly]::LoadWithPartialName("Microsoft.SqlServer.SmoExtended")

$srv = New-Object Microsoft.SqlServer.Management.Smo.Server $Server
$db = $srv.Databases[$Database]
if (-not $db) { throw "Không tìm thấy CSDL $Database trên $Server" }

Write-Host "==> Bắt đầu xuất CSDL $Database từ $Server..." -ForegroundColor Cyan

$sb = New-Object System.Text.StringBuilder
[void]$sb.AppendLine("-- =========================================================================================")
[void]$sb.AppendLine("-- HE THONG QUAN LY KHO HANG PHU QUOC LTTQ - WINDOWS FORMS (.NET 4.7.2)")
[void]$sb.AppendLine("-- SCRIPT TAO CO SO DU LIEU TRON GOI (FULL DATABASE + TABLES + DATA + VIEWS + CONSTRAINTS)")
[void]$sb.AppendLine("-- Co so du lieu: $Database")
[void]$sb.AppendLine("-- Sinh tu dong boi: tools/XuatScriptCSDL.ps1 vao luc $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')")
[void]$sb.AppendLine("-- ")
[void]$sb.AppendLine("-- HUONG DAN SU DUNG:")
[void]$sb.AppendLine("--   1. Mo SQL Server Management Studio (SSMS) hoac Azure Data Studio.")
[void]$sb.AppendLine("--   2. Ket noi vao SQL Server (vi du: localhost\SQLEXPRESS hoac localhost hoac .).")
[void]$sb.AppendLine("--   3. Mo file TaoCSDL_DayDu.sql nay va bam Execute (F5).")
[void]$sb.AppendLine("--   4. Sau khi chay thanh cong, kiem tra App.config trong project xem da dung Server name chua.")
[void]$sb.AppendLine("-- ")
[void]$sb.AppendLine("-- TAI KHOAN DANG NHAP HE THONG MAC DINH (Mat khau: 123456):")
[void]$sb.AppendLine("--   - quanly / 123456  (Vai tro: Quan ly - Toan quyen moi kho)")
[void]$sb.AppendLine("--   - thukho / 123456  (Vai tro: Thu kho - Quan ly Kho trung tam)")
[void]$sb.AppendLine("--   - nvkho  / 123456  (Vai tro: Nhan vien kho - Lam viec tai Kho trung tam)")
[void]$sb.AppendLine("-- =========================================================================================")
[void]$sb.AppendLine("")
[void]$sb.AppendLine("USE [master];")
[void]$sb.AppendLine("GO")
[void]$sb.AppendLine("")
[void]$sb.AppendLine("IF DB_ID(N'$Database') IS NULL")
[void]$sb.AppendLine("BEGIN")
[void]$sb.AppendLine("    CREATE DATABASE [$Database] COLLATE Vietnamese_CI_AS;")
[void]$sb.AppendLine("    PRINT N'==> [OK] Da tao moi CSDL $Database.';")
[void]$sb.AppendLine("END")
[void]$sb.AppendLine("ELSE")
[void]$sb.AppendLine("BEGIN")
[void]$sb.AppendLine("    PRINT N'==> [THONG BAO] CSDL $Database da ton tai, tiep tuc kiem tra bang.';")
[void]$sb.AppendLine("END")
[void]$sb.AppendLine("GO")
[void]$sb.AppendLine("")
[void]$sb.AppendLine("USE [$Database];")
[void]$sb.AppendLine("GO")
[void]$sb.AppendLine("SET NOCOUNT ON;")
[void]$sb.AppendLine("GO")
[void]$sb.AppendLine("")

$tables = $db.Tables | Where-Object { -not $_.IsSystemObject }
Write-Host "1. Đang xuất cấu trúc 30 bảng (Schema, PK, Defaults, Checks, Indexes)..." -ForegroundColor Yellow

# Scripting Options cho Schema bảng (KHÔNG kèm FK để tránh lỗi phụ thuộc khi tạo bảng và nạp dữ liệu)
$optSchema = New-Object Microsoft.SqlServer.Management.Smo.ScriptingOptions
$optSchema.ScriptSchema = $true
$optSchema.ScriptData = $false
$optSchema.DriPrimaryKey = $true
$optSchema.DriDefaults = $true
$optSchema.DriChecks = $true
$optSchema.DriUniqueKeys = $true
$optSchema.DriForeignKeys = $false
$optSchema.Indexes = $true
$optSchema.ScriptBatchTerminator = $true
$optSchema.NoCollation = $false

$scripterSchema = New-Object Microsoft.SqlServer.Management.Smo.Scripter $srv
$scripterSchema.Options = $optSchema

[void]$sb.AppendLine("-- =========================================================================================")
[void]$sb.AppendLine("-- PHAN 1: TAO CAU TRUC BANG (TABLES, PRIMARY KEYS, DEFAULTS, CHECKS, INDEXES)")
[void]$sb.AppendLine("-- =========================================================================================")

foreach ($t in $tables) {
    [void]$sb.AppendLine("-- -----------------------------------------------------------------------------------------")
    [void]$sb.AppendLine("-- Bang: [$($t.Schema)].[$($t.Name)]")
    [void]$sb.AppendLine("-- -----------------------------------------------------------------------------------------")
    $createStatements = $scripterSchema.Script(@($t.Urn))
    foreach ($stmt in $createStatements) {
        $trimmed = $stmt.Trim()
        if ($trimmed.Length -gt 0) {
            [void]$sb.AppendLine($trimmed)
            if (-not $trimmed.EndsWith("GO")) { [void]$sb.AppendLine("GO") }
        }
    }
    [void]$sb.AppendLine("")
}

Write-Host "2. Đang xuất dữ liệu mẫu (Data) cho các bảng..." -ForegroundColor Yellow

[void]$sb.AppendLine("-- =========================================================================================")
[void]$sb.AppendLine("-- PHAN 2: NAP DU LIEU MAU VAO CAC BANG (DATA INSERT)")
[void]$sb.AppendLine("-- =========================================================================================")

$optData = New-Object Microsoft.SqlServer.Management.Smo.ScriptingOptions
$optData.ScriptSchema = $false
$optData.ScriptData = $true
$optData.ScriptBatchTerminator = $true

$scripterData = New-Object Microsoft.SqlServer.Management.Smo.Scripter $srv
$scripterData.Options = $optData

foreach ($t in $tables) {
    if ($t.RowCount -gt 0) {
        [void]$sb.AppendLine("-- Du lieu bang: [$($t.Schema)].[$($t.Name)] ($($t.RowCount) dong)")
        $hasIdentity = $false
        foreach ($col in $t.Columns) {
            if ($col.Identity) { $hasIdentity = $true; break }
        }
        if ($hasIdentity) {
            [void]$sb.AppendLine("IF NOT EXISTS (SELECT 1 FROM [$($t.Schema)].[$($t.Name)])")
            [void]$sb.AppendLine("BEGIN")
            [void]$sb.AppendLine("    SET IDENTITY_INSERT [$($t.Schema)].[$($t.Name)] ON;")
        } else {
            [void]$sb.AppendLine("IF NOT EXISTS (SELECT 1 FROM [$($t.Schema)].[$($t.Name)])")
            [void]$sb.AppendLine("BEGIN")
        }

        $dataStatements = $scripterData.Script(@($t.Urn))
        foreach ($dstmt in $dataStatements) {
            $dtrimmed = $dstmt.Trim()
            if ($dtrimmed.Length -gt 0) {
                # Chỉ lấy các lệnh INSERT thực sự
                if ($dtrimmed.StartsWith("INSERT", [System.StringComparison]::OrdinalIgnoreCase)) {
                    [void]$sb.AppendLine("    " + $dtrimmed)
                }
            }
        }

        if ($hasIdentity) {
            [void]$sb.AppendLine("    SET IDENTITY_INSERT [$($t.Schema)].[$($t.Name)] OFF;")
        }
        [void]$sb.AppendLine("END")
        [void]$sb.AppendLine("GO")
        [void]$sb.AppendLine("")
    }
}

Write-Host "3. Đang xuất các ràng buộc khóa ngoại (Foreign Keys)..." -ForegroundColor Yellow

[void]$sb.AppendLine("-- =========================================================================================")
[void]$sb.AppendLine("-- PHAN 3: KHOA NGOAI (FOREIGN KEY CONSTRAINTS)")
[void]$sb.AppendLine("-- =========================================================================================")

foreach ($t in $tables) {
    foreach ($fk in $t.ForeignKeys) {
        $fkStmts = $fk.Script()
        foreach ($fks in $fkStmts) {
            $fktr = $fks.Trim()
            if ($fktr.Length -gt 0) {
                [void]$sb.AppendLine($fktr)
                if (-not $fktr.EndsWith("GO")) { [void]$sb.AppendLine("GO") }
            }
        }
    }
}
[void]$sb.AppendLine("")

Write-Host "4. Đang xuất các View nghiệp vụ..." -ForegroundColor Yellow

[void]$sb.AppendLine("-- =========================================================================================")
[void]$sb.AppendLine("-- PHAN 4: CAC VIEW NGHIEP VU KHO (VIEWS)")
[void]$sb.AppendLine("-- =========================================================================================")

# Script theo thứ tự: vw_BienDongKho trước, vw_TonKhoHienTai sau (vì vw_TonKhoHienTai query từ vw_BienDongKho)
$orderedViewNames = @('vw_BienDongKho', 'vw_TonKhoHienTai')
foreach ($vName in $orderedViewNames) {
    $v = $db.Views[$vName]
    if ($v) {
        [void]$sb.AppendLine("-- View: [$($v.Schema)].[$($v.Name)]")
        [void]$sb.AppendLine("IF OBJECT_ID(N'[$($v.Schema)].[$($v.Name)]', 'V') IS NOT NULL")
        [void]$sb.AppendLine("    DROP VIEW [$($v.Schema)].[$($v.Name)];")
        [void]$sb.AppendLine("GO")
        $vStmts = $v.Script()
        foreach ($vs in $vStmts) {
            $vtr = $vs.Trim()
            if ($vtr.Length -gt 0) {
                [void]$sb.AppendLine($vtr)
                if (-not $vtr.EndsWith("GO")) { [void]$sb.AppendLine("GO") }
            }
        }
        [void]$sb.AppendLine("")
    }
}

[void]$sb.AppendLine("-- =========================================================================================")
[void]$sb.AppendLine("-- HOAN TAT KHOI TAO CO SO DU LIEU")
[void]$sb.AppendLine("-- =========================================================================================")
[void]$sb.AppendLine("PRINT N'=================================================================';")
[void]$sb.AppendLine("PRINT N'==> KHOI TAO CSDL $Database THANH CONG 100%!';")
[void]$sb.AppendLine("PRINT N'==> San sang khoi chay ung dung Windows Forms BTL-LTTQ-2.');")
[void]$sb.AppendLine("PRINT N'=================================================================';")
[void]$sb.AppendLine("GO")

$full = [System.IO.Path]::GetFullPath($Out)
$utf8Bom = New-Object System.Text.UTF8Encoding $true
[System.IO.File]::WriteAllText($full, $sb.ToString(), $utf8Bom)

$sizeMb = [math]::Round((Get-Item $full).Length / 1KB, 2)
Write-Host "==> [THANH CONG] Đã tạo file: $full ($sizeMb KB)" -ForegroundColor Green
