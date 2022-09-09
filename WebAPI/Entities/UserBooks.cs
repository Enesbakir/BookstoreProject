using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Entities{

    public class UserBooks{
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
    }
}