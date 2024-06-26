using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using server_elearning.Models;
using server_elearning.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace server_elearning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class EClassroomController : ControllerBase
    {
        private APICoreDBContext _dbcontext;

        private readonly IJWTManagerRepository _jWTManager;

        public EClassroomController(IJWTManagerRepository jWTManager, APICoreDBContext dbcontext)
        {
            _dbcontext = dbcontext;
            this._jWTManager = jWTManager;
        }

        // GET: api/<EClassroom>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<EClassroom>/5
        [HttpGet("GetXNHT")]
        public async Task<XNHocTap> Get(int idht)
        {
            //var lsbaithi = _dbcontext.BaiThi.ToList();
            var res = await (from h in _dbcontext.XNHocTap.Where(x=>x.IDHT ==idht)
                             join n in _dbcontext.NhanVien on h.NVID equals n.ID
                             join lh in _dbcontext.LopHoc on h.LHID equals lh.IDLH
                             join nd in _dbcontext.NoiDungDT on lh.NDID equals nd.IDND
                             select new XNHocTap
                             {
                                 IDHT = h.IDHT,
                                 NVID = n.ID,
                                 LHID = h.LHID,
                                 NgayTG = h.NgayTG,
                                 NgayHT = h.NgayHT,
                                 PBID = h.PBID,
                                 XNTG = h.XNTG,
                                 XNHT = h.XNHT,
                                 VTID = h.VTID,
                                 LopHoc = lh,
                                 NoiDungDT = nd,
                                 NhanVien = n,
                                 BaiThi = _dbcontext.BaiThi.Where(x => x.IDNV == n.ID && x.IDLH == lh.IDLH).OrderByDescending(x => x.LanThi).FirstOrDefault()??null
                             }).FirstOrDefaultAsync();
            //if (all != true)
            //{
            //    res = res.Where(x => x.LopHoc.TGKTLH.AddDays(1) > DateTime.Now).ToList();
            //}
            //if (XNTG != null) res = res.Where(x => x.XNTG == XNTG).ToList();
            //returnXNHT result = new returnXNHT { results = res, total = res.Count };
            return res;
        }

        // GET api/<EClassroom>/5
        [HttpGet("{id}")]
        public async Task<returnXNHT> Get(int id,bool? XNTG, bool? all)
        {
            //List<BaiThi> lsbaithi = _dbcontext.BaiThi.Where(x => x.IDNV == id).ToList();
            var res = await (from h in _dbcontext.XNHocTap
                       join n in _dbcontext.NhanVien.Where(x => x.ID == id) on h.NVID equals n.ID
                       join lh in _dbcontext.LopHoc on h.LHID equals lh.IDLH
                       join nd in _dbcontext.NoiDungDT on lh.NDID equals nd.IDND
                       select new XNHocTap
                       {
                           IDHT = h.IDHT,
                           NVID = n.ID,
                           LHID =h.LHID,
                           NgayTG =h.NgayTG,
                           NgayHT =h.NgayHT,
                           PBID =h.PBID,
                           XNTG =h.XNTG,
                           XNHT =h.XNHT,
                           VTID =h.VTID,
                           LopHoc =lh,
                           NoiDungDT =nd,
                           NhanVien = n,
                           BaiThi = _dbcontext.BaiThi.Where(x=> x.IDLH ==lh.IDLH && x.IDNV == n.ID).OrderByDescending(x=>x.LanThi).FirstOrDefault()??null
                       }).OrderByDescending(x=>x.NgayHT).ToListAsync();
            if (all != true)
            {
                res = res.Where(x=>x.LopHoc.TGKTLH.AddDays(1)> DateTime.Now).ToList();
            }
            if (XNTG != null) res = res.Where(x=>x.XNHT == XNTG).ToList();
            returnXNHT result = new returnXNHT { results = res,total =res.Count} ;
            return result;
        }

        // POST api/<EClassroom>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<EClassroom>/5
        [HttpPut("XNTG")]
        public async Task<IActionResult> PutXNTG(int idnv,int idlh)
        {
            if (idnv == 0 || idlh ==0)
            {
                return BadRequest();
            }
            try
            {
                var XN = _dbcontext.XNHocTap.Where(x => x.NVID == idnv && x.LHID == idlh).FirstOrDefault();
                if (XN.XNTG == false) {
                    var result = _dbcontext.Database.ExecuteSqlRaw("EXEC XNHocTap_update {0},{1},{2},{3},{4},{5},{6},{7},{8}", XN.IDHT, idnv, idlh, DateTime.Now, XN.NgayHT, true, XN.XNHT, XN.PBID, XN.VTID);
                    await _dbcontext.SaveChangesAsync();
                    return NoContent();
                }
                else
                {
                    return Problem("Tham gia lại bài học");
                }
               
            }
            catch (DbUpdateConcurrencyException)
            {
                return Problem("Có lỗi xảy ra");
            }
        }

        [HttpPut("XNHT")]
        public async Task<IActionResult> PutXNHT(int idht)
        {
            if (idht == 0)
            {
                return BadRequest();
            }
            try
            {
                var XN = _dbcontext.XNHocTap.Where(x => x.IDHT == idht).FirstOrDefault();
                if (XN.XNHT == false)
                {
                    var result = _dbcontext.Database.ExecuteSqlRaw("EXEC XNHocTap_update {0},{1},{2},{3},{4},{5},{6},{7},{8}", XN.IDHT, XN.NVID, XN.LHID, XN.NgayTG, DateTime.Now, XN.XNTG, true, XN.PBID, XN.VTID);
                    await _dbcontext.SaveChangesAsync();
                    return NoContent();
                }
                else
                {
                    return Problem("Đã hoàn thành lớp học");
                }

            }
            catch (DbUpdateConcurrencyException)
            {
                return Problem("Có lỗi xảy ra");
            }
        }

        // DELETE api/<EClassroom>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
