using System;

namespace server_elearning.Models
{
    public class PhieuXacNhanKCCD
    {
        public int ID { get; set; }
        //public Nullable<int> DeNghiDTID { get; set; }
        //public Nullable<int> HocVienID { get; set; }
        public string HVTruocDatDuoc { get; set; }
        public string HVTruocCanCaiThien { get; set; }
        public string HVSauDatDuoc { get; set; }
        public string HVSauCanCaiThien { get; set; }
        //public Nullable<double> GVLyThuyetTruocDT { get; set; }
        //public Nullable<double> GVThucHanhTruocDT { get; set; }
        //public string GVNhanXetLTTruocDT { get; set; }
        //public string GVNhanXetTHTruocDT { get; set; }
        //public Nullable<double> GVLyThuyetSauDT { get; set; }
        //public Nullable<double> GVThucHanhSauDT { get; set; }
        //public string GVNhanXetLTSauDT { get; set; }
        //public string GVNhanXetTHSauDT { get; set; }
        //public Nullable<int> GVKetLuan { get; set; }
        //public string GVKetLuanYKienKhac { get; set; }
        public Nullable<int> HVDeXuat { get; set; }
        public string HVDeXuatKhac { get; set; }
        //public Nullable<System.DateTime> HVNgayXacNhan { get; set; }
        public Nullable<int> IDTinhTrang { get; set; }
    }
}
