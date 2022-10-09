using System.ComponentModel.DataAnnotations;

namespace Api.Dto.Users
{
    public class LoginUserDto
    {
        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
