using SkiaSharp;

namespace Application.Helpers
{
    public record DocumentMeta(string Path, string? ThumbnailPath = null);

    public static class DocumentHelper
    {
        public static int ThumbnailWidth { get; set; } = 96;
        public static string Storage { get; set; } = "wwwroot/uploads/";

        public static async Task<DocumentMeta> StoreDocumentAsync(this Stream stream, string fileName, string extension, bool makeThumbnail = false, CancellationToken cancellationToken = default)
        {
            void CheckDirectoryExists(string storage)
            {
                if (!Directory.Exists(storage))
                    Directory.CreateDirectory(storage);
            }

            async Task WriteStreamAsync(string fileName, Stream stream)
            {
                using var fileStream = File.Create(Path.Combine(Directory.GetCurrentDirectory(), fileName));

                stream.Position = 0;
                stream.Seek(0, SeekOrigin.Begin);
                await stream.CopyToAsync(fileStream, cancellationToken);

                await fileStream.FlushAsync(cancellationToken);
            }

            async Task<string> WriteDocumentAsync(string identity)
            {
                var path = Path.Combine(Storage);
                CheckDirectoryExists(path);
                var fileName = Path.Combine(path, $"{identity}.{extension}");
                await WriteStreamAsync(fileName, stream);
                return fileName.Replace('\\', '/');
            }

            async Task<string> WriteThumbnailAsync(string identity, Stream thumbnail)
            {
                var path = Path.Combine(Storage, "Thumbnails");
                CheckDirectoryExists(path);
                var fileName = Path.Combine(path, $"{identity}.{extension}");
                await WriteStreamAsync(fileName, thumbnail);
                return fileName.Replace('\\', '/');
            }

            string? thumbnail = null;

            using var image = SKImage.FromEncodedData(stream);
            if (image != null && makeThumbnail)
            {
                var bitmap = SKBitmap.FromImage(image);
                var ratioFactor = ThumbnailWidth / bitmap.Width;
                var height = bitmap.Height * ratioFactor;

                using SKBitmap scaledBitmap = bitmap.Resize(new SKImageInfo(ThumbnailWidth, height), SKFilterQuality.Medium);
                using SKImage scaledImage = SKImage.FromBitmap(scaledBitmap);

                thumbnail = await WriteThumbnailAsync(fileName, scaledImage.Encode(SKEncodedImageFormat.Png, 100).AsStream());
            }

            var path = await WriteDocumentAsync(fileName);

            return new DocumentMeta(path, thumbnail);
        }

        public static Task<DocumentMeta> StoreDocumentAsync(this Stream stream, string extension, bool makeThumbnail = false, CancellationToken cancellationToken = default)
            => StoreDocumentAsync(stream, Guid.NewGuid().ToString(), extension, makeThumbnail, cancellationToken);
    }
}
