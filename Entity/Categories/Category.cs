namespace Entity.Categorys
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryImage { get; set; }
        public ICollection<CategorySub> CategoriesSubs { get; set; } = new List<CategorySub>();
    }
}
