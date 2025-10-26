using System;
using System.Collections.Generic;

namespace Quanlythuvien.Models;

public partial class TblTacGium
{
    public int MaTg { get; set; }

    public string? TenTg { get; set; }

    public string? Mota { get; set; }

    public virtual ICollection<TblSach> TblSaches { get; set; } = new List<TblSach>();
}
