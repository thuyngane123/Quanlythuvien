using System;
using System.Collections.Generic;

namespace Quanlythuvien.Models;

public partial class TblSach
{
    public int MaSach { get; set; }

    public int MaTl { get; set; }

    public int MaNxb { get; set; }

    public int MaTg { get; set; }

    public string? TenSach { get; set; }

    public int? Soluong { get; set; }

    public string? Trangthai { get; set; }

    public int? NamXb { get; set; }

    public string? Anh { get; set; }

    public double? Gia { get; set; }

    public string? Mota { get; set; }

    public virtual TblNxb MaNxbNavigation { get; set; } = null!;

    public virtual TblTacGium MaTgNavigation { get; set; } = null!;

    public virtual TblTheLoai MaTlNavigation { get; set; } = null!;

    public virtual ICollection<TblMuonTra> TblMuonTras { get; set; } = new List<TblMuonTra>();

    public virtual ICollection<TblTacGium> MaTgs { get; set; } = new List<TblTacGium>();
}
