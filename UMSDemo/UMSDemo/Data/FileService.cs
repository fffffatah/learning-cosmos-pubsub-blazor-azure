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
        public async Task<List<EntityLayer.File>> GetFiles()
        {
            return await FileLogic.GetAll();
        }

        public async Task<EntityLayer.File> Get(Guid id)
        {
            return await FileLogic.Get(id);
        }

        public async Task<bool> Update(EntityLayer.File file)
        {
            return await FileLogic.Update(file);
        }

        public async Task<bool> AddFile(EntityLayer.File file)
        {
            return await FileLogic.Add(file);
        }

        public async Task<bool> DeleteFile(Guid id)
        {
            return await FileLogic.Delete(id);
        }
    }
}
