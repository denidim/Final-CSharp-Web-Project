namespace FindATrade.Common
{
    using System;
    using System.IO;

    public static class ImageNameGenerator
    {
        public static string GenerateFileName(string fileName)
        {
            var name = Path.GetFileNameWithoutExtension(fileName);
            var extension = Path.GetExtension(fileName);
            return $"{name}-{DateTime.Now.ToUniversalTime().ToString("yyyyMMddHHmmss")}{extension}";
        }
    }
}
