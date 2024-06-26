using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace server_elearning.Models
{
    public class TaiKhoan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        //public int? IDCa { get; set; }
        //public int? IDVT { get; set; }
        //public int? IDQuyen { get; set; }
    }
}
