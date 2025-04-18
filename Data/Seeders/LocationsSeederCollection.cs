using Entity.Markets.Locations;

namespace Data.Seeders
{
    public static class LocationsSeederCollection
    {
        public static List<Region> GetRegions() => new List<Region>
        {
            new Region { Name = "Avrupa", Code = "EU" },
            new Region { Name = "Orta Doğu", Code = "MENA" },
            new Region { Name = "Afrika", Code = "AFR" },
            new Region { Name = "Orta Asya", Code = "CAS" },
            new Region { Name = "Kuzey Amerika", Code = "NA" },
            new Region { Name = "Güney Amerika", Code = "SA" },
            new Region { Name = "Okyanusya", Code = "OCN" },
            new Region { Name = "Uzak Doğu", Code = "FEA" }
        };
    }
}
