using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server_elearning.Models;

namespace server_elearning.Models
{
    public class APICoreDBContext:DbContext
    {
        public DbSet<NoiDungDT> NoiDungDT { get; set; }
        public DbSet<LopHoc> LopHoc { get; set; }
        public DbSet<XNHocTap> XNHocTap { get; set; }
        public DbSet<NhanVien> NhanVien { get; set; }
        public DbSet<NhanVienSQL> NhanVienSQL { get; set; }
        public DbSet<TaiKhoanSQL> TaiKhoanSQL { get; set; }
        public DbSet<TaiKhoan> TaiKhoan { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Tokens> Tokens { get; set; }
        public DbSet<CauHoi> CauHoi { get; set; }
        public DbSet<CauHoiDeThi> CauHoiDeThi { get; set; }
        public DbSet<DanhSachDA> DanhSachDA { get; set; }
        public DbSet<DeThi> DeThi { get; set; }
        public DbSet<BaiThi> BaiThi { get; set; }
        public DbSet<CTBaiThi> CTBaiThi { get; set; }
        public DbSet<FResult> FResult { get; set; }
        public DbSet<ApproveKCCD> ApproveKCCD { get; set; }
        public DbSet<PhieuXacNhanKCCD> PhieuXacNhanKCCD { get; set; }
        public APICoreDBContext(DbContextOptions<APICoreDBContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaiKhoanSQL>().HasNoKey();
            modelBuilder.Entity<NhanVien>().ToTable("NhanVien");
            modelBuilder.Entity<Users>().HasNoKey(); 
            modelBuilder.Entity<Tokens>().HasNoKey();
            modelBuilder.Entity<XNHocTap>().HasNoKey();
            modelBuilder.Entity<LopHoc>().ToTable("LopHoc");
            modelBuilder.Entity<NoiDungDT>().ToTable("NoiDungDT");
            modelBuilder.Entity<CauHoi>().ToTable("CauHoi");
            modelBuilder.Entity<CauHoiDeThi>().ToTable("CauHoiDeThi");
            modelBuilder.Entity<DanhSachDA>().ToTable("DanhSachDA");
            modelBuilder.Entity<DeThi>().ToTable("DeThi");
            modelBuilder.Entity<BaiThi>().ToTable("BaiThi");
            modelBuilder.Entity<CTBaiThi>().ToTable("CTBaiThi");
            modelBuilder.Entity<FResult>().HasNoKey();
            modelBuilder.Entity<PhieuXacNhanKCCD>().ToTable("PhieuXacNhanKCCD");
        }
        public DbSet<server_elearning.Models.ContentKCCD> ContentKCCD { get; set; }
   
    }
}
