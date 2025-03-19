namespace Services.Files.IServices
{
    public interface IPdfService
    {
        Task<string> UploadPdfAsync(Stream pdfStream, string fileName);

        Task DeletePdfAsync(string fileName);
    }
}
