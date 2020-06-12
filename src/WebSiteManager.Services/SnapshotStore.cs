using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WebSiteManager.Services
{
    public class SnapshotStore : ISnapshotStore
    {
        public ServiceResult Save(MemoryStream snapshotStream, string path)
        {
            var serviceResult = new ServiceResult();

            if (IsImage(snapshotStream))
            {
                serviceResult.AddError(ErrorType.InvalidInput, "Stream does not contain image");
                return serviceResult;
            }

            using var file = new FileStream(path, FileMode.Create);
            snapshotStream.WriteTo(file);

            return serviceResult;
        }

        private bool IsImage(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);

            var jpg = new List<string> { "FF", "D8" };
            var bmp = new List<string> { "42", "4D" };
            var gif = new List<string> { "47", "49", "46" };
            var png = new List<string> { "89", "50", "4E", "47", "0D", "0A", "1A", "0A" };
            var imgTypes = new List<List<string>> { jpg, bmp, gif, png };

            var bytesIterated = new List<string>();

            for (int i = 0; i < 8; i++)
            {
                var bit = stream.ReadByte().ToString("X2");
                bytesIterated.Add(bit);

                var isImage = imgTypes.Any(img => !img.Except(bytesIterated).Any());
                if (isImage)
                {
                    return true;
                }
            }

            return false;
        }
    }
}