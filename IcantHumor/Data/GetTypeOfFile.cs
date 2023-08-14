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
                    return TypeOfFile.Img;
                case ".jpg":
                    return TypeOfFile.Img;
                case ".jpeg":
                    return TypeOfFile.Img;
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
