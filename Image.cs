namespace Saber.Core
{
    public static class Image
    {
        public static string[] Extensions { get; set; } = new string[]
        {
            ".apng",
            ".avif",
            ".gif",
            ".jpeg",
            ".jpg",
            ".png",
            ".svg",
            ".webp",
            ".bmp",
            ".ico",
            ".tiff"
        };

        public static void Shrink(string filename, string outfile, int width)
        {
            Delegates.Image.Shrink(filename, outfile, width);
        }

        public static void ConvertPngToJpg(string filename, string outfile, int quality = 100)
        {
            Delegates.Image.ConvertPngToJpg(filename, outfile, quality);
        }
    }
}
