using System.ComponentModel.DataAnnotations;

namespace UserService.Model
{
    public class Executors
    {
        [Key]
        public int ID_Executor { get; set; }
        public int ID_User { get; set; }
    }
}