namespace ApiCRUDE.Controllers
{
    public class TokenEntity
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string AuthToken { get; set; }
        public System.DateTime IssuedOn { get; set; }
        public System.DateTime ExpiresOn { get; set; }
    }
}