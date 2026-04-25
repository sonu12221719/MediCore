namespace IdentityService.API.DTOs
{
    public class RegisterRequestDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }

        public virtual PatientRequestDto? patientRequestDtos {get;set;}
    }
}
