using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiClimate.Domain
{
    [Table("TokenAplicacao")]
    public class TokenAplicacao
    {
        [Key]
        public int Id { get; set; }

        public string Token { get; set; }

        public DateTime Created_at { get; set; }

        public DateTime? Updated_at { get; set; }

        public DateTime Expired_at { get; set; }

        public int? Id_UserAplication { get; set; }

        public bool Active { get; set; }
    }
}
