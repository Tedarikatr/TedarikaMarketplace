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

        public static List<Country> GetEuropeanCountries(int regionId) => new List<Country>
        {
            new Country { Name = "Almanya", Code = "DE", RegionId = regionId },
            new Country { Name = "Fransa", Code = "FR", RegionId = regionId },
            new Country { Name = "İtalya", Code = "IT", RegionId = regionId },
            new Country { Name = "İspanya", Code = "ES", RegionId = regionId },
            new Country { Name = "Hollanda", Code = "NL", RegionId = regionId },
            new Country { Name = "Belçika", Code = "BE", RegionId = regionId },
            new Country { Name = "Avusturya", Code = "AT", RegionId = regionId },
            new Country { Name = "Polonya", Code = "PL", RegionId = regionId },
            new Country { Name = "Macaristan", Code = "HU", RegionId = regionId },
            new Country { Name = "Romanya", Code = "RO", RegionId = regionId },
            new Country { Name = "Bulgaristan", Code = "BG", RegionId = regionId },
            new Country { Name = "Çekya", Code = "CZ", RegionId = regionId },
            new Country { Name = "Yunanistan", Code = "GR", RegionId = regionId },
            new Country { Name = "Slovakya", Code = "SK", RegionId = regionId },
            new Country { Name = "Portekiz", Code = "PT", RegionId = regionId },
            new Country { Name = "İrlanda", Code = "IE", RegionId = regionId },
            new Country { Name = "Litvanya", Code = "LT", RegionId = regionId },
            new Country { Name = "Letonya", Code = "LV", RegionId = regionId },
            new Country { Name = "Estonya", Code = "EE", RegionId = regionId },
            new Country { Name = "Hırvatistan", Code = "HR", RegionId = regionId },
            new Country { Name = "Slovenya", Code = "SI", RegionId = regionId },
            new Country { Name = "Lüksemburg", Code = "LU", RegionId = regionId },
            new Country { Name = "Malta", Code = "MT", RegionId = regionId },
            new Country { Name = "Finlandiya", Code = "FI", RegionId = regionId },
            new Country { Name = "Danimarka", Code = "DK", RegionId = regionId },
            new Country { Name = "İsveç", Code = "SE", RegionId = regionId },
            new Country { Name = "Norveç", Code = "NO", RegionId = regionId }
        };
    }
}
