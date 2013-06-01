using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Origins_Remake.Util;
using Microsoft.Xna.Framework;
using Origins_Remake.Levels.Tiles;
using Origins_Remake.Levels.Objects;

namespace Origins_Remake.Levels
{
    class PerlinLevel : Level
    {
        public PerlinLevel(int size, int tileWidth, int tileHeight)
            : base(size, size, tileWidth, tileHeight)
        {
            GenerateLevel();
            GenerateObjects();
        }

        void GenerateLevel()
        {
            Texture2D noiseMap = NoiseGenerator.GenerateTextureData(WidthInTiles);
            Color[] colors = new Color[WidthInTiles * WidthInTiles];
            noiseMap.GetData<Color>(colors);

            for (int x = 0; x < WidthInTiles; x++)
            {
                for (int y = 0; y < HeightInTiles; y++)
                {
                    float v = colors[x + y * WidthInTiles].R;

                    if (v < 100 && v >= 80)
                        map[x, y] = new SandTile(new Point(x, y));
                    else if (v < 80)
                        map[x, y] = new WaterTile(new Point(x, y));
                    else
                        map[x, y] = new GrassTile(new Point(x, y));
                }
            }

            noiseMap.Dispose();
        }

        void GenerateObjects()
        {
            Texture2D noiseMap = NoiseGenerator.GenerateTextureData(WidthInTiles);
            Color[] colors = new Color[WidthInTiles * WidthInTiles];
            noiseMap.GetData<Color>(colors);

            Random rand = new Random();

            foreach (Tile tile in map)
            {
                if (tile.GetType() == typeof(GrassTile)) 
                if (rand.NextDouble() > .95)
                    objects.Add(new GrassShrub(new Vector2(tile.Position.X * tileWidth, tile.Position.Y * tileHeight)));
            }

            int currentCount = objects.Count;
            int treeCount = 30;

            do
            {
                var point = new Vector2(rand.Next(0, realWidth), rand.Next(0, realHeight));
                var tree = new Tree(point);
                var trunkPoint = point + new Vector2(tree.Width / 2, tree.Height);

                if (tree.Bounds.X + tree.Bounds.Width > realWidth ||
                    tree.Bounds.Y + tree.Bounds.Height > realHeight) 
                    continue;

                if (!(map[Vector2Tile(trunkPoint).X, Vector2Tile(trunkPoint).Y].GetType() == typeof(GrassTile)))
                    continue;

                objects.Add(tree);

            } while (objects.Count < treeCount + currentCount);

            noiseMap.Dispose();
        }
    }
}
