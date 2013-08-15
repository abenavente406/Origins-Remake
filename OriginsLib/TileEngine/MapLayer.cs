using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace OriginsLib.TileEngine
{
    [Serializable]
    public class MapLayer
    {
        #region Fields

        public enum GenerationAlgorithm
        {
            Dungeon_DrunkenWalk,
            PerlinNoise
        }

        private Tile[,] map;
        private TileMap parent;
        private bool isVisible = true;
        private float opacity = 1.0f;
        
        #endregion

        #region Properties

        /// <summary>
        /// Returns the width of the layer in tiles
        /// </summary>
        public int WidthInTiles
        {
            get { return parent.WidthInTiles; }
        }

        /// <summary>
        /// Returns the height of the layer in tiles
        /// </summary>
        public int HeightInTiles
        {
            get { return parent.HeightInTiles; }
        }

        /// <summary>
        /// Gets whether the layer is visible or not
        /// </summary>
        public bool Visible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }

        /// <summary>
        /// Gets the opacity of the level
        /// </summary>
        public float Opacity
        {
            get { return opacity; }
            set { opacity = MathHelper.Clamp(value, 0, 1); }
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes a new map layer with an existing map
        /// </summary>
        /// <param name="map"></param>
        public MapLayer(TileMap parent, Tile[,] map)
        {
            this.parent = parent;
            this.map = (Tile[,])map.Clone();
        }

        /// <summary>
        /// Initializes a new map layer
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public MapLayer(TileMap parent)
        {
            this.parent = parent;
            map = new Tile[HeightInTiles, WidthInTiles];

            for (int y = 0; y < HeightInTiles; y++)
            {
                for (int x = 0; x < WidthInTiles; x++)
                {
                    map[y, x] = new Tile(-1, -1);
                }
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Gets the tile at the position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Tile GetTile(int x, int y)
        {
            if (y >= HeightInTiles || x >= WidthInTiles ||
                  y < 0 || x < 0)
                return null;

            return map[y, x];   
        }

        /// <summary>
        /// Sets the tile at the position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="t">Tile to replace the selected tile</param>
        public void SetTile(int x, int y, Tile t)
        {
            if (y >= HeightInTiles || x >= WidthInTiles ||
                y < 0 || x < 0)
                return;

            map[y, x] = t;
        }

        /// <summary>
        /// Sets the tile at the position by creating a tile
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="index">Index of the tile</param>
        /// <param name="tileSet">Tile set of the tile</param>
        public void SetTile(int x, int y, int index, int tileSet)
        {
            if (y >= HeightInTiles || x >= WidthInTiles ||
                  y < 0 || x < 0)
                return;

            map[y, x] = new Tile(index, tileSet);
        }

        public static MapLayer GenerateFromAlgorithm(GenerationAlgorithm algorithm, 
            TileMap level, int selectedTileSet, int groundTile = 0, int wallTile = 0)
        {
            Random rand = new Random();            
            Tile[,] layerTiles = new Tile[level.HeightInTiles, level.WidthInTiles];

            // TODO: Algorithm logic
            switch (algorithm)
            {
                case GenerationAlgorithm.Dungeon_DrunkenWalk:
                    {
                        List<Room> rooms = new List<Room>();

                        for (int x = 0; x < level.WidthInTiles; x++)
                            for (int y = 0; y < level.HeightInTiles; y++)
                            {
                                layerTiles[y, x] = new Tile(wallTile, selectedTileSet);
                                level.SetCollision(x, y, true);
                            }

                        int roomsMin = (int)(level.WidthInTiles * level.HeightInTiles) / 300;
                        int roomsMax = (int)(level.WidthInTiles * level.HeightInTiles) / 150;
                        int roomCount = 30;

                        int widthRoot = (int)Math.Sqrt(level.WidthInPixels * 2);
                        int heightRoot = (int)Math.Sqrt(level.HeightInPixels * 2);

                        int minimumWidth = (int)4 * Engine.TileWidth;
                        int maximumWidth = (int)8 * Engine.TileHeight;
                        int minimumHeight = (int)3 * Engine.TileWidth;
                        int maximumHeight = (int)10 * Engine.TileHeight;

                        do
                        {
                            bool ok = false;
                            Room room = new Room();

                            room.X = (int)Math.Round(rand.Next(0, level.WidthInPixels) / (double)Engine.TileWidth) * Engine.TileWidth;
                            room.Y = (int)Math.Round(rand.Next(0, level.HeightInPixels) / (double)Engine.TileHeight) * Engine.TileHeight;
                            room.Width = (int)Math.Round(rand.Next(minimumWidth, maximumWidth) / (double)Engine.TileWidth) * Engine.TileWidth;
                            room.Height = (int)Math.Round(rand.Next(minimumHeight, maximumHeight) / (double)Engine.TileHeight) * Engine.TileHeight;

                            if (room.X < 0 || room.X > level.WidthInPixels - room.Width ||
                                room.Y < 0 || room.Y > level.HeightInPixels - room.Height)
                                continue;

                            ok = true;

                            if (rooms.Count > 0)
                            {
                                foreach (Room r in rooms)
                                    if (r.Bounds.Intersects(room.Bounds))
                                        ok = false;
                            }

                            if (ok)
                                rooms.Add(room);

                        } while (rooms.Count < roomCount);

                        rooms.Add(new Room()
                        {
                            X = 0,
                            Y = 0,
                            Width = 10 * Engine.TileWidth,
                            Height = 10 * Engine.TileHeight
                        });

                        List<Room> usableRooms = rooms;
                        List<Cell> connectedTiles = new List<Cell>();
                        int connections = roomCount;
                        int index = 0;

                        for (int i = 0; i < connections - 1; i++)
                        {
                            Room room = rooms[index];
                            usableRooms.Remove(room);

                            Room connectToRoom = usableRooms[rand.Next(usableRooms.Count)];

                            double sideStepChance = 0.4;

                            Vector2 pointA = new Vector2(rand.Next(room.Bounds.X, room.Bounds.X + room.WidthInTiles),
                                rand.Next(room.Bounds.Y, room.Bounds.Y + room.HeightInTiles));
                            Vector2 pointB = new Vector2(rand.Next(connectToRoom.Bounds.X, connectToRoom.Bounds.X + connectToRoom.WidthInTiles),
                                rand.Next(connectToRoom.Bounds.Y, connectToRoom.Bounds.Y + connectToRoom.HeightInTiles));

                            while (pointB != pointA)
                            {
                                if (rand.NextDouble() < sideStepChance)
                                {
                                    if (pointB.X != pointA.X)
                                    {
                                        if (pointB.X < pointA.X)
                                            pointB.X++;
                                        else
                                            pointB.X--;
                                    }
                                }
                                else if (pointB.Y != pointA.Y)
                                {
                                    if (pointB.Y < pointA.Y)
                                        pointB.Y++;
                                    else
                                        pointB.Y--;
                                }

                                if (pointB.X < level.WidthInTiles && pointB.Y < level.HeightInTiles)
                                {
                                    level.SetCollision((int)pointB.X, (int)pointB.Y, false); 
                                    layerTiles[(int)(pointB.Y), (int)(pointB.X)] = new Tile(-1, -1);
                                    
                                }
                            }
                        }

                        foreach (Room r in rooms)
                        {
                            for (int x = (int)r.Position.X; x < r.Width + r.Position.X; x++)
                            {
                                for (int y = (int)r.Position.Y; y < r.Height + r.Position.Y; y++)
                                {
                                    if (x / 32 == r.Position.X / 32 || x / 32 == (int)(r.WidthInTiles + (r.Position.X / 32) - 1) ||
                                        y / 32 == r.Position.Y / 32 || y / 32 == (int)(r.HeightInTiles + (r.Position.Y / 32) - 1))
                                    {

                                    }
                                    else
                                    {
                                        level.SetCollision((int)(x / Engine.TileWidth), (int)(y / Engine.TileHeight), false);
                                        layerTiles[(int)(y / Engine.TileHeight), (int)(x / Engine.TileWidth)] = new Tile(-1, -1);
                                    }
                                }
                            }
                        }
                    }
                    break;
                case GenerationAlgorithm.PerlinNoise:
                    break;
            }

            return new MapLayer(level, layerTiles);
        }
        #endregion
    }


    public struct Room
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public int WidthInTiles
        {
            get { return Width / 32; }
        }
        public int HeightInTiles
        {
            get { return Height / 32; }
        }

        public Rectangle Bounds
        {
            get { return new Rectangle(X / 32, Y / 32, WidthInTiles, HeightInTiles); }
        }
        public Vector2 Position
        {
            get { return new Vector2(X, Y); }
            set { X = (int)value.X; Y = (int)value.Y; }
        }

        public void Draw(SpriteBatch batch, Texture2D texture)
        {
            batch.Draw(texture, new Rectangle((int)Position.X,
                (int)Position.Y, Width, Height), Color.White);
        }
    }

    public struct Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
    public class MazeCell
    {
        public float x, y;
        public bool Wall = true;
        public bool Visited = false;
        public bool hasOpening = false;

        public Texture2D Texture { get; set; }

        public MazeCell(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Checks if the cell is bordered by an open cell
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="neighbor"></param>
        /// <param name="cells"></param>
        /// <returns></returns>
        public bool HasOpening(Vector2 cell, Vector2 neighbor, MazeCell[,] cells, TileMap level)
        {
            // Test each adjacent block minus the neighbor
            foreach (Vector2 dir in adjacent(level, cell))
                if (dir != neighbor)
                    if (!cells[(int)dir.X, (int)dir.Y].Wall)
                        return true;

            // If none of the cells are adjacent, the block doesn't border a wall
            return false;
        }

        /// <summary>
        /// Gets all adjacent cells (N, S, E, and W)
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public List<Vector2> adjacent(TileMap level, Vector2 cell)
        {
            List<Vector2> adjacents = new List<Vector2>();

            Vector2 N = new Vector2(cell.X, cell.Y - 1);
            Vector2 S = new Vector2(cell.X, cell.Y + 1);
            Vector2 W = new Vector2(cell.X - 1, cell.Y);
            Vector2 E = new Vector2(cell.X + 1, cell.Y);

            adjacents.Add(N);
            adjacents.Add(S);
            adjacents.Add(W);
            adjacents.Add(E);

            adjacents.ForEach(delegate(Vector2 dir)
            {
                if (dir.X <= 0 || dir.Y <= 0 ||
                    dir.X >= level.WidthInTiles ||
                    dir.Y >= level.HeightInTiles)
                    adjacents.Remove(dir);
            });

            return adjacents;
        }
    }
}
