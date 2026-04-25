namespace IdentityService.API.DTOs
{
    public class UserResponseDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
    }
}
