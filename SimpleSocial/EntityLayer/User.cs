using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string? Pass { get; set; }
        public bool IsAdmin { get; set; }
    }
    public class UserLoginModel
    {
        [Required]
        [StringLength(10)]
        public string? Username { get; set; }
        [Required]
        public string? Pass { get; set; }
    }
    public class UserRegistrationModel
    {
        [Required, StringLength(50, MinimumLength = 6)]
        public string? Name { get; set; }
        [Required, StringLength(16, MinimumLength = 6)]
        public string? Username { get; set; }
        [Required, RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "* Password Must be at least 8 in Length and Must Contain Uppercase, Number and a Special Character")]
        public string? Pass { get; set; }
        [Required, Compare("Pass")]
        public string? ConfirmPass { get; set; }
        public bool IsAdmin { get; set; }
    }
    public class UserUpdateModel
    {
        [Required, StringLength(50, MinimumLength = 6)]
        public string? Name { get; set; }
    }
}
