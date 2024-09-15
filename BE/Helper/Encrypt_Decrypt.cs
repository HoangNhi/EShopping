using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using MODELS.COMMON;
using MODELS.HETHONG.TAIKHOAN.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace BE.Helper
{
    public class Encrypt_Decrypt
    {
        public static string GenerateSalt()
        {
            var salt = RandomNumberGenerator.GetBytes(128 / 8);
            return Convert.ToBase64String(salt);
        }

        public static string EncodePassword(string pass, string salt)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                            password: pass!,
                            salt: Encoding.Unicode.GetBytes(salt),
                            prf: KeyDerivationPrf.HMACSHA256,
                            iterationCount: 100000,
                            numBytesRequested: 256 / 8));
            return hashed;
        }

        public static string GenerateJwtToken(MODELTaiKhoan taiKhoan, IConfiguration config)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Name, taiKhoan.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, taiKhoan.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(config["Jwt:Issuer"],
                config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddDays(CommonConst.ExpireTime),
                signingCredentials: credentials);

            return tokenHandler.WriteToken(token);
        }
    }
}
