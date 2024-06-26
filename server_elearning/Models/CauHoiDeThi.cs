using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace server_elearning.Models
{
    public class CauHoiDeThi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDCauHoiDeThi { get; set; }
        public Nullable<int> IDCauHoi { get; set; }
        public Nullable<int> IDDeThi { get; set; }
        public Nullable<double> Diem { get; set; }
        public Nullable<int> IDDAĐung { get; set; }
        public String TenĐA { get; set; }
        public virtual CauHoi CauHoi { get; set; }
    }
}
