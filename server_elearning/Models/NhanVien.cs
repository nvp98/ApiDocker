using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace server_elearning.Models
{
    public class NhanVien
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string MaNV { get; set; }
        public string MatKhau { get; set; }
        public string HoTen { get; set; }
        public int IDPhongBan { get; set; }
    }
    //public class LopHoc
    //{
    //    [Key]
    //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    public int IDLH { get; set; }
    //    public string MaLH { get; set; }
    //    public string TenLH { get; set; }
    //    public int? NDID { get; set; }
    //    public int? QuyDT { get; set; }
    //}

    public class NhanVienSQL
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string MaNV { get; set; }
        public string HoTen { get; set; }
        public int? IDPhongBan { get; set; }
        public bool? IsGV { get; set; }
    }

    public class TaiKhoanSQL
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string MaNV { get; set; }
        public string HoTen { get; set; }
        //public int? IDPhongBan { get; set; }
        //public bool? IsGV { get; set; }
    }

}
