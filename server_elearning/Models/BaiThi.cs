using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace server_elearning.Models
{
    public class BaiThi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDBaiThi { get; set; }
        public Nullable<int> IDLH { get; set; }
        public Nullable<int> IDDeThi { get; set; }
        public Nullable<int> IDND { get; set; }
        public Nullable<int> IDNV { get; set; }
        public Nullable<int> IDPhongBan { get; set; }
        public Nullable<int> IDViTri { get; set; }
        public Nullable<double> DiemSo { get; set; }
        public Nullable<System.DateTime> NgayThi { get; set; }
        public Nullable<bool> TinhTrang { get; set; }
        public Nullable<int> LanThi { get; set; }
        public Nullable<bool> Flag { get; set; }
    }
    public class STestDetail
    {
        public Nullable<int> IDLH { get; set; }
        public Nullable<int> IDNV { get; set; }
        public Nullable<int> LanThi { get; set; }
        public int IDBaiThi { get; set; }
        public BaiThi BaiThi { get; set; }
        public List<CTBaiThi> CTBaiThi { get; set; }
    }
    public class ConfirmSTest
    {
       public List<CTSTest> ListCTSTest { get; set; }
       public BaiThi BaiThi { get; set; }   
    }
    public class CTSTest
    {
        //public Nullable<int> IDBT { get; set; }
        public Nullable<int> IDCauHoi { get; set; }
        public Nullable<int> IDDapAnDung { get; set; }
        public Nullable<int> IDDApAnNV { get; set; }
        public Nullable<double> Diem { get; set; }
    }
}
