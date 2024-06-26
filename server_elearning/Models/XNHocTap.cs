using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace server_elearning.Models
{
    public class XNHocTap
    {
        public int IDHT { get; set; }
        public int NVID { get; set; }
        public int LHID { get; set; }
        public DateTime NgayTG { get; set; }
        public DateTime NgayHT { get; set; }
        public bool XNTG { get; set; }
        public bool XNHT { get; set; }
        public int PBID { get; set; }
        public int VTID { get; set; }
        [NotMapped]
        public NhanVien NhanVien { get; set; }
        [NotMapped]
        public LopHoc LopHoc { get; set; }
        [NotMapped]
        public NoiDungDT NoiDungDT { get; set; }
        [NotMapped]
        public BaiThi BaiThi { get; set; }
    }
    public class returnXNHT{
        public List<XNHocTap> results { get; set; }
        public int total { get; set; }

    }
    //public class Eclass
    //{
    //    public XNHocTap XNHocTap { get; set; }
    //    public NhanVien NhanVien { get; set; }
    //    public LopHoc LopHoc { get; set; }
    //    public NoiDungDT NoiDungDT { get; set; }
    //}
}
