using System;
using System.Collections.Generic;

namespace Quanlythuvien.Models;

public partial class TblMuonTra
{
    public int MaMuon { get; set; }

    public int MaKh { get; set; }

    public int MaSach { get; set; }

    public DateOnly? Ngaymuon { get; set; }

    public DateOnly? Ngaytra { get; set; }

    public string? Trangthai { get; set; }

    public virtual TblKhachHang MaKhNavigation { get; set; } = null!;

    public virtual TblSach MaSachNavigation { get; set; } = null!;
}
