using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server_elearning.Models;
using server_elearning.Repository;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace server_elearning.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Produces("application/json")]
    //[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : Controller
    {
        private APICoreDBContext _dbcontext;

        private readonly IJWTManagerRepository _jWTManager;

        public UserController(IJWTManagerRepository jWTManager, APICoreDBContext dbcontext)
        {
            _dbcontext = dbcontext;
            this._jWTManager = jWTManager;
        }
        // GET: UserController
        [HttpGet]
        //[Route("")]
        public async Task<List<NhanVienSQL>> GetNhanVienListAsync()
        {
            //var param = new SqlParameter("@search", "");
            return await _dbcontext.NhanVienSQL
                 .FromSqlRaw<NhanVienSQL>("NhanVien_select")
                 .ToListAsync();
        }

        // GET: UserController/Details/5
        [HttpGet("TaiKhoan")]
        public async Task<IEnumerable<TaiKhoanSQL>> GetTaiKhoanByIdAsync(string search)
        {
            var param = new SqlParameter("@search", search);
            //var parameters = new List<SqlParameter>();
            //parameters.Add(new SqlParameter("@ID", id));

            var productDetails = await _dbcontext.TaiKhoanSQL
                            .FromSqlRaw(@"exec TaiKhoan_select @search", param).ToListAsync();

            return productDetails;
        }

        //// GET: UserController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: UserController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: UserController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: UserController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: UserController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: UserController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // POST api/<UsersController>

        /// <summary>
        /// Creates a TodoItem.
        /// </summary>
        /// <param name="usersdata"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Todo
        ///     {
        ///        "MaNV": "HPDQ18461",
        ///        "password": "1"
        ///     }
        ///
        /// </remarks>
        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate(Users usersdata)
        {
            if (string.IsNullOrEmpty(usersdata.Password))
                return Unauthorized();
            string mk = Common.Encryptor.MD5Hash(usersdata.Password);
            UserID us = new UserID();
            if (!_dbcontext.NhanVien.Any(x => x.MaNV == usersdata.MaNV && x.MatKhau == mk))
            {
                return Unauthorized();
            }
            var a = _dbcontext.NhanVien.Where(x => x.MaNV == usersdata.MaNV).First();
            us.Id = a.ID;
            us.HoTen = a.HoTen;
            us.IDPB = a.IDPhongBan;
            var token = _jWTManager.Authenticate(usersdata,us);

            //String timeStamp = GetTimestamp(new DateTime());
            int now = ToUnixTimeSeconds(DateTime.UtcNow);
            if (token == null)
            {
                return Unauthorized();
            }
            else
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(token.Token);
                var exp = securityToken.Claims.FirstOrDefault(c => c.Type == "exp")?.Value;
                //Console.WriteLine(now.ToString());
                //Console.WriteLine(Int32.Parse(exp).ToString());
                if (Int32.Parse(exp) < now)
                {
                    return Unauthorized();
                }
            }
            return Ok(token);

        }
        public static int ToUnixTimeSeconds(DateTime date)
        {
            DateTime point = new DateTime(1970, 1, 1);
            TimeSpan time = date.Subtract(point);

            return (int)time.TotalSeconds;
        }

    }
}
