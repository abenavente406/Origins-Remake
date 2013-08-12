using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OriginsLib.Util;

namespace OriginsLib.TileEngine
{
    [Serializable]
    public class TileMap
    {
        #region Fields

        private List<TileSet> tileSets = new List<TileSet>();
        private List<MapLayer> layers = new List<MapLayer>();

        private int currentLayerIndex = 0;
        private int currentTileSetIndex = 0;

        private int widthInTiles = 0;
        private int heightInTiles = 0;

        private bool[,] collisionMap;
        
        #endregion

        #region Properties

        /// <summary>
        /// Gets the width of the map in tiles
        /// </summary>
        public int WidthInTiles
        {
            get { return widthInTiles; }
        }

        /// <summary>
        /// Gets the height of the map in tiles
        /// </summary>
        public int HeightInTiles
        {
            get { return heightInTiles; }
        }

        /// <summary>
        /// Gets the width of the map in pixels
        /// </summary>
        public int WidthInPixels
        {
            get { return widthInTiles * Engine.TileWidth; }
        }

        /// <summary>
        /// Gets the height of the map in pixels
        /// </summary>
        public int HeightInPixels
        {
            get { return heightInTiles * Engine.TileHeight; }
        }

        /// <summary>
        /// Gets the current layer index
        /// </summary>
        public int CurrentLayerIndex
        {
            get { return currentLayerIndex; }
            set
            {
                if (value < -1 || value > layers.Count)
                    value = 0;
                currentLayerIndex = value;
            }
        }

        /// <summary>
        /// Gets the current tile set index
        /// </summary>
        public int CurrentTileSetIndex
        {
            get { return currentTileSetIndex; }
            set
            {
                if (value < 0 || value > tileSets.Count)
                    value = 0;
                currentTileSetIndex = value;
            }
        }

        /// <summary>
        /// Gets the current layer
        /// </summary>
        public MapLayer CurrentLayer
        {
            get { return layers[CurrentLayerIndex]; }
        }

        /// <summary>
        /// Gets the current tile set
        /// </summary>
        public TileSet CurrentTileSet
        {
            get { return tileSets[CurrentTileSetIndex]; }
        }
        
