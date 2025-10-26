using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Quanlythuvien.Models;

public partial class QlthuVienContext : DbContext
{
    public QlthuVienContext()
    {
    }

    public QlthuVienContext(DbContextOptions<QlthuVienContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblKhachHang> TblKhachHangs { get; set; }

    public virtual DbSet<TblMuonTra> TblMuonTras { get; set; }

    public virtual DbSet<TblNhanVien> TblNhanViens { get; set; }

    public virtual DbSet<TblNxb> TblNxbs { get; set; }

    public virtual DbSet<TblSach> TblSaches { get; set; }

    public virtual DbSet<TblTacGium> TblTacGia { get; set; }

    public virtual DbSet<TblTheLoai> TblTheLoais { get; set; }

    public virtual DbSet<TblThongKe> TblThongKes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source= NGAMOM\\MSSQLSERVER01; initial catalog=QLThuVien; integrated security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblKhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKh);

            entity.ToTable("tblKhachHang");

            entity.Property(e => e.MaKh)
                .ValueGeneratedNever()
                .HasColumnName("MaKH");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Hoten)
                .HasMaxLength(100)
                .HasColumnName("hoten");
            entity.Property(e => e.Matkhau)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("matkhau");
            entity.Property(e => e.Sodienthoai).HasColumnName("sodienthoai");
            entity.Property(e => e.Tendangnhap)
                .HasMaxLength(50)
                .HasColumnName("tendangnhap");
            entity.Property(e => e.Trangthai)
                .HasMaxLength(50)
                .HasColumnName("trangthai");
        });

        modelBuilder.Entity<TblMuonTra>(entity =>
        {
            entity.HasKey(e => new { e.MaMuon, e.MaKh, e.MaSach }).HasName("PK_tblMuonTra_1");

            entity.ToTable("tblMuonTra");

            entity.Property(e => e.MaKh).HasColumnName("MaKH");
            entity.Property(e => e.Ngaymuon).HasColumnName("ngaymuon");
            entity.Property(e => e.Ngaytra).HasColumnName("ngaytra");
            entity.Property(e => e.Trangthai)
                .HasMaxLength(50)
                .HasColumnName("trangthai");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.TblMuonTras)
                .HasForeignKey(d => d.MaKh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblMuonTra_tblKhachHang");

            entity.HasOne(d => d.MaSachNavigation).WithMany(p => p.TblMuonTras)
                .HasForeignKey(d => d.MaSach)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblMuonTra_tblSach");
        });

        modelBuilder.Entity<TblNhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNv);

            entity.ToTable("tblNhanVien");

            entity.Property(e => e.MaNv)
                .ValueGeneratedNever()
                .HasColumnName("MaNV");
            entity.Property(e => e.Diachi)
                .HasMaxLength(50)
                .HasColumnName("diachi");
            entity.Property(e => e.Hoten)
                .HasMaxLength(50)
                .HasColumnName("hoten");
            entity.Property(e => e.Matkhau)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("matkhau");
            entity.Property(e => e.Sodienthoai).HasColumnName("sodienthoai");
            entity.Property(e => e.TenNv)
                .HasMaxLength(50)
                .HasColumnName("tenNV");
            entity.Property(e => e.Trangthai)
                .HasMaxLength(50)
                .HasColumnName("trangthai");
        });

        modelBuilder.Entity<TblNxb>(entity =>
        {
            entity.HasKey(e => e.MaNxb);

            entity.ToTable("tblNXB");

            entity.Property(e => e.MaNxb)
                .ValueGeneratedNever()
                .HasColumnName("MaNXB");
            entity.Property(e => e.Diachi)
                .HasMaxLength(50)
                .HasColumnName("diachi");
            entity.Property(e => e.Sodienthoai)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sodienthoai");
            entity.Property(e => e.TenNxb)
                .HasMaxLength(100)
                .HasColumnName("tenNXB");
        });

        modelBuilder.Entity<TblSach>(entity =>
        {
            entity.HasKey(e => e.MaSach);

            entity.ToTable("tblSach");

            entity.Property(e => e.Anh)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("anh");
            entity.Property(e => e.Gia).HasColumnName("gia");
            entity.Property(e => e.MaNxb).HasColumnName("MaNXB");
            entity.Property(e => e.MaTg).HasColumnName("MaTG");
            entity.Property(e => e.MaTl).HasColumnName("MaTL");
            entity.Property(e => e.Mota)
                .HasMaxLength(255)
                .HasColumnName("mota");
            entity.Property(e => e.NamXb).HasColumnName("namXB");
            entity.Property(e => e.Soluong).HasColumnName("soluong");
            entity.Property(e => e.TenSach)
                .HasMaxLength(255)
                .HasColumnName("tenSach");
            entity.Property(e => e.Trangthai)
                .HasMaxLength(50)
                .HasColumnName("trangthai");

            entity.HasOne(d => d.MaNxbNavigation).WithMany(p => p.TblSaches)
                .HasForeignKey(d => d.MaNxb)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblSach_tblNXB");

            entity.HasOne(d => d.MaTgNavigation).WithMany(p => p.TblSaches)
                .HasForeignKey(d => d.MaTg)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblSach_tblTacGia");

            entity.HasOne(d => d.MaTlNavigation).WithMany(p => p.TblSaches)
                .HasForeignKey(d => d.MaTl)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tblSach_tblTheLoai");
        });

        modelBuilder.Entity<TblTacGium>(entity =>
        {
            entity.HasKey(e => e.MaTg);

            entity.ToTable("tblTacGia");

            entity.Property(e => e.MaTg)
                .ValueGeneratedNever()
                .HasColumnName("MaTG");
            entity.Property(e => e.Mota)
                .HasMaxLength(50)
                .HasColumnName("mota");
            entity.Property(e => e.TenTg)
                .HasMaxLength(100)
                .HasColumnName("tenTG");
        });

        modelBuilder.Entity<TblTheLoai>(entity =>
        {
            entity.HasKey(e => e.MaTl);

            entity.ToTable("tblTheLoai");

            entity.Property(e => e.MaTl)
                .ValueGeneratedNever()
                .HasColumnName("MaTL");
            entity.Property(e => e.Mota)
                .HasMaxLength(50)
                .HasColumnName("mota");
            entity.Property(e => e.TenTl)
                .HasMaxLength(100)
                .HasColumnName("tenTL");
        });

        modelBuilder.Entity<TblThongKe>(entity =>
        {
            entity.HasKey(e => e.MaThongKe);

            entity.ToTable("tblThongKe");

            entity.Property(e => e.MaThongKe).ValueGeneratedNever();
            entity.Property(e => e.Noidung)
                .HasMaxLength(50)
                .HasColumnName("noidung");
            entity.Property(e => e.Thongke)
                .HasMaxLength(100)
                .HasColumnName("thongke");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
