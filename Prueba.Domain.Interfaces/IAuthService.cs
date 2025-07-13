using Prueba.Domain.Entities.Model;
using Prueba.Domain.Entities.Request;

namespace Prueba.Domain.Interfaces
{
    public interface IAuthService : IGenericService<Users>
    {
        Users Create(CreateUserRequest payload);
        string Auth(AuthRequest payload);
    }
}
