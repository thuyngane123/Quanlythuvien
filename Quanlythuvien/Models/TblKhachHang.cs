using System;
using System.Collections.Generic;

namespace Quanlythuvien.Models;

public partial class TblKhachHang
{
    public int MaKh { get; set; }

    public string? Tendangnhap { get; set; }

    public string? Matkhau { get; set; }

    public string? Hoten { get; set; }

    public string? Email { get; set; }

    public int? Sodienthoai { get; set; }

    public string? Trangthai { get; set; }

    public virtual ICollection<TblMuonTra> TblMuonTras { get; set; } = new List<TblMuonTra>();
}
