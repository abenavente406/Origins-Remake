using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameHelperLibrary;
using Microsoft.Xna.Framework.Graphics;
using OriginsLibrary.Util;

namespace LayerLevelEngine
{
    [Serializable]
    public class TileSheet
    {
        private Level parent;
        private int id;
        private string path;

        [NonSerialized]
        private SpriteSheet sheet;

        [NonSerialized]
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

            LoadTextures();
        }

        public Texture2D GetTexture(int x, int y)
        {
            return images[x, y];
        }

        public void LoadTextures()
        {
            SpriteSheet attempt;
            if (SheetManager.TileSheets.TryGetValue(this.id.ToString(), out attempt))
            {
                sheet = attempt;
            }
            else
            {
                SheetManager.AddTileSheet(this.id.ToString(), path, parent.TileWidth, parent.TileHeight);
                sheet = SheetManager.TileSheets[this.id.ToString()];
            }

            if (images == null)
            {
                images = new Texture2D[(int)(sheet.Width / sheet.SpriteWidth), (int)(sheet.Height / sheet.SpriteHeight)];
                for (int x = 0; x < images.GetLength(0); x++)
                {
                    for (int y = 0; y < images.GetLength(1); y++)
                    {
                        images[x, y] = sheet.GetSubImage(x, y);
                    }
                }
            }
        }
    }   
}
