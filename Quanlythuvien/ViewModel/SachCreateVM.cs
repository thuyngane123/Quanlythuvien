using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Quanlythuvien.Models
{
    public class SachCreateVM
    {
        [Required]
        public string TenSach { get; set; } = string.Empty;

        [Required]
        public int MaTl { get; set; }

        [Required]
        public int MaNxb { get; set; }

        [Required]
        public List<int> SelectedTgIds { get; set; } = new List<int>();

        public int? Soluong { get; set; }
        public int? NamXb { get; set; }
        public string? Anh { get; set; }
        public string? Mota { get; set; }
        public string? Trangthai { get; set; }
    }
}