# ==============================================================================
# Script: KiemTraMaHoa.ps1
# Muc dich: Kiem tra va dam bao 100% file ma nguon du an tuan thu:
#   1. Dinh dang UTF-8 with BOM (Byte Order Mark: 0xEF, 0xBB, 0xBF).
#   2. Khong chua bat ky ky tu loi ma hoa (Mojibake) nao.
# ==============================================================================

param (
    [switch]$AutoFixBom = $true
)

$rootDir = Split-Path -Parent $PSScriptRoot
if (-not $rootDir) {
    $rootDir = Get-Location
}

Write-Host "==============================================================" -ForegroundColor Cyan
Write-Host " HE THONG KIEM SOAT MA HOA & PHONG CHONG LOI FONT (UTF-8 BOM) " -ForegroundColor Cyan
Write-Host " Thu muc goc: $rootDir" -ForegroundColor Gray
Write-Host "==============================================================" -ForegroundColor Cyan

# Mau regex phat hien Mojibake (UTF-8 bi doc/ghi sai thanh Windows-1252/ANSI)
$mojibakePattern = '[\u00C3][\u0080-\u00BF]|[\u00C4][\u0090\u0091\u0082\u0083]|[\u00E1][\u00BA\u00BB]|[\u00E2][\u017E\u20AC\u0161]|\u00C6\u00A1|\u00C6\u00B0|\u00C5\u00A9|\u00C5\u00A0'

$targetExtensions = @("*.cs", "*.resx", "*.sql", "*.config", "*.md", "*.json", "*.editorconfig")

$files = Get-ChildItem -Path $rootDir -Recurse -Include $targetExtensions | Where-Object {
    $_.FullName -notmatch '\\bin\\' -and 
    $_.FullName -notmatch '\\obj\\' -and 
    $_.FullName -notmatch '\\\.git\\' -and
    $_.FullName -notmatch '\\scratch\\' -and
    $_.FullName -notmatch '\\\.system_generated\\'
}

$totalFiles = $files.Count
$missingBomFiles = @()
$mojibakeFiles = @()

$utf8WithBom = New-Object System.Text.UTF8Encoding($true)

foreach ($file in $files) {
    $bytes = [System.IO.File]::ReadAllBytes($file.FullName)
    $hasBom = ($bytes.Length -ge 3 -and $bytes[0] -eq 0xEF -and $bytes[1] -eq 0xBB -and $bytes[2] -eq 0xBF)
    
    if (-not $hasBom) {
        $missingBomFiles += $file.FullName
        if ($AutoFixBom) {
            # Tu dong bo sung UTF-8 BOM
            $content = [System.IO.File]::ReadAllText($file.FullName, [System.Text.Encoding]::UTF8)
            [System.IO.File]::WriteAllText($file.FullName, $content, $utf8WithBom)
        }
    }

    # Doc noi dung bang UTF-8 de kiem tra mojibake
    $text = [System.Text.Encoding]::UTF8.GetString($bytes)
    $matches = [regex]::Matches($text, $mojibakePattern)
    if ($matches.Count -gt 0) {
        $mojibakeFiles += [PSCustomObject]@{
            File = $file.FullName.Replace($rootDir, "")
            Count = $matches.Count
        }
    }
}

Write-Host "`nTong so file kiem tra: $totalFiles" -ForegroundColor White

if ($missingBomFiles.Count -gt 0) {
    if ($AutoFixBom) {
        Write-Host "[OK] Da tu dong bo sung UTF-8 BOM cho $($missingBomFiles.Count) file thieu BOM." -ForegroundColor Yellow
    } else {
        Write-Host "[CANH BAO] Phat hien $($missingBomFiles.Count) file thieu UTF-8 BOM!" -ForegroundColor Red
        $missingBomFiles | ForEach-Object { Write-Host "   - $_" -ForegroundColor DarkYellow }
    }
} else {
    Write-Host "[OK] 100% file da co san UTF-8 with BOM." -ForegroundColor Green
}

if ($mojibakeFiles.Count -gt 0) {
    Write-Host "`n[NGHIEM TRONG] Phat hien $($mojibakeFiles.Count) file bi loi phong chu (Mojibake):" -ForegroundColor Red
    $mojibakeFiles | Format-Table -AutoSize
    Write-Host "-> Yeu cau kiem tra va khac phuc cac file tren truoc khi dong phien lam viec!" -ForegroundColor Red
    exit 1
} else {
    Write-Host "[HOAN HAO] 0 loi font / 0 Mojibake duoc phat hien tren toan bo he thong!" -ForegroundColor Green
    Write-Host "==============================================================" -ForegroundColor Cyan
    exit 0
}
