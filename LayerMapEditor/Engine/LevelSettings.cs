using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace LayerMapEditor.Engine
{
    [Serializable]
    public class LevelSettings
    {
        private string name;
        private int widthInTiles;
        private int heightInTiles;
        private int widthInPixels;
        private int heightInPixels;
        private int tileWidth;
        private int tileHeight;

        public int WidthInTiles
        {
            get { return widthInTiles; }
            set
            {
                widthInTiles = value;
                widthInPixels = widthInTiles * tileWidth;
            }
        }

        public int HeightInTiles
        {
            get { return heightInTiles; }
            set
            {
                heightInTiles = value;
                heightInPixels = heightInTiles * tileHeight;
            }
        }

        public int TileWidth
        {
            get { return tileWidth; }
            set
            {
                tileWidth = value;
                widthInPixels = widthInTiles * tileWidth;
            }
        }

        public int TileHeight
        {
            get { return tileHeight; }
            set
            {
                tileHeight = value;
                heightInPixels = heightInTiles * tileHeight;
            }
        }

        public int WidthInPixels
        {
            get { return widthInPixels; }
        }

        public int HeightInPixels
        {
            get { return heightInPixels; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public LevelSettings()
            : this(0, 0, 0, 0)
        {
            
        }

        public LevelSettings(int width, int height, int tileWidth, int tileHeight)
        {
            name = "level1";
            widthInTiles = width;
            heightInTiles = height;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
            widthInPixels = 0;
            heightInPixels = 0;
        }
    }
}
