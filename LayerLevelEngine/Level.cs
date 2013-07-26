using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using OriginsLibrary.Util;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace LayerLevelEngine
{
    [Serializable]
    public class Level
    {
        private List<Layer> layers = new List<Layer>();
        private List<string> tileSheetPaths = new List<string>();
        private List<TileSheet> tileSheets = new List<TileSheet>();

        private int widthInTiles;
        private int heightInTiles;
        private int tileWidth;
        private int tileHeight;

        private bool[,] collisionMap;

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

        public Level(string path)
        {
            Load(path);
        }

        public Level(int widthInTiles, int heightInTiles, int tileWidth, int tileHeight)
        {
            this.widthInTiles = widthInTiles;
            this.heightInTiles = heightInTiles;
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
            
            // -------------------------------
            // Start temp code
            // -------------------------------
            Random rand = new Random();

            Tile[,] layer1 = new Tile[widthInTiles, heightInTiles];
            AddTileSheet(@"D:\grass.png");
            AddTileSheet(@"D:\watergrass.png");
            AddTileSheet(@"D:\dirt.png");

            /* Random level generation */
            //for (int x = 0; x < widthInTiles; x++)
            //{
            //    for (int y = 0; y < heightInTiles; y++)
            //    {
            //        int randGrass = rand.Next();

            //        switch (randGrass % 4)
            //        {
            //            case 0:
            //                layer1[x, y] = new Tile(this, 0, 32, 32, 1, 3, x * tileWidth, y * tileHeight);
            //                break;
            //            case 1:
            //                layer1[x, y] = new Tile(this, 0, 32, 32, 0, 5, x * tileWidth, y * tileHeight);
            //                break;
            //            case 2:
            //                layer1[x, y] = new Tile(this, 0, 32, 32, 1, 5, x * tileWidth, y * tileHeight);
            //                break;
            //            case 3:
            //                layer1[x, y] = new Tile(this, 0, 32, 32, 2, 5, x * tileWidth, y * tileHeight);
            //                break;
            //        }
            //    }
            //}

            /* Noise level generation */
            Noise noise = new Noise(new Random(), 2.0f, widthInTiles, heightInTiles);
            noise.initialise();

            for (int x = 0; x < WidthInTiles; x++)
            {
                for (int y = 0; y < HeightInTiles; y++)
                {
                    float perlin = noise.getNoise(x, y);
                    if (perlin > 0.8f)
                        layer1[x, y] = new Tile(this, 1, tileWidth, tileHeight, 1, 5, x * tileWidth, y * tileHeight);
                    else if (perlin <= .8f && perlin >= .2f)
                        layer1[x, y] = new Tile(this, 2, tileWidth, tileHeight, 1, 5, x * tileWidth, y * tileHeight);
                    else
                        layer1[x, y] = new Tile(this, 0, tileWidth, tileHeight, 1, 5, x * tileWidth, y * tileHeight);
                }
            }

            noise.printAsCSV();
            layers.Add(new Layer(this, layer1));
            // --------------------------------
            // End temp code
            // --------------------------------- 

            collisionMap = new bool[widthInTiles, heightInTiles];
        }

        public Level(Level level)
        {
            CleanSlate();
            this.layers = level.layers;
            this.tileSheetPaths = level.tileSheetPaths;
            this.tileSheets = level.tileSheets;
            this.widthInTiles = level.widthInTiles;
            this.heightInTiles = level.heightInTiles;
            this.tileWidth = level.tileWidth;
            this.tileHeight = level.tileHeight;
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

        public void Save(string path)
        {
            FileStream stream = File.Open(path, FileMode.OpenOrCreate);
            BinaryFormatter writer = new BinaryFormatter();
            writer.Serialize(stream, this);
            stream.Close();
        }

        public void Load(string path)
        {
            FileStream stream = File.Open(path, FileMode.Open);
            BinaryFormatter serializer = new BinaryFormatter();

            var level = (Level)(serializer.Deserialize(stream));

            CleanSlate();
            this.layers = level.layers;
            this.tileSheetPaths = level.tileSheetPaths;
            this.tileSheets = level.tileSheets;
            this.widthInTiles = level.widthInTiles;
            this.heightInTiles = level.heightInTiles;
            this.tileWidth = level.tileWidth;
            this.tileHeight = level.tileHeight;

            foreach (Layer l in layers)
                l.LoadLayer();

            stream.Close();
        }
    }
}
