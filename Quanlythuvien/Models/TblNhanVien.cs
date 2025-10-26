using System;
using System.Collections.Generic;

namespace Quanlythuvien.Models;

public partial class TblNhanVien
{
    public int MaNv { get; set; }

    public string? TenNv { get; set; }

    public string? Matkhau { get; set; }

    public string? Hoten { get; set; }

    public string? Diachi { get; set; }

    public int? Sodienthoai { get; set; }

    public string? Trangthai { get; set; }
}
