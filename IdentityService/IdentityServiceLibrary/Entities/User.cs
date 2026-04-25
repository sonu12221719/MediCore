using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IdentityServiceLibrary.Enums;

namespace IdentityServiceLibrary.Entities;
public class User
{
    [Key]
    [Column(TypeName ="varchar(4)")]
    public string UserId { get; set; } 
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public RoleOption Role { get; set; }
    public StatusOption Status { get; set; }
}

