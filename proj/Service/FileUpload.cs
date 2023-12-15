using BlazorInputFile;
using Entities;
using Microsoft.AspNetCore.Hosting; // Import the necessary namespace for IWebHostEnvironment
using System;
using System.IO;
using System.Threading.Tasks;

namespace proj.Service
{
    public class FileUpload : IFileUpload
    {
        private readonly IWebHostEnvironment _environment;

        public FileUpload(IWebHostEnvironment environment)
        {
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        // Inside the FileUpload service
        public async Task UploadAsync(IFileListEntry fileEntry, JobApplication job)
        {
            try
            {
                var path = Path.Combine(_environment.WebRootPath, fileEntry.Name); // Use WebRootPath instead of ContentRootPath
                Console.WriteLine($"File path: {path}");

                var ms = new MemoryStream();
                await fileEntry.Data.CopyToAsync(ms);

                using (FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    ms.WriteTo(file);
                }

                job.CVFile = fileEntry.Name;
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
