using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using server_elearning.Models;
using server_elearning.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace server_elearning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class STestController : ControllerBase
    {
        private APICoreDBContext _dbcontext;

        private readonly IJWTManagerRepository _jWTManager;

        public STestController(IJWTManagerRepository jWTManager, APICoreDBContext dbcontext)
        {
            _dbcontext = dbcontext;
            this._jWTManager = jWTManager;
        }
        // GET: api/<STestController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        /// <summary>
        /// id LopHoc
        /// </summary>
        // GET api/<STestController>/5
        [HttpGet("{id}")]
        public async Task<STestResult> Get(int id)
        {
            var res = await (from l in _dbcontext.LopHoc.Where(x => x.IDLH == id)
                             join dt in _dbcontext.DeThi on l.IDDeThi equals dt.IDDeThi
                             select new STestResult
                             {
                                 IDDeThi = (int)l.IDDeThi,
                                 IDND = (int)l.NDID,
                                 TenDe = dt.TenDe,
                                 GVID = dt.GVID,
                                 DiemChuan = dt.DiemChuan,
                                 ThoiGianLamBai = dt.ThoiGianLamBai,
                                 MaDe = dt.MaDe,
                                 TongSoCau = dt.TongSoCau,
                                 CauHoiDeThi = (from a in _dbcontext.CauHoiDeThi.Where(x=>x.IDDeThi ==l.IDDeThi)
                                                join cha in _dbcontext.CauHoi on a.IDCauHoi equals cha.IDCH
                                                join daa in _dbcontext.DanhSachDA on cha.IDDAĐung equals daa.IDDSĐA 
                                                select new CauHoiDeThi
                                                {
                                                  IDCauHoiDeThi =a.IDCauHoiDeThi,
                                                  IDCauHoi =a.IDCauHoi,
                                                  IDDeThi =a.IDDeThi,
                                                  Diem =a.Diem,
                                                  CauHoi = cha,
                                                  IDDAĐung = cha.IDDAĐung,
                                                  TenĐA =daa.TenĐA,
                                                }).ToList(),
                             }).FirstOrDefaultAsync();
            return res;
        }

        // POST api/<STestController>
        [HttpPost("Confirm")]
        public IActionResult Post(ConfirmSTest stestdata)
        {
            if(stestdata == null) { return NotFound(); }
            var ListStest = stestdata.ListCTSTest;
            int i = 0;
            if (ListStest.Count > 0 && stestdata.BaiThi != null)
            {
                var baithi = stestdata.BaiThi;
                var lanthi = _dbcontext.BaiThi.Where(x => x.IDNV ==baithi.IDNV  && x.IDDeThi == baithi.IDDeThi && x.IDLH == baithi.IDLH).ToList();
                if(lanthi.Count >= 3) { return Problem("Bạn đã thi quá 3 lần!"); }
                else if(lanthi.Where(x => x.TinhTrang == true).Count() > 0) { return Problem("Bạn đã thi đạt"); }
                else
                {
                    var outputBitParameter = new SqlParameter
                    {
                        ParameterName = "IDBaiThi",
                        SqlDbType = SqlDbType.Int,
                        Direction = ParameterDirection.Output
                    };
                   
                    if (i==0)
                    {
                      var result = _dbcontext.Database.ExecuteSqlRaw("EXEC BaiThi_insert {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},@IDBaiThi OUTPUT", baithi.IDLH,baithi.IDDeThi,baithi.IDND,baithi.IDNV,baithi.IDPhongBan,baithi.IDViTri,0, DateTime.Now, false,lanthi.Count+1, outputBitParameter);
                    }
                    var IDBaiThi = outputBitParameter.Value;
                    foreach (var S in ListStest)
                    {
                        if(S.IDDapAnDung == S.IDDApAnNV)
                        {
                            var result2 = _dbcontext.Database.ExecuteSqlRaw("EXEC CTBaiThi_insert {0},{1},{2},{3},{4}", IDBaiThi, S.IDCauHoi, S.IDDapAnDung, S.IDDApAnNV, S.Diem);
                        }
                        else
                        {
                            var result2 = _dbcontext.Database.ExecuteSqlRaw("EXEC CTBaiThi_insert {0},{1},{2},{3},{4}", IDBaiThi, S.IDCauHoi, S.IDDapAnDung, S.IDDApAnNV, 0);
                        }
                        i++;
                    }
                    double diemso = (double)_dbcontext.CTBaiThi.Where(x => x.IDBaiThi == (int)IDBaiThi).Sum(x => x.Diem);
                    var bt = _dbcontext.DeThi.Where(x => x.IDDeThi == baithi.IDDeThi).SingleOrDefault();
                    if(diemso >= bt.DiemChuan)
                    {
                        var kq = _dbcontext.Database.ExecuteSqlRaw("EXEC BaiThi_Update {0},{1},{2}", diemso, true, IDBaiThi);
                    }
                    else
                    {
                        var kq = _dbcontext.Database.ExecuteSqlRaw("EXEC BaiThi_Update {0},{1},{2}", diemso, false, IDBaiThi);
                    }
                    return StatusCode(201);
                }
              
            }
            else
            {
                return Problem("Kiểm tra lại nội dung!");
            }
        }

        // PUT api/<STestController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<STestController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
