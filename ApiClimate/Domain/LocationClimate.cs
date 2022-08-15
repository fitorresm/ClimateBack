using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiClimate.Domain
{
    [Table("LocationClimate")]
    public class LocationClimate
    {
        [Key]
        public int Id { get; set; }

        public int Id_UF { get; set; }

        public int Id_Region { get; set; }

        public int Id_City { get; set; }

        public int Id_ConditionClimate { get; set; }

        public DateTime Verification_Date { get; set; }

        public decimal Verification_Min { get; set; }

        public decimal Verification_Max { get; set; }

        public decimal Climate_Now { get; set; }
    }
}
