using System;
using System.Collections.Generic;

namespace Quanlythuvien.Models;

public partial class TblTheLoai
{
    public int MaTl { get; set; }

    public string? TenTl { get; set; }

    public string? Mota { get; set; }

    public virtual ICollection<TblSach> TblSaches { get; set; } = new List<TblSach>();
}
