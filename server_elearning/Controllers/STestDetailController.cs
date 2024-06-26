using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server_elearning.Models;
using server_elearning.Repository;
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
    public class STestDetailController : ControllerBase
    {
        private APICoreDBContext _dbcontext;

        private readonly IJWTManagerRepository _jWTManager;

        public STestDetailController(IJWTManagerRepository jWTManager, APICoreDBContext dbcontext)
        {
            _dbcontext = dbcontext;
            this._jWTManager = jWTManager;
        }
        // GET: api/<STestDetailController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        /// <summary>
        /// id =IDLH
        /// </summary>
        // GET api/<STestDetailController>/5
        [HttpGet("{id}")]
        public async Task<STestDetail> Get(int id,int? IDNV)
        {
            var ress = await _dbcontext.BaiThi.Where(x => x.IDLH == id && x.IDNV == IDNV).OrderByDescending(x=>x.LanThi).ToListAsync();
            if(ress.Count ==0) return new STestDetail();
            var maxStest = ress.FirstOrDefault();
            var res = await (from a in _dbcontext.BaiThi.Where(x => x.IDLH == id && x.IDNV == IDNV).OrderByDescending(x => x.LanThi)
                             select new STestDetail
                             {
                               IDLH = a.IDLH,
                               IDNV = a.IDNV,
                               LanThi = a.LanThi,
                               IDBaiThi =a.IDBaiThi,
                               BaiThi =a,
                               CTBaiThi = (from b in _dbcontext.CTBaiThi.Where(x=>x.IDBaiThi == a.IDBaiThi)
                                           select new CTBaiThi
                                           {
                                               Diem = b.Diem,
                                               IDCTBT = b.IDCTBT,
                                               IDBaiThi = b.IDBaiThi,
                                               IDCauHoi = b.IDCauHoi,
                                               IDDapAnDung = b.IDDapAnDung,
                                               IDDApAnNV = b.IDDApAnNV
                                           }).ToList(),
                             }).FirstOrDefaultAsync();
            //STestDetail result = new STestDetail
            //{
            //    CTBaiThi = res,
            //    IDDeThi = maxStest.IDDeThi,
            //    IDLH = maxStest.IDLH,
            //    IDND = maxStest.IDND,
            //    IDNV = maxStest.IDNV,
            //    LanThiEnd = maxStest.LanThi
            //};
            return res;
        }

        // POST api/<STestDetailController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<STestDetailController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<STestDetailController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
