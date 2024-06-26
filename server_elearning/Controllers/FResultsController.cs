using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server_elearning.Models;
using Microsoft.Data.SqlClient;

namespace server_elearning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FResultsController : ControllerBase
    {
        private readonly APICoreDBContext _context;

        public FResultsController(APICoreDBContext context)
        {
            _context = context;
        }

        // GET: api/FResults
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FResult>>> GetFResult()
        {
            return await _context.FResult.ToListAsync();
        }

        // GET: api/FResults/5
        [HttpGet("{id}")]
        public async Task<returnFResult> GetFResult(int id, string year)
        {
            int yearIndex = int.Parse(year);
            DateTime begin = new DateTime(yearIndex, 1,1);
            DateTime endd = new DateTime(yearIndex, 12, 1);
            List<FResult> KQua = new List<FResult>();
            

            foreach (DateTime day in EachMont(begin, endd))
            {
                DateTime firstDayOfMonth = new DateTime(day.Year, day.Month, 1);
                var idnv = new SqlParameter("@IDNV", System.Data.SqlDbType.Int);
                var thangdg = new SqlParameter("@ThangDG", System.Data.SqlDbType.DateTime);
                idnv.Value = id;
                thangdg.Value = firstDayOfMonth;
                SqlParameter[] parameters = {
            new SqlParameter("@IDNV", id),
            new SqlParameter("@ThangDG", firstDayOfMonth),
                };

                var fResult = await _context.FResult
                            .FromSqlRaw("KNL_LSDG_Select @IDNV, @ThangDG", parameters: new[] { new SqlParameter("@IDNV", id), new SqlParameter("@ThangDG", firstDayOfMonth) }).ToListAsync();

                if (fResult.Count != 0)
                {
                    fResult.FirstOrDefault().ThangDGStr = day.Month + "/" + day.Year;
                    KQua.Add(fResult.FirstOrDefault());
                }
                else {
                    KQua.Add(new FResult()
                    {
                        ThangDGStr = day.Month + "/" + day.Year,
                        ThangDG = firstDayOfMonth
                    });
                }
            }
            //if (KQua == null)
            //{
            //    return NotFound();
            //}
            returnFResult returnKQ = new returnFResult { fResults =KQua,total =KQua.Count};

            return returnKQ;

        }

        // PUT: api/FResults/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFResult(int id, FResult fResult)
        {
            if (id != fResult.IDLS)
            {
                return BadRequest();
            }

            _context.Entry(fResult).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FResultExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/FResults
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<FResult>> PostFResult(FResult fResult)
        {
            _context.FResult.Add(fResult);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFResult", new { id = fResult.IDLS }, fResult);
        }

        // DELETE: api/FResults/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FResult>> DeleteFResult(int id)
        {
            var fResult = await _context.FResult.FindAsync(id);
            if (fResult == null)
            {
                return NotFound();
            }

            _context.FResult.Remove(fResult);
            await _context.SaveChangesAsync();

            return fResult;
        }

        private bool FResultExists(int id)
        {
            return _context.FResult.Any(e => e.IDLS == id);
        }
        public static IEnumerable<DateTime> EachMont(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddMonths(1))
                yield return day;
        }
    }
}
