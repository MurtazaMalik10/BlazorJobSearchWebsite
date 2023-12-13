using BlazorInputFile;
using Entities;

namespace proj.Service
{
    public class FileUpload : IFileUpload
    {
        private readonly IWebHostEnvironment _environment;
        public FileUpload(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        // Inside the FileUpload service
        public async Task UploadAsync(IFileListEntry fileEntry, EntJobs job)
        {
            try
            {
                var path = Path.Combine(_environment.ContentRootPath, "wwwroot", fileEntry.Name);
                Console.WriteLine($"File path: {path}");

                var ms = new MemoryStream();
                await fileEntry.Data.CopyToAsync(ms);

                using (FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    ms.WriteTo(file);
                }

                job.Thumbnail = fileEntry.Name;
                Console.WriteLine($"File '{fileEntry.Name}' uploaded successfully. Thumbnail saved to EntJobs instance.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error uploading file: {ex.Message}");
                throw; // Re-throw the exception to capture it in the calling code
            }
        }


    }
}