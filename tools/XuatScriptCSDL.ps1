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

$sb = New-Object System.Text.StringBuilder
[void]$sb.AppendLine("-- =========================================================================================")
[void]$sb.AppendLine("-- SCRIPT TẠO CSDL ĐẦY ĐỦ: $Database (cấu trúc bảng, ràng buộc, view, dữ liệu mẫu)")
[void]$sb.AppendLine("-- Sinh tự động bởi tools/XuatScriptCSDL.ps1 lúc $(Get-Date -Format 'yyyy-MM-dd HH:mm')")
[void]$sb.AppendLine("-- Cách dùng: mở trong SSMS (kết nối localhost\SQLEXPRESS) và chạy toàn bộ.")
[void]$sb.AppendLine("-- Tài khoản mẫu: quanly / thukho / nvkho – mật khẩu 123456")
[void]$sb.AppendLine("-- =========================================================================================")
[void]$sb.AppendLine("IF DB_ID(N'$Database') IS NULL CREATE DATABASE [$Database];")
[void]$sb.AppendLine("GO")
[void]$sb.AppendLine("USE [$Database];")
[void]$sb.AppendLine("GO")
[void]$sb.AppendLine("IF EXISTS (SELECT 1 FROM sys.tables WHERE name = N'HangHoa')")
[void]$sb.AppendLine("BEGIN")
[void]$sb.AppendLine("    RAISERROR(N'CSDL $Database đã có dữ liệu. Script chỉ dùng cho CSDL trống. Hãy xóa hoặc đổi tên CSDL cũ trước khi chạy.', 16, 1);")
[void]$sb.AppendLine("    SET NOEXEC ON;")
[void]$sb.AppendLine("END")
[void]$sb.AppendLine("GO")

$opt = New-Object Microsoft.SqlServer.Management.Smo.ScriptingOptions
$opt.ScriptSchema = $true
$opt.ScriptData = $true
$opt.DriAll = $true
$opt.Indexes = $true
$opt.Triggers = $true
$opt.WithDependencies = $true
$opt.IncludeHeaders = $false
$opt.SchemaQualify = $true
$opt.AnsiPadding = $true
$opt.NoCollation = $true
$opt.ScriptBatchTerminator = $true
$opt.Encoding = [System.Text.Encoding]::UTF8

$scripter = New-Object Microsoft.SqlServer.Management.Smo.Scripter $srv
$scripter.Options = $opt
$urns = New-Object 'System.Collections.Generic.List[Microsoft.SqlServer.Management.Sdk.Sfc.Urn]'
foreach ($t in $db.Tables) { if (-not $t.IsSystemObject) { $urns.Add($t.Urn) } }
foreach ($v in $db.Views) { if (-not $v.IsSystemObject) { $urns.Add($v.Urn) } }

foreach ($line in $scripter.EnumScript($urns.ToArray())) {
    [void]$sb.AppendLine($line)
    if (-not $line.TrimEnd().EndsWith("GO")) { [void]$sb.AppendLine("GO") }
}
[void]$sb.AppendLine("SET NOEXEC OFF;")
[void]$sb.AppendLine("GO")
[void]$sb.AppendLine("PRINT N'=== ĐÃ TẠO XONG CSDL $Database ===';")
[void]$sb.AppendLine("GO")

$full = [System.IO.Path]::GetFullPath($Out)
[System.IO.File]::WriteAllText($full, $sb.ToString(), (New-Object System.Text.UTF8Encoding $true))
"Đã xuất: $full ($([math]::Round((Get-Item $full).Length / 1MB, 2)) MB)"
