namespace Entity.Markets.Locations
{
    public class Region
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string Code { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<Country> Countries { get; set; }
    }

}
