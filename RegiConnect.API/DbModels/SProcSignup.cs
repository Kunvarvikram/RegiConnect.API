namespace Regiconnect.Api.DbModels
{
    public class SProcSignup
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
