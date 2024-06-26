using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace server_elearning.Models
{
    public class CauHoi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDCH { get; set; }
        public string NoiDungCH { get; set; }
        public string DapAnA { get; set; }
        public string DapAnB { get; set; }
        public string DapAnC { get; set; }
        public string DapAnD { get; set; }
        public Nullable<int> IDDAĐung { get; set; }
        public Nullable<int> IDND { get; set; }
        public Nullable<int> GVID { get; set; }
    }
}
