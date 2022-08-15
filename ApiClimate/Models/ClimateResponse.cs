namespace ApiClimate.Models
{
    public class ClimateResponse
    {
       
        public string UF { get; set; }

        public string Region { get; set; }

        public string City { get; set; }

        public string ConditionClimate { get; set; }

        public DateTime Verification_Date { get; set; }

        public decimal Verification_Min { get; set; }

        public decimal Verification_Max { get; set; }

        public decimal Climate_Now { get; set; }
    }
}
