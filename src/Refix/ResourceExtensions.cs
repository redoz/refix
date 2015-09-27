using System.IO;
using System.Threading.Tasks;

namespace Refix
{
    public static class ResourceExtensions
    {
        public static async Task CopyToAsync(this Resource resource, string directory)
        {
            string destPath = Path.Combine(directory, resource.Name);
            using (var fileStream = File.Create(destPath))
            using (Stream resourceStream = resource.GetStream())
            {
                await resourceStream.CopyToAsync(fileStream);
                resourceStream.Close();
                fileStream.Close();
            }
        }
    }
}