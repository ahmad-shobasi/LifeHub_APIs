namespace LifeHub_APIs.Models
{
    public class LoginResponse
    {
        public string AccessToken { get; set; } = String.Empty;
        public string RefreshToken { get; set; } = String.Empty;
        public DateTime ExpirationDate { get; set; }
        public string UserName { get; set; } = String.Empty;
    }
}
