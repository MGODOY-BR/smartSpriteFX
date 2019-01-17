using smartSuite.smartSpriteFX.PictureEngine.Pictures.Data;

namespace smartSuite.smartSpriteFX.PictureEngine.Pictures.BitmapMatters
{
    public interface ITraditionalAlgorithmBuffer
    {
        IPictureDatabase Buffer { get; }
        long ColorCount { get; }
        int Height { get; }
        int Width { get; }
        /// <summary>
        /// Creates a picture database
        /// </summary>
        /// <returns></returns>
        IPictureDatabase CreatePictureDatabase();
    }
}