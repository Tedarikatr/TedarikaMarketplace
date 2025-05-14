namespace Entity.Incoterms
{
    public class Incoterm
    {
        public int Id { get; set; }

        public IncotermType Code { get; set; } 
        public string Name { get; set; }
        public string ShortCode { get; set; } 
        public string Description { get; set; } 

        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }

    }
    public enum IncotermType
    {
        EXW = 1,
        FOB = 2,
        CIF = 3,
        DAP = 4,
        DDP = 5
    }
}
