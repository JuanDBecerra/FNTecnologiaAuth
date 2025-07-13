namespace Prueba.Domain.Entities.Model
{
    public class Users
    {
        public int Usr_Id { get; set; }
        public string Usr_FirstName { get; set; } = string.Empty;
        public string Usr_LastName { get; set; } = string.Empty;
        public string Usr_DocumentNumber { get; set; } = string.Empty;
        public int Usr_RolId { get; set; }
        public string Password { get; set; } = string.Empty;

        public Rols? Rols { get; set; }
    }

}
