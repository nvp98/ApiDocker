using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace server_elearning.Models
{
    public class LopHoc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDLH { get; set; }
        public string MaLH { get; set; }
        public string TenLH { get; set; }
        public int? NDID { get; set; }
        public int? QuyDT { get; set; }
        public int? NamDT { get; set; }
        public DateTime TGBDLH { get; set; }
        public DateTime TGKTLH { get; set; }
        public int? GVID { get; set; }
        public int? IDDeThi { get; set; }
        public bool ToChucThi { get; set; }
    }

}
