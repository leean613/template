
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using System.IO;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public static class ResizeImageHelper
    {
        public async static Task ResizeToThumbSize(IFormFile file, Stream stream)
        {
            (Image Image, IImageFormat Format) imf = await Image.LoadWithFormatAsync(file.OpenReadStream());
            using (var image = imf.Image)
            {
                image.Mutate(
                    x => x.Resize(200, 200 * image.Height / image.Width));
                await image.SaveAsync(stream, imf.Format);
            }    
        }
    }
}
