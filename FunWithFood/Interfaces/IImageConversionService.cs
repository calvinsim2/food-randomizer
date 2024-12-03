namespace FunWithFood.Interfaces
{
    public interface IImageConversionService
    {
        Task<byte[]> ConvertFileToByteArray(IFormFile? imageFile);
        string? ConvertByteToBase64(byte[]? imageData);
        byte[]? ConvertBase64ToByte(string? imageBase64);
    }
}
