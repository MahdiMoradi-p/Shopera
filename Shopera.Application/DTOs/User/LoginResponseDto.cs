namespace Shopera.Application.DTOs.User
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}