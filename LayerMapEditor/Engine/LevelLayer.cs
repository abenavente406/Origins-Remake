using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace LayerMapEditor.Engine
{
    [Serializable]
    public class LevelLayer
    {
        private Level parent;
        private int widthInTiles;
        private int heightInTiles;
        private int tileWidth;
        private int tileHeight;

        public int WidthInTiles
        {
            get { return widthInTiles; }
            set { widthInTiles = value; }
        }

        public int HeightInTiles
        {
            get { return heightInTiles; }
            set { heightInTiles = value; }
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

        public LevelLayer()
        {
            WidthInTiles = 0;
            HeightInTiles = 0;
            tileWidth = 0;
            tileHeight = 0;
        }

        public LevelLayer(Level parent)
        {
            WidthInTiles = parent.LevelSettings.WidthInTiles;
            HeightInTiles = parent.LevelSettings.HeightInTiles;
            TileWidth = parent.LevelSettings.TileWidth;
            tileHeight = parent.LevelSettings.TileHeight;
        }
    }
}
