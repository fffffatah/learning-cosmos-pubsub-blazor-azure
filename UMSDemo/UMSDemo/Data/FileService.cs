using DataAccess;
using BusinessLogic;
using Microsoft.AspNetCore.Components.Forms;

namespace UMSDemo.Data
{
    public class FileService
    {
        public async Task<string> Upload(IBrowserFile file)
        {
            return await new BlobProvider().UploadFileToBlobAsync(file.Name, file);
        }
        public async Task<bool> Delete(string fileName)
        {
            return await new BlobProvider().DeleteFromBlobAsync(fileName);
        }
        public List<EntityLayer.File> GetFiles()
        {
            return FileLogic.GetAll();
        }

        public EntityLayer.File Get(Guid id)
        {
            return FileLogic.Get(id);
        }

        public bool Update(EntityLayer.File file)
        {
            return FileLogic.Update(file);
        }

        public bool AddFile(EntityLayer.File file)
        {
            return FileLogic.Add(file);
        }

        public bool DeleteFile(Guid id)
        {
            return FileLogic.Delete(id);
        }
    }
}
