namespace Data.Dtos.Categories
{
    public class CategorySubDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SubCategoryImage { get; set; }
        public int MainCategoryId { get; set; }
    }
    public class CategorySubCreateDto
    {
        public string Name { get; set; }
        public string SubCategoryImage { get; set; }
        public int MainCategoryId { get; set; }
    }

    public class CategorySubUpdateDto
    {
        public string Name { get; set; }
        public string SubCategoryImage { get; set; }
    }
}
