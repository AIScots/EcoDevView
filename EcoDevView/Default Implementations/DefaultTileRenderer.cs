using System;
using System.Drawing;
using System.IO;

namespace Eco.DevView
{
    public class DefaultTileRenderer : ITileRenderer
    {
        public Color GetColor(IBlock block)
        {
            return Color.FromArgb(0, (int)Math.Round((1f-block.Pollution) * 255), 0);
        }

        public void SaveBitmap(string path, Image bitmap)
        {
            bitmap.Save(path, System.Drawing.Imaging.ImageFormat.Png);
        }

        public Image LoadBitmap(string path)
        {
            if (!File.Exists(path))
                return null;

            using (var img = Image.FromFile(path))
                return new Bitmap(img);
        }
    }
}
