using IcantHumor.Models.Enums;

namespace IcantHumor.Data
{
    public static class GetTypeFile
    {
        public static TypeOfFile GetTypeOfFile(string fileName)
        {
            //Accept=".png, .jpg, .jpeg, .gif, .mp4"
            string Extension = Path.GetExtension(fileName);
            switch (Extension)
            {
                case ".png":
                    return TypeOfFile.Image;
                case ".jpg":
                    return TypeOfFile.Image;
                case ".jpeg":
                    return TypeOfFile.Image;
                case ".gif":
                    return TypeOfFile.Gif;
                case ".mp4":
                    return TypeOfFile.Video;
                default:
                    throw new Exception("Unknown extension of file");
            }
        }
    }
}
