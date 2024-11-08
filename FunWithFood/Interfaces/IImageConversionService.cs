using FunWithFood.Dto;

namespace FunWithFood.Interfaces
{
    public interface IImageConversionService
    {
        Task<byte[]?> ConvertFileToByteArray(IFormFile? imageFile);
        string? ConvertByteToBase64(byte[]? imageData);
    }
}
