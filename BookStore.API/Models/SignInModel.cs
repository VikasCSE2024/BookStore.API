using System.ComponentModel.DataAnnotations;

namespace BookStore.API.Models
{
    public class signInModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        
    }
}
