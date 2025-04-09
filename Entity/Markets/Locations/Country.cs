namespace Entity.Markets.Locations
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string Code { get; set; } 
        public ICollection<Province> Provinces { get; set; }
    }

}
