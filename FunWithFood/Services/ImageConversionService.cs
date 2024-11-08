using FunWithFood.Interfaces;

namespace FunWithFood.Services
{
    public class ImageConversionService : IImageConversionService
    {
        public ImageConversionService() { }

        public async Task<byte[]?> ConvertFileToByteArray(IFormFile? imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                await imageFile.CopyToAsync(memoryStream);

                return memoryStream.ToArray();
            }

            string? defaultImagePath = Path.Combine("wwwroot", "images", "default.jpg");
            if (File.Exists(defaultImagePath))
            {
                return await File.ReadAllBytesAsync(defaultImagePath);
            }

            return null;
        }

        public string? ConvertByteToBase64(byte[]? imageData)
        {
            string? imageBase64 = imageData is not null ? Convert.ToBase64String(imageData) : null;
            return imageBase64;
        }

    }
}
