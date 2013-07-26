using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LayerLevelEngine
{
    [Serializable]
    public class Tile
    {
        private Level parent;
        private Vector2 pos;

        private int width;
        private int height;

        private int sheetId;
        private int sourceX;
        private int sourceY;

        private TileSheet sheet;

        public Tile(Level level, int sheetId, int width, int height, int x, int y)
            : this(level, sheetId, width, height, 0, 0, x, y)
        {
        }

        public Tile(Level level, int sheetId, int width, int height, int sourceX, int sourceY, int x, int y)
        {
            this.width = width;
            this.height = height;
            this.sourceX = sourceX;
            this.sourceY = sourceY;

            this.parent = level;
            this.pos = new Vector2(x, y);
            this.sheetId = sheetId;

            LoadTileSheet();
        }

        public void LoadTileSheet()
        {
            this.sheet = parent.GetTileSheet(this.sheetId);
            this.sheet.LoadTextures();
        }

        public virtual void Update(GameTime gameTime)
        {
            return;
        }

        public virtual void Draw(SpriteBatch batch, GameTime gameTime, float opacity)
        {
            batch.Draw(sheet.GetTexture(sourceX, sourceY), pos, Color.White * opacity);
        }
    }
}
