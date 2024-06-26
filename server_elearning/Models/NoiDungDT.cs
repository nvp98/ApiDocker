using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace server_elearning.Models
{
    public class NoiDungDT
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDND { get; set; }
        public string MaND { get; set; }
        public string NoiDung { get; set; }
        public string VideoND { get; set; }
        public string ImageND { get; set; }
        public int BPLID { get; set; }
        public int LVDTID { get; set; }
        public int IDCTLVDT { get; set; }
        public int ThoiLuongDT { get; set; }
        public string FileDinhKem { get; set; }
    }
}
