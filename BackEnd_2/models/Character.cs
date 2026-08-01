using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Game106_API.Models
{
    public class Character
    {
        [Key]
        public int Id {get; set;}
        public int Account_id {get; set;}
        public string? Name {get; set;}
        public int Level {get; set;}
        public int Exp {get; set;}
        public DateTime Created_at {get; set;}
        public DateTime Updated_at {get; set;}
        [NotMapped]
        public string? Email {get; internal set;}
    }
}