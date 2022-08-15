using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiClimate.Domain
{
    [Table("UserAplication")]
    public class UserAplication
    {
        [Key]
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public DateTime Created_at { get; set; }

        public DateTime? Updated_at { get; set; }

        public DateTime? Deleted_at { get; set; }
    }
}
