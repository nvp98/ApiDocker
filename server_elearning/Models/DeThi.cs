using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace server_elearning.Models
{
    public class DeThi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDDeThi { get; set; }
        public string MaDe { get; set; }
        public string TenDe { get; set; }
        public Nullable<double> DiemChuan { get; set; }
        public Nullable<int> ThoiGianLamBai { get; set; }
        public Nullable<int> IDND { get; set; }
        public Nullable<int> GVID { get; set; }
        public Nullable<int> TongSoCau { get; set; }
        [NotMapped]
        public virtual NoiDungDT NoiDungDT { get; set; }
    }
    public class STestResult
    {
        public int IDDeThi { get; set; }
        public string MaDe { get; set; }
        public string TenDe { get; set; }
        public Nullable<double> DiemChuan { get; set; }
        public Nullable<int> ThoiGianLamBai { get; set; }
        public Nullable<int> IDND { get; set; }
        public Nullable<int> GVID { get; set; }
        public Nullable<int> TongSoCau { get; set; }
        public List<CauHoiDeThi> CauHoiDeThi { get; set; }
    }

}
