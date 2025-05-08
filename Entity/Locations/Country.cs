namespace Entity.Locations
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public int RegionId { get; set; }
        public Region Region { get; set; }

        public ICollection<Province> Provinces { get; set; }
        public ICollection<State> States { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
