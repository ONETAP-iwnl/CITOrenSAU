using System.ComponentModel.DataAnnotations;

namespace UserService.Model
{
    public class Authors
    {
        [Key]
        public int ID_Author { get; set; }
        public int ID_User { get; set; } //Foreign Key to Users
    }
}