namespace IdentityService.API.DTOs
{
    public class LoginResponseDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime LoginAt { get; set; }
    }
}
