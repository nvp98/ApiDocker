using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace server_elearning.Models
{
    public class CTBaiThi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDCTBT { get; set; }
        public Nullable<int> IDBaiThi { get; set; }
        public Nullable<int> IDCauHoi { get; set; }
        public Nullable<int> IDDapAnDung { get; set; }
        public Nullable<int> IDDApAnNV { get; set; }
        public Nullable<double> Diem { get; set; }
    }
}
