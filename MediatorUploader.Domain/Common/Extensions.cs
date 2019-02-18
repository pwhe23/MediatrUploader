using System.IO;

namespace MediatorUploader.Domain
{
    public static class Extensions
    {
        //REF: https://stackoverflow.com/a/7073124/366559
        public static byte[] ToArray(this Stream stream)
        {
            using(var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    };
}