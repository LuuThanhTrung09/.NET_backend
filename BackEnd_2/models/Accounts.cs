using System.ComponentModel.DataAnnotations;

namespace Game106_API.Models
{
    public class Account
    {
        [Key]
        public int Id {get; set;}
        public string? Email {get; set;}
        public string? Password {get; set;}
        public string? Password_Salt {get; set;}
        public string? Name {get; set;}
        public DateTime Created_at {get; set;} = DateTime.Now;
        public DateTime Updated_at {get; set;} = DateTime.Now;
    }
}