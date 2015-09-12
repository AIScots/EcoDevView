using System.Drawing;

namespace Eco.DevView
{
    public interface ITileRenderer
    {
        /// <summary>
        /// The <see cref="Color"/> that should be used to render <paramref name="block"/>
        /// </summary>
        /// <param name="block">Block that is to be rendered</param>
        /// <returns></returns>
        Color GetColor(IBlock block);

        /// <summary>
        /// Saves a rendered bitmap <paramref name="image"/> to <paramref name="path"/>
        /// </summary>
        /// <param name="path">Path the bitmap should be saved under</param>
        /// <param name="image">Bitmap to be saved</param>
        /// <remarks>
        /// The bitmap should be saved in the <see cref="System.Drawing.Imaging.ImageFormat.Png"/> format.
        /// </remarks>
        void SaveBitmap(string path, Image image);

        /// <summary>
        /// Attempts to load a saved bitmap from <paramref name="path"/>
        /// </summary>
        /// <param name="path">Path to the bitmap</param>
        /// <returns>An <see cref="Image"/> if available, <c>null</c> otherwise.</returns>
        Image LoadBitmap(string path);
    }
}
