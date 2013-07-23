using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameHelperLibrary;
using Microsoft.Xna.Framework.Graphics;
using OriginsLibrary.Util;

namespace LayerLevelEngine
{
    public class TileSheet
    {
        private Level parent;
        private int id;
        private string path;
        private SpriteSheet sheet;
        private Texture2D[,] images;

        public int ID
        {
            get { return id; }
        }

        public TileSheet(Level parent, string path)
        {
            this.parent = parent;
            this.id = parent.TileSheetCount;
            this.path = path;

            SheetManager.AddTileSheet(this.id.ToString(), path, parent.TileWidth, parent.TileHeight);
            sheet = SheetManager.TileSheets[id.ToString()];

            images = new Texture2D[(int)(sheet.Width / sheet.SpriteWidth), (int)(sheet.Height / sheet.SpriteHeight)];
            for (int x = 0; x < images.GetLength(0); x++)
            {
                for (int y = 0; y < images.GetLength(1); y++)
                {
                    images[x, y] = sheet.GetSubImage(x, y);
                }
            }
        }

        public Texture2D GetTexture(int x, int y)
        {
            return images[x, y];
        }
    }   
}
