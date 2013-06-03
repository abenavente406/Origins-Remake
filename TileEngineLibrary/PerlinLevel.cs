using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OriginsLibrary.Util;
using TileEngineLibrary.Tiles;
using System;

namespace TileEngineLibrary
{
    [Serializable]
    public class PerlinLevel : Level
    {
        public PerlinLevel(int size, int tileWidth, int tileHeight)
            : base(size, size, tileWidth, tileHeight)
        {
            GenerateLevel();
            // GenerateObjects();
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

        //void GenerateObjects()
        //{
        //    Texture2D noiseMap = NoiseGenerator.GenerateTextureData(WidthInTiles);
        //    Color[] colors = new Color[WidthInTiles * WidthInTiles];
        //    noiseMap.GetData<Color>(colors);

        //    Random rand = new Random();

        //    foreach (Tile tile in map)
        //    {
        //        if (tile.GetType() == typeof(GrassTile)) 
        //        if (rand.NextDouble() > .95)
        //            objects.Add(new GrassShrub(new Vector2(tile.Position.X * tileWidth, tile.Position.Y * tileHeight)));
        //    }

        //    int currentCount = objects.Count;
        //    int treeCount = 30;
           
        //    int loopCounter = 0;
        //    int maxLoops = 140;

        //    do
        //    {
        //        if (++loopCounter > maxLoops)
        //            break;

        //        var point = new Vector2(rand.Next(0, realWidth), rand.Next(0, realHeight));
        //        var tree = new Tree(point);
        //        var trunkPoint = point + new Vector2(tree.Width / 2, tree.Height);

        //        if (tree.Bounds.X + tree.Bounds.Width > realWidth ||
        //            tree.Bounds.Y + tree.Bounds.Height > realHeight) 
        //            continue;

        //        var trunkTile = Vector2Tile(trunkPoint);
        //        if (trunkTile.X < 0 || trunkTile.X >= WidthInTiles ||
        //            trunkTile.Y < 0 || trunkTile.Y >= HeightInTiles)
        //            continue;

        //        if (!(map[trunkTile.X, trunkTile.Y].GetType() == typeof(GrassTile)))
        //            continue;

        //        objects.Add(tree);

        //    } while (objects.Count < treeCount + currentCount);

        //    noiseMap.Dispose();
        //}
    }
}