        /// <summary>
        /// All the tile sets belonging to the map
        /// </summary>
        public List<TileSet> TileSets
        {
            get { return tileSets; }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Initialzes a new tile map
        /// </summary>
        /// <param name="map"></param>
        public TileMap(TileMap map)
        {
            LoadMap(map);
        }

        /// <summary>
        /// Initializes a new 'from-scratch' tile map
        /// </summary>
        /// <param name="widthInTiles"></param>
        /// <param name="heightInTiles"></param>
        public TileMap(int widthInTiles, int heightInTiles)
        {
            this.widthInTiles = widthInTiles;
            this.heightInTiles = heightInTiles;

            collisionMap = new bool[widthInTiles, heightInTiles];
        }

        #endregion

        #region Update/Draw

        /// <summary>
        /// Draws the map to the screen
        /// </summary>
        /// <param name="batch"></param>
        /// <param name="gameTime"></param>
        public void Draw(SpriteBatch batch, GameTime gameTime)
        {
            Rectangle destination = new Rectangle(0, 0, Engine.TileWidth, Engine.TileHeight);
            Tile tile;

            int startX = (int)(Camera.View.X / Engine.TileWidth) - 2;
            int startY = (int)(Camera.View.Y / Engine.TileHeight) - 2;

            int endX = (int)(Camera.View.X / Engine.TileWidth) 
                          + 
                       (int)(Camera.View.Width / Engine.TileWidth) + 2; 

            int endY = (int)(Camera.View.Y / Engine.TileHeight) 
                         + 
                       (int)(Camera.View.Height / Engine.TileHeight) + 2;

            foreach (MapLayer layer in layers)
            {
                if (layer.Visible && layer.Opacity > 0 && tileSets.Count != 0)
                {
                    for (int y = startY; y < endY; ++y)
                    {
                        if (y < 0 || y >= layer.WidthInTiles)
                            continue;

                        destination.Y = y * Engine.TileHeight;

                        for (int x = startX; x < endX; ++x)
                        {
                            if (x < 0 || x >= layer.WidthInTiles)
                                continue;

                            tile = layer.GetTile(x, y);

                            if (tile.TileIndex == -1 || tile.TileSet == -1)
                                continue;

                            destination.X = x * Engine.TileWidth;

                            batch.Draw(
                                tileSets[tile.TileSet].Image,
                                destination,
                                tileSets[tile.TileSet].SourceRectangles[tile.TileIndex],
                                Color.White * layer.Opacity);
                        }
                    }
                }
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Sets the tile of a layer
        /// </summary>
        /// <param name="x">X position of the tile</param>
        /// <param name="y">Y position of the tile</param>
        /// <param name="t">Tile to replace with the selected</param>
        /// <param name="currentLayer">Layer to edit</param>
        public void SetTile(int x, int y, Tile t, int currentLayer = -1)
        {
            SetTile(new Point(x, y), t, currentLayer);
        }

        /// <summary>
        /// Sets the tile of a layer
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="t">Tile to replace with the selected</param>
        /// <param name="currentLayer">Layer to edit</param>
        public void SetTile(Point pos, Tile t, int currentLayer = -1)
        {
            if (currentLayer != -1)
                CurrentLayerIndex = currentLayer;

            layers[CurrentLayerIndex].SetTile(pos.X, pos.Y, t);
        }

        /// <summary>
        /// Adds a layer to the map
        /// </summary>
        /// <param name="l"></param>
        public void AddLayer(MapLayer l)
        {
            layers.Add(l);
        }

        /// <summary>
        /// Adds a tile set to the map
        /// </summary>
        /// <param name="tileSet"></param>
        public void AddTileSet(TileSet tileSet)
        {
            tileSets.Add(tileSet);
        }

        /// <summary>
        /// Gets the tile set at an index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public TileSet GetTileSet(int index)
        {
            TileSet result;

            try
            {
                result = tileSets[index];
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Tile Set not found! Defaulting to null.");
                result = null;
            }

            return result;
        }

        /// <summary>
        /// Sets the collision at the point (x, y)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="collision"></param>
        public void SetCollision(int x, int y, bool collision)
        {
            if (x < 0 || x >= widthInTiles ||
                y < 0 || y >= HeightInTiles)
                return;

            collisionMap[y, x] = collision; 
        }

        /// <summary>
        /// Gets if there is a collision block detected here
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public bool GetIsCollision(int x, int y)
        {
            if (x < 0 || x >= widthInTiles ||
                y < 0 || y >= HeightInTiles)
                return true;

            return collisionMap[y, x];
        }

        #endregion

        #region IO

        /// <summary>
        /// Saves the map to an slf file
        /// </summary>
        /// <param name="path"></param>
        public void Save(string path)
        {
            BinaryFormatter serializer = new BinaryFormatter();
            var fs = File.Create(path);
            serializer.Serialize(fs, this);
            fs.Close();
            fs.Dispose();
        }

        /// <summary>
        /// Loads a map from an slf file
        /// </summary>
        /// <param name="path"></param>
        public void Load(string path)
        {
            BinaryFormatter serializer = new BinaryFormatter();
            var fs = File.Open(path, FileMode.Open);
            var tileMap = (TileMap)serializer.Deserialize(fs);
            fs.Close();
            fs.Dispose();

            LoadMap(tileMap);
        }

        /// <summary>
        /// Loads a map from an existing map
        /// </summary>
        /// <param name="map"></param>
        private void LoadMap(TileMap map)
        {
            this.tileSets = map.tileSets;
            this.layers = map.layers;
            this.widthInTiles = map.widthInTiles;
            this.heightInTiles = map.heightInTiles;
            this.collisionMap = (bool[,])map.collisionMap.Clone();

            foreach (TileSet t in tileSets)
                t.Initialize(t.Path);
        }

        #endregion
    }
}
