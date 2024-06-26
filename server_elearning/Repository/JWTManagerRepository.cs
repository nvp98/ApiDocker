using server_elearning.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace server_elearning.Repository
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        
        Dictionary<string, string> UsersRecords = new Dictionary<string, string>
	        {
		        { "user1","password1"},
		        { "user2","password2"},
		        { "user3","password3"},
	        };

		private readonly IConfiguration iconfiguration;
        //private APICoreDBContext _dbcontext;
        public JWTManagerRepository(IConfiguration iconfiguration)
		{
            this.iconfiguration = iconfiguration;
		}
		public Tokens Authenticate(Users users,UserID user)
		{
            //if (string.IsNullOrEmpty(users.Password))
            //    return null;
            //string mk = Common.Encryptor.MD5Hash(users.Password);

            //if (!UsersRecords.Any(x => x.))
            //{
            //    return null;
            //}
            var permClaims = new List<Claim>();
            //permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("ID", user.Id.ToString()));
            permClaims.Add(new Claim("MaNV", users.MaNV));
            permClaims.Add(new Claim("IDPB", user.IDPB.ToString()));
            permClaims.Add(new Claim("HoTen", user.HoTen));


            var tokenHandler = new JwtSecurityTokenHandler();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(iconfiguration["JwtAuth:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(iconfiguration["JwtAuth:Issuer"],
              iconfiguration["JwtAuth:Issuer"],
              permClaims,
              //expires: DateTime.UtcNow.AddHours(24), //change test minu
              expires: DateTime.UtcNow.AddMinutes(1),
              signingCredentials: credentials);

            return new Tokens { Token = tokenHandler.WriteToken(token) };
            //return new JwtSecurityTokenHandler().WriteToken(token);

            //// Else we generate JSON Web Token
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var tokenKey = Encoding.UTF8.GetBytes("nguyenvinhphuoc18461");
            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new Claim[]
            //  {
            // new Claim(ClaimTypes.Name, users.Name)
            //  }),
            //    Expires = DateTime.UtcNow.AddMinutes(10),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256)
            //};
            //var token = tokenHandler.CreateToken(tokenDescriptor);
            //return new Tokens { Token = tokenHandler.WriteToken(token) };

        }
	}
}
