using CoreGoDelivery.Domain.Consts.Regex;
using System.Text;
using System.Text.RegularExpressions;

namespace CoreGoDelivery.Infrastructure.FileBucket.MinIO.Extensions
{
    public static class GetContentType
    {
        public static string Get(string objectName)
        {
            var extensionMatch = Regex.Match(Path.GetExtension(objectName).ToLower(), RegexCollectionPatterns.GET_EXTENSION_FILE);

            if (extensionMatch.Success)
            {
                var contentTypeBuilder = new StringBuilder("image/");

                contentTypeBuilder.Append(extensionMatch.Groups[1].Value);

                return contentTypeBuilder.ToString();
            }
            else
            {
                return "application/octet-stream";
            }
        }
    }
}
