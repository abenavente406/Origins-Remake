using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using TileEngineLibrary.Tiles;

namespace TileEngineLibrary
{
    [Serializable]
    public class DungeonLevel : Level
    {
        Random rand = new Random();
        List<Room> rooms = new List<Room>();
        
        public DungeonLevel(int width, int height, int tileWidth, int tileHeight)
            : base(width, height, tileWidth, tileHeight)
        {
            GenerateLevel();
        }

        private void GenerateLevel()
        {
            for (int x = 0; x < WidthInTiles; x++)
            {
                for (int y = 0; y < HeightInTiles; y++) 
                {
                    map[x, y] = new DungeonWallTile(new Point(x, y));
                }
            }

            int roomsMin = (int)(WidthInTiles * HeightInTiles) / 300;
            int roomsMax = (int)(HeightInTiles * WidthInTiles) / 150;
            int roomCount = 10;

            int widthRoot = (int)Math.Sqrt(RealWidth * 2);
            int heightRoot = (int)Math.Sqrt(RealHeight * 2);

            int minimumWidth = (int)4;
            int maximumWidth = (int)8;
            int minimumHeight = (int)3;
            int maximumHeight = (int)10;

            do
            {
                bool ok = false;
                Room room = new Room();

                room.GridX = rand.Next(0, WidthInTiles);
                room.GridY = rand.Next(0, HeightInTiles);
                room.WidthInTiles = rand.Next(minimumWidth, maximumWidth);
                room.HeightInTiles = rand.Next(minimumHeight, maximumHeight);

                if (room.GridX < 0 || room.GridX > WidthInTiles - room.WidthInTiles ||
                    room.GridY < 0 || room.GridY > HeightInTiles - room.HeightInTiles)
                {
                    continue;
                }

                ok = true;

                if (rooms.Count > 0)
                {
                    foreach (Room r in rooms)
                    {
                        if (r.GridBounds.Intersects(room.GridBounds))
                            ok = false;
                    }
                }

                if (ok)
                    rooms.Add(room);
            } while (rooms.Count < roomCount);

            rooms.Add(new Room()
            {
                GridX = 0,
                GridY = 0,
                WidthInTiles = 10,
                HeightInTiles = 10
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

                Vector2 pointA = new Vector2(rand.Next(room.GridX, room.GridX + room.WidthInTiles),
                    rand.Next(room.GridY, room.GridY + room.HeightInTiles));
                Vector2 pointB = new Vector2(rand.Next(connectToRoom.GridX, connectToRoom.GridX + connectToRoom.WidthInTiles),
                    rand.Next(connectToRoom.GridY, connectToRoom.GridY + connectToRoom.HeightInTiles));
                
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

                    if (pointB.X < WidthInTiles && pointB.Y < HeightInTiles)
                    {
                        map[(int)pointB.X, (int)pointB.Y] = new DungeonFloorTile(new Point((int)pointB.X, (int)pointB.Y));
                    }
                }

                foreach (Room r in rooms)
                {
                    for (int x = (int)r.GridX; x < r.WidthInTiles + r.GridX; x++)
                    {
                        for (int y = (int)r.GridY; y < r.HeightInTiles + r.GridY; y++)
                        {
                            if (x == r.GridX || x == (int)(r.WidthInTiles + (r.GridX) - 1) ||
                                y == r.GridY || y == (int)(r.HeightInTiles + (r.GridY) - 1))
                            {
                                continue;
                            }
                            else
                            {
                                map[(int)(x), (int)y] = new DungeonFloorTile(new Point(x, y));
                            }
                        }
                    }
                }
            }
        }
    }

    [Serializable]
    struct Room
    {
        public int TileWidth { get { return 32; } }
        public int TileHeight { get{ return 32; } }
        public int WidthInTiles { get; set; }
        public int HeightInTiles { get; set; }
        public int RealWidth { get { return WidthInTiles * TileWidth; }}
        public int RealHeight { get { return HeightInTiles * TileHeight; } }

        public int GridX { get; set; }
        public int GridY { get; set; }
        public int RealX { get { return GridX * TileWidth; } }
        public int RealY { get { return GridY * TileHeight; } }

        public Rectangle RealBounds 
        { 
            get 
            { 
                return new Rectangle(RealX, RealY, RealWidth, RealHeight);
            }
        }

        public Rectangle GridBounds
        {
            get
            {
                return new Rectangle(GridX, GridY, WidthInTiles, HeightInTiles);
            }
        }
    }

    [Serializable]
    struct Cell
    {
        public int TileWidth { get { return 32; } }
        public int TileHeight { get  { return 32; } }
        public int GridX { get; set; }
        public int GridY { get; set; }
        public int RealX { get { return GridX / TileWidth; }}
        public int RealY { get { return GridY / TileHeight; }}
    }


}
