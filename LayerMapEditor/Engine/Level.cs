using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace LayerMapEditor.Engine
{
    [XmlRoot]
    public class Level
    {
        private LevelSettings levelSettings;
        private List<LevelLayer> layers;
        private List<TileSheet> tileSheets;

        [XmlElement("LevelSettings")]
        public LevelSettings LevelSettings
        {
            get { return levelSettings; }
            set { levelSettings = value; }
        }

        [XmlElement("LevelLayer")]
        public List<LevelLayer> Layers
        {
            get { return layers; }
            set { layers = value; }
        }

        [XmlElement("TileSheet")]
        public List<TileSheet> TileSheets
        {
            get { return tileSheets; }
            set { tileSheets = value; }
        }

        public Level()
        {
            levelSettings = new LevelSettings();
            layers = new List<LevelLayer>();
            tileSheets = new List<TileSheet>();
        }

        public Level(int width, int height, int tileWidth, int tileHeight)
        {
            levelSettings = new LevelSettings()
            {
                WidthInTiles = width,
                HeightInTiles = height,
                TileWidth = tileWidth,
                TileHeight = tileHeight
            };
            layers = new List<LevelLayer>();
            tileSheets = new List<TileSheet>();
        }

        public void Save(string path)
        {
            layers.Add(new LevelLayer(this));
            layers.Add(new LevelLayer(this));
            tileSheets.Add(new TileSheet(@"D:\grass.png", 32, 32));
            XmlSerializer serializer;

            List<Type> extraTypes = new List<Type>();
            extraTypes.Add(typeof(LevelSettings));
            extraTypes.Add(typeof(List<LevelLayer>));
            extraTypes.Add(typeof(LevelLayer));
            serializer = new XmlSerializer(typeof(Level), extraTypes.ToArray());

            FileStream stream = File.Create(path);

            serializer.Serialize(stream, this);
                
            stream.Close();
        }

        public void Load(string path)
        {
            XmlSerializer serializer;
            serializer = new XmlSerializer(typeof(Level));

            FileStream stream = File.Open(path, FileMode.Open);
            var level = (Level)serializer.Deserialize(stream);
            this.levelSettings = level.levelSettings;

            stream.Close();
        }
    }
}
