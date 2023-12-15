using BlazorInputFile;
using Entities;

namespace proj.Service
{
    public interface IFileUpload
    {
        Task UploadAsync(IFileListEntry file, JobApplication job);
       
    }
}