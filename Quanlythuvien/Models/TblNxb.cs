using System;
using System.Collections.Generic;

namespace Quanlythuvien.Models;

public partial class TblNxb
{
    public int MaNxb { get; set; }

    public string? TenNxb { get; set; }

    public string? Diachi { get; set; }

    public string? Sodienthoai { get; set; }

    public virtual ICollection<TblSach> TblSaches { get; set; } = new List<TblSach>();
}
