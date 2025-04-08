using System.Text.Json;

namespace API.Validators.DeliveryAddress
{
    public static class AddressJsonLoader
    {
        private static readonly string JsonPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "AddressSeedData.json");

        private static JsonDocument _cached;

        public static JsonDocument LoadJson()
        {
            if (_cached != null) return _cached;

            var jsonText = File.ReadAllText(JsonPath);
            _cached = JsonDocument.Parse(jsonText);
            return _cached;
        }

        public static bool IsValidAddress(string country, string province, string district)
        {
            var json = LoadJson();

            var countries = json.RootElement.EnumerateArray();
            foreach (var c in countries)
            {
                if (c.GetProperty("country").GetString() == country)
                {
                    var provinces = c.GetProperty("provinces").EnumerateArray();
                    foreach (var p in provinces)
                    {
                        if (p.GetProperty("name").GetString() == province)
                        {
                            var districts = p.GetProperty("districts").EnumerateArray();
                            return districts.Any(d => d.GetString() == district);
                        }
                    }
                }
            }
            return false;
        }
    }
}
