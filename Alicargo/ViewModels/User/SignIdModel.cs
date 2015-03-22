using System.ComponentModel.DataAnnotations;

namespace Alicargo.ViewModels.User
{
    public sealed class SignIdModel
    {
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [Required]
        public string Login { get; set; }

        public bool RememberMe { get; set; }
    }
}