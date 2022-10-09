using System.ComponentModel.DataAnnotations;

namespace Api.Dto.Users
{
    public class RegisterUserDto
    {
        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = null!;

        [Required]
        [DataType(DataType.EmailAddress)]
        public string EMail { get; set; } = null!;

        [Required]
        public string Cell { get; set; } = null!;

        [Required]
        public string FullName { get; set; } = null!;
    }
}
