using System.ComponentModel.DataAnnotations;

namespace Entity.Files
{
    public class FileUploadResult
    {
        public string FileName { get; set; }

        public string Url { get; set; }
    }
}
