namespace Entity.Markets.Locations
{
    public class District
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public int ProvinceId { get; set; }
        public Province Province { get; set; }
        public ICollection<Neighborhood> Neighborhoods { get; set; }
    }
}
