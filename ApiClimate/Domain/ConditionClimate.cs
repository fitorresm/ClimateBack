using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiClimate.Domain
{
    [Table("ConditionClimate")]
    public class ConditionClimate
    {
        [Key]
        public int Id { get; set; }

        public string Condition { get; set; }

        public DateTime Created_at { get; set; }

        public DateTime? Updated_at { get; set; }

        public DateTime? Deleted_at { get; set; }
    }
}
