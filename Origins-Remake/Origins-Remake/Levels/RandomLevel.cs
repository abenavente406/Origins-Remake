using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Origins_Remake.Levels.Tiles;
using Microsoft.Xna.Framework;

namespace Origins_Remake.Levels
{
    public class RandomLevel : Level
    {
        Random rand = new Random();

        public RandomLevel(int width, int height, int tileWidth, int tileHeight)
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
                    var currentPoint = new Point(x, y);

                    if (rand.NextDouble() > .15)
                        map[x, y] = new GrassTile(currentPoint);
                    else
                        map[x, y] = new WaterTile(currentPoint);
                }
            }
        }
    }
}
