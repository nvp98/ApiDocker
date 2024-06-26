using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace server_elearning.Models
{
    public class SuggetKCCD
    {
        public int ID { get; set; }
        public Nullable<int> DeNghiDTID { get; set; }
        public string TenND { get; set; }
        public Nullable<int> HocVienID { get; set; }
        public string HoTenHV { get; set; }
        public string MaNV { get; set; }
        public string VitriHV { get; set; }
        public string PhongBanHV { get; set; }
        public string HVTruocDatDuoc { get; set; }
        public string HVTruocCanCaiThien { get; set; }
        public string HVSauDatDuoc { get; set; }
        public string HVSauCanCaiThien { get; set; }
        public Nullable<double> GVLyThuyetTruocDT { get; set; }
        public Nullable<double> GVThucHanhTruocDT { get; set; }
        public string GVNhanXetLTTruocDT { get; set; }
        public string GVNhanXetTHTruocDT { get; set; }
        public Nullable<double> GVLyThuyetSauDT { get; set; }
        public Nullable<double> GVThucHanhSauDT { get; set; }
        public string GVNhanXetLTSauDT { get; set; }
        public string GVNhanXetTHSauDT { get; set; }
        public Nullable<int> GVKetLuan { get; set; }
        public string GVKetLuanYKienKhac { get; set; }
        public Nullable<int> HVDeXuat { get; set; }
        public string HVDeXuatKhac { get; set; }
        public DateTime HVNgayXacNhan { get; set; }
        public Nullable<int> IDTinhTrang { get; set; }
        public string HVDanhGia { get; set; }
        public int ThoiLuongDT { get; set; }
    }
    public class ContentKCCD
    {
        public int ID { get; set; }
        public Nullable<int> DeNghiDTID { get; set; }
        public Nullable<int> HocVienID { get; set; }
        public DateTime HVNgayXacNhan { get; set; }
        public Nullable<int> IDTinhTrang { get; set; }
        //public string HoTen { get; set; }
        //public string MaNV { get; set; }
        public string TenND { get; set; }
        public string TenPhongBan { get; set; }
        public string TenNhomNL { get; set; }
        public string TenLVDT { get; set; }
        public string HoTen1 { get; set; }
        public string MaNV1 { get; set; }
        public string HoTen2 { get; set; } 
        public string MaNV2 { get; set; }
        [NotMapped]
        public string TinhTrangPhieu { get; set; }

    }
    public class returnSuggestKCCD
    {
        public List<ContentKCCD> resultKCCDs { get; set; }
        public List<ApproveKCCD> resultApproveKCCDs { get; set; }
        public int total { get; set; }
    }
    public class ApproveKCCD
    {
        public int ID { get; set; }
        public Nullable<int> NoiDungKCCDID { get; set; }
        public string TenND { get; set; }
        public Nullable<int> LinhVucID { get; set; }
        public string TenLVDT { get; set; }
        public Nullable<int> NhomNangLucID { get; set; }
        public string TenNhomNL { get; set; }
        public Nullable<int> PhongBanID { get; set; }
        public string TenPhongBan { get; set; }
        public Nullable<int> HuongDan1 { get; set; }
        public string HoTen1 { get; set; }
        public string MaNV1 { get; set; }
        //public Nullable<int> ViTriID1 { get; set; }
        //public string TenViTri1 { get; set; }
        public Nullable<int> HuongDan2 { get; set; }
        public string HoTen2 { get; set; }
        public string MaNV2 { get; set; }
        //public Nullable<int> ViTriID2 { get; set; }
        //public string TenViTri2 { get; set; }
        public DateTime NgayTao { get; set; }
        public Nullable<int> TinhTrang { get; set; }
        public DateTime TuNgay { get; set; }
        public DateTime DenNgay { get; set; }
        public DateTime? NgayXN { get; set; }
        public int? SLHV { get; set; }
        public int? SLHVHT { get; set; }
        public int? SLHVXN { get; set; }
        [NotMapped]
        public int? TinhTrangPD { get; set; }
        [NotMapped]
        public string TinhTrangStr { get; set; }
    }
    public class returnSApproveKCCD
    {
        public List<ApproveKCCD> resultApproveKCCDs { get; set; }
        public int total { get; set; }
    }
}
