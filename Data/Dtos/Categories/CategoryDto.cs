namespace Data.Dtos.Categories
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryImage { get; set; }
    }

    public class CategoryCreateDto
    {
        public string CategoryName { get; set; }
        public string? CategoryImage { get; set; }  
    }

    public class CategoryUpdateDto
    {
        public string CategoryName { get; set; }
    }
}
