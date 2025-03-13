namespace Entity.Categorys
{
    public class CategorySub
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SubCategoryImage { get; set; }
        public int MainCategoryId { get; set; }
        public Category MainCategory { get; set; }
    }
}
