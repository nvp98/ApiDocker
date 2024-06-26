using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using server_elearning.Models;

namespace server_elearning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuggestKCCDController : ControllerBase
    {
        private readonly APICoreDBContext _context;

        public SuggestKCCDController(APICoreDBContext context)
        {
            _context = context;
        }

        // GET: api/SuggestKCCD
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContentKCCD>>> GetContentKCCD()
        {
            return await _context.ContentKCCD.ToListAsync();
        }

        // GET: api/SuggestKCCD/5
        [HttpGet("{id}")]
        public async  Task<returnSuggestKCCD> GetContentKCCD(int id)
        {
            var contentKCCD = await _context.ContentKCCD
                           .FromSqlRaw("PhieuXacNhanKCCD_select_APP @HocVienID", parameters: new[] { new SqlParameter("@HocVienID", id) }).ToListAsync();
            if(contentKCCD.Count > 0)
            {
                foreach (var a in contentKCCD)
                {
                    a.TinhTrangPhieu = a.IDTinhTrang != 0 ? a.IDTinhTrang == 2 ? "Hoàn thành" : "HV đã xác nhận" : "Chưa hoàn thành";
                }
            }
            var approveKCCD = await _context.ApproveKCCD
                          .FromSqlRaw("DeNghiKCCD_select_APP @NHD2ID", parameters: new[] { new SqlParameter("@NHD2ID", id) }).ToListAsync();
            if (approveKCCD.Count > 0)
            {
                foreach (var a in approveKCCD)
                {
                    a.TinhTrangPD = a.SLHV != 0 && a.SLHVXN == a.SLHV ? 1 : 0;
                    a.TinhTrangStr = a.TinhTrang == 2 ? "Đã phê duyệt" : "Chưa phê duyệt";
                }
            }

            //var contentKCCD = await _context.ContentKCCD.FindAsync(id);

            //if (contentKCCD == null)
            //{
            //    return NotFound();
            //}
            returnSuggestKCCD returnKQ = new returnSuggestKCCD { resultKCCDs = contentKCCD.OrderBy(x=>x.IDTinhTrang).ToList(),resultApproveKCCDs =approveKCCD.OrderByDescending(x=>x.TinhTrangPD).ToList(), total = contentKCCD.Count };

            return returnKQ;
        }

        // GET: api/SuggestKCCD/5
        [HttpGet("Approve")]
        public async Task<returnSApproveKCCD> GetApproveKCCD(int id)
        {
            var contentKCCD = await _context.ApproveKCCD
                           .FromSqlRaw("DeNghiKCCD_select_APP @NHD2ID", parameters: new[] { new SqlParameter("@NHD2ID", id) }).ToListAsync();
            if (contentKCCD.Count > 0)
            {
                foreach (var a in contentKCCD)
                {
                    a.TinhTrangPD = a.SLHV != 0 && a.SLHVXN == a.SLHV?1:0;
                    a.TinhTrangStr = a.TinhTrang == 2 ? "Đã phê duyệt" : "Chưa phê duyệt";
                }
            }

            //var contentKCCD = await _context.ContentKCCD.FindAsync(id);

            //if (contentKCCD == null)
            //{
            //    return NotFound();
            //}
            returnSApproveKCCD returnKQ = new returnSApproveKCCD { resultApproveKCCDs = contentKCCD, total = contentKCCD.Count };

            return returnKQ;
        }

        [HttpGet("NoteKCCD")]
        public async Task<PhieuXacNhanKCCD> GetNoteKCCD(int idp)
        {
            PhieuXacNhanKCCD rs =new PhieuXacNhanKCCD();
            var contentKCCD = await _context.PhieuXacNhanKCCD.Where(x=>x.ID ==idp).FirstOrDefaultAsync();
            if (contentKCCD != null) return contentKCCD;

            return rs;
        }


        // PUT: api/SuggestKCCD/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{idp}")]
        public async Task<IActionResult> PutContentKCCD(int idp, PhieuXacNhanKCCD PhieuXacNhanKCCD)
        {
            if (idp != PhieuXacNhanKCCD.ID)
            {
                return BadRequest();
            }

            _context.Entry(PhieuXacNhanKCCD).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.PhieuXacNhanKCCD.Any(e => e.ID == idp))
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

        [HttpPut("Confirm")]
        public async Task<IActionResult> PutAprroveKCCD(int iddn)
        {
            if (iddn == 0)
            {
                return BadRequest();
            }
         
            //_context.Entry(PhieuXacNhanKCCD).State = EntityState.Modified;

            try
            {
                var result = _context.Database.ExecuteSqlRaw("EXEC DeNghiKCCD_XacNhan {0},{1},{2}", iddn, 2, DateTime.Now);
                var result2 = _context.Database.ExecuteSqlRaw("EXEC PhieuXacNhanKCCD_XacNhan {0},{1},{2}", iddn, DateTime.Now, 2);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
                //if (!_context.PhieuXacNhanKCCD.Any(e => e. == iddn))
                //{
                //    return NotFound();
                //}
                //else
                //{
                //    throw;
                //}
            }

            return NoContent();
        }

        // POST: api/SuggestKCCD
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ContentKCCD>> PostContentKCCD(ContentKCCD contentKCCD)
        {
            _context.ContentKCCD.Add(contentKCCD);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContentKCCD", new { id = contentKCCD.ID }, contentKCCD);
        }

        // DELETE: api/SuggestKCCD/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ContentKCCD>> DeleteContentKCCD(int id)
        {
            var contentKCCD = await _context.ContentKCCD.FindAsync(id);
            if (contentKCCD == null)
            {
                return NotFound();
            }

            _context.ContentKCCD.Remove(contentKCCD);
            await _context.SaveChangesAsync();

            return contentKCCD;
        }

        private bool ContentKCCDExists(int id)
        {
            return _context.ContentKCCD.Any(e => e.ID == id);
        }
    }
}
