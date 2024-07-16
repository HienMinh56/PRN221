using Microsoft.AspNetCore.Http;
using Services.Interfaces;

namespace PerfumeStores.Services.Services
{
    public class ImageHandle : IImageHandle
    {
        public static readonly List<string> ImageExtensions = new List<string> { ".jpg", ".jpeg", ".jpe", ".bmp", ".gif", ".png" };

        public async Task<string?> AddImage(IFormFile file, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            if (file != null)
            {
                foreach (var f in ImageExtensions)
                {
                    if (file.FileName.Contains(f))
                    {
                        var fileUp = Path.Combine(environment.WebRootPath, "images", file.FileName);
                        using (var fileStream = new FileStream(fileUp, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                            return $"/images/{file.FileName}";
                        }
                    }
                }
            }
            return null;
        }
    }
}