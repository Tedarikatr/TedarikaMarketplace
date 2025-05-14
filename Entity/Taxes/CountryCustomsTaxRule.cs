using Entity.Incoterms;
using Entity.Locations;

namespace Entity.Taxes
{
    public class CountryCustomsTaxRule
    {
        public int Id { get; set; }

        public string GTIPCode { get; set; } 
        public int CountryId { get; set; }

        public int CountryCode { get; set; }
        public Country Country { get; set; }

        public IncotermType Incoterm { get; set; } 

        public decimal TaxRate { get; set; } 
        public string Description { get; set; } 

        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
    }
}
