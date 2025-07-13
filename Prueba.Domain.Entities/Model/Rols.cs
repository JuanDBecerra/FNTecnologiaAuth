namespace Prueba.Domain.Entities.Model
{
    public class Rols
    {
        public int Rol_Id {get; set; }
        public string Rol_Description { get; set; } = string.Empty;
        public ICollection<Users>? Users { get; set; }
    }
}
