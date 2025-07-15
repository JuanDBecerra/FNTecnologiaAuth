using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Prueba.Domain.Entities.Model;
using Prueba.Domain.Entities.Request;
using Prueba.Domain.Interfaces;
using Prueba.Infraestructure.Contexts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Prueba.Application.Services
{
    public class AuthService : GenericService<Users>, IAuthService
    {
        public AuthService(IRepository<Users> repository) : base(repository) { }

        public Users Create(CreateUserRequest payload)
        {
            var user = Get().FirstOrDefault(s => s.Usr_DocumentNumber.Equals(payload.DocumentNumber));
            if (user != null)
                throw new InvalidDataException($"Ya se encuentra registrado el usuario con documento número {payload.DocumentNumber}.");
            return Add(new() {
                Usr_FirstName = payload.Name,
                Usr_LastName = payload.LastName,
                Usr_DocumentNumber = payload.DocumentNumber,
                Password = payload.Password,
                Usr_RolId = payload.Rol                
            });
        }

        public string  Auth(AuthRequest payload)
        {
            var user = Get()
                .Include(s => s.Rols)
                .FirstOrDefault(s => s.Usr_DocumentNumber.Equals(payload.UserName))
                ?? throw new InvalidDataException("Usuario no registrado.");
            bool isValid = PasswordHelper.VerifyPassword(payload.Password, user.Password);

            if (!isValid)
                throw new InvalidDataException("Contraseña incorrecta");

            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(Users user)
        {
            var claims = new[]
            {
                new Claim("Rol", user.Rols.Rol_Description),
                new Claim("UserId", user.Usr_RolId.ToString()),
                new Claim("Name", user.Usr_FirstName),
                new Claim("LastName", user.Usr_LastName),
                new Claim("DocumentNumber", user.Usr_DocumentNumber),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("8y378237bsgf823yhr782y3i7823iuhwy1412"));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
