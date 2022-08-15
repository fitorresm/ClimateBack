using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiClimate.Domain
{
    [Table("UF")]
    public class UF
    {
        [Key]
        public int Id { get; set; }

        public string Sigla { get; set; }

        public DateTime Created_at { get; set; }

        public DateTime? Updated_at { get; set; }

        public DateTime? Deleted_at { get; set; }
    }
}
