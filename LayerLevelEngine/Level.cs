using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace LayerLevelEngine
{
    public class Level
    {
        private List<Layer> layers = new List<Layer>();
        private List<string> tileSheetPaths = new List<string>();
        private List<TileSheet> tileSheets = new List<TileSheet>();

        private int widthInTiles;
        private int heightInTiles;
        private int tileWidth;
        private int tileHeight;

        public int WidthInTiles
        {
            get { return widthInTiles; }
        }

        public int HeightInTiles
        {
            get { return heightInTiles; }
        }

        public int TileWidth
        {
            get { return tileWidth; }
        }

        public int TileHeight
        {
            get { return tileHeight; }
        }

        public int WidthInPixels
        {
            get { return widthInTiles * tileWidth; }
        }

        public int HeightInPixels
        {
            get { return heightInTiles * tileHeight; }
        }

        public int LayerCount
        {
            get { return layers.Count; }
        }

        public int TileSheetCount
        {
            get { return tileSheets.Count; }
        }

        public Level()
        {

        }

        public Level(Level level)
        {
            CleanSlate();
            this.layers = level.layers;
            this.tileSheetPaths = level.tileSheetPaths;
            this.tileSheets = level.tileSheets;
        }

        private void CleanSlate()
        {
            layers.Clear();
            tileSheetPaths.Clear();
            tileSheets.Clear();
        }

        public void AddTileSheet(string path)
        {
            tileSheetPaths.Add(path);
            tileSheets.Add(new TileSheet(this, path));
        }

        private void InitTileSheets()
        {
            tileSheetPaths.ForEach(s => AddTileSheet(s));
        }

        public TileSheet GetTileSheet(int id)
        {
            TileSheet result = tileSheets.Find((tileSheet) => tileSheet.ID == id);
            return result;
        }

        public void Update(GameTime gameTime)
        {
            layers.ForEach(l => l.Update(gameTime));
        }

        public void Draw(SpriteBatch batch, GameTime gameTime)
        {
            layers.ForEach(l => l.Draw(batch, gameTime));
        }
    }
}
