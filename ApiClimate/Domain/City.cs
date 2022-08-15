using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiClimate.Domain
{
    [Table("City")]
    public class City
    {
        [Key]
        public int Id { get; set; }

        public string Nome { get; set; }

        public int Id_UF { get; set; }

        public int Id_Region { get; set; }

        public DateTime Created_at { get; set; }

        public DateTime? Updated_at { get; set; }

        public DateTime? Deleted_at { get; set; }
    }
}
