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

        public async Task UploadAsync(IFileListEntry fileEntry, EntJobs job)
        {
            try
            {
                var path = Path.Combine(_environment.ContentRootPath, "wwwroot", fileEntry.Name);

                // Log the file path for debugging
                Console.WriteLine($"File path: {path}");

                var ms = new MemoryStream();
                await fileEntry.Data.CopyToAsync(ms);

                using (FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    ms.WriteTo(file);
                }

                // Save the file name to the Thumbnail property of the provided EntPosts instance
                job.Thumbnail = fileEntry.Name;

                // Log successful file upload
                Console.WriteLine($"File '{fileEntry.Name}' uploaded successfully. Thumbnail saved to EntJobs instance.");
            }
            catch (Exception ex)
            {
                // Log any exceptions during file upload
                Console.WriteLine($"Error uploading file: {ex.Message}");
            }
        }

    }
}