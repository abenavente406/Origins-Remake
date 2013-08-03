using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using Microsoft.Xna.Framework;

namespace LayerMapEditor.Engine
{
    [Serializable]
    public class TileSheet
    {
        private string path;
        private int tileWidth;
        private int tileHeight;

        private Rectangle[,] sourceRects;
        private BitmapImage image;

        public string Path
        {
            get { return path; }
            set
            {
                path = value;

                BitmapImage src = new BitmapImage();
                src.BeginInit();
                src.UriSource = new Uri(path, UriKind.Absolute);
                src.CacheOption = BitmapCacheOption.OnLoad;
                src.EndInit();

                image = src;

                src.get

                InitRects();
            }
        }

        public int TileWidth
        {
            get { return tileWidth; }
            set { tileWidth = value; }
        }

        public int TileHeight
        {
            get { return tileHeight; }
            set { tileHeight = value; }
        }

        public TileSheet()
        {
            TileWidth = 1;
            TileHeight = 1;
            path = "";
        }

        public TileSheet(String path, int tileWidth, int tileHeight)
        {
            TileWidth = tileWidth;
            TileHeight = tileHeight;
            Path = path; 
         }

        private void InitRects()
        {
            int numX = image.PixelWidth / tileWidth;
            int numY = image.PixelHeight / tileHeight;

            sourceRects = new Rectangle[numX, numY];

            for (int x = 0; x < numX; ++x)
            {
                for (int y = 0; y < numY; ++y)
                {
                    sourceRects[x, y] = new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight);
                }
            }
        }
    }
}
